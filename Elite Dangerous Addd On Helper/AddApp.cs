using System;
using System.Collections.Generic;
using Elite_Dangerous_Add_On_Helper.Model;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Elite_Dangerous_Add_On_Helper
{

    public partial class AddApp : Form
    {
        static readonly string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Elite Add On Helper\\";
        public Dictionary<string, AddOn> addOns = new Dictionary<string, AddOn>();
        public bool caninstall;
        public AddApp()
        {
            InitializeComponent();
            foreach (var addon in addOns.Values)
            {
                listaddons(addon);
            }

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
        private void button2_Click(object sender, EventArgs e)
        {
            // this is the cancel button

            this.Dispose();
        }
        private string fieldMissing(string field)
        {
            // user forgot to fill in a field, lets show a friendly reminder
            MessageBox.Show("You must enter a value for " + field, "Missing Info",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        private void listaddons(AddOn addOn)
        {
            richTextBox1.Text += addOn.FriendlyName;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // this is the add app routine
            //lets validate the fields:

            if (Tb_App_Name.Text == string.Empty)
            {
                fieldMissing("Application Name");
                return;
            }
            if (Tb_AppPath.Text == string.Empty)
            {
                fieldMissing("Application Path");
                return;
            }
            if (Tb_InstallationURL.Text != string.Empty)
            {
                caninstall = true;
            }

            if (!addOns.ContainsKey(Tb_App_Name.Text))
            {
                addOns.Add(Tb_App_Name.Text, new AddOn
                {
                    Enabled = Cb_Enable.Checked,
                    Installable = caninstall,
                    ProgramDirectory = Tb_AppPath.Text,
                    FriendlyName = Tb_App_Name.Text,
                    ExecutableName = Tb_App_Name.Text,
                    AutoDiscoverPath = "",
                    Scripts = Tb_App_Args.Text,
                    Url = Tb_InstallationURL.Text
                });
                SerializeAddons(addOns);
                this.Dispose();
            }

        }
    }
}
