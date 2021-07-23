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
    public partial class PetGearSettings : Form
    {
        private Vendor petGear { get; set; }
      
        public PetGearSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            petGear = dal.GetVendor((int)VendorName.PetGear);
            commonSettings.VendorInfo = petGear;
           
        }

        private void PetGearSettings_Load(object sender, EventArgs e)
        {
            DisplayPetGearInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayPetGearInfo()
        {
            string url;
            //string username;
            //string password;
            string inStockValue;
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";
            DAL dal = new DAL();

            dal.GetPetGearInfo(out url, out csvfolder, out csvfilename, out csvIsftp, out inStockValue);

            txtURL.Text = url;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            rbFTP.Checked = csvIsftp == "1" ? true : false;
            txtStockValue.Text = inStockValue;
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

            var ex = new Regex(@"\b(?<!\.)\d+(?!\.)\b", RegexOptions.CultureInvariant);
            if (!ex.IsMatch(txtStockValue.Text))
            {
                Util.ShowMessage("Please enter a valid InStockValue");
                txtStockValue.Focus();
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
                    = new DAL().SavePetGearSettings(txtURL.Text.Trim(), 
                                                   txtFolderName.Text, txtFileName.Text, rbFTP.Checked,txtStockValue.Text);

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
