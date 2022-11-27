using Elite_Dangerous_Add_On_Helper.Model;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Forms;
using System;
using System.Security.Cryptography;
using PropertyChanged;
using System.Runtime.CompilerServices;


// TODO LIST!
// Make a dependanciy between warthog being enabled and requiring a script to be specified
// fix edit funcitonality
// ....


namespace Elite_Dangerous_Add_On_Helper
{
    public partial class MainForm : Form
    {
        // setup some variables
        static readonly string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Elite Add On Helper\\";
        static readonly HttpClient client = new HttpClient();    //used by the install app function
        public List<string> processList = new List<string>();    // holds a list of launched aps
        string[] launchargs = Environment.GetCommandLineArgs();  // gets any command line args that were passed at run time
        private int currentControlRow = 0;
        public static readonly object Themes;
        // i need to know what the below is actually doing...
        public Dictionary<string, AddOn> addOns = new Dictionary<string, AddOn>();
        // Create the ToolTip and associate with the Form container.

        ToolTip toolTip1 = new ToolTip();




        public static CancellationToken WebCommsTimeout { get; private set; }

        public MainForm()
        {

            InitializeComponent();
            //menuStrip1.BackColor = Color.;
            this.menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            this.menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
            this.statusStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
            menuStrip1.BackColor = Color.FromArgb(64, 64, 64);

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            Load_prefs();
            updatemystatus("Ready");
            try
            {
                foreach (string launch in launchargs)   // was the prog run with /auto? if so launch all enabled apps
                {
                    if (launch == "/auto")
                    {
                        foreach (var addOn in addOns.Values)
                        {

                            if (addOn.Enabled)
                            {
                                updatemystatus(addOn.FriendlyName);
                                LaunchAddon(addOn);
                            }
                        }
                    }
                }
            }
            catch { }
        }


