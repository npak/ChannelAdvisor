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
    public partial class KwikTekSettings : Form
    {
        DAL dal = new DAL();
        protected Vendor KwikTek { get; set; }

        public KwikTekSettings()
        {
            InitializeComponent();

            KwikTek = dal.GetVendor((int) VendorName.KwikTek);

            commonSettings.VendorInfo = KwikTek;
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
            txtFTPServer.Text = dal.GetSettingValue("KwikTek_FTPServer");
            txtUserName.Text = dal.GetSettingValue("KwikTek_Login");
            txtPassword.Text = dal.GetSettingValue("KwikTek_Password");

            txtFolderName.Text = dal.GetSettingValue("KwikTek_CSVFolder");
            txtFileName.Text = dal.GetSettingValue("KwikTek_CSVFile");
            rbFTP.Checked = dal.GetSettingValue("KwikTek_CSVIsFTP")=="1" ? true:false;

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

                bool isSuccess = dal.UpdateSetting("KwikTek_FTPServer", txtFTPServer.Text);
                isSuccess &= dal.UpdateSetting("KwikTek_Login", txtUserName.Text);
                isSuccess &= dal.UpdateSetting("KwikTek_Password", txtPassword.Text);

                isSuccess &= dal.UpdateSetting("KwikTek_CSVFolder", txtFolderName.Text);
                isSuccess &= dal.UpdateSetting("KwikTek_CSVFile",txtFileName.Text);
                isSuccess &= dal.UpdateSetting("KwikTek_CSVIsFTP", rbFTP.Checked ? "1":"0");

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
