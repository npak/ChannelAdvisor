using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class OtherVendorsFTPOptions : Form
    {
        public OtherVendorsFTPOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayVendorInfo()
        {
            DAL dal = new DAL();
            txtFolderName.Text = dal.GetSettingValue("OtherVendors_CSVFolder");
            txtFileName.Text = dal.GetSettingValue("OtherVendors_CSVFIle");
            rbFTP.Checked = dal.GetSettingValue("OtherVendors_CSVIsFTP") == "1" ? true : false;
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
                DAL dal = new DAL();
                bool isSuccess = dal.UpdateSetting("OtherVendors_CSVFolder", txtFolderName.Text);
                isSuccess &= dal.UpdateSetting("OtherVendors_CSVFIle", txtFileName.Text);
                isSuccess &= dal.UpdateSetting("OtherVendors_CSVIsFTP", rbFTP.Checked ? "1" : "0");
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
                Util.ShowMessage("Please enter a Folder name");
                txtFolderName.Focus();
                return false;
            }

            if (txtFileName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a File name");
                txtFileName.Focus();
                return false;
            }
            
            return true;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OtherVendorsFTPOptions_Load(object sender, EventArgs e)
        {
            DisplayVendorInfo();
        }

  
    }
}