        #region controls
        private void CreateControls(AddOn addOn)
        {
            //Sets the y position of the controls based on how many rows (addons) there are
            var yPosition = ((currentControlRow) * 30) + 100;


            //Create checkbox
            CheckBox checkBox = new CheckBox();
            //Set the text to the friendly (human readable) addon name
            checkBox.Text = addOn.FriendlyName;
            //Autosize on
            checkBox.AutoSize = false;
            checkBox.Size = new Size(250, 25);
            //Data binding, super useful. If the box is checked, it updates the model, if you update the model in code, the box changes too!
            //this is basically saying "The box being checked on screen is linked to this specific addon object, and more specifically the enabled property"
            checkBox.DataBindings.Add("Checked", addOn, "Enabled", true, DataSourceUpdateMode.OnPropertyChanged);
            //Set the location on screen, this can be a bit trial and error
            checkBox.Location = new System.Drawing.Point(15, yPosition);
            //Add the checkbox to the controls for this form1 form
            // if we have a valid path, make it green!
            if (Path.Exists(addOn.ProgramDirectory) || Path.Exists(addOn.AutoDiscoverPath))
            {
                checkBox.BackColor = Color.LimeGreen;
                toolTip1.SetToolTip(checkBox, "Path Found");
            }
            else
            {
                // otherwise make it red to show its missing!
                checkBox.BackColor = Color.Red;
                toolTip1.SetToolTip(checkBox, "Path NOT Found");
            }
            addOn.EnableCheckbox = checkBox;
            Controls.Add(checkBox);
            // add a browse button
            Button button = new Button();
            button.Text = "Browse";
            toolTip1.SetToolTip(button, "Browse for a folder");
            button.Location = new System.Drawing.Point(277, yPosition);
            button.Size = new System.Drawing.Size(80, 30);
            button.BackColor= Color.LightGray;
            //To the buttons click method, add this method, and pass it the friendly name (to use as the AddOns dictionary key)
            button.Click += (sender, e) => HandleSelectPath(addOn.FriendlyName);
            addOn.SelectPathButton = button;
            Controls.Add(button);
            //create the textbox with the path
            TextBox textBox = new TextBox();
            textBox.Name = addOn.FriendlyName;
            textBox.Location = new System.Drawing.Point(360, yPosition);
            textBox.Size = new System.Drawing.Size(230, 30);
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            if (addOn.AutoDiscoverPath != string.Empty)
            {
                if (Directory.Exists(addOn.AutoDiscoverPath))
                {
                    addOn.ProgramDirectory = addOn.AutoDiscoverPath;
                }
            }
            textBox.DataBindings.Add("Text", addOn, "ProgramDirectory", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            addOn.AppDirectorytextbox = textBox;
            //Controls.Add(textBox);
            //add in install button
            if (addOn.Installable)
            {
                Button installButton = new Button();
                installButton.Text = "Install?";
                installButton.BackColor= Color.LightGray;
                installButton.Location = new System.Drawing.Point(600, yPosition);
                installButton.Size = new System.Drawing.Size(80, 30);
                //To the buttons click method, add this method, and pass it the friendly name (to use as the AddOns dictionary key)
                installButton.Click += (sender, e) => DoInstall(addOn);
                addOn.InstallButton = installButton;
                Controls.Add(installButton);
            }
            // create the edit button
            Button editButton = new Button();
            editButton.Text = "Edit";
            editButton.Location = new System.Drawing.Point(680, yPosition);
            editButton.Size = new System.Drawing.Size(80, 30);
            editButton.Click += (sender, e) => DoEdit(addOn);
            addOn.EditButton = editButton;
            Controls.Add(editButton);
            Controls.OfType<Button>().ToList().ForEach(button => button.BackColor = Color.WhiteSmoke);

            currentControlRow++;            //move to the next row
        }



        private void DeleteControls(AddOn addOn)
        {
            currentControlRow = 0;
            Controls.Remove(addOn.EnableCheckbox);
            Controls.Remove(addOn.AppDirectorytextbox);

            Controls.Remove(addOn.SelectPathButton);

            Controls.Remove(addOn.InstallButton);
            Controls.Remove(addOn.EditButton);
            if (addOn.InstallButton != null)
            {
                Controls.Remove(addOn.InstallButton);
            }



        }
        #endregion

        // My Functions
        #region functions
        private void Load_prefs()                                       //load preferences
        {
            if (!Path.Exists(settingsFilePath))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(settingsFilePath);
                }
                catch (System.IO.DirectoryNotFoundException) { }
            }
            // load all the textboxes with values from settings file
            updatemystatus("Checking file exists");
            if (Path.Exists(settingsFilePath))
            {
                if (File.Exists(settingsFilePath + "AddOns.json"))
                {
                    updatemystatus("Loading Settings");
                    addOns = DeserializeAddOns();

                }
                else
                {
                    // lets copy the default addons.json to the settings path..
                    // probably want to remove this and do the file copy in an installer..
                    // string defaultpath = AppDomain.CurrentDomain.BaseDirectory;
                    string startupPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "addons.json");
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
                        // Console.WriteLine(iox.Message);
                        updatemystatus("Settings error");
                    }

