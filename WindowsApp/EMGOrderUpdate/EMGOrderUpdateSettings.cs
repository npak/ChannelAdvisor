using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class EMGOrderUpdateSettings : Form
    {
        public EMGOrderUpdateSettings()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Display saved EMG Order Update Settings in the textboxes
        /// </summary>
        private void DisplayEMGInfo()
        {
            string customerID;
            string shipToID;
            string sendOrderURL;
            string getOrderStatusURL;
            string uomCode;
            string csvFile;
            string stoneedgeDBPath;

            DAL dal = new DAL();

            dal.GetEMGOrderUpdateSettings(out customerID,
                                            out shipToID,
                                            out sendOrderURL,
                                            out getOrderStatusURL,
                                            out uomCode,
                                            out csvFile,
                                            out stoneedgeDBPath);


            txtSendOrderURL.Text = sendOrderURL;
            txtGetOrderStatusURL.Text = getOrderStatusURL;
            txtShipToID.Text = shipToID;
            txtUOMCode.Text = uomCode;
            txtCSVFile.Text = csvFile;
            txtStoneEdgeDBPath.Text= stoneedgeDBPath;

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

        /// <summary>
        /// Validate the fields
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            if (txtSendOrderURL.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter Send Order URL.");
                txtSendOrderURL.Focus();
                return false;
            }
            if (txtGetOrderStatusURL.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter Get Order Status URL.");
                txtGetOrderStatusURL.Focus();
                return false;
            }
            if (txtShipToID.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter Ship To ID.");
                txtShipToID.Focus();
                return false;
            }
            if (txtUOMCode.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter UOM Code.");
                txtUOMCode.Focus();
                return false;
            }
            if (txtCSVFile.Text.Trim() == "")
            {
                Util.ShowMessage("Please select the CSV File.");
                txtCSVFile.Focus();
                return false;
            }
            if (txtStoneEdgeDBPath.Text.Trim() == "")
            {
                Util.ShowMessage("Please select StoneEdge Database Path");
                txtStoneEdgeDBPath.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EMGOrderUpdateSettings_Load(object sender, EventArgs e)
        {
            DisplayEMGInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                bool isSuccess 
                    = new DAL().SaveEMGOrderUpdateSettings(txtShipToID.Text.Trim(),
                                                            txtSendOrderURL.Text.Trim(),
                                                            txtGetOrderStatusURL.Text.Trim(),
                                                            txtUOMCode.Text.Trim(),
                                                            txtCSVFile.Text.Trim(),
                                                            txtStoneEdgeDBPath.Text.Trim());

                if (isSuccess)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("The record could not be saved!");
                }//end isSucess if

            }//end if
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog browse = new OpenFileDialog();
            browse.Title = "Select CSV File";
            browse.Filter = "CSV Files(*.csv)|*.csv";
            browse.InitialDirectory = @"C:\";

            if (browse.ShowDialog() == DialogResult.OK)
            {
                txtCSVFile.Text = browse.FileName;
            }//end if
        }//end event


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog browse = new OpenFileDialog();
            browse.Title = "Select mdb File";
            browse.Filter = "MDB Files(*.mdb)|*.mdb";
            browse.InitialDirectory = @"C:\";

            if (browse.ShowDialog() == DialogResult.OK)
            {
                txtStoneEdgeDBPath.Text = browse.FileName;
            }//end if
        }//end event

    }//end class

}//end namespace
