namespace ChannelAdvisor
{
    partial class BlockedSKUs
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgSKUs = new System.Windows.Forms.DataGridView();
            this.SKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewSKU = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtMultipleSKUs = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgSKUs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(574, 119);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 30);
            this.btnDelete.TabIndex = 27;
            this.btnDelete.Text = "-";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(309, 612);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 30);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgSKUs
            // 
            this.dgSKUs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSKUs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SKU,
            this.NewSKU});
            this.dgSKUs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgSKUs.Location = new System.Drawing.Point(12, 119);
            this.dgSKUs.Name = "dgSKUs";
            this.dgSKUs.Size = new System.Drawing.Size(556, 482);
            this.dgSKUs.TabIndex = 24;
            this.dgSKUs.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSKUs_CellValueChanged);
            // 
            // SKU
            // 
            this.SKU.DataPropertyName = "SKU";
            this.SKU.HeaderText = "SKU";
            this.SKU.Name = "SKU";
            this.SKU.Width = 120;
            // 
            // NewSKU
            // 
            this.NewSKU.DataPropertyName = "IsWildcard";
            this.NewSKU.HeaderText = "IsWildcard";
            this.NewSKU.Name = "NewSKU";
            this.NewSKU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NewSKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.NewSKU.Width = 250;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(222, 612);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 30);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(449, 46);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(119, 30);
            this.btnImport.TabIndex = 31;
            this.btnImport.Text = " Append to grid";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtMultipleSKUs
            // 
            this.txtMultipleSKUs.Location = new System.Drawing.Point(12, 28);
            this.txtMultipleSKUs.Multiline = true;
            this.txtMultipleSKUs.Name = "txtMultipleSKUs";
            this.txtMultipleSKUs.Size = new System.Drawing.Size(431, 77);
            this.txtMultipleSKUs.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 33;
            this.label1.Text = "Paste in:";
            // 
            // BlockedSKUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 649);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMultipleSKUs);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgSKUs);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BlockedSKUs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blocked SKUs";
            this.Load += new System.EventHandler(this.BlockedSKUs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgSKUs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
        

        #endregion
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgSKUs;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKU;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NewSKU;
        private System.Windows.Forms.TextBox txtMultipleSKUs;
        private System.Windows.Forms.Label label1;
    }
}