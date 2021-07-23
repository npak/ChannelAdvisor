namespace ChannelAdvisor
{
    partial class EMGOrderUpdateLogs
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
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnDelete1MonthOldLogs = new System.Windows.Forms.Button();
            this.btnDeleteLogs = new System.Windows.Forms.Button();
            this.btnShowLogs = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgUnSuccessfulUpdates = new ChannelAdvisor.CADataGridView();
            this.uScheduleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uOrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgSuccessfulUpdates = new ChannelAdvisor.CADataGridView();
            this.sScheduleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgSchedules = new ChannelAdvisor.CADataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalOrders = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgUnSuccessfulUpdates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSuccessfulUpdates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSchedules)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(110, 49);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(121, 23);
            this.dtpTo.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = "To Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 29;
            this.label2.Text = "From Date:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(110, 16);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(121, 23);
            this.dtpFrom.TabIndex = 28;
            // 
            // btnDelete1MonthOldLogs
            // 
            this.btnDelete1MonthOldLogs.Location = new System.Drawing.Point(415, 12);
            this.btnDelete1MonthOldLogs.Name = "btnDelete1MonthOldLogs";
            this.btnDelete1MonthOldLogs.Size = new System.Drawing.Size(248, 30);
            this.btnDelete1MonthOldLogs.TabIndex = 42;
            this.btnDelete1MonthOldLogs.Text = "Delete All Logs Over 1 Month Old";
            this.btnDelete1MonthOldLogs.UseVisualStyleBackColor = true;
            this.btnDelete1MonthOldLogs.Click += new System.EventHandler(this.btnDelete1MonthOldLogs_Click);
            // 
            // btnDeleteLogs
            // 
            this.btnDeleteLogs.Location = new System.Drawing.Point(278, 48);
            this.btnDeleteLogs.Name = "btnDeleteLogs";
            this.btnDeleteLogs.Size = new System.Drawing.Size(121, 30);
            this.btnDeleteLogs.TabIndex = 41;
            this.btnDeleteLogs.Text = "Delete Logs";
            this.btnDeleteLogs.UseVisualStyleBackColor = true;
            this.btnDeleteLogs.Click += new System.EventHandler(this.btnDeleteLogs_Click);
            // 
            // btnShowLogs
            // 
            this.btnShowLogs.Location = new System.Drawing.Point(278, 12);
            this.btnShowLogs.Name = "btnShowLogs";
            this.btnShowLogs.Size = new System.Drawing.Size(121, 30);
            this.btnShowLogs.TabIndex = 40;
            this.btnShowLogs.Text = "Show Logs";
            this.btnShowLogs.UseVisualStyleBackColor = true;
            this.btnShowLogs.Click += new System.EventHandler(this.btnShowLogs_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 16);
            this.label4.TabIndex = 43;
            this.label4.Text = "EMG Order Updates:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 16);
            this.label5.TabIndex = 45;
            this.label5.Text = "Successful Updates:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 473);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 16);
            this.label1.TabIndex = 47;
            this.label1.Text = "UnSuccessful Updates:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(332, 654);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 30);
            this.btnClose.TabIndex = 49;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgUnSuccessfulUpdates
            // 
            this.dgUnSuccessfulUpdates.AllowUserToAddRows = false;
            this.dgUnSuccessfulUpdates.AllowUserToDeleteRows = false;
            this.dgUnSuccessfulUpdates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUnSuccessfulUpdates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uScheduleID,
            this.uOrderNo,
            this.uStatus,
            this.uMessage});
            this.dgUnSuccessfulUpdates.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgUnSuccessfulUpdates.Location = new System.Drawing.Point(24, 497);
            this.dgUnSuccessfulUpdates.Name = "dgUnSuccessfulUpdates";
            this.dgUnSuccessfulUpdates.Size = new System.Drawing.Size(677, 150);
            this.dgUnSuccessfulUpdates.TabIndex = 50;
            // 
            // uScheduleID
            // 
            this.uScheduleID.DataPropertyName = "ScheduleID";
            this.uScheduleID.HeaderText = "ID";
            this.uScheduleID.Name = "uScheduleID";
            this.uScheduleID.Visible = false;
            // 
            // uOrderNo
            // 
            this.uOrderNo.DataPropertyName = "OrderNo";
            this.uOrderNo.HeaderText = "OrderNo";
            this.uOrderNo.Name = "uOrderNo";
            // 
            // uStatus
            // 
            this.uStatus.DataPropertyName = "Status";
            this.uStatus.HeaderText = "Status";
            this.uStatus.Name = "uStatus";
            // 
            // uMessage
            // 
            this.uMessage.DataPropertyName = "Message";
            this.uMessage.HeaderText = "Message";
            this.uMessage.Name = "uMessage";
            this.uMessage.Width = 400;
            // 
            // dgSuccessfulUpdates
            // 
            this.dgSuccessfulUpdates.AllowUserToAddRows = false;
            this.dgSuccessfulUpdates.AllowUserToDeleteRows = false;
            this.dgSuccessfulUpdates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSuccessfulUpdates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sScheduleID,
            this.sOrderNo,
            this.sStatus,
            this.sMessage});
            this.dgSuccessfulUpdates.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgSuccessfulUpdates.Location = new System.Drawing.Point(24, 309);
            this.dgSuccessfulUpdates.Name = "dgSuccessfulUpdates";
            this.dgSuccessfulUpdates.Size = new System.Drawing.Size(677, 150);
            this.dgSuccessfulUpdates.TabIndex = 46;
            // 
            // sScheduleID
            // 
            this.sScheduleID.DataPropertyName = "ScheduleID";
            this.sScheduleID.HeaderText = "ID";
            this.sScheduleID.Name = "sScheduleID";
            this.sScheduleID.Visible = false;
            // 
            // sOrderNo
            // 
            this.sOrderNo.DataPropertyName = "OrderNo";
            this.sOrderNo.HeaderText = "OrderNo";
            this.sOrderNo.Name = "sOrderNo";
            // 
            // sStatus
            // 
            this.sStatus.DataPropertyName = "Status";
            this.sStatus.HeaderText = "Status";
            this.sStatus.Name = "sStatus";
            // 
            // sMessage
            // 
            this.sMessage.DataPropertyName = "Message";
            this.sMessage.HeaderText = "Message";
            this.sMessage.Name = "sMessage";
            this.sMessage.Width = 400;
            // 
            // dgSchedules
            // 
            this.dgSchedules.AllowUserToAddRows = false;
            this.dgSchedules.AllowUserToDeleteRows = false;
            this.dgSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSchedules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.sDate,
            this.TotalOrders});
            this.dgSchedules.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgSchedules.Location = new System.Drawing.Point(24, 125);
            this.dgSchedules.Name = "dgSchedules";
            this.dgSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSchedules.Size = new System.Drawing.Size(677, 150);
            this.dgSchedules.TabIndex = 44;
            this.dgSchedules.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSchedules_CellClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // sDate
            // 
            this.sDate.DataPropertyName = "Date";
            this.sDate.HeaderText = "Date";
            this.sDate.Name = "sDate";
            this.sDate.Width = 200;
            // 
            // TotalOrders
            // 
            this.TotalOrders.DataPropertyName = "TotalOrders";
            this.TotalOrders.HeaderText = "Total Orders";
            this.TotalOrders.Name = "TotalOrders";
            this.TotalOrders.Width = 200;
            // 
            // EMGOrderUpdateLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 691);
            this.Controls.Add(this.dgUnSuccessfulUpdates);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgSuccessfulUpdates);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgSchedules);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDelete1MonthOldLogs);
            this.Controls.Add(this.btnDeleteLogs);
            this.Controls.Add(this.btnShowLogs);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFrom);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EMGOrderUpdateLogs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EMG Order Update Logs";
            this.Load += new System.EventHandler(this.EMGOrderUpdateLogs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgUnSuccessfulUpdates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSuccessfulUpdates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSchedules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Button btnDelete1MonthOldLogs;
        private System.Windows.Forms.Button btnDeleteLogs;
        private System.Windows.Forms.Button btnShowLogs;
        private CADataGridView dgSchedules;
        private System.Windows.Forms.Label label4;
        private CADataGridView dgSuccessfulUpdates;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn sScheduleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn sStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn sMessage;
        private CADataGridView dgUnSuccessfulUpdates;
        private System.Windows.Forms.DataGridViewTextBoxColumn uScheduleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn uOrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn uStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn uMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn sDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalOrders;
    }
}