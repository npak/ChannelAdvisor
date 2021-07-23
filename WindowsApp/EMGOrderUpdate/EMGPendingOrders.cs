using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class EMGPendingOrders : Form
    {

        DAL dal = new DAL();

        public EMGPendingOrders()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EMGPendingOrders_Load(object sender, EventArgs e)
        {
            PopulateOrders();
        }//end load


        /// <summary>
        /// 
        /// </summary>
        private void PopulateOrders()
        {
            dgOrders.DataSource = dal.GetEMGOrdersSent().Tables[0];

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgOrders.SelectedRows.Count > 0)
            {
                if (!dgOrders.SelectedRows[0].IsNewRow)
                {
                    string orderNo = dgOrders.SelectedRows[0].Cells[0].Value.ToString();

                    //try to delete row
                    if(Util.ShowConfirmation("Delete Order No: " + orderNo + "?"))
                    {
                        if (dal.DeleteEMGOrdersSentOrder(orderNo))
                        {
                            dgOrders.Rows.Remove(dgOrders.SelectedRows[0]);
                            dgOrders.EndEdit();
                        }
                        else
                        {
                            Util.ShowMessage("Could not delete Order");
                        }//end dal if
                    }//end confirmation if

                }//end if

            }//end row count
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

    }//end class

}//end namespace
