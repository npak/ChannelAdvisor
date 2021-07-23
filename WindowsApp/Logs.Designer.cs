namespace ChannelAdvisor
{
    partial class Logs
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
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbVendor = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCopyLocalErrors = new System.Windows.Forms.Button();
            this.btnCopyCAErrors = new System.Windows.Forms.Button();
            this.btnShowLogs = new System.Windows.Forms.Button();
            this.btnDeleteLogs = new System.Windows.Forms.Button();
            this.btnDelete1WeekOldLogs = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VendorFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgCAErrors = new ChannelAdvisor.CADataGridView();
            this.CAErrorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAErrorDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgProfiles = new ChannelAdvisor.CADataGridView();
            this.ProfileID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Profile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgLocalErrors = new ChannelAdvisor.CADataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgSchedules = new ChannelAdvisor.CADataGridView();
            this.sID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sVendorFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgCAErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProfiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLocalErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSchedules)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vendor:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(346, 654);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 30);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbVendor
            // 
            this.cmbVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendor.FormattingEnabled = true;
            this.cmbVendor.Location = new System.Drawing.Point(102, 12);
            this.cmbVendor.Name = "cmbVendor";
            this.cmbVendor.Size = new System.Drawing.Size(203, 24);
            this.cmbVendor.TabIndex = 23;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(102, 46);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(121, 23);
            this.dtpFrom.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "From Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "To Date:";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(102, 79);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(121, 23);
            this.dtpTo.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "Channel Advisor Updates:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 300);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 16);
            this.label5.TabIndex = 29;
            this.label5.Text = "Local Errors:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 478);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 16);
            this.label6.TabIndex = 32;
            this.label6.Text = "Channel Advisor Errors:";
            // 
            // btnCopyLocalErrors
            // 
            this.btnCopyLocalErrors.Location = new System.Drawing.Point(700, 322);
            this.btnCopyLocalErrors.Name = "btnCopyLocalErrors";
            this.btnCopyLocalErrors.Size = new System.Drawing.Size(31, 30);
            this.btnCopyLocalErrors.TabIndex = 35;
            this.btnCopyLocalErrors.Text = "C";
            this.btnCopyLocalErrors.UseVisualStyleBackColor = true;
            this.btnCopyLocalErrors.Click += new System.EventHandler(this.btnCopyLocalErrors_Click);
            // 
            // btnCopyCAErrors
            // 
            this.btnCopyCAErrors.Location = new System.Drawing.Point(737, 497);
            this.btnCopyCAErrors.Name = "btnCopyCAErrors";
            this.btnCopyCAErrors.Size = new System.Drawing.Size(31, 30);
            this.btnCopyCAErrors.TabIndex = 36;
            this.btnCopyCAErrors.Text = "C";
            this.btnCopyCAErrors.UseVisualStyleBackColor = true;
            this.btnCopyCAErrors.Click += new System.EventHandler(this.btnCopyCAErrors_Click);
            // 
            // btnShowLogs
            // 
            this.btnShowLogs.Location = new System.Drawing.Point(346, 12);
            this.btnShowLogs.Name = "btnShowLogs";
            this.btnShowLogs.Size = new System.Drawing.Size(121, 30);
            this.btnShowLogs.TabIndex = 37;
            this.btnShowLogs.Text = "Show Logs";
            this.btnShowLogs.UseVisualStyleBackColor = true;
            this.btnShowLogs.Click += new System.EventHandler(this.btnShowLogs_Click);
            // 
            // btnDeleteLogs
            // 
            this.btnDeleteLogs.Location = new System.Drawing.Point(346, 48);
            this.btnDeleteLogs.Name = "btnDeleteLogs";
            this.btnDeleteLogs.Size = new System.Drawing.Size(121, 30);
            this.btnDeleteLogs.TabIndex = 38;
            this.btnDeleteLogs.Text = "Delete Logs";
            this.btnDeleteLogs.UseVisualStyleBackColor = true;
            this.btnDeleteLogs.Click += new System.EventHandler(this.btnDeleteLogs_Click);
            // 
            // btnDelete1WeekOldLogs
            // 
            this.btnDelete1WeekOldLogs.Location = new System.Drawing.Point(483, 12);
            this.btnDelete1WeekOldLogs.Name = "btnDelete1WeekOldLogs";
            this.btnDelete1WeekOldLogs.Size = new System.Drawing.Size(248, 30);
            this.btnDelete1WeekOldLogs.TabIndex = 39;
            this.btnDelete1WeekOldLogs.Text = "Delete All Logs Over 1 Month Old";
            this.btnDelete1WeekOldLogs.UseVisualStyleBackColor = true;
            this.btnDelete1WeekOldLogs.Click += new System.EventHandler(this.btnDelete1WeekOldLogs_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // VendorFile
            // 
            this.VendorFile.DataPropertyName = "VendorFile";
            this.VendorFile.HeaderText = "Vendor File";
            this.VendorFile.Name = "VendorFile";
            this.VendorFile.Width = 200;
            // 
            // dgCAErrors
            // 
            this.dgCAErrors.AllowUserToAddRows = false;
            this.dgCAErrors.AllowUserToDeleteRows = false;
            this.dgCAErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCAErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CAErrorID,
            this.CAErrorDesc});
            this.dgCAErrors.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgCAErrors.Location = new System.Drawing.Point(238, 497);
            this.dgCAErrors.Name = "dgCAErrors";
            this.dgCAErrors.Size = new System.Drawing.Size(493, 150);
            this.dgCAErrors.TabIndex = 34;
            // 
            // CAErrorID
            // 
            this.CAErrorID.DataPropertyName = "ID";
            this.CAErrorID.HeaderText = "ID";
            this.CAErrorID.Name = "CAErrorID";
            this.CAErrorID.Visible = false;
            // 
            // CAErrorDesc
            // 
            this.CAErrorDesc.DataPropertyName = "ErrorDesc";
            this.CAErrorDesc.HeaderText = "Error Description";
            this.CAErrorDesc.Name = "CAErrorDesc";
            this.CAErrorDesc.Width = 420;
            // 
            // dgProfiles
            // 
            this.dgProfiles.AllowUserToAddRows = false;
            this.dgProfiles.AllowUserToDeleteRows = false;
            this.dgProfiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProfiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProfileID,
            this.Profile,
            this.CAFile});
            this.dgProfiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgProfiles.Location = new System.Drawing.Point(16, 497);
            this.dgProfiles.Name = "dgProfiles";
            this.dgProfiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProfiles.Size = new System.Drawing.Size(215, 150);
            this.dgProfiles.TabIndex = 33;
            this.dgProfiles.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProfiles_CellContentDoubleClick);
            this.dgProfiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProfiles_CellClick);
            // 
            // ProfileID
            // 
            this.ProfileID.DataPropertyName = "ProfileID";
            this.ProfileID.HeaderText = "ID";
            this.ProfileID.Name = "ProfileID";
            this.ProfileID.Visible = false;
            // 
            // Profile
            // 
            this.Profile.DataPropertyName = "Profile";
            this.Profile.HeaderText = "Profile";
            this.Profile.Name = "Profile";
            this.Profile.Width = 150;
            // 
            // CAFile
            // 
            this.CAFile.DataPropertyName = "CAFile";
            this.CAFile.HeaderText = "CA File";
            this.CAFile.Name = "CAFile";
            this.CAFile.Width = 150;
            // 
            // dgLocalErrors
            // 
            this.dgLocalErrors.AllowUserToAddRows = false;
            this.dgLocalErrors.AllowUserToDeleteRows = false;
            this.dgLocalErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLocalErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.ErrorDesc});
            this.dgLocalErrors.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgLocalErrors.Location = new System.Drawing.Point(16, 322);
            this.dgLocalErrors.Name = "dgLocalErrors";
            this.dgLocalErrors.Size = new System.Drawing.Size(677, 150);
            this.dgLocalErrors.TabIndex = 31;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // ErrorDesc
            // 
            this.ErrorDesc.DataPropertyName = "ErrorDesc";
            this.ErrorDesc.HeaderText = "Error Description";
            this.ErrorDesc.Name = "ErrorDesc";
            this.ErrorDesc.Width = 530;
            // 
            // dgSchedules
            // 
            this.dgSchedules.AllowUserToAddRows = false;
            this.dgSchedules.AllowUserToDeleteRows = false;
            this.dgSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSchedules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sID,
            this.sDate,
            this.sVendorFile});
            this.dgSchedules.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgSchedules.Location = new System.Drawing.Point(16, 142);
            this.dgSchedules.Name = "dgSchedules";
            this.dgSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSchedules.Size = new System.Drawing.Size(677, 150);
            this.dgSchedules.TabIndex = 30;
            this.dgSchedules.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSchedules_CellContentDoubleClick);
            this.dgSchedules.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSchedules_CellClick);
            this.dgSchedules.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSchedules_CellContentClick);
            // 
            // sID
            // 
            this.sID.DataPropertyName = "ID";
            this.sID.HeaderText = "ID";
            this.sID.Name = "sID";
            this.sID.Visible = false;
            // 
            // sDate
            // 
            this.sDate.DataPropertyName = "Date";
            this.sDate.HeaderText = "Date";
            this.sDate.Name = "sDate";
            this.sDate.Width = 200;
            // 
            // sVendorFile
            // 
            this.sVendorFile.DataPropertyName = "VendorFile";
            this.sVendorFile.HeaderText = "Vendor File";
            this.sVendorFile.Name = "sVendorFile";
            this.sVendorFile.Width = 200;
            // 
            // Logs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 692);
            this.Controls.Add(this.btnDelete1WeekOldLogs);
            this.Controls.Add(this.btnDeleteLogs);
            this.Controls.Add(this.btnShowLogs);
            this.Controls.Add(this.btnCopyCAErrors);
            this.Controls.Add(this.btnCopyLocalErrors);
            this.Controls.Add(this.dgCAErrors);
            this.Controls.Add(this.dgProfiles);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgLocalErrors);
            this.Controls.Add(this.dgSchedules);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.cmbVendor);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Logs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Logs";
            this.Load += new System.EventHandler(this.Logs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCAErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProfiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLocalErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSchedules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        

        

        

        

        

        

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbVendor;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private CADataGridView dgSchedules;
        private CADataGridView dgLocalErrors;
        private System.Windows.Forms.Label label6;
        private CADataGridView dgProfiles;
        private CADataGridView dgCAErrors;
        private System.Windows.Forms.Button btnCopyLocalErrors;
        private System.Windows.Forms.Button btnCopyCAErrors;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn VendorFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorDesc;
        private System.Windows.Forms.Button btnShowLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAErrorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAErrorDesc;
        private System.Windows.Forms.Button btnDeleteLogs;
        private System.Windows.Forms.Button btnDelete1WeekOldLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfileID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Profile;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn sID;
        private System.Windows.Forms.DataGridViewTextBoxColumn sDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn sVendorFile;
    }
}