
using Elite_Dangerous_Add_On_Helper.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using Octokit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;






// TODO LIST!
// Make a dependanciy between warthog being enabled and requiring a script to be specified

// ....


namespace Elite_Dangerous_Add_On_Helper
{
    public partial class MainForm : Form
    {
        // setup some variables
        static readonly string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Elite Add On Helper\\";
        static readonly HttpClient client = new HttpClient();   //used by the install app function
        public List<string> processList = new List<string>();   // holds a list of launched aps
        string[] launchargs = Environment.GetCommandLineArgs(); // gets any command line args that were passed at run time
        private int currentControlRow = 0;                      // used by createitems
        private bool mouseDown;                                 //used by proc to drag app around
        private Point lastLocation;                             //used by proc to drag app around
        private bool needsupdate = false;
        
        bool loaded = false;
        // i need to know what the below is actually doing... not got my head around it yet
        public Dictionary<string, AddOn> addOns = new Dictionary<string, AddOn>();

        // Create the ToolTip for use in createitems.
        ToolTip toolTip1 = new ToolTip();
        private Task<string> webresult;

        public static CancellationToken WebCommsTimeout { get; private set; }       //required for install code


        public MainForm()
        {

            InitializeComponent();
            //menuStrip1.BackColor = Color.;
            this.menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            this.menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
            this.statusStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
            menuStrip1.BackColor = Color.FromArgb(64, 64, 64);
            //string version = System.Windows.Forms.Application.ProductVersion;
            this.Text = String.Format("Elite Dangerous Add On Helper V{0}", AssemblyVersion);
            if (Properties.Settings.Default.ShouldUpgrade)
            {

                Properties.Settings.Default.Upgrade();

                Properties.Settings.Default.ShouldUpgrade = false;

            }
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            //check for a new release..
            // url for latest release
            // https://github.com/jimmyeao/Elite-Dangerous-Add-On-Helper/releases/latest/download/Elite.Add.On.Helper.msi
            //url to check version https://github.com/jimmyeao/Elite-Dangerous-Add-On-Helper/releases/latest
            // < a href = "/jimmyeao/Elite-Dangerous-Add-On-Helper/releases/tag/V1.88.28.0" data - view - component = "true" class="Link--primary" data-turbo-frame="repo-content-turbo-frame">V1.88.28.0</a>
            CheckGitHubNewerVersion();

            toolTip1.ShowAlways = true;
            Load_prefs();
        updatemystatus("Ready");
            loaded = true;
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
            var yPosition = ((currentControlRow) * 30) + 0;

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
            if (Path.Exists(addOn.ProgramDirectory) || Path.Exists(addOn.AutoDiscoverPath) || addOn.WebApp != String.Empty)
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
            if (addOn.Enabled == true)
            {
                checkBox.Checked = true;
            }
            else
            {
                checkBox.Checked = false;
            }

            addOn.EnableCheckbox = checkBox;
            panel1.Controls.Add(checkBox);
            this.Refresh();
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
            if (addOn.WebApp== String.Empty)    //no point adding an explorer browse box for a URL!
            {
                panel1.Controls.Add(button);
            }
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
            //panel1.Controls.Add(textBox);     //removed for form neatness, only required for debugging
            //add in install button
            if (addOn.Installable)
            {
                Button installButton = new Button();
                installButton.Text = "Install?";
                installButton.BackColor= Color.LightGray;
                installButton.Location = new System.Drawing.Point(450, yPosition);
                installButton.Size = new System.Drawing.Size(80, 30);
                //To the buttons click method, add this method, and pass it the friendly name (to use as the AddOns dictionary key)
                installButton.Click += (sender, e) => DoInstall(addOn);
                addOn.InstallButton = installButton;
                if (!Path.Exists(addOn.ProgramDirectory))
                {
                    panel1.Controls.Add(installButton);
                }
            }
            // create the edit button
            Button editButton = new Button();
            editButton.Text = "Edit";
            editButton.Location = new System.Drawing.Point(360, yPosition);
            editButton.Size = new System.Drawing.Size(80, 30);
            editButton.BackColor= Color.LightGray;
            editButton.Click += (sender, e) => DoEdit(addOn);
            toolTip1.SetToolTip(editButton, "Edit App");
            addOn.EditButton = editButton;
            panel1.Controls.Add(editButton);

            // lets create a delete button too to remove an app
            Button deleteButton = new Button();
            deleteButton.Text = "X";
            deleteButton.Location = new System.Drawing.Point(540, yPosition);
            deleteButton.Size = new System.Drawing.Size(30, 30);
            deleteButton.Click += (sender, e) => DeleteAddon(addOn);
            addOn.DeleteButton = deleteButton;
            toolTip1.SetToolTip(deleteButton, "Delete App from List");
            if (addOn.FriendlyName != "Elite")
            {
                panel1.Controls.Add(deleteButton);
            }
            panel1.Controls.OfType<Button>().ToList().ForEach(button => button.BackColor = Color.WhiteSmoke);
            currentControlRow++;            //move to the next row
        }

