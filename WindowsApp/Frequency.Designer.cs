namespace ChannelAdvisor
{
    partial class Frequency
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
            System.Windows.Forms.ListViewItem listViewItem43 = new System.Windows.Forms.ListViewItem("Monday");
            System.Windows.Forms.ListViewItem listViewItem44 = new System.Windows.Forms.ListViewItem("Tuesday");
            System.Windows.Forms.ListViewItem listViewItem45 = new System.Windows.Forms.ListViewItem("Wednesday");
            System.Windows.Forms.ListViewItem listViewItem46 = new System.Windows.Forms.ListViewItem("Thursday");
            System.Windows.Forms.ListViewItem listViewItem47 = new System.Windows.Forms.ListViewItem("Friday");
            System.Windows.Forms.ListViewItem listViewItem48 = new System.Windows.Forms.ListViewItem("Saturday");
            System.Windows.Forms.ListViewItem listViewItem49 = new System.Windows.Forms.ListViewItem("Sunday");
            System.Windows.Forms.ListViewItem listViewItem50 = new System.Windows.Forms.ListViewItem("Monday");
            System.Windows.Forms.ListViewItem listViewItem51 = new System.Windows.Forms.ListViewItem("Tuesday");
            System.Windows.Forms.ListViewItem listViewItem52 = new System.Windows.Forms.ListViewItem("Wednesday");
            System.Windows.Forms.ListViewItem listViewItem53 = new System.Windows.Forms.ListViewItem("Thursday");
            System.Windows.Forms.ListViewItem listViewItem54 = new System.Windows.Forms.ListViewItem("Friday");
            System.Windows.Forms.ListViewItem listViewItem55 = new System.Windows.Forms.ListViewItem("Saturday");
            System.Windows.Forms.ListViewItem listViewItem56 = new System.Windows.Forms.ListViewItem("Sunday");
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbProfileToUpdate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgFrequencyTimes = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstWeekDays = new System.Windows.Forms.ListView();
            this.WeekDay = new System.Windows.Forms.ColumnHeader();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbVendors = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgProfiles = new System.Windows.Forms.DataGridView();
            this.btnMultiDelete = new System.Windows.Forms.Button();
            this.dgMultiFrequencyTimes = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lstMultiWeekdays = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Update = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Profile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APIKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFrequencyTimes)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProfiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiFrequencyTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbProfileToUpdate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.dgFrequencyTimes);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lstWeekDays);
            this.groupBox1.Location = new System.Drawing.Point(12, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(554, 202);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Single Profile Frequency";
            // 
            // cmbProfileToUpdate
            // 
            this.cmbProfileToUpdate.FormattingEnabled = true;
            this.cmbProfileToUpdate.Location = new System.Drawing.Point(354, 46);
            this.cmbProfileToUpdate.Name = "cmbProfileToUpdate";
            this.cmbProfileToUpdate.Size = new System.Drawing.Size(168, 24);
            this.cmbProfileToUpdate.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Profile To Update:";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(296, 46);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 30);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "-";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgFrequencyTimes
            // 
            this.dgFrequencyTimes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFrequencyTimes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgFrequencyTimes.Location = new System.Drawing.Point(157, 46);
            this.dgFrequencyTimes.Name = "dgFrequencyTimes";
            this.dgFrequencyTimes.Size = new System.Drawing.Size(133, 143);
            this.dgFrequencyTimes.TabIndex = 23;
            this.dgFrequencyTimes.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgFrequencyTimes_EditingControlShowing);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 21;
            this.label3.Text = "Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "Weekdays:";
            // 
            // lstWeekDays
            // 
            this.lstWeekDays.CheckBoxes = true;
            this.lstWeekDays.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.WeekDay});
            this.lstWeekDays.GridLines = true;
            this.lstWeekDays.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem43.StateImageIndex = 0;
            listViewItem44.StateImageIndex = 0;
            listViewItem45.StateImageIndex = 0;
            listViewItem46.StateImageIndex = 0;
            listViewItem47.StateImageIndex = 0;
            listViewItem48.StateImageIndex = 0;
            listViewItem49.StateImageIndex = 0;
            this.lstWeekDays.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem43,
            listViewItem44,
            listViewItem45,
            listViewItem46,
            listViewItem47,
            listViewItem48,
            listViewItem49});
            this.lstWeekDays.Location = new System.Drawing.Point(9, 46);
            this.lstWeekDays.MultiSelect = false;
            this.lstWeekDays.Name = "lstWeekDays";
            this.lstWeekDays.Size = new System.Drawing.Size(138, 141);
            this.lstWeekDays.TabIndex = 15;
            this.lstWeekDays.UseCompatibleStateImageBehavior = false;
            this.lstWeekDays.View = System.Windows.Forms.View.List;
            // 
            // WeekDay
            // 
            this.WeekDay.Width = 100;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(352, 524);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 30);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(265, 524);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 30);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbVendors
            // 
            this.cmbVendors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendors.FormattingEnabled = true;
            this.cmbVendors.Location = new System.Drawing.Point(91, 19);
            this.cmbVendors.Name = "cmbVendors";
            this.cmbVendors.Size = new System.Drawing.Size(168, 24);
            this.cmbVendors.TabIndex = 25;
            this.cmbVendors.SelectedIndexChanged += new System.EventHandler(this.cmbVendors_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = "Vendor:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dgProfiles);
            this.groupBox2.Controls.Add(this.btnMultiDelete);
            this.groupBox2.Controls.Add(this.dgMultiFrequencyTimes);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lstMultiWeekdays);
            this.groupBox2.Location = new System.Drawing.Point(12, 264);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(671, 240);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Multi Profile Frequency";
            // 
            // dgProfiles
            // 
            this.dgProfiles.AllowUserToAddRows = false;
            this.dgProfiles.AllowUserToDeleteRows = false;
            this.dgProfiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProfiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Update,
            this.Profile,
            this.APIKey});
            this.dgProfiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgProfiles.Location = new System.Drawing.Point(340, 41);
            this.dgProfiles.Name = "dgProfiles";
            this.dgProfiles.Size = new System.Drawing.Size(312, 192);
            this.dgProfiles.TabIndex = 25;
            // 
            // btnMultiDelete
            // 
            this.btnMultiDelete.Location = new System.Drawing.Point(296, 41);
            this.btnMultiDelete.Name = "btnMultiDelete";
            this.btnMultiDelete.Size = new System.Drawing.Size(36, 30);
            this.btnMultiDelete.TabIndex = 24;
            this.btnMultiDelete.Text = "-";
            this.btnMultiDelete.UseVisualStyleBackColor = true;
            this.btnMultiDelete.Click += new System.EventHandler(this.btnMultiDelete_Click);
            // 
            // dgMultiFrequencyTimes
            // 
            this.dgMultiFrequencyTimes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMultiFrequencyTimes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgMultiFrequencyTimes.Location = new System.Drawing.Point(157, 41);
            this.dgMultiFrequencyTimes.Name = "dgMultiFrequencyTimes";
            this.dgMultiFrequencyTimes.Size = new System.Drawing.Size(133, 143);
            this.dgMultiFrequencyTimes.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(154, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 16);
            this.label6.TabIndex = 21;
            this.label6.Text = "Time:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Weekdays:";
            // 
            // lstMultiWeekdays
            // 
            this.lstMultiWeekdays.CheckBoxes = true;
            this.lstMultiWeekdays.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstMultiWeekdays.GridLines = true;
            this.lstMultiWeekdays.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem50.StateImageIndex = 0;
            listViewItem51.StateImageIndex = 0;
            listViewItem52.StateImageIndex = 0;
            listViewItem53.StateImageIndex = 0;
            listViewItem54.StateImageIndex = 0;
            listViewItem55.StateImageIndex = 0;
            listViewItem56.StateImageIndex = 0;
            this.lstMultiWeekdays.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem50,
            listViewItem51,
            listViewItem52,
            listViewItem53,
            listViewItem54,
            listViewItem55,
            listViewItem56});
            this.lstMultiWeekdays.Location = new System.Drawing.Point(9, 41);
            this.lstMultiWeekdays.MultiSelect = false;
            this.lstMultiWeekdays.Name = "lstMultiWeekdays";
            this.lstMultiWeekdays.Size = new System.Drawing.Size(138, 141);
            this.lstMultiWeekdays.TabIndex = 15;
            this.lstMultiWeekdays.UseCompatibleStateImageBehavior = false;
            this.lstMultiWeekdays.View = System.Windows.Forms.View.List;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 100;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // Update
            // 
            this.Update.HeaderText = "";
            this.Update.Name = "Update";
            this.Update.Width = 30;
            // 
            // Profile
            // 
            this.Profile.DataPropertyName = "Profile";
            this.Profile.HeaderText = "Profile";
            this.Profile.Name = "Profile";
            this.Profile.ReadOnly = true;
            this.Profile.Width = 220;
            // 
            // APIKey
            // 
            this.APIKey.DataPropertyName = "ProfileAPIKey";
            this.APIKey.HeaderText = "APIKey";
            this.APIKey.Name = "APIKey";
            this.APIKey.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "Profile(s) To Update:";
            // 
            // Frequency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 566);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmbVendors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frequency";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frequency";
            this.Load += new System.EventHandler(this.Frequency_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFrequencyTimes)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProfiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiFrequencyTimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lstWeekDays;
        private System.Windows.Forms.ColumnHeader WeekDay;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgFrequencyTimes;
        private System.Windows.Forms.ComboBox cmbVendors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cmbProfileToUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnMultiDelete;
        private System.Windows.Forms.DataGridView dgMultiFrequencyTimes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lstMultiWeekdays;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.DataGridView dgProfiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Update;
        private System.Windows.Forms.DataGridViewTextBoxColumn Profile;
        private System.Windows.Forms.DataGridViewTextBoxColumn APIKey;
        private System.Windows.Forms.Label label5;
    }
}