                    //InitialAddonsSetup();
                }
            }

            foreach (var addon in addOns.Values)
            {
                CreateControls(addon);
            }
            this.Refresh();
            this.Size = new Size(this.Width, this.Height + 25);
            updatemystatus(Properties.Settings.Default.VR.ToString());
            if (Properties.Settings.Default.VR == true)
            {
                Rb_Vr.Checked = true;
            }
            else
            {
                Rb_NonVR.Checked = true;
            }
            if (Properties.Settings.Default.CLOSE == true)
            {
                Cb_CloseOnExit.Checked = true;
            }
            else
            {
                Cb_CloseOnExit.Checked = false;
            }

        }
        private void DoEdit(object sender)                              //send object to edit form (BROKEN!!)
        {
            var EditApp = new EditApp(addOns);


            //EditApp.sender = sender;
            EditApp.ShowDialog();

        }
        private void pictureBox1_Click(object sender, EventArgs e)      // show about box if logo clicked
        {
            using (About box = new About())
            {
                box.ShowDialog(this);
            }
        }
        private void updatemystatus(string status)                      // function to update the status bar
        {

            toolStripStatusLabel1.Text = status;
            toolStripStatusLabel1.Invalidate();
            statusStrip1.Invalidate();
            statusStrip1.Refresh();
        }
        private void DownloadFileAndExecute(string link)                // function to download and install add on
        {

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
        private void LaunchAddon(AddOn addOn)                           // function to launch enabled applications
        {
            // set up a list to track which apps we launched

            //different apps have different args, so lets set up a string to hold them
            string args;
            // TARGET requires a path to a script, if that path has spaces, we need to quote them - set a string called quote we can use to top and tail
            const string quote = "\"";
            var path = $"{addOn.ProgramDirectory}/{addOn.ExecutableName}";
            // are we launching TARGET? 
            if (string.Equals(addOn.ExecutableName, "targetgui.exe", StringComparison.OrdinalIgnoreCase))
            {
                // -r is to specify a script
                args = "-r " + quote + addOn.Scripts + quote;
            }
            else
            {
                // ok its not target, leave the argumnets as is
                args = addOn.Scripts;
            }
            // are we launching Elite? Lets check if the users wants VR mode
            if (string.Equals(addOn.ExecutableName, "edlaunch.exe", StringComparison.OrdinalIgnoreCase) && Rb_Vr.Checked)
            {
                //enable vr mode args
                args = "/VR";
            }
            if (File.Exists(path))      // worth checking the app we want to launch actually exists...
            {
                try
                {
                    var info = new ProcessStartInfo(path);
                    info.Arguments = args;
                    info.UseShellExecute = true;
                    info.WorkingDirectory = @addOn.ProgramDirectory;
                    Process proc = Process.Start(info);
                    proc.EnableRaisingEvents = true;
                    processList.Add(proc.ProcessName);
                    if (proc.ProcessName == "EDLaunch")
                    {
                        proc.Exited += new EventHandler(ProcessExitHandler);
                    }
                    System.Threading.Thread.Sleep(10);
                    proc.Refresh();
                    if (proc.ProcessName == "EdLaunch")
                    {
                        // WaitForEdLaunch();
                    }

                }
                catch
                {
                    // oh dear, something want horribluy wrong..
                    updatemystatus($"An Error occured trying to launch {addOn.FriendlyName}..");
                }

            }
            else
            {
                // yeah, that path didnt exist...
                updatemystatus($"Unable to launch {addOn.FriendlyName}..");

            }



        }
        internal static Dictionary<string, AddOn> DeserializeAddOns()   //read settings to json and load into objects
        {
            var Json = File.ReadAllText(settingsFilePath + "AddOns.json");
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, AddOn>>(Json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                });
            }
            catch
            {
                //oops something went wrong
                //updatemystatus("Prefs file corrupt, please delete and re run");
                return null;
            }
        }
        internal static void SerializeAddons(object addOns)             // grabs all objects and saves states in json
        {
            var Json = JsonConvert.SerializeObject(addOns, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });

            File.WriteAllText(settingsFilePath + "AddOns.json", Json);
        }
        //static string Folderpath(string path)
        //{
        //    string mypath;
        //    if (path == string.Empty)
        //    {
        //        mypath = Environment.SpecialFolder.MyComputer.ToString();
        //    }
        //    else
        //    {
        //        mypath = path;
        //    }
        //    FolderBrowserDialog diag = new FolderBrowserDialog
        //    {
        //        // set the root folder or it defaults to desktop
        //        SelectedPath = mypath
        //    };
        //    if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        return diag.SelectedPath;
        //    }
        //    else { return null; }
        //}
        public void HandleSelectPath(string dictKey)                   // browse for an exe and update add on with path and exe name
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
                // need to get just the path element here, but store filename as well
                addOn.ProgramDirectory = Path.GetDirectoryName(file);
                addOn.ExecutableName = openDialog.SafeFileName;
                //addOn.ProgramDirectory = file;
            }
            currentControlRow = 0;
            foreach (var addon in addOns.Values)
            {
                DeleteControls(addon);
            }
            this.Refresh();
            foreach (var addon in addOns.Values)
            {
                CreateControls(addon);
            }
            this.Refresh();

            addOns[dictKey] = addOn; //overwrite the existing addon in the dictionary with the updated model
                                     //redraw

        }
        private void ProcessExitHandler(object sender, EventArgs args)  //triggered when EDLaunch exits
        {
            // of Edlaunch has quit, does the user want us to kill all the apps?
            if (Cb_CloseOnExit.Checked)
            {
                foreach (string p in processList)
                    foreach (var process in Process.GetProcessesByName(p))
                    {
                        // Temp is a document which you need to kill.
                        if (process.ProcessName.Contains(p))
                            process.CloseMainWindow();
                    }
            }
        }
        private void Bt_Launch_Click(object sender, EventArgs e)        //launch apps button pressed
        {

            foreach (var addOn in addOns.Values)
            {

                if (addOn.Enabled)
                {
                    updatemystatus("Launching " + addOn.FriendlyName);
                    LaunchAddon(addOn);
                }
            }
            //lets breath a little to let things start up..
            updatemystatus("Ready");
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
            if (Rb_Vr.Checked)
            {

                Properties.Settings.Default.VR = true;
            }
            else
            {

                Properties.Settings.Default.VR = false;
            }

            Properties.Settings.Default.Save();
        }
        #endregion menuitems
        #region installs
        private void DoInstall(AddOn addOn)
        {
            updatemystatus($"Installing {addOn.FriendlyName}");
            DownloadFileAndExecute(addOn.Url);
            updatemystatus("Ready");
        }
        //private void DoEdit(object sender, EventArgs e)
        //{
        //    var AddApp = new AddApp(addOns);
        //    Edit.ShowDialog();
        //    updatemystatus($"Editing {addOn.FriendlyName}");

        //}
        #endregion installs  
        #region menu items
        private void openPrefsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = settingsFilePath,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private void addApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var AddApp = new AddApp(addOns);
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

        private void areYouSureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // this proc resets prefs to default

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (About box = new About())
            {
                box.ShowDialog(this);
            }
        }
        #endregion
        private void Rb_Vr_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Vr.Checked)
            {

                Properties.Settings.Default.VR = true;
            }
            else
            {

                Properties.Settings.Default.VR = false;
            }

            Properties.Settings.Default.Save();
        }
        private void Rb_NonVR_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Vr.Checked)
            {

                Properties.Settings.Default.VR = true;
            }
            else
            {

                Properties.Settings.Default.VR = false;
            }

            Properties.Settings.Default.Save();
        }
        private void Cb_CloseOnExit_CheckedChanged(object sender, EventArgs e)
        {
            if (Cb_CloseOnExit.Checked)
            {

                Properties.Settings.Default.CLOSE = true;
            }
            else
            {

                Properties.Settings.Default.CLOSE = false;
            }

            Properties.Settings.Default.Save();
        }



        //private void fileToolStripMenuItem_MouseHover(object sender, EventArgs e)
        //{
        //    fileToolStripMenuItem.BackColor = Color.Gray;
        //    fileToolStripMenuItem.ForeColor =Color.Red;
        //}

        //private void fileToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        //{
        //    fileToolStripMenuItem.BackColor = Color.Gray;
        //    fileToolStripMenuItem.ForeColor =Color.Orange;
        //}
        private bool mouseDown;
        private Point lastLocation;





        //code to allow dragging of form if we hide the border..
        private void MainForm_MouseDown_1(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void MainForm_MouseUp_1(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }

}