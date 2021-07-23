using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class DressUpAmericaSettings : Form
    {
        private Vendor DressUpAmerica { get; set; }

        public DressUpAmericaSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            DressUpAmerica = dal.GetVendor((int)VendorName.DressUpAmerica);

            commonSettings.VendorInfo = DressUpAmerica;
        }

        private void DressUpAmerica_Load(object sender, EventArgs e)
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

            dal.GetDressUpAmericaInfo(out url, out csvfolder, out csvfilename, out csvIsftp);

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
            //check whether url has been entered
            if (txtURL.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a URL.");
                return;
            }

            if (txtFolderName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a CSV Folder name");
                txtFolderName.Focus();
                return;
            }

            if (txtFileName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a CSV File name");
                txtFileName.Focus();
                return;
            }
            if (!commonSettings.SaveVendorCommonSettings())
            {
                Util.ShowMessage("Vendor common settings could not be saved");
                return;
            }

            bool isSuccess
                    = new DAL().SaveDressUpAmericaSettings(txtURL.Text.Trim(), txtFolderName.Text, txtFileName.Text, rbFTP.Checked);

            if (isSuccess)
            {
                Util.ShowMessage("Record Saved!");
            }
            else
            {
                Util.ShowMessage("The record could not be saved!");
            }//end isSucess if
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
