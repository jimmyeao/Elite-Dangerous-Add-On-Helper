﻿using System;
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
        public AddApp(Dictionary<string, AddOn> addonDictionary)

        {
            addOns = addonDictionary;
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
            return "";
        }
        private void listaddons(AddOn addOn)
        {


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
            if (Tb_AppPath.Text == string.Empty && Tb_WebApURL.Text == string.Empty)
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
                    Enabled = Cb_Enable.Checked,            // this will always have a bool state of checked or unchecked
                    Installable = caninstall,               // bool caninstall set to true if an installation URL exists. Note, validity of url not yet checked
                    ProgramDirectory = Tb_AppPath.Text,     // mandatory field unless webapp url set
                    FriendlyName = Tb_App_Name.Text,        // mandatory field
                    ExecutableName = Tb_AppExeName.Text,    // mandatory field
                    AutoDiscoverPath = "",                  // not implimented unless webapp url set
                    Scripts = Tb_App_Args.Text,             // not implimented, required for TARGET scripts
                    Url = Tb_InstallationURL.Text,          // non mandatory
                    WebApp = Tb_WebApURL.Text               // non mandatory
                });
                //SerializeAddons(addOns);
                this.Dispose();
            }

        }
        private void FindSelectPath()
        {

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select A File";
            openDialog.Filter = "Executable files (*.exe)|*.exe";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openDialog.FileName;

                if (file == string.Empty)
                {

                }
                else
                {
                    Tb_AppExeName.Text = openDialog.SafeFileName;
                    Tb_AppPath.Text = Path.GetDirectoryName(file);
                    // if user hant filled in an app name, lets generate it from the exe.
                    if (Tb_App_Name.Text == string.Empty)
                    {
                        Tb_App_Name.Text = Tb_AppExeName.Text.Replace(".exe", "");
                    }
                }

            }

            //return null;

        }

        private void Bt_BrowsePath_Click(object sender, EventArgs e)
        {
            FindSelectPath();


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Tb_WebApURL_TextChanged(object sender, EventArgs e)
        {
            if (Tb_WebApURL.Text == String.Empty)
            {
                Tb_AppExeName.Enabled= true;
                Tb_AppPath.Enabled= true;
                Tb_App_Args.Enabled= true;
                Tb_InstallationURL.Enabled= true;
                Bt_BrowseArgs.Enabled= true;
                Bt_BrowsePath.Enabled= true;
            }
            else
            {
                Tb_AppExeName.Enabled= false;
                Tb_AppPath.Enabled= false;
                Tb_App_Args.Enabled= false;
                Tb_InstallationURL.Enabled= false;
                Bt_BrowsePath.Enabled= false;
                Bt_BrowseArgs.Enabled= false;
            }
        }
    }
}
