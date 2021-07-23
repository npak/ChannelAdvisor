using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class WynitBasicData : Form
    {
        public WynitBasicData()
        {
            InitializeComponent();
            waitLabel.Visible = false;
            MaxUPCLengthText.Text = "14";
            OutputFolderText.Text = Properties.Settings.Default.WynitBasicData_OutputFolder;
            SupplierCodeText.Text = Properties.Settings.Default.WynitBasicData_SupplierCode;
            WarehouseLocationText.Text = Properties.Settings.Default.WynitBasicData_WarehouseLocation;
            DCCodeText.Text = Properties.Settings.Default.WynitBasicData_DCCode;
        }

        private void OutputFolderButton_Click(object sender, EventArgs e)
        {
            if (OutputFolderDialog.ShowDialog() == DialogResult.OK)
            {
                OutputFolderText.Text = OutputFolderDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Validate all input data
        /// </summary>
        /// <param name="aMaxUPCLength">Max UPC Length parameter</param>
        /// <returns>Result of validation</returns>
        private bool ValidateData(out int aMaxUPCLength)
        {
            aMaxUPCLength = 0;
            if (string.IsNullOrEmpty(OutputFolderText.Text))
            {
                Util.ShowMessage("Please select output folder");
                OutputFolderButton.Focus();
                return false;
            }
            
            if (!FileAndDirectoryUtils.ValidateFileNamePrefix(FilePrefixText.Text))
            {
                Util.ShowMessage("File name prefix contains impossible symbols!");
                FilePrefixText.Focus();
                return false;
            }

            if (!int.TryParse(MaxUPCLengthText.Text, out aMaxUPCLength))
            {
                Util.ShowMessage("Incorrect Max UPC Length!");
                MaxUPCLengthText.Focus();
                return false;
            }

            if (aMaxUPCLength <= 0)
            {
                Util.ShowMessage("Max UPC Length should be positive");
                MaxUPCLengthText.Focus();
                return false;
            }

            return true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            int lMaxUPCLength;
            if (ValidateData(out lMaxUPCLength))
            {
                waitLabel.Visible = true;
                SaveButton.Enabled = false;
                CloseButton.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                string lOutputFolder = OutputFolderText.Text;
                if (lOutputFolder.Substring(lOutputFolder.Length - 1, 1) != @"\")
                    lOutputFolder += @"\";

                try
                {
                    WynitService lWynitService = new WynitService();
                    lWynitService.GetWynitBasicData(lOutputFolder, FilePrefixText.Text, lMaxUPCLength, SupplierCodeText.Text,
                            WarehouseLocationText.Text, DCCodeText.Text);
                    Properties.Settings.Default.WynitBasicData_OutputFolder = OutputFolderText.Text;
                    Properties.Settings.Default.WynitBasicData_SupplierCode = SupplierCodeText.Text;
                    Properties.Settings.Default.WynitBasicData_WarehouseLocation = WarehouseLocationText.Text;
                    Properties.Settings.Default.WynitBasicData_DCCode = DCCodeText.Text;
                    Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    Util.ShowMessage(ex.Message);
                }
                finally
                {
                    waitLabel.Visible = false;
                    SaveButton.Enabled = true;
                    CloseButton.Enabled = true;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
