using Elite_Dangerous_Add_On_Helper.Model;
using System.Diagnostics;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Data;


// TODO LIST!
// Make a dependanciy between warthog being enabled and requiring a script to be specified
// Load prefs populates fields
// deal with arguments in launch apps
// ....


namespace Elite_Dangerous_Add_On_Helper
{
    public partial class MainForm : Form
    {
        // setup a folder for settings0
        // static readonly string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static readonly string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Elite Add On Helper/";
        static readonly HttpClient client = new HttpClient();
        static readonly string[] appnames = { "Ed Enginer", "Ed Market Connector","Ed Discovery","Voiceattack","ED Odyysey Materials Helper Launcher","T.A.R.G.E.T.","AussieDroid Warthog Script","Elite Dangerous Launcher" };
        /// <summary>
        /// List of all addons
        /// </summary>
        public Dictionary<string, AddOn> addOns = new Dictionary<string, AddOn>();

        public static CancellationToken WebCommsTimeout { get; private set; }

        public MainForm()
        {
           
            InitializeComponent();
            Load_prefs();
            updatemystatus("Ready");
        }



        // My Functions
        #region functions
        private void Load_prefs()
        {
            // load all the textboxes with values from settings file
            updatemystatus("Checking file exists");
            if (File.Exists(settingsFilePath + "AddOns.json"))
            {
                updatemystatus("Loading Settings");
                addOns = DeserializeAddOns();
               
            }
            else
            {
                updatemystatus("Settings not found");
                InitialAddonsSetup();
            }
            foreach (var addon in addOns.Values)
            {
                CreateControls(addon);
            }


        }
        private int currentControlRow = 0;
        private void CreateControls(AddOn addOn)
        {
            //Sets the y position of the controls based on how many rows (addons) there are
            var yPosition = ((currentControlRow) * 22) + 150;

            //Create checkbox
            CheckBox checkBox = new CheckBox();
            //Set the text to the friendly (human readable) addon name
            checkBox.Text = addOn.FriendlyName;
            //Autosize on
            checkBox.AutoSize = true;
            //Data binding, super useful. If the box is checked, it updates the model, if you update the model in code, the box changes too!
            //this is basically saying "The box being checked on screen is linked to this specific addon object, and more specifically the enabled property"
            checkBox.DataBindings.Add("Checked", addOn, "Enabled", true, DataSourceUpdateMode.OnPropertyChanged);
            //Set the location on screen, this can be a bit trial and error
            checkBox.Location = new System.Drawing.Point(15, yPosition);
            //Add the checkbox to the controls for this form1 form
            Controls.Add(checkBox);

            Button button = new Button();
            button.Text = "Select Path...";
            button.Location = new System.Drawing.Point(277, yPosition);
            button.Size = new System.Drawing.Size(80, 25);
            //To the buttons click method, add this method, and pass it the friendly name (to use as the AddOns dictionary key)
            button.Click += (sender, e) => HandleSelectPath(addOn.FriendlyName);
            Controls.Add(button);

            TextBox textBox = new TextBox();
            textBox.Name = addOn.FriendlyName;
            textBox.Location = new System.Drawing.Point(360, yPosition);
            textBox.Size = new System.Drawing.Size(230, 25);
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            textBox.DataBindings.Add("Text", addOn, "ProgramDirectory", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            Controls.Add(textBox);

            if (addOn.Installable)
            {
                Button installButton = new Button();
                installButton.Text = "Install?";
                installButton.Location = new System.Drawing.Point(600, yPosition);
                installButton.Size = new System.Drawing.Size(80, 25);
                //To the buttons click method, add this method, and pass it the friendly name (to use as the AddOns dictionary key)
                installButton.Click += (sender, e) => DoInstall(addOn);
                Controls.Add(installButton);
            }


            currentControlRow++;
        }
       
        private void InitialAddonsSetup()
        {
            //Test data below, dictionary key should match friendly name
            if (!addOns.ContainsKey("Ed Engineer"))
            {
                addOns.TryAdd("Ed Engineer", new AddOn
                {
                    Enabled = false,
                    Installable = true,
                    ProgramDirectory = "",
                    FriendlyName = "Ed Engineer",
                    ExecutableName = "EDEngineer.exe",
                    AutoDiscoverPath = "",
                    Scripts = "",
                    Url = "https://raw.githubusercontent.com/msarilar/EDEngineer/master/EDEngineer/releases/setup.exe"
                });
            }
            if (!addOns.ContainsKey("Ed Market Connector"))
            {
                addOns.Add("Ed Market Connector", new AddOn
                {
                    Enabled = false,
                    Installable = true,
                    ProgramDirectory = "",
                    FriendlyName = "Ed Market Connector",
                    ExecutableName = "EDMarketConnector.exe",
                    AutoDiscoverPath = "C:\\Program Files (x86)\\EDMarketConnector",
                    Scripts = "",
                    Url = "https://github.com/EDCD/EDMarketConnector/releases/download/Release%2F5.5.0/EDMarketConnector_win_5.5.0.msi"

                });
            }
            if (!addOns.ContainsKey("VoiceAttack"))
            {
                addOns.Add("VoiceAttack", new AddOn
                {
                    Enabled = false,
                    Installable = false,
                    ProgramDirectory = "",
                    FriendlyName = "VoiceAttack",
                    ExecutableName = "VoiceAttack.exe",
                    AutoDiscoverPath = "",
                    Scripts = ""
                });
            }
            if (!addOns.ContainsKey("Ed Discovery"))
            {
                addOns.Add("Ed Discovery", new AddOn
                {
                    Enabled = false,
                    Installable = true,
                    ProgramDirectory = "",
                    FriendlyName = "ED Discovery",
                    ExecutableName = "EDDiscovery.exe",
                    AutoDiscoverPath = "C:\\Program Files\\EDDiscovery",
                    Scripts = "",
                    Url = "https://github.com/EDDiscovery/EDDiscovery/releases/download/Release_15.1.4/EDDiscovery-15.1.4.exe"
                });
            }
            if (!addOns.ContainsKey("ED Odyysey Materials Helper"))
            {
                addOns.Add("ED Odyysey Materials Helper", new AddOn
                {
                    Enabled = false,
                    Installable = true,
                    ProgramDirectory = "",
                    FriendlyName = "ED Odyysey Materials Helper",
                    ExecutableName = "Elite Dangerous Odyssey Materials Helper Launcher.exe",
                    AutoDiscoverPath = "",
                    Scripts = ""
                });
            }
            if (!addOns.ContainsKey("T.A.R.G.E.T."))
            {
                addOns.Add("T.A.R.G.E.T.", new AddOn
                {
                    Enabled = false,
                    Installable = false,
                    ProgramDirectory = "",
                    FriendlyName = "T.A.R.G.E.T.",
                    ExecutableName = "TARGETGUI.exe",
                    AutoDiscoverPath = "",
                    Scripts = ""
                });
            }
            if (!addOns.ContainsKey("Elite"))
            {
                addOns.Add("Elite", new AddOn
                {
                    Enabled = false,
                    Installable = false,
                    ProgramDirectory = "",
                    FriendlyName = "Elite",
                    ExecutableName = "EDLaunch.exe",
                    AutoDiscoverPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Elite Dangerous\\",
                    Scripts = ""
                });
            }


        }
        private void updatemystatus(string status)
        {
            // function to update the status bar
            toolStripStatusLabel1.Text = status;
            toolStripStatusLabel1.Invalidate();
            statusStrip1.Invalidate();
            statusStrip1.Refresh();
        }
        private void DownloadFileAndExecute(string link)
        {
            // download and install function
            // where are we going to save it?
            string filename = Path.GetFileName(link);
            // new code
            //string requestString = link;
            if (link != string.Empty)
            {
                updatemystatus("Downloading..");
                var GetTask = client.GetAsync(link);
                GetTask.Wait(WebCommsTimeout); // WebCommsTimeout is in milliseconds
                if (!GetTask.Result.IsSuccessStatusCode)
                {
                    // write an error
                    updatemystatus("There was an error, please install manually");
                    return;
                }
                if (!File.Exists(filename))
                {
                    using (var fs = new FileStream(filename, FileMode.CreateNew))
                    {
                        var ResponseTask = GetTask.Result.Content.CopyToAsync(fs);
                        ResponseTask.Wait(WebCommsTimeout);
                        updatemystatus("Installing..");
                        Process.Start(filename);
                    }
                }
                else
                {
                    updatemystatus("File already Downloaded!");
                    const string message = "File already downloaded, are you sure you want to install?";
                    const string caption = "Already Installed?";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                    // If the no button was pressed ...
                    if (result == DialogResult.Yes)
                    {
                        // cancel the closure of the form.
                        updatemystatus("Installing..");
                        var p = new Process();
                        p.StartInfo = new ProcessStartInfo(filename)
                        {
                            UseShellExecute = true
                        };
                        p.Start();
                        //Process.Start(filename);
                    }
                }
                // end new code
            }
            
        }

        private void LaunchAddon(AddOn addOn)
        {
            var path = $"{addOn.ProgramDirectory}/{addOn.ExecutableName}";

            if (File.Exists(path))
            {
                updatemystatus($"Launching {addOn.FriendlyName}..");
                Process.Start(path);
            }
            else
            {
                updatemystatus($"Unable to launch {addOn.FriendlyName}..");

            }
            System.Threading.Thread.Sleep(2000);

        }
        internal static Dictionary<string, AddOn> DeserializeAddOns()
        {
            var Json = File.ReadAllText(settingsFilePath + "AddOns.json");

            return JsonConvert.DeserializeObject<Dictionary<string, AddOn>>(Json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                });
        }

