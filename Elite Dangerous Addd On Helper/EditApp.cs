using Elite_Dangerous_Add_On_Helper.Model;
using Newtonsoft.Json;


namespace Elite_Dangerous_Add_On_Helper
{
    public partial class EditApp : Form
    {

        static readonly string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Elite Add On Helper\\";
        public AddOn AddOn;

        // public string test;

        //public EditApp(AddOn addon)
        //{
        //    // addOns = addonDictionary;

        //    InitializeComponent();
        //    AddOn = addon;

        //    //Tb_AppName.Text = addOns.;
        //    Tb_AppPath.Text = addon.ProgramDirectory;
        //    Tb_Arguments.Text= addon.Scripts;
        //    Tb_Autodiscover.Text= addon.AutoDiscoverPath;
        //    Tb_ExeName.Text= settingsFilePath;
        //    Tb_InstallURL.Text= settingsFilePath; ;
        //}

        internal static void SerializeAddons(object addOns)
        {
            var Json = JsonConvert.SerializeObject(addOns, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });

            File.WriteAllText(settingsFilePath + "AddOns.json", Json);
        }
        public EditApp(AddOn addon)
        {
            InitializeComponent();
            AddOn = addon;
            //ok, we have the addon instance, lets populate the form.
            Tb_AppName.Text = addon.FriendlyName;
            Tb_AppPath.Text = addon.ProgramDirectory;
            Tb_Arguments.Text= addon.Scripts;
            Tb_Autodiscover.Text= addon.AutoDiscoverPath;
            Tb_ExeName.Text= AddOn.ExecutableName;
            Tb_InstallURL.Text= addon.Url; ;
            Tb_WebAppURL.Text = addon.WebApp;
            //disable fields if its a web app
            if (addon.WebApp != string.Empty)
            {
                Tb_AppPath.Enabled= false; Tb_ExeName.Enabled= false;
                Tb_InstallURL.Enabled= false;
                Tb_Autodiscover.Enabled= false;
                Tb_Arguments.Enabled= false;
            }
        }

        private void Bt_Save_Click(object sender, EventArgs e)
        {
            AddOn.FriendlyName = Tb_AppName.Text;
            AddOn.ProgramDirectory= Tb_AppPath.Text;
            AddOn.ExecutableName = Tb_ExeName.Text;
            AddOn.ProgramDirectory = Tb_AppPath.Text;
            AddOn.AutoDiscoverPath= Tb_Autodiscover.Text;
            AddOn.Url = Tb_InstallURL.Text;
            AddOn.WebApp = Tb_WebAppURL.Text;
            AddOn.Scripts = Tb_Arguments.Text;
            this.Dispose();

        }

        private void Bt_Delete_Click(object sender, EventArgs e)
        {

        }

        private void Bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Bt_Browse_Click(object sender, EventArgs e)
        {
            //addOns.TryGetValue(dictKey, out var addOn); //get the AddOn model as "addOn" using the dictionary key

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select A File";
            openDialog.Filter = "Executable files (*.exe)|*.exe";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openDialog.FileName;
                // need to get just the path element here, but store filename as well
                Tb_AppPath.Text = Path.GetDirectoryName(file);
                Tb_ExeName.Text = openDialog.SafeFileName;
                //addOn.ProgramDirectory = file;
            }


        }

        private void Bt_BrowseArgs_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select A Script File";
            openDialog.Filter = "Executable files (*.*)|*.*";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openDialog.FileName;
                // need to get just the path element here, but store filename as well
                Tb_Arguments.Text = file;

            }
        }

        private void Tb_WebAppURL_TextChanged(object sender, EventArgs e)
        {
            if (Tb_WebAppURL.Text != string.Empty)
            {
                Tb_AppPath.Enabled= false; Tb_ExeName.Enabled= false;
                Tb_InstallURL.Enabled= false;
                Tb_Autodiscover.Enabled= false;
                Tb_Arguments.Enabled= false;
            }
            else
            {
                Tb_AppPath.Enabled= true; Tb_ExeName.Enabled= true;
                Tb_InstallURL.Enabled= true;
                Tb_Autodiscover.Enabled= true;
                Tb_Arguments.Enabled= true;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Tb_Arguments_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
