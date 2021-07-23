using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class EMGExtractor : Form
    {
        EMGExtractorSettings mSettings;

        public EMGExtractor()
        {
            InitializeComponent();
            UPCRadioButton.Checked = true;

            LoadSettings();
            SelectFolderText.Text = Properties.Settings.Default.EMGExtractor_OutputFolder;
        }

        /// <summary>
        /// Load settings from DB
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                mSettings = EMGExtractorSettings.Load();

                URLText.Text = mSettings.URLToLoadXML;
                MaxTitleLengthText.Text = mSettings.AuctionTitleMaxLength.ToString();
                MaxCAStoreTitleLengthText.Text = mSettings.CAStoreTitleMaxLength.ToString();
                MaxClassificationLengthText.Text = mSettings.ClassificationMaxLength.ToString();
                WarrantyLabelText.Text = mSettings.WarrantyLabel;
                DefaultWarrantyValueText.Text = mSettings.WarrantyDefaultValue;
                SupplierCodeText.Text = mSettings.SupplierCode;
                WarehouseLocationText.Text = mSettings.WarehouseLocation;
                DCCodeText.Text = mSettings.DCCode;
                FilePrefixText.Text = mSettings.OutputFilePrefix;
            }
            catch
            {
                Util.ShowMessage("Cannot load EMG extractor settings");
                this.Close();
            }
        }

        /// <summary>
        /// Validate input data on main tab
        /// </summary>
        /// <returns>Result of validtion</returns>
        private bool ValidateMainData()
        {
            if (fromFileCheckBox.Checked && string.IsNullOrEmpty(selectedFileText.Text))
            {
                Util.ShowMessage("Please select source file");
                selectFileButton.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(SelectFolderText.Text))
            {
                Util.ShowMessage("Please select output folder");
                SelectFolderButton.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get valid values from rich text box
        /// </summary>
        /// <returns>List of strings</returns>
        private List<string> GetEnteredValues()
        {
            List<string> lResult = new List<string>();

            foreach (string lValue in SKURichTextBox.Lines)
                if (!string.IsNullOrEmpty(lValue))
                {
                    if (!lValue.Contains("SKU"))
                        lResult.Add(lValue.Trim());
                    else
                    {
                        // For error messages, like: Error for SKU: SCF30013 'The specified Sku does not exist!'
                        string[] lStrings = lValue.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < lStrings.Length; i++)
                        {
                            if ((lStrings[i].Contains("SKU")) && (i < lStrings.Length - 1))
                            {
                                lResult.Add(lStrings[i + 1].Trim());
                                break;
                            }
                        }
                    }
                }
            return lResult;
        }

        /// <summary>
        /// Start button click
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateMainData())
            {
                ((Button)sender).Text = "Processing...";
                ((Button)sender).Enabled = false;
                EMGExtractorTabControl.Enabled = false;
                CloseButton.Enabled = false;
                Cursor = Cursors.WaitCursor;
                try
                {
                    if (fromFileCheckBox.Checked)
                    {
                        Extractors.EMGExtractor extractor = new Extractors.EMGExtractor(mSettings, GetEnteredValues(), SKURadioButton.Checked,
                            selectedFileText.Text, SelectFolderText.Text, wholeFileRadio.Checked);
                        extractor.Extract();
                    }
                    else
                    {
                        EMGService lEMGService = new EMGService();
                        lEMGService.Extract(mSettings, GetEnteredValues(), SKURadioButton.Checked, SelectFolderText.Text, wholeFileRadio.Checked);
                    }

                    Properties.Settings.Default.EMGExtractor_OutputFolder = SelectFolderText.Text;
                    Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    Util.ShowMessage(ex.Message);
                }
                finally
                {
                    ((Button)sender).Text = "Start";
                    ((Button)sender).Enabled = true;
                    CloseButton.Enabled = true;
                    Cursor = Cursors.Default;
                    EMGExtractorTabControl.Enabled = true;
                    Util.ShowMessage("Operation completed successfully");
                }
            }
        }

        /// <summary>
        /// Validate and get input data on Settings tab
        /// </summary>
        /// <returns>Result of validation</returns>
        private bool ValidateAndGetInputData()
        {
            int lAuctionTitleLength;
            if (!int.TryParse(MaxTitleLengthText.Text, out lAuctionTitleLength) || (lAuctionTitleLength <= 0))
            {
                Util.ShowMessage("Please enter valid Auction Title Length");
                MaxTitleLengthText.Focus();
                return false;
            }
            int lCAStoreTitleLength;
            if (!int.TryParse(MaxCAStoreTitleLengthText.Text, out lCAStoreTitleLength) || (lCAStoreTitleLength <= 0))
            {
                Util.ShowMessage("Please enter valid CA Store Title Length");
                MaxCAStoreTitleLengthText.Focus();
                return false;
            }
            int lClassificationLength;
            if (!int.TryParse(MaxClassificationLengthText.Text, out lClassificationLength) || (lClassificationLength <= 0))
            {
                Util.ShowMessage("Please enter valid Classification Length");
                MaxCAStoreTitleLengthText.Focus();
                return false;
            }
            if (!FileAndDirectoryUtils.ValidateFileNamePrefix(FilePrefixText.Text))
            {
                Util.ShowMessage("File Prefix contains invalid characters");
                FilePrefixText.Focus();
                return false;
            }

            mSettings.AuctionTitleMaxLength = lAuctionTitleLength;
            mSettings.CAStoreTitleMaxLength = lCAStoreTitleLength;
            mSettings.ClassificationMaxLength = lClassificationLength;
            mSettings.WarrantyLabel = WarrantyLabelText.Text;
            mSettings.WarrantyDefaultValue = DefaultWarrantyValueText.Text;
            mSettings.SupplierCode = SupplierCodeText.Text;
            mSettings.WarehouseLocation = WarehouseLocationText.Text;
            mSettings.DCCode = DCCodeText.Text;
            mSettings.OutputFilePrefix = FilePrefixText.Text;

            return true;
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            if (ValidateAndGetInputData())
            {
                if (mSettings.Update())
                    Util.ShowMessage("Settings saved");
                else
                    Util.ShowMessage("Cannot save settings");
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EMGExtractorTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Name == "SettingsTab")
            {
                LoadSettings();
            }
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string lOutputFolder = folderBrowserDialog1.SelectedPath;
                if (lOutputFolder.Substring(lOutputFolder.Length - 1, 1) != @"\")
                    lOutputFolder += @"\";
                SelectFolderText.Text = lOutputFolder;
            }
        }

        private void SKURadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                skusRadio.Text = "SKU(s):";
        }

        private void UPCRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                skusRadio.Text = "UPC(s):";
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFileText.Text = fileDialog.FileName;
            }
        }

        private void fromFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fromFileCheckBox.Checked)
            {
                selectedFileText.Enabled = true;
                selectFileButton.Enabled = true;
            }
            else
            {
                selectedFileText.Enabled = false;
                selectFileButton.Enabled = false;
            }
        }

        private void wholeFileRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                SKURichTextBox.Enabled = false;
        }

        private void skusRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                SKURichTextBox.Enabled = true;
        }
    }
}
