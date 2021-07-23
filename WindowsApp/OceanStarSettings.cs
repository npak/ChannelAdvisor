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
    public partial class OceanStarSettings : Form
    {
        DAL dal = new DAL();
        private Vendor OceanStar { get; set; }

        public OceanStarSettings()
        {
            InitializeComponent();

            OceanStar  = dal.GetVendor((int)VendorName.OceanStar);

            commonSettings.VendorInfo = OceanStar;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KwikTekSettings_Load(object sender, EventArgs e)
        {
            DisplayKwikTekInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayKwikTekInfo()
        {
            txtFTPServer.Text = dal.GetSettingValue("OceanStar_FTPServer");
            txtUserName.Text = dal.GetSettingValue("Oceanstar_Login");
            txtPassword.Text = dal.GetSettingValue("Oceanstar_Password");
            txtFolderName.Text = dal.GetSettingValue("OceanStar_CSVFolder");
            txtFileName.Text = dal.GetSettingValue("OceanStar_CSVFIle");
            rbFTP.Checked = dal.GetSettingValue("OceanStar_CSVIsFTP") == "1" ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                if (!commonSettings.SaveVendorCommonSettings())
                {
                    Util.ShowMessage("Vendor common settings could not be saved");
                    return;
                }

                bool isSuccess = dal.UpdateSetting("OceanStar_FTPServer", txtFTPServer.Text);
                isSuccess &= dal.UpdateSetting("OceanStar_Login", txtUserName.Text);
                isSuccess &= dal.UpdateSetting("OceanStar_Password", txtPassword.Text);
                
                isSuccess &= dal.UpdateSetting("OceanStar_CSVFolder",txtFolderName.Text);
                isSuccess &= dal.UpdateSetting("OceanStar_CSVFIle", txtFileName.Text);
                isSuccess &= dal.UpdateSetting("OceanStar_CSVIsFTP",rbFTP.Checked ? "1":"0");
                
                if (isSuccess)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("The record could not be saved!");
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(txtFTPServer.Text))
            {
                Util.ShowMessage("Please enter FTP Server");
                txtFTPServer.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                Util.ShowMessage("Please enter FTP Login");
                txtUserName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                Util.ShowMessage("Please enter FTP Password");
                txtPassword.Focus();
                return false;
            }
            /// csv

            if (txtFolderName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a CSV Folder name");
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
