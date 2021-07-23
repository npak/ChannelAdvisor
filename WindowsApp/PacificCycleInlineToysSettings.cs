using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class PacificCycleInlineToysSettings : Form
    {
        private Vendor PacificCycle;

        public PacificCycleInlineToysSettings()
        {
            InitializeComponent();

            PacificCycle = new DAL().GetVendor((int)VendorName.PacificCycle);

            commonSettings.VendorInfo = PacificCycle;
        }

        private void PacificCycleSettings_Load(object sender, EventArgs e)
        {
            DisplayPacificCycleInlineToysInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayPacificCycleInlineToysInfo()
        {
            string url;
            string username;
            string password;
            string qtyFor100;

            DAL dal = new DAL();

            dal.GetPacificCycleInlineToysInfo(out url, out username, out password, out qtyFor100);

            txtURL.Text = url;
            txtUsername.Text = username;
            txtPassword.Text = password;
            txt100QtyValue.Text = qtyFor100;
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
                    = new DAL().SavePacficCycleInlineToysSettings(txtURL.Text.Trim(),
                                                        txtUsername.Text.Trim(),
                                                        txtPassword.Text.Trim(),
                                                        txt100QtyValue.Text.Trim());
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
