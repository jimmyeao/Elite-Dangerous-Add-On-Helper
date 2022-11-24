using Elite_Dangerous_Add_On_Helper.Model;
using System.Diagnostics;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;


// TODO LIST!
// Make a dependanciy between warthog being enabled and requiring a script to be specified
// Load prefs populates fields
// deal with arguments in launch apps
// ....


namespace Elite_Dangerous_Add_On_Helper
{
    public partial class MainForm : Form
    {
        // setup a folder for settings
        static readonly string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static readonly string settingsFilePath = directory + "/Elite Add On Helper/";
        static readonly string[] appnames = { "Ed Enginer", "Ed Market Connector","Ed Discovery","Voiceattack","Elite Dangerous Odyysey Materials Helper Launcher","T.A.R.G.E.T.","AussieDroid Warthog Script","Elite Dangerous Launcher" };
        /// <summary>
        /// List of all addons
        /// </summary>
        public Dictionary<string, AddOn> addOns = new Dictionary<string, AddOn>();
        public MainForm()
        {
            InitializeComponent();
            Load_prefs();   
        }
        private void Load_prefs()
        {
            // load all the textboxes with values from settings file
            if (File.Exists(settingsFilePath + "AddOns.json"))
            {
               
                addOns = DeserializeAddOns();
                foreach(string app in appnames)
                {

                    string cb = "Cb_" + app.Replace(" ", "_");
                    
                    addOns[app] = new AddOn();

                }
            }
            else
            {
                InitialAddonsSetup();
            }


        }
        private void InitialAddonsSetup()
        {
            //Test data below, dictionary key should match friendly name
            if (!addOns.ContainsKey("Ed Engineer"))
            {
                addOns.Add("Ed Engineer", new AddOn
                {
                    Enabled = false,
                    Installable = false,
                    ProgramDirectory = "",
                    FriendlyName = "Ed Engineer",
                    ExecutableName = "EDEngineer.exe",
                    AutoDiscoverPath = "",
                    Scripts = ""
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
                    Scripts = ""
                });
            }
            if (!addOns.ContainsKey("VoiceAttack"))
            {
                addOns.Add("VoiceAttack", new AddOn
                {
                    Enabled = false,
                    Installable = true,
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
                    Scripts = ""
                });
            }
            if (!addOns.ContainsKey("Elite Dangerous Odyysey Materials Helper"))
            {
                addOns.Add("Elite Dangerous Odyysey Materials Helper", new AddOn
                {
                    Enabled = false,
                    Installable = true,
                    ProgramDirectory = "",
                    FriendlyName = "Elite Dangerous Odyysey Materials Helper",
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
                    Installable = true,
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
                    Installable = true,
                    ProgramDirectory = "",
                    FriendlyName = "Elite",
                    ExecutableName = "EDLaunch.exe",
                    AutoDiscoverPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Elite Dangerous\\",
                    Scripts = ""
                });
            }


        }


        // My Functions
        #region functions

        static void DownloadFileAndExecute(string link)
        {
            WebClient wc = new WebClient();
            string filename = Path.GetFileName(link);
            wc.DownloadFile(link, filename);
            Process.Start(filename);
        }
        private void updatestatus(string status)
        {
            // function to update the status bar
            toolStripStatusLabel1.Text = status;
            toolStripStatusLabel1.Invalidate();
            statusStrip1.Invalidate();
            statusStrip1.Refresh();
        }
        private void LaunchAddon(AddOn addOn)
        {
            var path = $"{addOn.ProgramDirectory}/{addOn.ExecutableName}";

            if (File.Exists(path))
            {
                updatestatus($"Launching {addOn.FriendlyName}..");
                Process.Start(path);
            }
            else
            {
                updatestatus($"Unable to launch {addOn.FriendlyName}..");

            }
            System.Threading.Thread.Sleep(2000);

        }
        internal static Dictionary<string, AddOn> DeserializeAddOns()
        {
            var Json = File.ReadAllText(settingsFilePath + "AddOns.json");

            return JsonConvert.DeserializeObject<Dictionary<string, AddOn>>(Json,
                new JsonSerializerSettings
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
        private string Folderpath()
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
        #endregion
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
        //try to detect paths for the applications
        // TODO get path and exe names to make launching a simple loop operation.
  
        # region installs
        private void Btn_install_EdEngineer_Click(object sender, EventArgs e)
        {
            updatestatus("Installing Ed Engineer");
            DownloadFileAndExecute("https://raw.githubusercontent.com/msarilar/EDEngineer/master/EDEngineer/releases/setup.exe");
            updatestatus("Ready");
        }

        private void Btn_install_edmc_Click(object sender, EventArgs e)
        {
            updatestatus("Installing EDMC");
            DownloadFileAndExecute("https://github.com/EDCD/EDMarketConnector/releases/download/Release%2F5.5.0/EDMarketConnector_win_5.5.0.msi");
            updatestatus("Ready");
        }

        private void Btn_install_EDDiscovery_Click(object sender, EventArgs e)
        {
            updatestatus("Installing ED Discovery");
            DownloadFileAndExecute("https://github.com/EDDiscovery/EDDiscovery/releases/download/Release_15.1.4/EDDiscovery-15.1.4.exe");
            updatestatus("Ready");
        }

        private void Btn_install_edomhl_Click(object sender, EventArgs e)
        {
            updatestatus("Installing ED Odyysesy Materials Helper");
            DownloadFileAndExecute("https://github.com/jixxed/ed-odyssey-materials-helper/releases/download/1.100/Elite.Dangerous.Odyssey.Materials.Helper-1.100.msi");
            updatestatus("Ready");
        }

        #endregion installs      
        // autodetect function
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
            updatestatus("This may take a while.. Searching for EDMC");
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
                updatestatus("EDMC Not found");

            }
            progressBar1.PerformStep();
            progressBar1.Refresh();
            updatestatus("This may take a while.. Searching for Ed Engineer");
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
                updatestatus("Ed Engineer Not found");
            }
            updatestatus("This may take a while.. Searching for Voice Attack");
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
                        updatestatus("Voice Attack Not found");

                    }
                }
            }
            progressBar1.PerformStep();
            progressBar1.Refresh();
            updatestatus("This may take a while.. Searching for ED Discovery");
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
                updatestatus("ED Discovery Not found");

            }
            progressBar1.PerformStep();
            progressBar1.Refresh();
            updatestatus("This may take a while.. Searching for ED Odyysey Materials Helper");

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
                updatestatus(" ED Odyysey Materials Helper not found");
            }

            progressBar1.PerformStep();
            progressBar1.Refresh();
            updatestatus("This may take a while.. Searching for T.A.R.G.E.T");
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
                updatestatus(" ED Odyysey Materials Helper not found");
            }

            progressBar1.PerformStep();
            progressBar1.Refresh();
            updatestatus("This may take a while.. Searching for Elite Dangerous");

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
                updatestatus("Elite launcher not found");
            }
            
            progressBar1.Value = 1;
            progressBar1.Refresh();
            updatestatus("Ready");
        }
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
             SerializeAddons(addOns);
        }
        #endregion menuitems
        #region launch items
        private void Bt_Launch_Click(object sender, EventArgs e)
        {
            foreach (var addOn in addOns.Values)
            {
                if (addOn.Enabled)
                {
                    LaunchAddon(addOn);
                }
            }
            System.Threading.Thread.Sleep(2000);
            updatestatus("Ready");
            // for ref how to open a webpage in default browser
            //Process.Start("https://www.google.com/");

        }
        #endregion
    }
}