        #endregion
        #region functions
        #region autoupdate
        // Code for Autoupdate check
        private async System.Threading.Tasks.Task CheckGitHubNewerVersion()
        {
            //Get all releases from GitHub
            //Source: https://octokitnet.readthedocs.io/en/latest/getting-started/
            GitHubClient client = new GitHubClient(new ProductHeaderValue("Elite-Dangerous-Add-On-Helper"));
            IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("jimmyeao", "Elite-Dangerous-Add-On-Helper");

            //Setup the versions
            Version latestGitHubVersion = new Version(releases[0].TagName);
            Version localVersion = new Version(AssemblyVersion); 
                                                         //Only tested with numeric values.

            //Compare the Versions
            //Source: https://stackoverflow.com/questions/7568147/compare-version-numbers-without-using-split-function
            int versionComparison = localVersion.CompareTo(latestGitHubVersion);
            if (versionComparison < 0)
            {
                //The version on GitHub is more up to date than this local release. - need to prompt for download
                needsupdate = true;  //remove after testing!!!
                var result = MessageBox.Show("A new version is availabe, Do you want to update?", "Update Available",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                // If the yes button was pressed ...
                if (result == DialogResult.Yes)
                {

                    //do somenthing
                    //C
                    DownloadFileAndExecute("https://github.com/jimmyeao/Elite-Dangerous-Add-On-Helper/releases/latest/download/Elite.Add.On.Helper.msi");

                    

                }

            }
            else if (versionComparison > 0)
            {
                //This local version is greater than the release version on GitHub.
                needsupdate = false;
            
            }
            else
            {
                //This local Version and the Version on GitHub are equal.
                needsupdate = false;
            }
        }

        #endregion autoupdate
        private void DeleteAddon(AddOn addOn)
        {
            // delete addon
            addOns.Remove(addOn.FriendlyName);
            DeleteControls(addOn);
            //save new list
            SerializeAddons(addOns, Cb_Profiles.Text);
            //delete controls on form
            foreach (var addon in addOns.Values)
            {
                DeleteControls(addon);
            }
            //recreate controls on form
            foreach (var addon in addOns.Values)
            {
                CreateControls(addon);
            }


        }
        private void DeleteControls(AddOn addOn)
        {
            currentControlRow = 0;
            //currentControlRow -= 1;
            panel1.Controls.Remove(addOn.EnableCheckbox);
            panel1.Controls.Remove(addOn.AppDirectorytextbox);

            panel1.Controls.Remove(addOn.SelectPathButton);

            panel1.Controls.Remove(addOn.InstallButton);
            panel1.Controls.Remove(addOn.EditButton);
            if (addOn.InstallButton != null)
            {
                panel1.Controls.Remove(addOn.InstallButton);
            }
            panel1.Controls.Remove(addOn.DeleteButton);

            this.Refresh();

        }
        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(250, 90);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Name";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 29);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 29);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }
        private static DialogResult Showdialogue(string message, string caption)
        {
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return result;

        }
        private void Load_prefs()
        {
            //load preferences
            ////////////////
            if (Path.Exists(settingsFilePath))
            {
                DirectoryInfo d = new DirectoryInfo(@settingsFilePath);                 //which directory do we want to search?
                FileInfo[] fileArray = d.GetFiles("*.json");                            //Get all json files into array
                foreach (FileInfo file in fileArray)
                {
                    var result = System.IO.Path.GetFileNameWithoutExtension(file.Name); //this gets us the filename with no extension
                    if (result != "Default")
                    {

                        Cb_Profiles.Items.Add(result);                              //add the filename to the settings..

                    }
                }
                if (Properties.Settings.Default.ActiveProfile.Length > 0)               //check for a saved profile
                {
                    Cb_Profiles.SelectedIndex = Cb_Profiles.FindStringExact(Properties.Settings.Default.ActiveProfile);
                }
                else
                {
                    //no saved profile, default to the deault!
                    Cb_Profiles.SelectedIndex = Cb_Profiles.FindStringExact("AddOns");
                }
            }
            ////////////////

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
                if (File.Exists(settingsFilePath + $"{Cb_Profiles.Text}.json"))
                {
                    updatemystatus("Loading Settings");
                    addOns = DeserializeAddOns(Cb_Profiles.Text);

                }
                else
                {
                    // lets copy the default addons.json to the settings path..
                    // should never get here, settings file should be installed by installer, code for dev use only really
                    // string defaultpath = AppDomain.CurrentDomain.BaseDirectory;
                    string startupPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "addons.json");
                    string sourceFile = startupPath;
                    string destinationFile = settingsFilePath + "AddOns.json";
                    try
                    {
                        File.Copy(sourceFile, destinationFile, true);
                        updatemystatus("Settings copied");
                        updatemystatus("Loading Settings");
                        addOns = DeserializeAddOns("AddOns");
                        Cb_Profiles.SelectedIndex = Cb_Profiles.FindStringExact("AddOns");
                        if (Cb_Profiles.SelectedIndex == -1)
                        {
                            Cb_Profiles.Items.Add("AddOns");
                            Cb_Profiles.SelectedIndex = Cb_Profiles.FindStringExact("AddOns");
                        }//need to check if its -1 and add an item!
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


            if (Properties.Settings.Default.CLOSE == true)
            {
                Cb_CloseOnExit.Checked = true;
            }
            else
            {
                Cb_CloseOnExit.Checked = false;
            }

        }
        private void DoEdit(AddOn addOn)                              //send object to edit form (!!)
        {
            var EditApp = new EditApp(addOn);
            EditApp.ShowDialog();
            //DeleteControls(addOn);
            SerializeAddons(addOns, Cb_Profiles.Text);
            if (Path.Exists(addOn.ProgramDirectory) || Path.Exists(addOn.AutoDiscoverPath) || addOn.WebApp != String.Empty)
            {
                addOn.EnableCheckbox.BackColor = Color.LimeGreen;
                toolTip1.SetToolTip(addOn.EnableCheckbox, "Path Found");
            }
            else
            {
                // otherwise make it red to show its missing!
                addOn.EnableCheckbox.BackColor = Color.Red;
                toolTip1.SetToolTip(addOn.EnableCheckbox, "Path NOT Found");
            }


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
            System.IO.Directory.CreateDirectory(Environment.GetEnvironmentVariable("USERPROFILE") + @"\" + "Downloads\\EliteAddons\\");   //check the folder exists, and create if it doesnt
            string filename = Environment.GetEnvironmentVariable("USERPROFILE") + @"\" + "Downloads\\EliteAddons\\"  + Path.GetFileName(link);

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
                    using (var fs = new FileStream(filename, System.IO.FileMode.CreateNew))
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
                        if(link == "https://github.com/jimmyeao/Elite-Dangerous-Add-On-Helper/releases/latest/download/Elite.Add.On.Helper.msi")
                        {
                            System.Windows.Forms.Application.Exit();
                        }
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
                        if (link == "https://github.com/jimmyeao/Elite-Dangerous-Add-On-Helper/releases/latest/download/Elite.Add.On.Helper.msi")
                        {
                            System.Windows.Forms.Application.Exit();
                        }

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
                    System.Threading.Thread.Sleep(50);
                    proc.Refresh();


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
                //are we launching a web app?
                if (addOn.WebApp != String.Empty)
                {
                    //ok lets launch it in default browser
                    updatemystatus("Launching " + addOn.FriendlyName);
                    string target = addOn.WebApp;
                    Process.Start(new ProcessStartInfo(target) { UseShellExecute = true });
                    //System.Diagnostics.Process.Start(target);
                }
                else
                {
                    updatemystatus($"Unable to launch {addOn.FriendlyName}..");
                }

            }
            updatemystatus("All apps launched, waiting for EDLaunch Exit..");
            notifyIcon1.BalloonTipText = "All Apps running, waiting for exit";
            this.WindowState = FormWindowState.Minimized;


        }
        internal static Dictionary<string, AddOn> DeserializeAddOns(string profile)   //read settings to json and load into objects
        {
            var Json = File.ReadAllText(settingsFilePath + $"{profile}.json");
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
        internal static void SerializeAddons(object addOns, string filename)             // grabs all objects and saves states in json
        {
            var Json = JsonConvert.SerializeObject(addOns, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });

            File.WriteAllText(settingsFilePath + $"{filename}.json", Json);
            Properties.Settings.Default.ActiveProfile  = filename;
        }
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
            if (addOn.AutoDiscoverPath != String.Empty)
            {
                openDialog.InitialDirectory = addOn.AutoDiscoverPath;
            }
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openDialog.FileName;
                // need to get just the path element here, but store filename as well
                addOn.ProgramDirectory = Path.GetDirectoryName(file);
                addOn.ExecutableName = openDialog.SafeFileName;
                if (Path.Exists(addOn.ProgramDirectory) || Path.Exists(addOn.AutoDiscoverPath) || addOn.WebApp != String.Empty)
                {
                    //checkBox.addOn.FriendlyName.BackColor = Color.LimeGreen;
                    addOn.EnableCheckbox.BackColor = Color.LimeGreen;
                    toolTip1.SetToolTip(addOn.EnableCheckbox, "Path Found");
                    //SetToolTip(addOn.EnableCheckbox, "Path Found");
                }
                else
                {
                    // otherwise make it red to show its missing!
                    addOn.EnableCheckbox.BackColor = Color.Red;
                    toolTip1.SetToolTip(addOn.EnableCheckbox, "Path NOT Found");
                }
                //addOn.ProgramDirectory = file;
            }
            //currentControlRow = 0;
            //foreach (var addon in addOns.Values)
            //{
            //    DeleteControls(addon);
            //}
            //this.Refresh();
            //currentControlRow = 0;
            //foreach (var addon in addOns.Values)
            //{
            //    CreateControls(addon);
            //}
            //this.Refresh();

            addOns[dictKey] = addOn; //overwrite the existing addon in the dictionary with the updated model
                                     //redraw

        }
        private void ProcessExitHandler(object sender, EventArgs args)  //triggered when EDLaunch exits
        {
            // of Edlaunch has quit, does the user want us to kill all the apps?
            if (Cb_CloseOnExit.Checked)
            {
                //Process[] process;
                try
                {
                    foreach (string p in processList)
                        foreach (Process process in Process.GetProcessesByName(p))
                        {
                            // Temp is a document which you need to kill.
                            if (process.ProcessName.Contains(p))
                                process.CloseMainWindow();

                        }
                }
                catch
                {
                    // if something went wrong, I dont want to know about it..

                }
                // doesnt seem to want to kill voice attack nicely..
                try
                {
                    Process[] procs = Process.GetProcessesByName("VoiceAttack");
                    foreach (var proc in procs) { proc.Kill(); }        //sadly this means next time it starts, it will complain it was shutddown in an unclean fashion
                }
                catch (Exception ex)
                { // if something went wrong, I dont want to know about it..
                }
                // Ed Odyysey Materials Helper is a little strange, lets deal with its multiple running processes..
                try
                {
                    Process[] procs = Process.GetProcessesByName("Elite Dangerous Odyssey Materials Helper");
                    foreach (var proc in procs) { proc.CloseMainWindow(); }
                }
                catch (Exception ex)
                { // if something went wrong, I dont want to know about it..
                }
                //sleep for 5 secondfs then quit
                for (int i = 5; i != 0; i--)
                {

                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }
            }
        }
        private void refreshForm()
        {
            currentControlRow = 0;
            try
            {
                foreach (var addon in addOns.Values)
                {
                    DeleteControls(addon);
                }
                //recreate controls on form
                foreach (var addon in addOns.Values)
                {
                    CreateControls(addon);
                }
            }
            catch
            {
                // oops
            }
            this.Refresh();
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
            //  lets breath a little to let things start up..
            // updatemystatus("Ready");
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
            SerializeAddons(addOns, Cb_Profiles.Text);


            Properties.Settings.Default.Save();
        }
        private void openPrefsFolderToolStripMenuItem_Click(object sender, EventArgs e)     //Open folder containing settings file in windows explorer
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = settingsFilePath,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private void addApplicationToolStripMenuItem_Click(object sender, EventArgs e)      //add an application
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

