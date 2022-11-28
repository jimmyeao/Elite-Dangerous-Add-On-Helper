using Elite_Dangerous_Add_On_Helper.Model;
using Newtonsoft.Json;


namespace Elite_Dangerous_Add_On_Helper
{
    public partial class EditApp : Form
    {

        static readonly string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Elite Add On Helper\\";
        public AddOn AddOn;

        public string test;

        public EditApp(AddOn addon)

        {
            // addOns = addonDictionary;
            AddOn = addon;

            //Tb_AppName.Text = addOns.;
            Tb_AppPath.Text = addon.ProgramDirectory;
            Tb_Arguments.Text= addon.Scripts;
            Tb_Autodiscover.Text= addon.AutoDiscoverPath;
            Tb_ExeName.Text= settingsFilePath;
            Tb_InstallURL.Text= settingsFilePath; ;
            InitializeComponent();

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
        public EditApp(object sender)
        {
            InitializeComponent();

        }
        //private void HandleSelectPath(string dictKey)
        //{
        //    addOns.TryGetValue(dictKey, out var addOn); //get the AddOn model as "addOn" using the dictionary key

        //    if (addOn == null)
        //    {
        //        return;
        //    }
        //    OpenFileDialog openDialog = new OpenFileDialog();
        //    openDialog.Title = "Select A File";
        //    openDialog.Filter = "Executable files (*.exe)|*.exe";
        //    if (openDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        string file = openDialog.FileName;
        //        // need to get just the path element here, but store filename as well
        //        addOn.ProgramDirectory = Path.GetDirectoryName(file);
        //        addOn.ExecutableName = openDialog.SafeFileName;
        //        //addOn.ProgramDirectory = file;
        //    }

        //    addOns[dictKey] = addOn; //overwrite the existing addon in the dictionary with the updated model

        //}

        private void Bt_Save_Click(object sender, EventArgs e)
        {

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

        }

        private void Bt_BrowseArgs_Click(object sender, EventArgs e)
        {

        }


    }
}
