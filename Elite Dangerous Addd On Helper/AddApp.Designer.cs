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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Application Path";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Application Arguments";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 150);
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
            this.Tb_AppPath.Location = new System.Drawing.Point(190, 53);
            this.Tb_AppPath.Name = "Tb_AppPath";
            this.Tb_AppPath.Size = new System.Drawing.Size(278, 27);
            this.Tb_AppPath.TabIndex = 5;
            // 
            // Tb_App_Args
            // 
            this.Tb_App_Args.Location = new System.Drawing.Point(190, 100);
            this.Tb_App_Args.Name = "Tb_App_Args";
            this.Tb_App_Args.Size = new System.Drawing.Size(278, 27);
            this.Tb_App_Args.TabIndex = 6;
            // 
            // Tb_InstallationURL
            // 
            this.Tb_InstallationURL.Location = new System.Drawing.Point(190, 147);
            this.Tb_InstallationURL.Name = "Tb_InstallationURL";
            this.Tb_InstallationURL.Size = new System.Drawing.Size(278, 27);
            this.Tb_InstallationURL.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(190, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 8;
            this.button1.Text = "Add..";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(290, 199);
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
            this.Cb_Enable.Location = new System.Drawing.Point(12, 184);
            this.Cb_Enable.Name = "Cb_Enable";
            this.Cb_Enable.Size = new System.Drawing.Size(83, 24);
            this.Cb_Enable.TabIndex = 10;
            this.Cb_Enable.Text = "Enable?";
            this.Cb_Enable.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(557, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(231, 222);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            // 
            // AddApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 244);
            this.Controls.Add(this.richTextBox1);
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
        private RichTextBox richTextBox1;
    }
}