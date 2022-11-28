namespace Elite_Dangerous_Add_On_Helper
{
    partial class AddApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddApp));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Tb_App_Name = new System.Windows.Forms.TextBox();
            this.Tb_AppPath = new System.Windows.Forms.TextBox();
            this.Tb_App_Args = new System.Windows.Forms.TextBox();
            this.Tb_InstallationURL = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Cb_Enable = new System.Windows.Forms.CheckBox();
            this.Bt_BrowsePath = new System.Windows.Forms.Button();
            this.Bt_BrowseArgs = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Tb_AppExeName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Tb_WebApURL = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Application Path";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(12, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Application Arguments";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(12, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Installation URL";
            // 
            // Tb_App_Name
            // 
            this.Tb_App_Name.Location = new System.Drawing.Point(190, 6);
            this.Tb_App_Name.Name = "Tb_App_Name";
            this.Tb_App_Name.Size = new System.Drawing.Size(278, 27);
            this.Tb_App_Name.TabIndex = 4;
            // 
            // Tb_AppPath
            // 
            this.Tb_AppPath.Location = new System.Drawing.Point(190, 56);
            this.Tb_AppPath.Name = "Tb_AppPath";
            this.Tb_AppPath.Size = new System.Drawing.Size(278, 27);
            this.Tb_AppPath.TabIndex = 5;
            // 
            // Tb_App_Args
            // 
            this.Tb_App_Args.Location = new System.Drawing.Point(190, 156);
            this.Tb_App_Args.Name = "Tb_App_Args";
            this.Tb_App_Args.Size = new System.Drawing.Size(278, 27);
            this.Tb_App_Args.TabIndex = 6;
            // 
            // Tb_InstallationURL
            // 
            this.Tb_InstallationURL.Location = new System.Drawing.Point(190, 206);
            this.Tb_InstallationURL.Name = "Tb_InstallationURL";
            this.Tb_InstallationURL.Size = new System.Drawing.Size(278, 27);
            this.Tb_InstallationURL.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(190, 318);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 8;
            this.button1.Text = "Add..";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(290, 318);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 9;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Cb_Enable
            // 
            this.Cb_Enable.AutoSize = true;
            this.Cb_Enable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Cb_Enable.Location = new System.Drawing.Point(12, 303);
            this.Cb_Enable.Name = "Cb_Enable";
            this.Cb_Enable.Size = new System.Drawing.Size(83, 24);
            this.Cb_Enable.TabIndex = 10;
            this.Cb_Enable.Text = "Enable?";
            this.Cb_Enable.UseVisualStyleBackColor = true;
            // 
            // Bt_BrowsePath
            // 
            this.Bt_BrowsePath.Location = new System.Drawing.Point(474, 52);
            this.Bt_BrowsePath.Name = "Bt_BrowsePath";
            this.Bt_BrowsePath.Size = new System.Drawing.Size(77, 29);
            this.Bt_BrowsePath.TabIndex = 12;
            this.Bt_BrowsePath.Text = "Browse";
            this.Bt_BrowsePath.UseVisualStyleBackColor = true;
            this.Bt_BrowsePath.Click += new System.EventHandler(this.Bt_BrowsePath_Click);
            // 
            // Bt_BrowseArgs
            // 
            this.Bt_BrowseArgs.Location = new System.Drawing.Point(474, 155);
            this.Bt_BrowseArgs.Name = "Bt_BrowseArgs";
            this.Bt_BrowseArgs.Size = new System.Drawing.Size(77, 29);
            this.Bt_BrowseArgs.TabIndex = 13;
            this.Bt_BrowseArgs.Text = "Browse";
            this.Bt_BrowseArgs.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(12, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Executable Name";
            // 
            // Tb_AppExeName
            // 
            this.Tb_AppExeName.Location = new System.Drawing.Point(190, 106);
            this.Tb_AppExeName.Name = "Tb_AppExeName";
            this.Tb_AppExeName.Size = new System.Drawing.Size(278, 27);
            this.Tb_AppExeName.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(12, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "WebApp URL";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // Tb_WebApURL
            // 
            this.Tb_WebApURL.Location = new System.Drawing.Point(190, 259);
            this.Tb_WebApURL.Name = "Tb_WebApURL";
            this.Tb_WebApURL.Size = new System.Drawing.Size(278, 27);
            this.Tb_WebApURL.TabIndex = 17;
            this.Tb_WebApURL.TextChanged += new System.EventHandler(this.Tb_WebApURL_TextChanged);
            // 
            // AddApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(566, 358);
            this.Controls.Add(this.Tb_WebApURL);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Tb_AppExeName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Bt_BrowseArgs);
            this.Controls.Add(this.Bt_BrowsePath);
            this.Controls.Add(this.Cb_Enable);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Tb_InstallationURL);
            this.Controls.Add(this.Tb_App_Args);
            this.Controls.Add(this.Tb_AppPath);
            this.Controls.Add(this.Tb_App_Name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox Tb_App_Name;
        private TextBox Tb_AppPath;
        private TextBox Tb_App_Args;
        private TextBox Tb_InstallationURL;
        private Button button1;
        private Button button2;
        private CheckBox Cb_Enable;
        private Button Bt_BrowsePath;
        private Button Bt_BrowseArgs;
        private Label label5;
        private TextBox Tb_AppExeName;
        private Label label6;
        private TextBox Tb_WebApURL;
    }
}