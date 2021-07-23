using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class EMGOrderUpdateLogs : Form
    {
        DAL dal = new DAL();

        public EMGOrderUpdateLogs()
        {
            InitializeComponent();
        }

        private void EMGOrderUpdateLogs_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

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
            dgSuccessfulUpdates.DataSource = null;
            dgUnSuccessfulUpdates.DataSource = null;


            DataTable dtSchedules = dal.GetEMGOrderUpdateSchedules(dtpFrom.Value.ToString("MM/dd/yy"),
                                                                    dtpTo.Value.ToString("MM/dd/yy")).Tables[0];
            dgSchedules.DataSource = dtSchedules;


            //Format Grid again
            dgSchedules.Columns[0].Visible = false;

            dgSchedules.Columns[1].ReadOnly = true;
            dgSchedules.Columns[1].Width = 200;

            dgSchedules.Columns[2].ReadOnly = true;
            dgSchedules.Columns[2].Width = 200;

            //Populate other grids if schedules exist
            if (dtSchedules.Rows.Count > 0)
            {
                scheduleID = new Guid(dtSchedules.Rows[0]["ID"].ToString());

                PopulateSuccessfulUpdates(scheduleID);

                PopulateUnSuccessfulUpdates(scheduleID);
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
        private void PopulateSuccessfulUpdates(Guid scheduleID)
        {
            dgSuccessfulUpdates.DataSource = null;
            DataTable dtUpdates = dal.GetEMGSuccessfulUpdates(scheduleID).Tables[0];

            dgSuccessfulUpdates.DataSource = dtUpdates;

            if (dtUpdates.Rows.Count > 0)
            {
                //Format grid columns
                dgSuccessfulUpdates.Columns["sScheduleID"].Visible = false;

                dgSuccessfulUpdates.Columns[1].ReadOnly = false;
                dgSuccessfulUpdates.Columns[1].Width = 100;

                dgSuccessfulUpdates.Columns[2].ReadOnly = false;
                dgSuccessfulUpdates.Columns[2].Width = 100;

                dgSuccessfulUpdates.Columns[3].ReadOnly = false;
                dgSuccessfulUpdates.Columns[3].Width = 400;

            }//end if
            
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        private void PopulateUnSuccessfulUpdates(Guid scheduleID)
        {
            dgUnSuccessfulUpdates.DataSource = null;
            DataTable dtUpdates = dal.GetEMGUnSuccessfulUpdates(scheduleID).Tables[0];

            dgUnSuccessfulUpdates.DataSource = dtUpdates;

            if (dtUpdates.Rows.Count > 0)
            {
                //Format grid columns
                dgUnSuccessfulUpdates.Columns["uScheduleID"].Visible = false;

                dgUnSuccessfulUpdates.Columns[1].ReadOnly = false;
                dgUnSuccessfulUpdates.Columns[1].Width = 100;

                dgUnSuccessfulUpdates.Columns[2].ReadOnly = false;
                dgUnSuccessfulUpdates.Columns[2].Width = 100;

                dgUnSuccessfulUpdates.Columns[3].ReadOnly = false;
                dgUnSuccessfulUpdates.Columns[3].Width = 400;
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

                
                Guid scheduleID = new Guid(dgSchedules.SelectedRows[0].Cells["ID"].Value.ToString());

                PopulateSuccessfulUpdates(scheduleID);
                PopulateUnSuccessfulUpdates(scheduleID);
            }
            catch (Exception ex)
            {
                Util.ShowMessage("The program encountered an error. Error Message: " + ex.Message);
            }//end catch

        }

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

                    dal.DeleteOrderUpdateSchedule(dtpFrom.Value.ToString("MM/dd/yy"),
                                                    dtpTo.Value.ToString("MM/dd/yy"));

                    PopulateSchedules();
                    

                }//end if

            }
            catch (Exception ex)
            {
                Util.ShowMessage("The program encountered an error. Error Message: " + ex.Message);
            }
        }//end event

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete1MonthOldLogs_Click(object sender, EventArgs e)
        {
            if (Util.ShowConfirmation("Are you sure you want to Delete all logs over 1 month old?"))
            {

                dal.DeleteEMGOrderUpdateSchedulesOver1MonthOld();

                Util.ShowMessage("All Logs older than 1 month have been deleted.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }//end event


    }//end class
}//end namespace
