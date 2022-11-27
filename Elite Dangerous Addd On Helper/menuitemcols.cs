using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// this file controls the colours of the menu strip
namespace Elite_Dangerous_Add_On_Helper
{
    public class CustomColorTable : ProfessionalColorTable
    {
        //a bunch of other overrides...
        public override Color StatusStripBorder
        {
            get { return Color.DarkGray; }
        }
        public override Color MenuBorder
        {
            get { return Color.DarkGray; }
        }
        public override Color MenuItemSelected
        {
            get { return Color.Yellow; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.Orange; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.Yellow; }
        }
        public override Color ToolStripBorder
        {
            get { return Color.DarkGray; }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return Color.DarkGray; }
        }
        public override Color ToolStripGradientBegin
        {
            get { return Color.DarkGray; }
        }
        public override Color ToolStripGradientEnd
        {
            get { return Color.DarkGray; }
        }
        public override Color ToolStripGradientMiddle
        {
            get { return Color.DarkGray; }
        }
    }
}
