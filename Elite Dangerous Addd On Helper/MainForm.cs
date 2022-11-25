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
using System.IO.Pipes;



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
        static readonly string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Elite Add On Helper\\";
        static readonly HttpClient client = new HttpClient();
        static readonly string[] appnames = { "Ed Enginer", "Ed Market Connector", "Ed Discovery", "Voiceattack", "ED Odyysey Materials Helper Launcher", "T.A.R.G.E.T.", "AussieDroid Warthog Script", "Elite Dangerous Launcher" };
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
                // lets copy the default addons.json to the settings path..
                // probably want to remove this and do the file copy in an installer..
                string defaultpath = AppDomain.CurrentDomain.BaseDirectory;
                string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "addons.json");
                string sourceFile = startupPath;
                string destinationFile = settingsFilePath + "AddOns.json";
                try
                {
                    File.Copy(sourceFile, destinationFile, true);
                    updatemystatus("Settings copied");
                    updatemystatus("Loading Settings");
                    addOns = DeserializeAddOns();
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                    updatemystatus("Settings error");
                }

                //InitialAddonsSetup();
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
            var yPosition = ((currentControlRow) * 30) + 150;

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
            button.Size = new System.Drawing.Size(80, 30);
            //To the buttons click method, add this method, and pass it the friendly name (to use as the AddOns dictionary key)
            button.Click += (sender, e) => HandleSelectPath(addOn.FriendlyName);
            Controls.Add(button);

            TextBox textBox = new TextBox();
            textBox.Name = addOn.FriendlyName;
            textBox.Location = new System.Drawing.Point(360, yPosition);
            textBox.Size = new System.Drawing.Size(230, 30);
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            textBox.DataBindings.Add("Text", addOn, "ProgramDirectory", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            Controls.Add(textBox);

            if (addOn.Installable)
            {
                Button installButton = new Button();
                installButton.Text = "Install?";
                installButton.Location = new System.Drawing.Point(600, yPosition);
                installButton.Size = new System.Drawing.Size(80, 30);
                //To the buttons click method, add this method, and pass it the friendly name (to use as the AddOns dictionary key)
                installButton.Click += (sender, e) => DoInstall(addOn);
                Controls.Add(installButton);
            }
            richTextBox1.Text += addOn.FriendlyName + "\r\n";

            currentControlRow++;
        }
        private void DeleteControls(AddOn addOn)
        {
            //Sets the y position of the controls based on how many rows (addons) there are
            var yPosition = ((currentControlRow) * 30) + 150;

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
            Controls.Remove(checkBox);

            Button button = new Button();
            button.Text = "Select Path...";
            button.Location = new System.Drawing.Point(277, yPosition);
            button.Size = new System.Drawing.Size(80, 25);
            //To the buttons click method, add this method, and pass it the friendly name (to use as the AddOns dictionary key)
            button.Click += (sender, e) => HandleSelectPath(addOn.FriendlyName);
            Controls.Remove(button);

            TextBox textBox = new TextBox();
            textBox.Name = addOn.FriendlyName;
            textBox.Location = new System.Drawing.Point(360, yPosition);
            textBox.Size = new System.Drawing.Size(230, 25);
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            textBox.DataBindings.Add("Text", addOn, "ProgramDirectory", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            Controls.Remove(textBox);

            if (addOn.Installable)
            {
                Button installButton = new Button();
                installButton.Text = "Install?";
                installButton.Location = new System.Drawing.Point(600, yPosition);
                installButton.Size = new System.Drawing.Size(80, 25);
                //To the buttons click method, add this method, and pass it the friendly name (to use as the AddOns dictionary key)
                installButton.Click += (sender, e) => DoInstall(addOn);
                Controls.Remove(installButton);
            }
            richTextBox1.Text = "";

            currentControlRow++;
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
                        fs.Close();
                        updatemystatus("Installing..");
                        var p = new Process();
                        p.StartInfo = new ProcessStartInfo(filename)
                        {
                            UseShellExecute = true
                        };
                        p.Start();
                    }
                }
                else
                {
                    //looks like the filealready exists - is it installed? Should we continue?
                    updatemystatus("File already Downloaded!");
                    const string message = "File already downloaded, are you sure you want to install?";
                    const string caption = "Already Installed?";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                    // If the yes button was pressed ...
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
                try
                {
                    var p = new Process();
                    p.StartInfo = new ProcessStartInfo(path)
                    {
                        UseShellExecute = true
                    };
                    p.Start();
                    //Process.Start(path);
                }
                catch
                {
                    updatemystatus($"An Error occured trying to launch {addOn.FriendlyName}..");
                }
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
        #region installs


        private void DoInstall(AddOn addOn)
        {
            updatemystatus($"Installing {addOn.FriendlyName}");
            DownloadFileAndExecute(addOn.Url);
            updatemystatus("Ready");
        }
        #endregion installs  

        private void Bt_AddApp_Click(object sender, EventArgs e)
        {
            var AddApp = new AddApp();
            AddApp.ShowDialog();
            foreach (var addon in addOns.Values)
            {
                DeleteControls(addon);
            }
            foreach (var addon in addOns.Values)
            {
                CreateControls(addon);
            }
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}