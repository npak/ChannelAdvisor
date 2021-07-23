using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class PacificCycleSettings : Form
    {
        private Vendor PacificCycle { get; set; }

        public PacificCycleSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            PacificCycle = dal.GetVendor((int)VendorName.PacificCycle);

            commonSettings.VendorInfo = PacificCycle;
        }

        private void PacificCycleSettings_Load(object sender, EventArgs e)
        {
            DisplayPacificCycleInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayPacificCycleInfo()
        {
            string url;
            string username;
            string password;
            string qtyFor100;
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";
            DAL dal = new DAL();

            dal.GetPacificCycleInfo(out url, out username, out password, out qtyFor100, out csvfolder, out csvfilename, out csvIsftp);

            txtURL.Text = url;
            txtUsername.Text = username;
            txtPassword.Text = password;
            txt100QtyValue.Text = qtyFor100;
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
            if (txt100QtyValue.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter the Qty to be passed for 100+ Qty");
                txt100QtyValue.Focus();
                return false;
            }
            int qty;
            if (!Int32.TryParse(txt100QtyValue.Text.Trim(), out qty))
            {
                Util.ShowMessage("Please enter a valid qty");
                txt100QtyValue.Focus();
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
                    = new DAL().SavePacficCycleSettings(txtURL.Text.Trim(), 
                                                        txtUsername.Text.Trim(), 
                                                        txtPassword.Text.Trim(),
                                                        txt100QtyValue.Text.Trim(), txtFolderName.Text, txtFileName.Text, rbFTP.Checked);

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
