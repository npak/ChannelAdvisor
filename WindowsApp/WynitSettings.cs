using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class WynitSettings : Form
    {
        DAL dal = new DAL();
        private Vendor Wynit { get; set; }

        public WynitSettings()
        {
            InitializeComponent();

            Wynit = dal.GetVendor((int)VendorName.Wynit);

            commonSettings.VendorInfo = Wynit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WynitSettings_Load(object sender, EventArgs e)
        {
            DisplayWynitInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayWynitInfo()
        {
            string ftpServer = "";
            string ftpUserName = "";
            string ftpPassword = "";

            dal.GetWynitInfo(out ftpServer, out ftpUserName, out ftpPassword);

            txtFTPServer.Text = ftpServer;
            txtUserName.Text = ftpUserName;
            txtPassword.Text = ftpPassword;
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

                bool isSuccess = dal.SaveWynitInfo(txtFTPServer.Text, txtUserName.Text, txtPassword.Text);

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
            if (txtFTPServer.Text == "")
            {
                Util.ShowMessage("Please enter FTP Server");
                txtFTPServer.Focus();
                return false;
            }
            if (txtUserName.Text == "")
            {
                Util.ShowMessage("Please enter FTP Login");
                txtUserName.Focus();
                return false;
            }
            if (txtPassword.Text == "")
            {
                Util.ShowMessage("Please enter FTP Password");
                txtPassword.Focus();
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
