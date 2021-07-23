using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class Logs : Form
    {
        DAL dal = new DAL();

        public Logs()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logs_Load(object sender, EventArgs e)
        {
            
            //Load vendors
            Util.PopulateVendors(cmbVendor);

            //Display current date in date pickers
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;

        }//end event

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
                if (dtpTo.Value < dtpFrom.Value)
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
        private void PopulateSchedules()
        {
            //Set all datasources to null
            dgSchedules.DataSource = null;
            dgLocalErrors.DataSource = null;
            dgProfiles.DataSource = null;
            dgCAErrors.DataSource = null;


            DataTable dtSchedules = dal.GetSchedules((int)cmbVendor.SelectedValue,
                                                            dtpFrom.Value.ToString("MM/dd/yy"),
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

                //Display Local Error log
                PopulateLocalErrors(scheduleID);

                //Display profiles
                PopulateScheduleProfiles(scheduleID);
                
                
                
                
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        Guid scheduleID = Guid.Empty;
        private void dgSchedules_CellClick(object sender, 
                                            System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (!(e.RowIndex >= 0)) return;

                //Clear grids
                dgLocalErrors.DataSource = null;
                dgProfiles.DataSource = null;
                dgCAErrors.DataSource = null;

                //scheduleID = new Guid(dgSchedules.Rows[e.RowIndex].Cells["sID"].Value.ToString());
                scheduleID = new Guid(dgSchedules.Rows[e.RowIndex].Cells[0].Value.ToString());

                //Display Local Error log
                PopulateLocalErrors(scheduleID);

                //Display profiles
                PopulateScheduleProfiles(scheduleID);
            }
            catch (Exception ex)
            {
                Util.ShowMessage("The program encountered an error. Error Message: " + ex.Message);
            }//end catch
        }//end event


        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        private void PopulateScheduleProfiles(Guid scheduleID)
        {
            DataTable dtProfiles = dal.GetScheduleProfiles(scheduleID).Tables[0];

            dgProfiles.DataSource = dtProfiles;

            if (dtProfiles.Rows.Count > 0)
            {
                //format columns
                dgProfiles.Columns[0].Visible = false;

                dgProfiles.Columns[1].ReadOnly = true;
                dgProfiles.Columns[1].Width = 150;

                dgProfiles.Columns[2].ReadOnly = true;
                dgProfiles.Columns[2].Width = 180;


                if (dgProfiles.Rows.Count > 0)
                {
                    //Display CA error log
                    PopulateCAErrorLog(scheduleID, (int)dgProfiles.Rows[0].Cells["ProfileID"].Value);
                }//end inner if

            }//end main if

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="profileID"></param>
        private void PopulateCAErrorLog(Guid scheduleID, int profileID)
        {
            DataTable dtCAErrors = dal.GetCAErrorLog(scheduleID, profileID).Tables[0];

            dgCAErrors.DataSource = dtCAErrors;

            if (dtCAErrors.Rows.Count > 0)
            {
                //format grid
                dgCAErrors.Columns[0].Visible = false;

                dgCAErrors.Columns[1].ReadOnly = false;
                dgCAErrors.Columns[1].Width = 420;
            }//end if

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProfiles_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (!(e.RowIndex >= 0)) return;

                int profileID = (int)dgProfiles.Rows[e.RowIndex].Cells["ProfileID"].Value;
                //Guid scheduleID = new Guid(dgSchedules.SelectedRows[0].Cells["ID"].Value.ToString());

                PopulateCAErrorLog(scheduleID, profileID);
            }
            catch (Exception ex)
            {
                Util.ShowMessage("The program encountered an error. Error Message: " + ex.Message);
            }//end catch


        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        private void PopulateLocalErrors(Guid scheduleID)
        {
            DataTable dtLocalErrors = dal.GetLocalErrorLog(scheduleID).Tables[0];

            dgLocalErrors.DataSource = dtLocalErrors;

            if (dtLocalErrors.Rows.Count > 0)
            {
                //Format grid columns
                dgLocalErrors.Columns["ID"].Visible = false;

                dgLocalErrors.Columns["ErrorDesc"].Width = 530;
                dgLocalErrors.Columns["ErrorDesc"].ReadOnly = false;
            }//end if
            
        }//end method

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }//end event

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyLocalErrors_Click(object sender, EventArgs e)
        {
            Util.CopyGridToClipBoard(dgLocalErrors, "ErrorDesc");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyCAErrors_Click(object sender, EventArgs e)
        {
            Util.CopyGridToClipBoard(dgCAErrors, dgCAErrors.Columns[1].Name);
        }//end event

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
                if (dtpTo.Value < dtpFrom.Value)
                {
                    Util.ShowMessage("From Date cannot be greater than To Date");
                    return;
                }//end if

                //Prompt confirmation
                if (Util.ShowConfirmation("Are you sure you want to delete all logs between " + dtpFrom.Value.ToString("MM/dd/yyyy") + " and " + dtpTo.Value.ToString("MM/dd/yyyy")) == true)
                {
                    //Delete CA files first
                    DataTable dtCAFiles = dal.GetCAFilesForDate((int)cmbVendor.SelectedValue,
                                                dtpFrom.Value.ToString("MM/dd/yy"),
                                                dtpTo.Value.ToString("MM/dd/yy")).Tables[0];
                    DeleteCAFiles(dtCAFiles);

                    
                    //Delete Vendor files first
                    DataTable dtVendorFiles = dal.GetVendorFilesForDate((int)cmbVendor.SelectedValue,
                                                dtpFrom.Value.ToString("MM/dd/yy"),
                                                dtpTo.Value.ToString("MM/dd/yy")).Tables[0];
                    DeleteVendorFiles(dtVendorFiles);


                    //Delete Vendor files first

                    dal.DeleteSchedule((int)cmbVendor.SelectedValue,
                                                dtpFrom.Value.ToString("MM/dd/yy"),
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
        private void btnDelete1WeekOldLogs_Click(object sender, EventArgs e)
        {
            if(Util.ShowConfirmation("Are you sure you want to Delete all logs over 1 month old?"))
            {
                try
                { 
                //Delete CA files first
                DataTable dtCAFiles = dal.GetCAFilesOver1MonthOld().Tables[0];
                DeleteCAFiles(dtCAFiles);

                //Delete vendor files
                DataTable dtVendorFiles = dal.GetVendorFilesOver1MonthOld().Tables[0];
                DeleteVendorFiles(dtVendorFiles);

                dal.DeleteScheduleOver1MonthOld();

                Util.ShowMessage("All Logs older than 1 month have been deleted.");
                }
                catch (Exception ex)
                {
                    Util.ShowMessage("Error deleting: " +ex.Message);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        private void DeleteVendorFiles(DataTable dt)
        {
            //loop datatable
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["VendorFile"] != null && dr["FileArchive"] != null)
                {
                    if (!String.IsNullOrEmpty(dr["VendorFile"].ToString()) &&
                        !String.IsNullOrEmpty(dr["FileArchive"].ToString()))
                    {
                        string file = dr["FileArchive"].ToString() +
                            SettingsConstant.VendorFile_Folder_Name + "\\" +
                            dr["VendorFile"].ToString();

                        try
                        {
                            if (System.IO.File.Exists(file))
                                System.IO.File.Delete(file);
                        }
                        catch (Exception ex)
                        {
                            Util.ShowMessage("Error deleting vendor files: "  + ex.Message);
                        }//end try catch
                    }//end if
                }//end if
                //string file = 
            }//end foreach
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        private void DeleteCAFiles(DataTable dt)
        {
            //loop datatable
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CAFile"] != null && dr["FileArchive"] != null)
                {
                    if (!String.IsNullOrEmpty(dr["CAFile"].ToString()) && 
                        !String.IsNullOrEmpty(dr["FileArchive"].ToString()))
                    {
                        string file = dr["FileArchive"].ToString() +
                            SettingsConstant.CAFiles_Folder_Name + "\\" +
                            dr["CAFile"].ToString();

                        try
                        {
                            if (System.IO.File.Exists(file))
                                System.IO.File.Delete(file);
                        }
                        catch(Exception ex)
                        {
                            Util.ShowMessage("Error deleting CAFiles: "  + ex.Message);
                        }//end try catch
                    }//end if
                }//end if
                //string file = 
            }//end foreach
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProfiles_CellContentDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    string fileName = dgProfiles.Rows[e.RowIndex].Cells[2].Value.ToString();

                    if (!String.IsNullOrEmpty(fileName))
                    {
                        string vendorFolder = CAUtil.GetVendorFolder((int)cmbVendor.SelectedValue);

                        vendorFolder += SettingsConstant.CAFiles_Folder_Name + "\\" + fileName;

                        System.Diagnostics.Process.Start(vendorFolder);
                    }//end if
                }//end if
            }
            catch (Exception ex)
            {
                Util.ShowMessage(ex.Message);
            }
            
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgSchedules_CellContentDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    string fileName = dgSchedules.Rows[e.RowIndex].Cells[2].Value.ToString();

                    if (!String.IsNullOrEmpty(fileName))
                    {
                        string vendorFolder = CAUtil.GetVendorFolder((int)cmbVendor.SelectedValue);

                        if (fileName.Contains(","))
                        {
                            OpenMultipleVendorFiles(vendorFolder, fileName);
                        }
                        else
                        {
                            vendorFolder += SettingsConstant.VendorFile_Folder_Name + "\\" + fileName;
                            System.Diagnostics.Process.Start(vendorFolder);
                        }//end if

                    }//end if
                }//end if
            }
            catch (Exception ex)
            {
                Util.ShowMessage(ex.Message);
            }

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="files"></param>
        private void OpenMultipleVendorFiles(string folder, string vendorFiles)
        {
            string[] files = vendorFiles.Split(Convert.ToChar(","));

            //loop and open files
            for (int x = 0; x < files.GetLength(0); x++)
            {
                string file = folder + SettingsConstant.VendorFile_Folder_Name + "\\" + files[x];
                System.Diagnostics.Process.Start(file);
            }//end for

        }

        private void dgSchedules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }//end method

    }//end class


}//end namespace

