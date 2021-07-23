using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class BenchmarkSettings : Form
    {
        private Vendor Benchmark { get; set; }

        public BenchmarkSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            Benchmark = dal.GetVendor((int)VendorName.Benchmark);

            commonSettings.VendorInfo = Benchmark;
        }

        private void Benchmark_Load(object sender, EventArgs e)
        {
            DisplayBenchmarkInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayBenchmarkInfo()
        {
            string url;
            string username;
            string password;
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";
            DAL dal = new DAL();

            dal.GetBenchmarkInfo(out url, out username, out password, out csvfolder, out csvfilename, out csvIsftp);

            txtURL.Text = url;
            txtUsername.Text = username;
            txtPassword.Text = password;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            rbFTP.Checked = csvIsftp == "1" ? true : false; 
        }

        /// <summary>
        /// 
        /// </summary>
        private bool ValidateForm()
        {
            if (txtURL.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid URL");
                txtURL.Focus();
                return false;
            }
            if (txtUsername.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid username");
                txtUsername.Focus();
                return false;
            }
            if (txtPassword.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter the password");
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (!commonSettings.SaveVendorCommonSettings())
                {
                    Util.ShowMessage("Vendor common settings could not be saved");
                    return;
                }

                bool isSuccess 
                    = new DAL().SaveBenchmarkSettings(txtURL.Text.Trim(), 
                                                        txtUsername.Text.Trim(), 
                                                        txtPassword.Text.Trim(),
                                                        txtFolderName.Text, txtFileName.Text, rbFTP.Checked);

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


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
