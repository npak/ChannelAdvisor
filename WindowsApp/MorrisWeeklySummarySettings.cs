using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class MorrisWeeklySummarySettings : Form
    {
        private Vendor Morris { get; set; }

        public MorrisWeeklySummarySettings()
        {
            InitializeComponent();

            //DAL dal = new DAL();
            //Morris = dal.GetVendor((int)VendorName.Morris);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MorrisDailySummarySettings_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }//end event


        /// <summary>
        /// 
        /// </summary>
        private void DisplayInfo()
        {
            string url = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvCreditfolder = "";
            string csvCreditfilename = "";
            DAL dal = new DAL();

            dal.GetMorrisWeeklySummarySettings(out url, out csvfolder,out csvfilename, out csvCreditfolder, out csvCreditfilename);
            txtURL.Text = url;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            txtCreditFolderName.Text = csvCreditfolder;
            txtCrediFileName.Text = csvCreditfilename;
                    }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {

                bool isSuccess = new DAL().SaveMorrisWeeklySummarySettings(txtURL.Text.Trim(), txtFolderName.Text,txtFileName.Text,txtCreditFolderName.Text.Trim(),txtCrediFileName.Text.Trim());

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

            if (txtCreditFolderName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a Credit Folder name");
                txtCreditFolderName.Focus();
                return false;
            }
            //if (txtFileName.Text.Trim() == "")
            //{
            //    Util.ShowMessage("Please enter a File name");
            //    txtFileName.Focus();
            //    return false;
            //}

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
     

    }
}
