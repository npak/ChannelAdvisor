using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ChannelAdvisor
{
    public partial class ShipstationSettings : Form
    {
        DAL dal = new DAL();

        public ShipstationSettings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            DisplayValues();
        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void DisplayValues()
        {
            string fromZip = "";
            string toZip = "";
            string stamp = "";
            string ups = "";
            string input = "";
            string output = "";
            string lev_priorityMail = "";
            string lev_firstClassMail = "";
            string lev_parcelSelect = "";
            string uspsMarkup1 = "";
            string uspsMarkupParcel = "";
            string uspsMarkupPriority = "";
            string requireSignature = "";

            dal.GetShipstationInfo(out fromZip, out toZip, out stamp, out ups, out input, out output, out lev_priorityMail, out lev_firstClassMail, out lev_parcelSelect
                , out uspsMarkup1, out uspsMarkupParcel, out uspsMarkupPriority, out requireSignature);
            txtFromZip.Text =fromZip;
            txtToZip.Text = toZip;
            txtstamps.Text = stamp;
            txtUSPInsurance.Text = ups;
            txtInputFile.Text = input;
            txtOutputFile.Text = output;
            txtPriorityMailCutoff.Text = lev_priorityMail;
            txt1ClassMailCutoff.Text = lev_firstClassMail;
            txtParcelSelectCutoff.Text = lev_parcelSelect;
            txtUSPSMarkup1cl.Text = uspsMarkup1;
            txtUSPSMarkupParcel.Text = uspsMarkupParcel;
            txtUSPSMarkupPriority.Text = uspsMarkupPriority;
            txtRequireSignature.Text = requireSignature;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                bool isSuccess = dal.SaveShipstationSettings(txtFromZip.Text.Trim(), txtToZip.Text.Trim(),txtstamps.Text.Trim(),txtUSPInsurance.Text.Trim(),txtInputFile.Text.Trim(),txtOutputFile.Text.Trim(),
                                                             txtPriorityMailCutoff.Text.Trim(),txt1ClassMailCutoff.Text.Trim(), txtParcelSelectCutoff.Text.Trim()
                                                             ,txtUSPSMarkup1cl.Text.Trim(), txtUSPSMarkupParcel.Text.Trim(), txtUSPSMarkupPriority.Text.Trim(),txtRequireSignature.Text.Trim());

                if (isSuccess)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("There was some problem while trying to save the record. The record could not be saved.");
                }

            }
        }//end method

        private bool isValid()
        {
            // decimal 
            System.Text.RegularExpressions.Regex numericRegex = new System.Text.RegularExpressions.Regex(@"^(0|[1-9]\d*)?(\.\d+)?(?<=\d)$");

            if (txtFromZip.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid From Postal Code");
                txtFromZip.Focus();
                return false;
            }
            if (txtToZip.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid To Postal Code");
                txtToZip.Focus();
                return false;
            }

            if (txtstamps.Text.Trim() == "" || !numericRegex.IsMatch(txtstamps.Text.Trim()))
            {
                Util.ShowMessage("Please enter a valid USPS Insurance Value");
                txtstamps.Focus();
                return false;
            }

            if (txtUSPInsurance.Text.Trim() == "" || !numericRegex.IsMatch(txtUSPInsurance.Text.Trim()))
            {
                Util.ShowMessage("Please enter a valid UPS Insuranve Value");
                txtUSPInsurance.Focus();
                return false;
            }

            
            if (txtPriorityMailCutoff.Text.Trim() == "" || !numericRegex.IsMatch(txtPriorityMailCutoff.Text.Trim()))
            {
                Util.ShowMessage("Please enter a valid USPS Priority Mail Value");
                txtPriorityMailCutoff.Focus();
                return false;
            }

            if (txtParcelSelectCutoff.Text.Trim() == "" || !numericRegex.IsMatch(txtParcelSelectCutoff.Text.Trim()))
            {
                Util.ShowMessage("Please enter a valid USPS Parcel Select Value");
                txtParcelSelectCutoff.Focus();
                return false;
            }

            if (txt1ClassMailCutoff.Text.Trim() == "" || !numericRegex.IsMatch(txt1ClassMailCutoff.Text.Trim()))
            {
                Util.ShowMessage("Please enter a valid USPS Class Mail Value");
                txt1ClassMailCutoff.Focus();
                return false;
            }

            if (txtRequireSignature.Text.Trim() == "" || !numericRegex.IsMatch(txtRequireSignature.Text.Trim()))
            {
                Util.ShowMessage("Please enter a valid Value for Reqire Sugnature.");
                txt1ClassMailCutoff.Focus();
                return false;
            }

            if (txtInputFile.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Input File Name");
                txtInputFile.Focus();
                return false;
            }

            if (txtOutputFile.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Output File Name");
                txtOutputFile.Focus();
                return false;
            }
            if  (txtUSPSMarkup1cl.Text.Trim() != "" && !numericRegex.IsMatch(txtUSPSMarkup1cl.Text.Trim()))
            {
                Util.ShowMessage("Please enter a valid USPS Markup First Class Mail");
                txtUSPSMarkup1cl.Focus();
                return false;
            }

            if (txtUSPSMarkupParcel.Text.Trim() != "" && !numericRegex.IsMatch(txtUSPSMarkupParcel.Text.Trim()))
            {
                Util.ShowMessage("Please enter a valid USPS Parcel Select Ground");
                txtUSPSMarkupParcel.Focus();
                return false;
            }

            if (txtUSPSMarkupPriority.Text.Trim() != "" && !numericRegex.IsMatch(txtUSPSMarkupPriority.Text.Trim()))
            {
                Util.ShowMessage("Please enter a valid USPS Markup Priority Mail");
                txtUSPSMarkupPriority.Focus();
                return false;
            }
            return true;
        }

    }//end class

}//end namespace
