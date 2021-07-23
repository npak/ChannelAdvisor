using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace ChannelAdvisor
{
    public partial class LWAPISettings : Form
    {
        DAL dal = new DAL();

        public LWAPISettings()
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
            string fileName = "";
            string folderName = "";
            string appID = "";
            string appSecret = "";
            string token = "";
            string fromID = "";
            string toID = "";

            dal.GetLWAPIInfo(out folderName, out fileName, out appID, out appSecret, out token, out fromID, out toID);
            txtFolder.Text = folderName;
            txtFileName.Text = fileName;
            txtApplicationId.Text = appID;
            txtApplicationSecret.Text = appSecret;
            txtToken.Text = token;
            txtOrderIdFrom.Text = fromID;
            txtOrderIdTo.Text = toID; 
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
                bool isSuccess = dal.SaveLWAPISettings(txtFolder.Text.Trim(),txtFileName.Text.Trim(), txtApplicationId.Text.Trim(), txtApplicationSecret.Text.Trim(), txtToken.Text.Trim(),txtOrderIdFrom.Text, txtOrderIdTo.Text);

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
            
            if (txtFolder.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Input File Name");
                txtFolder.Focus();
                return false;
            }

            if (txtFileName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Output File Name");
                txtFileName.Focus();
                return false;
            }

            //
            if (txtApplicationId.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Application ID");
                txtApplicationId.Focus();
                return false;
            }

            if (txtApplicationSecret.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Application Secret");
                txtApplicationSecret.Focus();
                return false;
            }

            if (txtToken.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Token");
                txtToken.Focus();
                return false;
            }

            if (txtOrderIdFrom.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid OrderID");
                txtOrderIdFrom.Focus();
                return false;
            }

            return true;
        }

    }//end class

}//end namespace
