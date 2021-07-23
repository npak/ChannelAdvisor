namespace ChannelAdvisor
{
    partial class EMGOrderStatusLogs
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete1MonthOldLogs = new System.Windows.Forms.Button();
            this.btnDeleteLogs = new System.Windows.Forms.Button();
            this.btnShowLogs = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgOrderStatus = new ChannelAdvisor.CADataGridView();
            this.dgSchedules = new ChannelAdvisor.CADataGridView();
            this.sID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipReference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsStoneEdgeUpdated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrderStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSchedules)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 282);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 16);
            this.label5.TabIndex = 56;
            this.label5.Text = "Order(s) Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 16);
            this.label4.TabIndex = 54;
            this.label4.Text = "EMG Get Order Status Logs:";
            // 
            // btnDelete1MonthOldLogs
            // 
            this.btnDelete1MonthOldLogs.Location = new System.Drawing.Point(408, 11);
            this.btnDelete1MonthOldLogs.Name = "btnDelete1MonthOldLogs";
            this.btnDelete1MonthOldLogs.Size = new System.Drawing.Size(248, 30);
            this.btnDelete1MonthOldLogs.TabIndex = 53;
            this.btnDelete1MonthOldLogs.Text = "Delete All Logs Over 1 Month Old";
            this.btnDelete1MonthOldLogs.UseVisualStyleBackColor = true;
            this.btnDelete1MonthOldLogs.Click += new System.EventHandler(this.btnDelete1MonthOldLogs_Click);
            // 
            // btnDeleteLogs
            // 
            this.btnDeleteLogs.Location = new System.Drawing.Point(271, 47);
            this.btnDeleteLogs.Name = "btnDeleteLogs";
            this.btnDeleteLogs.Size = new System.Drawing.Size(121, 30);
            this.btnDeleteLogs.TabIndex = 52;
            this.btnDeleteLogs.Text = "Delete Logs";
            this.btnDeleteLogs.UseVisualStyleBackColor = true;
            this.btnDeleteLogs.Click += new System.EventHandler(this.btnDeleteLogs_Click);
            // 
            // btnShowLogs
            // 
            this.btnShowLogs.Location = new System.Drawing.Point(271, 11);
            this.btnShowLogs.Name = "btnShowLogs";
            this.btnShowLogs.Size = new System.Drawing.Size(121, 30);
            this.btnShowLogs.TabIndex = 51;
            this.btnShowLogs.Text = "Show Logs";
            this.btnShowLogs.UseVisualStyleBackColor = true;
            this.btnShowLogs.Click += new System.EventHandler(this.btnShowLogs_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(103, 48);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(121, 23);
            this.dtpTo.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 49;
            this.label3.Text = "To Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 48;
            this.label2.Text = "From Date:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(103, 15);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(121, 23);
            this.dtpFrom.TabIndex = 47;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(407, 562);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 30);
            this.btnClose.TabIndex = 58;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgOrderStatus
            // 
            this.dgOrderStatus.AllowUserToAddRows = false;
            this.dgOrderStatus.AllowUserToDeleteRows = false;
            this.dgOrderStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOrderStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderNo,
            this.OrderStatus,
            this.ShipReference,
            this.ShippingMethod,
            this.ShippingCost,
            this.ShipDate,
            this.NetAmount,
            this.Payment,
            this.PaymentDate,
            this.Status,
            this.ErrorMessage,
            this.IsStoneEdgeUpdated});
            this.dgOrderStatus.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgOrderStatus.Location = new System.Drawing.Point(17, 304);
            this.dgOrderStatus.Name = "dgOrderStatus";
            this.dgOrderStatus.Size = new System.Drawing.Size(856, 237);
            this.dgOrderStatus.TabIndex = 57;
            // 
            // dgSchedules
            // 
            this.dgSchedules.AllowUserToAddRows = false;
            this.dgSchedules.AllowUserToDeleteRows = false;
            this.dgSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSchedules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sID,
            this.sDate});
            this.dgSchedules.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgSchedules.Location = new System.Drawing.Point(17, 120);
            this.dgSchedules.Name = "dgSchedules";
            this.dgSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSchedules.Size = new System.Drawing.Size(531, 150);
            this.dgSchedules.TabIndex = 55;
            this.dgSchedules.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSchedules_CellClick);
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
            // OrderNo
            // 
            this.OrderNo.DataPropertyName = "OrderNo";
            this.OrderNo.HeaderText = "OrderNo";
            this.OrderNo.Name = "OrderNo";
            // 
            // OrderStatus
            // 
            this.OrderStatus.DataPropertyName = "OrderStatus";
            this.OrderStatus.HeaderText = "Order Status";
            this.OrderStatus.Name = "OrderStatus";
            // 
            // ShipReference
            // 
            this.ShipReference.DataPropertyName = "ShipReference";
            this.ShipReference.HeaderText = "Tracking No.";
            this.ShipReference.Name = "ShipReference";
            this.ShipReference.Width = 150;
            // 
            // ShippingMethod
            // 
            this.ShippingMethod.DataPropertyName = "ShippingMethod";
            this.ShippingMethod.HeaderText = "Shipping Method";
            this.ShippingMethod.Name = "ShippingMethod";
            // 
            // ShippingCost
            // 
            this.ShippingCost.DataPropertyName = "ShippingCost";
            this.ShippingCost.HeaderText = "Shipping Cost";
            this.ShippingCost.Name = "ShippingCost";
            this.ShippingCost.Width = 75;
            // 
            // ShipDate
            // 
            this.ShipDate.DataPropertyName = "ShipDate";
            this.ShipDate.HeaderText = "Ship Date";
            this.ShipDate.Name = "ShipDate";
            // 
            // NetAmount
            // 
            this.NetAmount.DataPropertyName = "NetAmount";
            this.NetAmount.HeaderText = "Net Amt";
            this.NetAmount.Name = "NetAmount";
            this.NetAmount.Width = 75;
            // 
            // Payment
            // 
            this.Payment.DataPropertyName = "Payment";
            this.Payment.HeaderText = "Payment";
            this.Payment.Name = "Payment";
            this.Payment.Width = 75;
            // 
            // PaymentDate
            // 
            this.PaymentDate.DataPropertyName = "PaymentDate";
            this.PaymentDate.HeaderText = "Payment Date";
            this.PaymentDate.Name = "PaymentDate";
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 75;
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.DataPropertyName = "ErrorMessage";
            this.ErrorMessage.HeaderText = "Error Message";
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Width = 300;
            // 
            // IsStoneEdgeUpdated
            // 
            this.IsStoneEdgeUpdated.DataPropertyName = "IsStoneEdgeUpdated";
            this.IsStoneEdgeUpdated.HeaderText = "StoneEdge Updated";
            this.IsStoneEdgeUpdated.Name = "IsStoneEdgeUpdated";
            this.IsStoneEdgeUpdated.ReadOnly = true;
            // 
            // EMGOrderStatusLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 600);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgOrderStatus);
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
            this.Name = "EMGOrderStatusLogs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EMG Order Status Logs";
            ((System.ComponentModel.ISupportInitialize)(this.dgOrderStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSchedules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private CADataGridView dgOrderStatus;
        private System.Windows.Forms.Label label5;
        private CADataGridView dgSchedules;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelete1MonthOldLogs;
        private System.Windows.Forms.Button btnDeleteLogs;
        private System.Windows.Forms.Button btnShowLogs;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn sID;
        private System.Windows.Forms.DataGridViewTextBoxColumn sDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShipReference;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShipDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Payment;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorMessage;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsStoneEdgeUpdated;
    }
}