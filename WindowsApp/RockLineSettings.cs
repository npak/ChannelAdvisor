using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class RockLineSettings : Form
    {
        private Vendor RockLine { get; set; }

        public RockLineSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            RockLine = dal.GetVendor((int)VendorName.Rockline);

            commonSettings.VendorInfo = RockLine;
        }


        private void RockLineSettings_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayInfo()
        {
            string url = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";
            DAL dal = new DAL();

            dal.GetRockLineInfo(out url, out csvfolder, out csvfilename, out csvIsftp);

            txtURL.Text = url;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            rbFTP.Checked = csvIsftp == "1" ? true : false; 
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
                        = new DAL().SaveRockLineSettings(txtURL.Text.Trim(), txtFolderName.Text, txtFileName.Text, rbFTP.Checked);
                if (isSuccess)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("The record could not be saved!");
                }//end isSucess if
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool ValidateForm()
        {
            if (!commonSettings.ValidateInput())
                return false;

            if (txtURL.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid URL");
                txtURL.Focus();
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

    }

}
