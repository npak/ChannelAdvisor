using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace ChannelAdvisor
{
    
    /// <summary>
    /// Static class for utility functions
    /// </summary>
    public static class Util
    {
        

        /// <summary>
        /// method to display message
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMessage(string message)
        {
            MessageBox.Show(message,"Channel Advisor Updater");
        }//end method

        /// <summary>
        /// method to display message
        /// </summary>
        /// <param name="message"></param>
        public static bool ShowConfirmation(string message)
        {
            if (MessageBox.Show(message, "Channel Advisor Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }//end if
        }//end method

        /// <summary>
        /// Method to populate the vendor dropdown
        /// </summary>
        public static void PopulateVendorsOLD(ComboBox cmbVendors)
        {
            cmbVendors.ValueMember = "ID";
            cmbVendors.DisplayMember = "Vendor";
            cmbVendors.DataSource = new DAL().GetVendors().Tables[0];
        }//end method

            /// <summary>
        /// Method to populate the vendor dropdown
        /// </summary>
        public static void PopulateVendors(ComboBox cmbVendors)
        {
            cmbVendors.ValueMember = "ID";
            cmbVendors.DisplayMember = "Vendor";
            System.Data.DataTable tbl = new DAL().GetVendors().Tables[0];
            string expression = "ID >1 and  ID <> 15 and  ID <> 24";
            tbl.DefaultView.RowFilter = expression;
            cmbVendors.DataSource = tbl;
                //new DAL().GetVendors().Tables[0];
        }//end method

        /// <summary>
        /// Method to populate the vendor dropdown
        /// </summary>
        public static void PopulateVendorsWith24(ComboBox cmbVendors)
        {
            cmbVendors.ValueMember = "ID";
            cmbVendors.DisplayMember = "Vendor";
            System.Data.DataTable tbl = new DAL().GetVendors().Tables[0];
            string expression = "ID >1 and  ID <> 15";
            tbl.DefaultView.RowFilter = expression;
            cmbVendors.DataSource = tbl;
            //new DAL().GetVendors().Tables[0];
        }//end method
 
        /// <summary>
        /// Method to populate the profile dropdown
        /// </summary>
        public static void PopulateProfiles(ComboBox cmbProfile)
        {
            cmbProfile.ValueMember = "ID";
            cmbProfile.DisplayMember = "Profile";
            cmbProfile.DataSource = new DAL().GetProfiles().Tables[0];
        }//end method

        
        /// <summary>
        /// Method to validate time
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool ValidateTime(string text)
        {
            Regex reg = new Regex("^([0-1][0-9]|[2][0-3]):([0-5][0-9])$");
            return reg.IsMatch(text);
        }//end method


        /// <summary>
        /// Method to copy contents of grid to clipboard
        /// </summary>
        /// <param name="dgView"></param>
        /// <param name="columnNo"></param>
        public static void CopyGridToClipBoard(DataGridView dgView, string column)
        {
            try
            {
                string copiedText = "";

                //loop the grid
                foreach (DataGridViewRow dRow in dgView.Rows)
                {
                    copiedText += dRow.Cells[column].Value.ToString() + "\r\n";
                }//end foreach

                if (copiedText != "")
                {
                    Clipboard.SetText(copiedText, TextDataFormat.UnicodeText);
                }
                
                //Clipboard.SetData(DataFormats.Text, copiedText);
            }
            catch (Exception ex)
            {
                Util.ShowMessage("The program encountered an error. Error Message: " + ex.Message);
            }
            
        }//end method

    }//end class

}//end namespace
