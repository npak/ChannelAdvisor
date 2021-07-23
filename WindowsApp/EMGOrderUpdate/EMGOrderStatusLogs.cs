using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class EMGOrderStatusLogs : Form
    {
        DAL dal = new DAL();

        public EMGOrderStatusLogs()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowLogs_Click(object sender, EventArgs e)
        {
            try
            {
                //validate
                if (dtpTo.Value < dtpFrom.Value.AddSeconds(-5))
                {
                    Util.ShowMessage("From Date cannot be greater than To Date");
                    return;
                }

                //Populate Grid
                PopulateSchedules();
            }
            catch (Exception ex)
            {
                Util.ShowMessage("The program encountered an error. Error Message: " + ex.Message);
            }
        }//end event


        /// <summary>
        /// 
        /// </summary>
        Guid scheduleID = Guid.Empty;
        private void PopulateSchedules()
        {
            //Set all datasources to null
            dgSchedules.DataSource = null;
            dgOrderStatus.DataSource = null;


            DataTable dtSchedules = dal.GetEMGOrderStatusSchedule(dtpFrom.Value.ToString("MM/dd/yy"),
                                                                    dtpTo.Value.ToString("MM/dd/yy")).Tables[0];
            dgSchedules.DataSource = dtSchedules;


            //Format Grid again
            dgSchedules.Columns[0].Visible = false;

            dgSchedules.Columns[1].ReadOnly = true;
            dgSchedules.Columns[1].Width = 200;
            

            //Populate other grids if schedules exist
            if (dtSchedules.Rows.Count > 0)
            {
                scheduleID = new Guid(dtSchedules.Rows[0]["ID"].ToString());

                PopulateOrderStatus(scheduleID);
                
            }//end if

            //Check if records were found
            if (dtSchedules.Rows.Count == 0)
            {
                Util.ShowMessage("No records found!");
            }

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        private void PopulateOrderStatus(Guid scheduleID)
        {
            dgOrderStatus.DataSource = null;

            DataTable dtUpdates = dal.GetEMGOrderStatus(scheduleID).Tables[0];

            dgOrderStatus.DataSource = dtUpdates;

            if (dtUpdates.Rows.Count > 0)
            {
                
                //Format grid columns
                dgOrderStatus.Columns["OrderNo"].Width = 100;
                dgOrderStatus.Columns["OrderStatus"].Width = 100;
                dgOrderStatus.Columns["ShipReference"].Width = 150;
                dgOrderStatus.Columns["ShippingMethod"].Width = 100;
                dgOrderStatus.Columns["ShippingCost"].Width = 75;
                dgOrderStatus.Columns["ShipDate"].Width = 100;
                dgOrderStatus.Columns["NetAmount"].Width = 75;
                dgOrderStatus.Columns["Payment"].Width = 75;
                dgOrderStatus.Columns["PaymentDate"].Width = 100;
                dgOrderStatus.Columns["Status"].Width = 75;
                dgOrderStatus.Columns["ErrorMessage"].Width = 300;
                dgOrderStatus.Columns["IsStoneEdgeUpdated"].Width = 100;
                

                dgOrderStatus.Columns["OrderNo"].DisplayIndex = 0;
                dgOrderStatus.Columns["OrderStatus"].DisplayIndex = 1;
                dgOrderStatus.Columns["ShipReference"].DisplayIndex = 2;
                dgOrderStatus.Columns["ShippingMethod"].DisplayIndex = 3;
                dgOrderStatus.Columns["ShippingCost"].DisplayIndex = 4;
                dgOrderStatus.Columns["ShipDate"].DisplayIndex = 5;
                dgOrderStatus.Columns["NetAmount"].DisplayIndex = 6;
                dgOrderStatus.Columns["Payment"].DisplayIndex = 7;
                dgOrderStatus.Columns["PaymentDate"].DisplayIndex = 8;
                dgOrderStatus.Columns["Status"].DisplayIndex = 9;
                dgOrderStatus.Columns["ErrorMessage"].DisplayIndex = 10;
                dgOrderStatus.Columns["IsStoneEdgeUpdated"].DisplayIndex = 11;

                dgOrderStatus.Columns["OrderNo"].HeaderText = "Order No";
                dgOrderStatus.Columns["OrderStatus"].HeaderText = "Order Status";
                dgOrderStatus.Columns["ShipReference"].HeaderText = "Tracking No.";
                dgOrderStatus.Columns["ShippingMethod"].HeaderText = "Shipping Method";
                dgOrderStatus.Columns["ShippingCost"].HeaderText = "Shipping Cost";
                dgOrderStatus.Columns["ShipDate"].HeaderText = "Ship Date";
                dgOrderStatus.Columns["NetAmount"].HeaderText = "Net Amt";
                dgOrderStatus.Columns["Payment"].HeaderText = "Payment";
                dgOrderStatus.Columns["PaymentDate"].HeaderText = "Payment Date";
                dgOrderStatus.Columns["Status"].HeaderText = "Status";
                dgOrderStatus.Columns["ErrorMessage"].HeaderText = "Error Message";
                dgOrderStatus.Columns["IsStoneEdgeUpdated"].HeaderText = "StoneEdge Updated";

                dgOrderStatus.Columns["IsStoneEdgeUpdated"].ReadOnly = true;
            }//end if
            
            

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgSchedules_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (!(e.RowIndex >= 0)) return;

                Guid scheduleID = new Guid(dgSchedules.SelectedRows[0].Cells[0].Value.ToString());

                PopulateOrderStatus(scheduleID);
            }
            catch (Exception ex)
            {
                Util.ShowMessage("The program encountered an error. Error Message: " + ex.Message);
            }//end catch
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteLogs_Click(object sender, EventArgs e)
        {
            try
            {
                //validate
                if (dtpTo.Value < dtpFrom.Value.AddSeconds(-5))
                {
                    Util.ShowMessage("From Date cannot be greater than To Date");
                    return;
                }//end if

                //Prompt confirmation
                if (Util.ShowConfirmation("Are you sure you want to delete all logs between " + dtpFrom.Value.ToString("MM/dd/yyyy") + " and " + dtpTo.Value.ToString("MM/dd/yyyy")) == true)
                {

                    dal.DeleteOrderStatusSchedule(dtpFrom.Value.ToString("MM/dd/yy"),
                                                    dtpTo.Value.ToString("MM/dd/yy"));

                    PopulateSchedules();
                }//end if

            }
            catch (Exception ex)
            {
                Util.ShowMessage("The program encountered an error. Error Message: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete1MonthOldLogs_Click(object sender, EventArgs e)
        {
            if (Util.ShowConfirmation("Are you sure you want to Delete all logs over 1 month old?"))
            {
                
                dal.DeleteEMGOrderStatusSchedulesOver1MonthOld();

                Util.ShowMessage("All Logs older than 1 month have been deleted.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        
    }//end class


}//end namespace
