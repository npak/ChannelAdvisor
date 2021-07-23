using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class EMGSettings : Form
    {
        private Vendor EMG { get; set; }

        public EMGSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            EMG = dal.GetVendor((int)VendorName.EMG);

            commonSettings.VendorInfo = EMG;
        }

        /// <summary>
        /// Display saved EMG Info in the textboxes
        /// </summary>
        private void DisplayEMGInfo()
        {
            string emgFTP;
            string ftpUser;
            string ftpPassword;
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";
        
            DAL dal = new DAL();

            dal.GetEMGInfo(out emgFTP, out ftpUser,out ftpPassword, out csvfolder, out csvfilename, out csvIsftp);

            txtFTPServer.Text = emgFTP;
            txtUserName.Text = ftpUser;
            txtPassword.Text = ftpPassword;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            rbFTP.Checked = csvIsftp == "1" ? true : false; 
        }//end method

        /// <summary>
        /// Close Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Save click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (!commonSettings.SaveVendorCommonSettings())
                {
                    Util.ShowMessage("Vendor common settings could not be saved");
                    return;
                }

                bool isSuccess = new DAL().SaveEMGInfo(txtFTPServer.Text, txtUserName.Text, txtPassword.Text, txtFolderName.Text,txtFileName.Text,rbFTP.Checked);

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
        /// Validate the fields
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            if (txtFTPServer.Text == "")
            {
                Util.ShowMessage("Please enter EMG FTP.");
                txtFTPServer.Focus();
                return false;
            }
            if (txtUserName.Text == "")
            {
                Util.ShowMessage("Please enter FTP User Name.");
                txtUserName.Focus();
                return false;
            }

            if (txtPassword.Text == "")
            {
                Util.ShowMessage("Please enter FTP Password.");
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
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EMGSettings_Load(object sender, EventArgs e)
        {
            DisplayEMGInfo();
        }
    }

}