        private void areYouSureToolStripMenuItem_Click(object sender, EventArgs e)          //tbc delete an app, may be moved into edit form
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
        #region save misc settings

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
        #endregion  
        #region moveform
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
        #endregion
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private void Bt_AddProfile_Click(object sender, EventArgs e)
        {
            //add profile

            //promt for name of profile
            string input = "Name?";

            ShowInputDialog(ref input);
            if (input != "Name?")
            {
                //ok valid input, lets do some stuff
                //is it a valid string?
                if (Regex.Match(input, @"^[a-zA-Z0-9]*$").Success)
                {
                    //valid string

                    Cb_Profiles.Items.Add(input);

                    //update the addons

                    Properties.Settings.Default.ActiveProfile = input;
                    Properties.Settings.Default.Save();

                    //save the profile

                    SerializeAddons(addOns, input);
                    Cb_Profiles.SelectedIndex = Cb_Profiles.FindStringExact(input);
                    refreshForm();

                }
                else
                {
                    //whhhooops invalid string!

                }
            }
            else
            {
                //user never typed anything..

            }

        }

        private void Bt_RemoveProfile_Click(object sender, EventArgs e)
        {
            var result = Showdialogue("Are You sure", "Delete Profile");
            if (result == DialogResult.Yes)
            {
                if (File.Exists($"{settingsFilePath}\\{Cb_Profiles.Text}.json"))
                {
                    File.Delete($"{settingsFilePath}\\{Cb_Profiles.Text}.json");
                }
                Cb_Profiles.Items.Remove(Cb_Profiles.Text);
                Cb_Profiles.SelectedIndex = 0;

            }
        }

        private void Cb_Profiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //logic on change
            if (loaded == true)
            {
                //clear form
                foreach (var addOn in addOns)
                {
                    DeleteControls(addOn.Value);

                }
                // remove apps
                addOns.Clear();
                // load json
                addOns = DeserializeAddOns(Cb_Profiles.Text);
                //recreate form
                //Load_prefs();
                foreach (var addOn in addOns)
                {
                    CreateControls(addOn.Value);
                }
            }

        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
           
                //if the form is minimized  
                //hide it from the task bar  
                //and show the system tray icon (represented by the NotifyIcon control)  
                if (this.WindowState == FormWindowState.Minimized)
                {
                    Hide();
                    notifyIcon1.Visible = true;

                notifyIcon1.ShowBalloonTip(1000);
            }
            
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
            notifyIcon1.BalloonTipText = "Ready";
        }
    }

}