namespace ChannelAdvisor
{
    partial class SearchStats
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
            this.phraseGrid = new System.Windows.Forms.DataGridView();
            this.phraseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phraseCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skuGrid = new System.Windows.Forms.DataGridView();
            this.skuColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skuCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.termGrid = new System.Windows.Forms.DataGridView();
            this.termColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.termCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profileLabel = new System.Windows.Forms.Label();
            this.profileCombo = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.phraseGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skuGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.termGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // phraseGrid
            // 
            this.phraseGrid.AllowUserToAddRows = false;
            this.phraseGrid.AllowUserToDeleteRows = false;
            this.phraseGrid.AllowUserToResizeRows = false;
            this.phraseGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.phraseGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.phraseGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.phraseColumn,
            this.phraseCountColumn});
            this.phraseGrid.Location = new System.Drawing.Point(15, 90);
            this.phraseGrid.Name = "phraseGrid";
            this.phraseGrid.ReadOnly = true;
            this.phraseGrid.RowHeadersVisible = false;
            this.phraseGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.phraseGrid.Size = new System.Drawing.Size(330, 438);
            this.phraseGrid.TabIndex = 3;
            // 
            // phraseColumn
            // 
            this.phraseColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.phraseColumn.FillWeight = 70F;
            this.phraseColumn.HeaderText = "Phrase";
            this.phraseColumn.Name = "phraseColumn";
            this.phraseColumn.ReadOnly = true;
            // 
            // phraseCountColumn
            // 
            this.phraseCountColumn.FillWeight = 30F;
            this.phraseCountColumn.HeaderText = "Count";
            this.phraseCountColumn.Name = "phraseCountColumn";
            this.phraseCountColumn.ReadOnly = true;
            // 
            // skuGrid
            // 
            this.skuGrid.AllowUserToAddRows = false;
            this.skuGrid.AllowUserToDeleteRows = false;
            this.skuGrid.AllowUserToResizeRows = false;
            this.skuGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.skuGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skuGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.skuColumn,
            this.skuCountColumn});
            this.skuGrid.Location = new System.Drawing.Point(375, 90);
            this.skuGrid.Name = "skuGrid";
            this.skuGrid.ReadOnly = true;
            this.skuGrid.RowHeadersVisible = false;
            this.skuGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.skuGrid.Size = new System.Drawing.Size(330, 438);
            this.skuGrid.TabIndex = 4;
            // 
            // skuColumn
            // 
            this.skuColumn.FillWeight = 70F;
            this.skuColumn.HeaderText = "SKU";
            this.skuColumn.Name = "skuColumn";
            this.skuColumn.ReadOnly = true;
            // 
            // skuCountColumn
            // 
            this.skuCountColumn.FillWeight = 30F;
            this.skuCountColumn.HeaderText = "Count";
            this.skuCountColumn.Name = "skuCountColumn";
            this.skuCountColumn.ReadOnly = true;
            // 
            // termGrid
            // 
            this.termGrid.AllowUserToAddRows = false;
            this.termGrid.AllowUserToDeleteRows = false;
            this.termGrid.AllowUserToResizeRows = false;
            this.termGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.termGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.termGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.termColumn,
            this.termCountColumn});
            this.termGrid.Location = new System.Drawing.Point(736, 90);
            this.termGrid.Name = "termGrid";
            this.termGrid.ReadOnly = true;
            this.termGrid.RowHeadersVisible = false;
            this.termGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.termGrid.Size = new System.Drawing.Size(330, 438);
            this.termGrid.TabIndex = 5;
            // 
            // termColumn
            // 
            this.termColumn.FillWeight = 70F;
            this.termColumn.HeaderText = "Term";
            this.termColumn.Name = "termColumn";
            this.termColumn.ReadOnly = true;
            // 
            // termCountColumn
            // 
            this.termCountColumn.FillWeight = 30F;
            this.termCountColumn.HeaderText = "Count";
            this.termCountColumn.Name = "termCountColumn";
            this.termCountColumn.ReadOnly = true;
            // 
            // profileLabel
            // 
            this.profileLabel.AutoSize = true;
            this.profileLabel.Location = new System.Drawing.Point(12, 27);
            this.profileLabel.Name = "profileLabel";
            this.profileLabel.Size = new System.Drawing.Size(51, 14);
            this.profileLabel.TabIndex = 0;
            this.profileLabel.Text = "Profile:";
            // 
            // profileCombo
            // 
            this.profileCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileCombo.FormattingEnabled = true;
            this.profileCombo.Location = new System.Drawing.Point(69, 24);
            this.profileCombo.Name = "profileCombo";
            this.profileCombo.Size = new System.Drawing.Size(180, 22);
            this.profileCombo.TabIndex = 1;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(255, 23);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(186, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Get statistics";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Search phrase occurrence:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(375, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "Search SKU occurrence:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(736, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "Search term occurrence:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(489, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "Total number of searches:";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(667, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 14);
            this.label5.TabIndex = 10;
            this.label5.Visible = false;
            // 
            // SearchStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 552);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.termGrid);
            this.Controls.Add(this.skuGrid);
            this.Controls.Add(this.phraseGrid);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.profileCombo);
            this.Controls.Add(this.profileLabel);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchStats";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search analysis";
            this.Load += new System.EventHandler(this.SearchStats_Load);
            ((System.ComponentModel.ISupportInitialize)(this.phraseGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skuGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.termGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label profileLabel;
        private System.Windows.Forms.ComboBox profileCombo;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn phraseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phraseCountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skuColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skuCountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn termColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn termCountColumn;
        private System.Windows.Forms.DataGridView phraseGrid;
        private System.Windows.Forms.DataGridView skuGrid;
        private System.Windows.Forms.DataGridView termGrid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}