        internal static void SerializeAddons(object addOns)
        {
            var Json = JsonConvert.SerializeObject(addOns, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });

            File.WriteAllText(settingsFilePath + "AddOns.json", Json);
        }
        static string Folderpath()
        {
            FolderBrowserDialog diag = new FolderBrowserDialog
            {
                // set the root folder or it defaults to desktop
                RootFolder = Environment.SpecialFolder.MyComputer
            };
            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return diag.SelectedPath;
            }
            else { return null; }
        }
        
        private void HandleSelectPath(string dictKey)
        {
            addOns.TryGetValue(dictKey, out var addOn); //get the AddOn model as "addOn" using the dictionary key

            if (addOn == null)
            {
                return;
            }
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select A File";
            openDialog.Filter = "Executable files (*.exe)|*.exe";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openDialog.FileName;
                addOn.ProgramDirectory = file;
            }

            addOns[dictKey] = addOn; //overwrite the existing addon in the dictionary with the updated model

        }
         private void btn_autodetect_Click_1(object sender, EventArgs e)
        {
            // Display the ProgressBar control.
            progressBar1.Visible = true;
            // Set Minimum to 1 to represent the first file being copied.
            progressBar1.Minimum = 1;
            // Set Maximum to the total number of files to copy.
            int Totalchecked = nonvrtab.Controls.OfType<System.Windows.Forms.CheckBox>().Count();
            progressBar1.Maximum = Totalchecked;
            Console.WriteLine(Totalchecked);
            // Set the initial value of the ProgressBar.
            progressBar1.Value = 1;
            // Set the Step property to a value of 1 to represent each step.
            progressBar1.Step = 1;
            List<String> Driveletter = new List<string>();                                  //who has more than 10 local drives???
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady && d.DriveType == DriveType.Fixed)
                {
                    // Store Drives in list..
                    // Drives;

                    Driveletter.Add(d.ToString());

                }
            }
            string pathtocheck;
            updatemystatus("This may take a while.. Searching for EDMC");
            // lets check the default path
            // 
            progressBar1.PerformStep();
            progressBar1.Refresh();

            pathtocheck = @"C:\Program Files (x86)\EDMarketConnector";
            if (Directory.Exists(pathtocheck))
            {
                // found it!
                Tb_Ed_Market_Connector.Text = pathtocheck;
                Tb_Ed_Market_Connector.Refresh();
                Cb_Ed_Market_Connector.Checked = true;
            }
            else
            {
                updatemystatus("EDMC Not found");

            }
            progressBar1.PerformStep();
            progressBar1.Refresh();
            updatemystatus("This may take a while.. Searching for Ed Engineer");
            // lets get the users appdata/local folder...
            string Foldertosearch = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\apps";
            //now we need an array to hold the search result (Edengineer leases stuff behind when it updates resulting in mulitple copies)
            string[] result;
            // now lets search app data for EdEngineer..           
            result = Directory.GetFiles(Foldertosearch, "EDEngineer.exe", SearchOption.AllDirectories);

            //ok so we have a list of possible candidates, lets get the last one..

            if (File.Exists(result.Last()))
            {
                // found it!
                string edeng = result.Last();                       //get the last (usually most recent, meh) version of the file.
                edeng = edeng.Replace(@"\EDEngineer.exe", "");      //take of the exe name so we jjust have the path
                Tb_Ed_Engineer.Text = edeng;                         //update the textbox
                Tb_Ed_Engineer.Refresh();                            // refresh the textbox
                Cb_Ed_Engineer.Checked = true;                       // enable the app by checking the checkbox
            }
            else
            {
                updatemystatus("Ed Engineer Not found");
            }
            updatemystatus("This may take a while.. Searching for Voice Attack");
            progressBar1.PerformStep();
            progressBar1.Refresh();
            // lets check the default path
            // lets also search well known locations on all local fixed drives
            pathtocheck = @"C:\Program Files (x86)\Steam\steamapps\commonVoiceAttack";
            if (Directory.Exists(pathtocheck))
            {
                // found it!
                Tb_Voiceattack.Text = pathtocheck;
                Tb_Voiceattack.Refresh();
                Cb_Voiceattack.Checked = true;
            }
            else                                    // not found in default? lets search all local drives for steam apps
            {
                foreach (string d in Driveletter)
                {
                    pathtocheck = d + @"SteamLibrary\steamapps\common\VoiceAttack";

                    if (Directory.Exists(pathtocheck))
                    {
                        // found it!
                        Tb_Voiceattack.Text = pathtocheck;
                        Tb_Voiceattack.Refresh();
                        Cb_Voiceattack.Checked = true;
                    }
                    else
                    {
                        updatemystatus("Voice Attack Not found");

                    }
                }
            }
            progressBar1.PerformStep();
            progressBar1.Refresh();
            updatemystatus("This may take a while.. Searching for ED Discovery");
            // lets check the default path
            // 
            pathtocheck = @"C:\Program Files\EDDiscovery";
            if (Directory.Exists(pathtocheck))
            {
                // found it!
                Tb_Ed_Discovery.Text = pathtocheck;
                Tb_Ed_Discovery.Refresh();
                Cb_Ed_Discovery.Checked = true;
            }
            else
            {
                updatemystatus("ED Discovery Not found");

            }
            progressBar1.PerformStep();
            progressBar1.Refresh();
            updatemystatus("This may take a while.. Searching for ED Odyysey Materials Helper");

            // lets check the default path
            // 

            pathtocheck = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Elite Dangerous Odyssey Materials Helper Launcher";

            if (Directory.Exists(pathtocheck))
            {
                // found it!
                Tb_Elite_Dangerous_Odyssey_Materials_Helper_Launcher.Text = pathtocheck;
                Tb_Elite_Dangerous_Odyssey_Materials_Helper_Launcher.Refresh();
                Cb_Elite_Dangerous_Odyssey_Materials_Helper_Launcher.Checked = true;
            }
            else
            {
                updatemystatus(" ED Odyysey Materials Helper not found");
            }

            progressBar1.PerformStep();
            progressBar1.Refresh();
            updatemystatus("This may take a while.. Searching for T.A.R.G.E.T");
            // lets check the default path
            // 
            pathtocheck = @"c:\program files (x86)\Thrustmaster\TARGET\x64";
            if (Directory.Exists(pathtocheck))
            {
                // found it!
                Tb_T_A_R_G_E_T_.Text = pathtocheck;
                Tb_T_A_R_G_E_T_.Refresh();
                Cb_TARGET.Checked = true;
            }
            else
            {
                updatemystatus(" ED Odyysey Materials Helper not found");
            }

            progressBar1.PerformStep();
            progressBar1.Refresh();
            updatemystatus("This may take a while.. Searching for Elite Dangerous");

            // lets check the default path
            // 
            pathtocheck = @"C:\Program Files (x86)\Steam\steamapps\common\Elite Dangerous\";

            if (Directory.Exists(pathtocheck))
            {
                // found it!
                Tb_Elite_Dangerous_Launcher.Text = pathtocheck;
                Tb_Elite_Dangerous_Launcher.Refresh();
                Cb_Elite_Dangerous_Launcher.Checked = true;
            }
            else                                    // not found in default? lets search all local drives for steam apps
            {
                foreach (string d in Driveletter)
                {


                    pathtocheck = d + @"SteamLibrary\steamapps\common\Elite Dangerous";

                    if (Directory.Exists(pathtocheck))
                    {
                        // found it!
                        Tb_Elite_Dangerous_Launcher.Text = pathtocheck;
                        Tb_Elite_Dangerous_Launcher.Refresh();
                        Cb_Elite_Dangerous_Launcher.Checked = true;
                    }
                }
            }
            if (Tb_Elite_Dangerous_Launcher.Text == null)
            {
                updatemystatus("Elite launcher not found");
            }
            
            progressBar1.Value = 1;
            progressBar1.Refresh();
            updatemystatus("Ready");
        }
        #endregion
        #region browse functions
        private void Bt_Ed_Engineer_Click(object sender, EventArgs e)
        {
            string mypath = Folderpath();
            if (mypath != null)
            {
                Tb_Ed_Engineer.Text = mypath;
            }
        }

        private void Bt_Ed_Market_Connector_Click(object sender, EventArgs e)
        {
            string mypath = Folderpath();
            if (mypath != null)
            {
                Tb_Ed_Market_Connector.Text = mypath;
            }
        }
        private void Bt_Ed_Discovery_Click(object sender, EventArgs e)
        {
            string mypath = Folderpath();
            if (mypath != null)
            {
                Tb_Ed_Discovery.Text = mypath;
            }
        }

        private void Bt_Voiceattack_Click(object sender, EventArgs e)
        {
            string mypath = Folderpath();
            if (mypath != null)
            {
                Tb_Voiceattack.Text = mypath;
            }
        }

        private void Bt_Elite_Dangerous_Odyssey_Materials_Helper_Launcher_Click(object sender, EventArgs e)
        {
            string mypath = Folderpath();
            if (mypath != null)
            {
                Tb_Elite_Dangerous_Odyssey_Materials_Helper_Launcher.Text = mypath;
            }
        }

        private void Bt_T_A_R_G_E_T__Click(object sender, EventArgs e)
        {
            string mypath = Folderpath();
            if (mypath != null)
            {
                Tb_T_A_R_G_E_T_.Text = mypath;
            }
        }

        private void Bt_AussieDroid_Warthog_Script_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Title = "Select A File",
                Filter = "Thrustmaster Files (*.tmc)|*.tmc"
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openDialog.FileName;
                Tb_AussieDroid_Warthog_Script.Text = file;
            }
        }

        private void Bt_Elite_Dangerous_Launcher_Click(object sender, EventArgs e)
        {
            string mypath = Folderpath();
            if (mypath != null)
            {
                Tb_Elite_Dangerous_Launcher.Text = mypath;
            }
        }
        #endregion
        #region menuitems
        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savePreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
                updatemystatus("Saving Prefs");
             SerializeAddons(addOns);
        }
        #endregion menuitems
        #region launch items
        private void Bt_Launch_Click(object sender, EventArgs e)
        {
            
            foreach (var addOn in addOns.Values)
            {
                updatemystatus(addOn.ToString());
                if (addOn.Enabled)
                {
                    updatemystatus(addOn.ToString());
                    LaunchAddon(addOn);
                }
            }
            System.Threading.Thread.Sleep(2000);
            updatemystatus("Ready");
            // for ref how to open a webpage in default browser
            //Process.Start("https://www.google.com/");

        }
        #endregion
        # region installs
    private void Bt_Install_Ed_Engineer_Click(object sender, EventArgs e)
    {
        updatemystatus("Installing Ed Engineer");
        DownloadFileAndExecute("https://raw.githubusercontent.com/msarilar/EDEngineer/master/EDEngineer/releases/setup.exe");
        updatemystatus("Ready");
    }

    private void Bt_Install_Ed_Market_Connector_Click(object sender, EventArgs e)
    {
        updatemystatus("Installing EDMC");
        DownloadFileAndExecute("https://github.com/EDCD/EDMarketConnector/releases/download/Release%2F5.5.0/EDMarketConnector_win_5.5.0.msi");
        updatemystatus("Ready");
    }

    private void Bt_Install_Ed_Discovery_Click(object sender, EventArgs e)
    {
        updatemystatus("Installing ED Discovery");
        DownloadFileAndExecute("https://github.com/EDDiscovery/EDDiscovery/releases/download/Release_15.1.4/EDDiscovery-15.1.4.exe");
        updatemystatus("Ready");

    }

    private void DoInstall(AddOn addOn)
    {
        updatemystatus($"Installing {addOn.FriendlyName}");
        DownloadFileAndExecute(addOn.Url);
        updatemystatus("Ready");
    }
    #endregion installs  
    }
}