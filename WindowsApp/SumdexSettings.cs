using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class SumdexSettings :Form
    {
        private Vendor Sumdex { get; set; }

        public SumdexSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            Sumdex = dal.GetVendor((int)VendorName.Sumdex);

            commonSettings.VendorInfo = Sumdex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();

            browse.Description = "Please select a folder to scan files";
            browse.ShowNewFolderButton = true;
            browse.RootFolder = Environment.SpecialFolder.MyComputer;
            browse.SelectedPath = Environment.SpecialFolder.MyComputer.ToString();

            if (browse.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = browse.SelectedPath;

                if (txtFolder.Text.Substring(txtFolder.Text.Length - 2, 1) != "\\")
                    txtFolder.Text += "\\";

            }//end if
        }//end event

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void DisplayInfo()
        {
            DAL dal = new DAL();
            
            txtFolder.Text = dal.GetSumdexFolder();
            txtFolderName.Text = dal.GetSettingValue("Sumdex_CSVFolder");
            txtFileName.Text = dal.GetSettingValue("Sumdex_CSVFile");
            rbFTP.Checked = dal.GetSettingValue("Sumdex_CSVIsFTP") == "1" ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                //check if fields have been entered
                if (txtFolder.Text.Trim() == "")
                {
                    Util.ShowMessage("Please select folder to scan Sumdex files");
                    return;
                }

                if (!commonSettings.SaveVendorCommonSettings())
                {
                    Util.ShowMessage("Vendor common settings could not be saved");
                    return;
                }

                //save
                DAL dal = new DAL();
                bool isSuccess = new DAL().SaveSumdexSettings(txtFolder.Text);
                isSuccess &= dal.UpdateSetting("Sumdex_CSVFolder", txtFolderName.Text);
                isSuccess &= dal.UpdateSetting("Sumdex_CSVFile", txtFileName.Text);
                isSuccess &= dal.UpdateSetting("Sumdex_CSVIsFTP", rbFTP.Checked ? "1" : "0");
                if (isSuccess)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("The record could not be saved!");
                }//end isSucess if
            }
        }//end event

        /// <summary>
        /// 
        /// </summary>
        private bool ValidateForm()
        {
            
            if (txtFolderName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a SCV Folder name");
                txtFolderName.Focus();
                return false;
            }

            if (txtFileName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a CSV File name");
                txtFileName.Focus();
                return false;
            }
            
            
            return true;
        }//end method

        private void SumdexSettings_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        } 
    }

}
