﻿namespace ChannelAdvisor
{
    partial class BenchmarkSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.commonSettings = new ChannelAdvisor.CommonVendorSettings();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAPI = new System.Windows.Forms.RadioButton();
            this.rbFTP = new System.Windows.Forms.RadioButton();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Benchmark Information";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(54, 271);
            this.txtURL.MaxLength = 100;
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(474, 22);
            this.txtURL.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "URL:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(91, 303);
            this.txtUsername.MaxLength = 100;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(181, 22);
            this.txtUsername.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 306);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 14);
            this.label3.TabIndex = 18;
            this.label3.Text = "Username:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(365, 303);
            this.txtPassword.MaxLength = 100;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(165, 22);
            this.txtPassword.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 14);
            this.label4.TabIndex = 20;
            this.label4.Text = "Password:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(177, 425);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 26);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(290, 425);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 26);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // commonSettings
            // 
            this.commonSettings.IsNameEditable = false;
            this.commonSettings.Location = new System.Drawing.Point(13, 13);
            this.commonSettings.Name = "commonSettings";
            this.commonSettings.Size = new System.Drawing.Size(550, 230);
            this.commonSettings.TabIndex = 26;
            this.commonSettings.VendorInfo = null;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAPI);
            this.groupBox1.Controls.Add(this.rbFTP);
            this.groupBox1.Location = new System.Drawing.Point(392, 345);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 74);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            // 
            // rbAPI
            // 
            this.rbAPI.AutoSize = true;
            this.rbAPI.Checked = true;
            this.rbAPI.Location = new System.Drawing.Point(6, 21);
            this.rbAPI.Name = "rbAPI";
            this.rbAPI.Size = new System.Drawing.Size(115, 18);
            this.rbAPI.TabIndex = 62;
            this.rbAPI.TabStop = true;
            this.rbAPI.Text = "Update by API";
            this.rbAPI.UseVisualStyleBackColor = true;
            // 
            // rbFTP
            // 
            this.rbFTP.AutoSize = true;
            this.rbFTP.Location = new System.Drawing.Point(6, 45);
            this.rbFTP.Name = "rbFTP";
            this.rbFTP.Size = new System.Drawing.Size(116, 18);
            this.rbFTP.TabIndex = 63;
            this.rbFTP.Text = "Update by FTP";
            this.rbFTP.UseVisualStyleBackColor = true;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(99, 392);
            this.txtFileName.MaxLength = 20;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(244, 22);
            this.txtFileName.TabIndex = 68;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 395);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 14);
            this.label6.TabIndex = 69;
            this.label6.Text = "File name:";
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(113, 362);
            this.txtFolderName.MaxLength = 200;
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(228, 22);
            this.txtFolderName.TabIndex = 66;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 366);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 14);
            this.label8.TabIndex = 67;
            this.label8.Text = "Folder name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 339);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(213, 16);
            this.label7.TabIndex = 65;
            this.label7.Text = "Benchmark CSV Information";
            // 
            // BenchmarkSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 457);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.commonSettings);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BenchmarkSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Benchmark Settings";
            this.Load += new System.EventHandler(this.Benchmark_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private CommonVendorSettings commonSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAPI;
        private System.Windows.Forms.RadioButton rbFTP;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}