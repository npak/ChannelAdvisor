namespace ChannelAdvisor
{
    partial class PreviewAndUpdate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnGetInventory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateCA = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbVendors = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCopyLocalErrors = new System.Windows.Forms.Button();
            this.btnCopyCAErrors = new System.Windows.Forms.Button();
            this.cmbProfile = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.showInStockOnly = new System.Windows.Forms.CheckBox();
            this.dgCAErrors = new ChannelAdvisor.CADataGridView();
            this.CAErrors = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAErrorType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgErrors = new ChannelAdvisor.CADataGridView();
            this.Errors = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgInventory = new ChannelAdvisor.CADataGridView();
            this.UPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarkupPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DomesticShipping = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarkupPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgCAErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgInventory)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetInventory
            // 
            this.btnGetInventory.Location = new System.Drawing.Point(544, 6);
            this.btnGetInventory.Name = "btnGetInventory";
            this.btnGetInventory.Size = new System.Drawing.Size(141, 30);
            this.btnGetInventory.TabIndex = 13;
            this.btnGetInventory.Text = "Get Inventory";
            this.btnGetInventory.UseVisualStyleBackColor = true;
            this.btnGetInventory.Click += new System.EventHandler(this.btnGetInventory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Inventory:";
            // 
            // btnUpdateCA
            // 
            this.btnUpdateCA.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCA.Location = new System.Drawing.Point(947, 560);
            this.btnUpdateCA.Name = "btnUpdateCA";
            this.btnUpdateCA.Size = new System.Drawing.Size(99, 65);
            this.btnUpdateCA.TabIndex = 21;
            this.btnUpdateCA.Text = "Update Linnworks";
            this.btnUpdateCA.UseVisualStyleBackColor = true;
            this.btnUpdateCA.Click += new System.EventHandler(this.btnUpdateCA_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(950, 631);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 30);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbVendors
            // 
            this.cmbVendors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendors.FormattingEnabled = true;
            this.cmbVendors.Location = new System.Drawing.Point(85, 9);
            this.cmbVendors.Name = "cmbVendors";
            this.cmbVendors.Size = new System.Drawing.Size(168, 24);
            this.cmbVendors.TabIndex = 25;
            this.cmbVendors.SelectedIndexChanged += new System.EventHandler(this.cmbVendors_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "Vendor:";
            // 
            // btnCopyLocalErrors
            // 
            this.btnCopyLocalErrors.Location = new System.Drawing.Point(444, 487);
            this.btnCopyLocalErrors.Name = "btnCopyLocalErrors";
            this.btnCopyLocalErrors.Size = new System.Drawing.Size(25, 30);
            this.btnCopyLocalErrors.TabIndex = 28;
            this.btnCopyLocalErrors.Text = "C";
            this.btnCopyLocalErrors.UseVisualStyleBackColor = true;
            this.btnCopyLocalErrors.Click += new System.EventHandler(this.btnCopyLocalErrors_Click);
            // 
            // btnCopyCAErrors
            // 
            this.btnCopyCAErrors.Location = new System.Drawing.Point(945, 487);
            this.btnCopyCAErrors.Name = "btnCopyCAErrors";
            this.btnCopyCAErrors.Size = new System.Drawing.Size(25, 30);
            this.btnCopyCAErrors.TabIndex = 29;
            this.btnCopyCAErrors.Text = "C";
            this.btnCopyCAErrors.UseVisualStyleBackColor = true;
            this.btnCopyCAErrors.Click += new System.EventHandler(this.btnCopyCAErrors_Click);
            // 
            // cmbProfile
            // 
            this.cmbProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfile.FormattingEnabled = true;
            this.cmbProfile.Location = new System.Drawing.Point(339, 9);
            this.cmbProfile.Name = "cmbProfile";
            this.cmbProfile.Size = new System.Drawing.Size(168, 24);
            this.cmbProfile.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = "Profile:";
            // 
            // showInStockOnly
            // 
            this.showInStockOnly.AutoSize = true;
            this.showInStockOnly.Location = new System.Drawing.Point(474, 667);
            this.showInStockOnly.Name = "showInStockOnly";
            this.showInStockOnly.Size = new System.Drawing.Size(162, 20);
            this.showInStockOnly.TabIndex = 32;
            this.showInStockOnly.Text = "Display in stock only";
            this.showInStockOnly.UseVisualStyleBackColor = true;
            // 
            // dgCAErrors
            // 
            this.dgCAErrors.AllowUserToAddRows = false;
            this.dgCAErrors.AllowUserToDeleteRows = false;
            this.dgCAErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCAErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CAErrors,
            this.CAErrorType});
            this.dgCAErrors.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgCAErrors.Location = new System.Drawing.Point(474, 487);
            this.dgCAErrors.Name = "dgCAErrors";
            this.dgCAErrors.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCAErrors.RowTemplate.Height = 40;
            this.dgCAErrors.Size = new System.Drawing.Size(468, 174);
            this.dgCAErrors.TabIndex = 27;
            // 
            // CAErrors
            // 
            this.CAErrors.DataPropertyName = "ErrorDesc";
            this.CAErrors.HeaderText = "Ecommerce Errors";
            this.CAErrors.Name = "CAErrors";
            this.CAErrors.Width = 400;
            // 
            // CAErrorType
            // 
            this.CAErrorType.DataPropertyName = "ErrorType";
            this.CAErrorType.HeaderText = "ErrorType";
            this.CAErrorType.Name = "CAErrorType";
            this.CAErrorType.Visible = false;
            // 
            // dgErrors
            // 
            this.dgErrors.AllowUserToAddRows = false;
            this.dgErrors.AllowUserToDeleteRows = false;
            this.dgErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Errors,
            this.ErrorType});
            this.dgErrors.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgErrors.Location = new System.Drawing.Point(12, 487);
            this.dgErrors.Name = "dgErrors";
            this.dgErrors.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgErrors.RowTemplate.Height = 40;
            this.dgErrors.Size = new System.Drawing.Size(429, 174);
            this.dgErrors.TabIndex = 26;
            // 
            // Errors
            // 
            this.Errors.DataPropertyName = "ErrorDesc";
            this.Errors.HeaderText = "Local Errors";
            this.Errors.Name = "Errors";
            this.Errors.Width = 360;
            // 
            // ErrorType
            // 
            this.ErrorType.DataPropertyName = "ErrorType";
            this.ErrorType.HeaderText = "ErrorType";
            this.ErrorType.Name = "ErrorType";
            this.ErrorType.Visible = false;
            // 
            // dgInventory
            // 
            this.dgInventory.AllowUserToAddRows = false;
            this.dgInventory.AllowUserToDeleteRows = false;
            this.dgInventory.AllowUserToOrderColumns = true;
            this.dgInventory.AllowUserToResizeRows = false;
            this.dgInventory.ColumnHeadersHeight = 40;
            this.dgInventory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UPC,
            this.SKU,
            this.Qty,
            this.Price,
            this.MarkupPercentage,
            this.MAP,
            this.DomesticShipping,
            this.MarkupPrice,
            this.RetailPrice,
            this.Description});
            this.dgInventory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgInventory.Location = new System.Drawing.Point(12, 65);
            this.dgInventory.Name = "dgInventory";
            this.dgInventory.Size = new System.Drawing.Size(1034, 416);
            this.dgInventory.TabIndex = 19;
            this.dgInventory.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgInventory_ColumnHeaderMouseClick);
            this.dgInventory.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgInventory_EditingControlShowing);
            // 
            // UPC
            // 
            this.UPC.DataPropertyName = "UPC";
            this.UPC.HeaderText = "UPC";
            this.UPC.Name = "UPC";
            this.UPC.ReadOnly = true;
            this.UPC.Width = 120;
            // 
            // SKU
            // 
            this.SKU.DataPropertyName = "SKU";
            this.SKU.HeaderText = "SKU";
            this.SKU.Name = "SKU";
            this.SKU.ReadOnly = true;
            this.SKU.Width = 120;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle1;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.Width = 70;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Price.DefaultCellStyle = dataGridViewCellStyle2;
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Width = 70;
            // 
            // MarkupPercentage
            // 
            this.MarkupPercentage.DataPropertyName = "MarkupPercentage";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MarkupPercentage.DefaultCellStyle = dataGridViewCellStyle3;
            this.MarkupPercentage.HeaderText = "Markup %";
            this.MarkupPercentage.Name = "MarkupPercentage";
            this.MarkupPercentage.ReadOnly = true;
            this.MarkupPercentage.Width = 70;
            // 
            // MAP
            // 
            this.MAP.DataPropertyName = "MAP";
            this.MAP.HeaderText = "MAP";
            this.MAP.Name = "MAP";
            this.MAP.ReadOnly = true;
            this.MAP.Width = 70;
            // 
            // DomesticShipping
            // 
            this.DomesticShipping.DataPropertyName = "DomesticShipping";
            this.DomesticShipping.HeaderText = "Domestic Shipping";
            this.DomesticShipping.Name = "DomesticShipping";
            this.DomesticShipping.ReadOnly = true;
            // 
            // MarkupPrice
            // 
            this.MarkupPrice.DataPropertyName = "MarkupPrice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MarkupPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.MarkupPrice.HeaderText = "Calc. Price";
            this.MarkupPrice.Name = "MarkupPrice";
            this.MarkupPrice.Width = 70;
            // 
            // RetailPrice
            // 
            this.RetailPrice.DataPropertyName = "RetailPrice";
            this.RetailPrice.HeaderText = "Retail Price";
            this.RetailPrice.Name = "RetailPrice";
            this.RetailPrice.Width = 70;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 480;
            // 
            // PreviewAndUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 691);
            this.Controls.Add(this.showInStockOnly);
            this.Controls.Add(this.cmbProfile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCopyCAErrors);
            this.Controls.Add(this.btnCopyLocalErrors);
            this.Controls.Add(this.dgCAErrors);
            this.Controls.Add(this.dgErrors);
            this.Controls.Add(this.cmbVendors);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdateCA);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgInventory);
            this.Controls.Add(this.btnGetInventory);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreviewAndUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview And Update";
            this.Load += new System.EventHandler(this.PreviewAndUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCAErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgInventory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetInventory;
        private CADataGridView dgInventory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateCA;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbVendors;
        private System.Windows.Forms.Label label2;
        private CADataGridView dgErrors;
        private System.Windows.Forms.DataGridViewTextBoxColumn Errors;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorType;
        private CADataGridView dgCAErrors;
        private System.Windows.Forms.Button btnCopyLocalErrors;
        private System.Windows.Forms.Button btnCopyCAErrors;
        private System.Windows.Forms.ComboBox cmbProfile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox showInStockOnly;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAErrors;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAErrorType;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarkupPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAP;
        private System.Windows.Forms.DataGridViewTextBoxColumn DomesticShipping;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarkupPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}