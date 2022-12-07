﻿namespace Elite_Dangerous_Add_On_Helper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Bt_Launch = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPrefsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Rb_Vr = new System.Windows.Forms.RadioButton();
            this.Rb_NonVR = new System.Windows.Forms.RadioButton();
            this.Cb_CloseOnExit = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Cb_Profiles = new System.Windows.Forms.ComboBox();
            this.Bt_AddProfile = new System.Windows.Forms.Button();
            this.Bt_RemoveProfile = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Bt_Launch
            // 
            this.Bt_Launch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Bt_Launch.Location = new System.Drawing.Point(770, 154);
            this.Bt_Launch.Name = "Bt_Launch";
            this.Bt_Launch.Size = new System.Drawing.Size(91, 29);
            this.Bt_Launch.TabIndex = 35;
            this.Bt_Launch.Text = "Launch!";
            this.Bt_Launch.UseVisualStyleBackColor = true;
            this.Bt_Launch.Click += new System.EventHandler(this.Bt_Launch_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(5, 5);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(873, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fileToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addApplicationToolStripMenuItem,
            this.openPrefsFolderToolStripMenuItem,
            this.savePreferencesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // addApplicationToolStripMenuItem
            // 
            this.addApplicationToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.addApplicationToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.addApplicationToolStripMenuItem.Name = "addApplicationToolStripMenuItem";
            this.addApplicationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.addApplicationToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.addApplicationToolStripMenuItem.Text = "Add Application";
            this.addApplicationToolStripMenuItem.Click += new System.EventHandler(this.addApplicationToolStripMenuItem_Click);
            // 
            // openPrefsFolderToolStripMenuItem
            // 
            this.openPrefsFolderToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.openPrefsFolderToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.openPrefsFolderToolStripMenuItem.Name = "openPrefsFolderToolStripMenuItem";
            this.openPrefsFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.openPrefsFolderToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.openPrefsFolderToolStripMenuItem.Text = "Open Prefs Folder";
            this.openPrefsFolderToolStripMenuItem.Click += new System.EventHandler(this.openPrefsFolderToolStripMenuItem_Click);
            // 
            // savePreferencesToolStripMenuItem
            // 
            this.savePreferencesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.savePreferencesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.savePreferencesToolStripMenuItem.Name = "savePreferencesToolStripMenuItem";
            this.savePreferencesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.savePreferencesToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.savePreferencesToolStripMenuItem.Text = "Save Preferences";
            this.savePreferencesToolStripMenuItem.Click += new System.EventHandler(this.savePreferencesToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(139, 26);
            this.aboutToolStripMenuItem.Text = "About..";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(858, 20);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(5, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(873, 26);
            this.statusStrip1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(770, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(91, 89);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(10, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "Enabled";
            // 
            // Rb_Vr
            // 
            this.Rb_Vr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Rb_Vr.AutoSize = true;
            this.Rb_Vr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rb_Vr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rb_Vr.Location = new System.Drawing.Point(770, 189);
            this.Rb_Vr.Name = "Rb_Vr";
            this.Rb_Vr.Size = new System.Drawing.Size(43, 24);
            this.Rb_Vr.TabIndex = 37;
            this.Rb_Vr.Text = "Vr";
            this.Rb_Vr.UseVisualStyleBackColor = false;
            this.Rb_Vr.CheckedChanged += new System.EventHandler(this.Rb_Vr_CheckedChanged);
            // 
            // Rb_NonVR
            // 
            this.Rb_NonVR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Rb_NonVR.AutoSize = true;
            this.Rb_NonVR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rb_NonVR.Checked = true;
            this.Rb_NonVR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rb_NonVR.Location = new System.Drawing.Point(770, 219);
            this.Rb_NonVR.Name = "Rb_NonVR";
            this.Rb_NonVR.Size = new System.Drawing.Size(75, 24);
            this.Rb_NonVR.TabIndex = 38;
            this.Rb_NonVR.TabStop = true;
            this.Rb_NonVR.Text = "Non Vr";
            this.Rb_NonVR.UseVisualStyleBackColor = false;
            this.Rb_NonVR.CheckedChanged += new System.EventHandler(this.Rb_NonVR_CheckedChanged);
            // 
            // Cb_CloseOnExit
            // 
            this.Cb_CloseOnExit.AutoSize = true;
            this.Cb_CloseOnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Cb_CloseOnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Cb_CloseOnExit.Location = new System.Drawing.Point(115, 69);
            this.Cb_CloseOnExit.Name = "Cb_CloseOnExit";
            this.Cb_CloseOnExit.Size = new System.Drawing.Size(161, 24);
            this.Cb_CloseOnExit.TabIndex = 39;
            this.Cb_CloseOnExit.Text = "Close Apps on Exit?";
            this.Cb_CloseOnExit.UseVisualStyleBackColor = false;
            this.Cb_CloseOnExit.CheckedChanged += new System.EventHandler(this.Cb_CloseOnExit_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(8, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(536, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.TabIndex = 41;
            this.label2.Text = "Delete";
            // 
            // Cb_Profiles
            // 
            this.Cb_Profiles.FormattingEnabled = true;
            this.Cb_Profiles.Location = new System.Drawing.Point(293, 67);
            this.Cb_Profiles.Name = "Cb_Profiles";
            this.Cb_Profiles.Size = new System.Drawing.Size(237, 28);
            this.Cb_Profiles.TabIndex = 42;
            // 
            // Bt_AddProfile
            // 
            this.Bt_AddProfile.Location = new System.Drawing.Point(293, 36);
            this.Bt_AddProfile.Name = "Bt_AddProfile";
            this.Bt_AddProfile.Size = new System.Drawing.Size(94, 29);
            this.Bt_AddProfile.TabIndex = 43;
            this.Bt_AddProfile.Text = "Add..";
            this.Bt_AddProfile.UseVisualStyleBackColor = true;
            // 
            // Bt_RemoveProfile
            // 
            this.Bt_RemoveProfile.Location = new System.Drawing.Point(436, 36);
            this.Bt_RemoveProfile.Name = "Bt_RemoveProfile";
            this.Bt_RemoveProfile.Size = new System.Drawing.Size(94, 29);
            this.Bt_RemoveProfile.TabIndex = 44;
            this.Bt_RemoveProfile.Text = "Remove";
            this.Bt_RemoveProfile.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(883, 483);
            this.Controls.Add(this.Bt_RemoveProfile);
            this.Controls.Add(this.Bt_AddProfile);
            this.Controls.Add(this.Cb_Profiles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Cb_CloseOnExit);
            this.Controls.Add(this.Rb_NonVR);
            this.Controls.Add(this.Rb_Vr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.Bt_Launch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Elite Dangerous Addon Helper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown_1);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem savePreferencesToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Button Bt_Launch;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private StatusStrip statusStrip1;
        private PictureBox pictureBox1;
        private ToolStripMenuItem openPrefsFolderToolStripMenuItem;
        private ToolStripMenuItem addApplicationToolStripMenuItem;
        private Label label1;
        private RadioButton Rb_Vr;
        private RadioButton Rb_NonVR;
        private CheckBox Cb_CloseOnExit;
        private Panel panel1;
        private Label label2;
        private ComboBox Cb_Profiles;
        private Button Bt_AddProfile;
        private Button Bt_RemoveProfile;
    }
}