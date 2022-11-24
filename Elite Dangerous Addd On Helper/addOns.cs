using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite_Dangerous_Add_On_Helper.Model
{
    [Serializable]
    public class AddOn : INotifyPropertyChanged
    {
        /// <summary>
        /// Add on for if enabled (box checked)
        /// </summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// String to file location on computer
        /// </summary>
        public string ProgramDirectory { get; set; } = string.Empty;

        /// <summary>
        /// Human readable name for displaying on screen etc
        /// </summary>
        public string FriendlyName { get; set; } = string.Empty;

        /// <summary>
        /// Name of the executable
        /// </summary>
        public string ExecutableName { get; set; } = string.Empty;

        /// <summary>
        /// If install button should show
        /// </summary>
        public bool Installable { get; set; } = false;

        /// <summary>
        /// Path for auto discovery.
        /// </summary>
        public string AutoDiscoverPath { get; set; } = string.Empty;

        /// <summary>
        /// Path for extras e.g. TARGET scripts.
        /// </summary>
        public string Scripts { get; set; } = string.Empty;

#pragma warning disable 67 
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67

    }
}
