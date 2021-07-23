using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using log4net;
using ChannelAdvisor.VendorServices;
using System.Threading;

namespace ChannelAdvisor
{
    public class Schedule
    {
        public readonly ILog log = LogManager.GetLogger(typeof(Schedule));
        private int _vendorID;
        private int _sleepTime;
        private DateTime _lastRunTime = DateTime.Now.Subtract(TimeSpan.FromDays(1));
        DAL dal = new DAL();
        public List<string> FilesToDelete;

        int defaultSleepTime = 15; //to-change make it 15
        string csvIsftp = "0";

        /// <summary>
        /// 
        /// </summary>
        public int SleepTime
        {
            get
            {
                return _sleepTime;
            }
        }//end property

        /// <summary>
        /// 
        /// </summary>
        public int DefaultSleepTime
        {
            get
            {
                return defaultSleepTime;
            }
        }//end property

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        public Schedule(int vendorID)
        {
            _sleepTime = defaultSleepTime;
            _vendorID = vendorID;

            //Get last run time
            _lastRunTime = dal.GetLastScheduleRunTime(_vendorID);

            if (_lastRunTime == null)
            {
                _lastRunTime = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            }//end if
        }//end constructor

        #region Public Properties

        public int VendorID
        {
            get
            {
                return _vendorID;
            }
            set
            {
                _vendorID = value;
            }
        }//end property

        public DateTime LastRunTime
        {
            get
            {
                return _lastRunTime;
            }
            set
            {
                _lastRunTime = value;
            }
        }//end property

        #endregion


        /// <summary>
        /// Method which would return the sleep time based on the 
        /// format
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetSleepTime()
        {
            return TimeSpan.FromMinutes(_sleepTime);
        }

        /// <summary>
        /// public method which checks the class members and determines whether 
        /// the batch is scheduled to be run at the current time
        /// </summary>
        /// <returns></returns>
        public List<int> IsScheduled()
        {
            //list to hold profiles
            List<int> profiles = new List<int>();

            //Check if auto update is disabled
            if (dal.IsAutoUpdateDisabled())
            {
                System.Diagnostics.Debug.WriteLine("Auto Update Disabled...");
                return profiles;
            }

            IsSingleProfileFreqScheduled(profiles);
            IsMultiProfileFreqScheduled(profiles);

            //if (IsWeekDay())
            //{
            //    return IsTime();
            //}

            //return false;
            return profiles;
        }//end method

        /// <summary>
        /// Method that will check whether multi profile frequency is scheduled
        /// </summary>
        /// <param name="profiles"></param>
        private void IsMultiProfileFreqScheduled(List<int> profiles)
        {
            if (IsWeekDay(false))
            {
                if (IsTime(false))
                {
                    DataTable dtProfiles = dal.GetMultiProfileFreqProfiles(this._vendorID).Tables[0];
                    int profileID;
                    //loop and add profiles
                    foreach (DataRow dr in dtProfiles.Rows)
                    {
                        profileID = (int)dr[0];
                        if (!profiles.Contains(profileID))
                        {
                            //testRock
                            if (this._vendorID == 11)
                            {
                                log.Debug(" ************ IsMultiProfileFreqScheduled add profile");
                            }

                            profiles.Add(profileID);
                        }//end if
                    }//end foreach
                }//end if
            }//end if
        }//end method

        /// <summary>
        /// Method that will check whether single profile frequency is scheduled
        /// </summary>
        /// <param name="profiles"></param>
        private void IsSingleProfileFreqScheduled(List<int> profiles)
        {
            if (IsWeekDay(true))
            {
                if (IsTime(true))
                {
                    //Get the profile
                    DataTable dtProfile = dal.GetSingleProfileFreqProfiles(this._vendorID).Tables[0];
                    if (dtProfile.Rows.Count > 0)
                    {
                        //testRock
                        if (this._vendorID == 11)
                        {
                            log.Debug(" ************ IsSingleProfileFreqScheduled add profile");
                        }

                        profiles.Add((int)dtProfile.Rows[0][0]);
                    }//end if

                }//end if
            }//end if
        }//end method


        /// <summary>
        /// Check if current day is in frequency weekdays
        /// </summary>
        /// <returns></returns>
        private bool IsWeekDay(bool isSingleProfileFreq)
        {
            DataSet dsWeekDays = new DataSet();

            if (isSingleProfileFreq)
            {
                dsWeekDays = dal.GetSingleProfileFreqWeekDays(this._vendorID); //.GetFrequencyWeekDays(this._vendorID);
            }
            else
            {
                dsWeekDays = dal.GetMultiProfileFreqWeekDays(this._vendorID);
            }


            //loop through datatable and find out whether current day
            //is in the weekday list
            if (dsWeekDays.Tables.Count > 0)
            {
                foreach (DataRow dr in dsWeekDays.Tables[0].Rows)
                {
                    if (DateTime.Today.DayOfWeek.ToString() == dr[0].ToString() && (bool)dr[1] == true)
                    {
                        //testRock
                        if (this._vendorID ==11)
                        {
                            log.Debug(" ************ Rock: " + dr[0].ToString() + ":"+ DateTime.Today.DayOfWeek.ToString());
                        }

                        return true;
                    }//end if
                }//end for each
            }//end if

            return false;
        }//end method

        /// <summary>
        /// Method to check whether its time to run the schedule
        /// </summary>
        /// <returns></returns>
        private bool IsTime(bool isSingleProfileFreq)
        {
            DataSet dsTimes = new DataSet();

            if (isSingleProfileFreq)
            {
                dsTimes = dal.GetSingleProfileFreqTimes(this._vendorID); //.GetFrequencyTimes(this._vendorID);
            }
            else
            {
                dsTimes = dal.GetMultiProfileFreqTimes(this._vendorID);
            }//end if



            if (dsTimes.Tables.Count > 0)
            {
                //loop through the datatable
                foreach (DataRow dr in dsTimes.Tables[0].Rows)
                {
                    DateTime fTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dr[0].ToString());

                    //Check if current time is greater than frequency time
                    if (DateTime.Now > Convert.ToDateTime(fTime))
                    {
                        //check if fTime is greater than last run time
                        if (Convert.ToDateTime(fTime) > this._lastRunTime)
                        {
                            //testRock
                            if (this._vendorID == 11)
                            {
                                log.Debug(" ************ Rock F/D/L: " + fTime.ToString() + ":" + DateTime.Now.ToString() + ":" + this._lastRunTime.ToString());
                            }

                            return true;
                        }//end if

                    }
                    else
                    {
                        //Check is next scheduled time is less than 15 minutes
                        if (Convert.ToDateTime(fTime).Subtract(DateTime.Now).Minutes < defaultSleepTime)
                        {
                            this._sleepTime = Convert.ToDateTime(fTime).Subtract(DateTime.Now).Minutes + 1;
                        }
                    }//end if
                }//end for each
            }//end if             

            return false;
        }//end method

        /// <summary>
        /// Method that will update Channel Advisor
        /// </summary>
        public void UpdateLinnworks(List<int> profiles)
        {
            //--------Put code to run update here-----------
                UpdateLinnworksForVendor(profiles);
            //--------end update code-----------------------
            //this._lastRunTime = DateTime.Now;
            this._sleepTime = defaultSleepTime;//to-change make it 15 again
        }//end method


        /// <summary>
        /// Method that would update profiles for each vendor
        /// </summary>

        private void UpdateLinnworksForVendor(List<int> profiles)
        {
            //log.Debug("UpdateLinnworksForVendor:");
            UpdateToFTP uftp = new UpdateToFTP(this.VendorID);
            csvIsftp = uftp.Settings();

            //Dictionary to maintain CA error log for each profile
            Dictionary<int, List<ErrorLog>> profileErrorLogs
                    = new Dictionary<int, List<ErrorLog>>();

            //Dictionary to store CA files
            Dictionary<int, string> profileCAFiles = new Dictionary<int, string>();

            //Clear to-delete file
            this.FilesToDelete = null;

            InventoryUpdateService invService = new InventoryUpdateService();
            var linnworksService = new ChannelAdvisor.VendorServices.LinnworksService();

            InventoryUpdateServiceDTO invUpdateServiceDTO = null;
            //Call Sevice based on vendor

            // if vendor is AZ then we need get dttable(ccsv file data)
            DataTable azdt = new DataTable();
            string skuPrefix = "";


            if (_vendorID == 18)
            {
                AZService az = new AZService();
                skuPrefix = az.VendorInfo.SkuPrefix;
                invUpdateServiceDTO = az.GetInventoryListForService();
                azdt = az.dtProducts;
                //                VendorServiceFactory.GetVendorService(_vendorID).GetInventoryListForService();
            }
            else
                invUpdateServiceDTO = VendorServiceFactory.GetVendorService(_vendorID).GetInventoryListForService();

            if ((invUpdateServiceDTO == null) ||  (invUpdateServiceDTO.WithoutResult))
                return;

            //Get files to delete
            this.FilesToDelete = invUpdateServiceDTO.ToDeleteFiles;
            List<ErrorLog> profileErrors = new List<ErrorLog>();

            // if cache is not updating
            for (int x = 0; x < profiles.Count; x++)
            {
                //Update the markup prices
                invService.UpdateMarkupPrice(_vendorID,
                                                profiles[x],
                                                invUpdateServiceDTO);

                uftp.invUpdSrvcDTO = invUpdateServiceDTO;
                uftp.ProfileID = profiles[x];

                // if vendor is AZ save csv with renamed fields
                if (_vendorID == 18)
                    profileErrors = uftp.ExportAZCSV(azdt, skuPrefix);

                profileErrors = uftp.ExportCSV();

                profileErrorLogs.Add(profiles[x], profileErrors);

                //create file
                string caFile = CAUtil.CreateCAFile(_vendorID, profiles[x], invUpdateServiceDTO.InventoryDTO);
                profileCAFiles.Add(profiles[x], caFile);

            }//end for

            //Save Logs to database
            dal.SaveScheduleLogs(_vendorID,
                                    invUpdateServiceDTO.ErrorLogDTO,
                                    profileErrorLogs,
                                    invUpdateServiceDTO.VendorFile,
                                    invUpdateServiceDTO.CAFile,
                                    profileCAFiles);

            //If files exist then delete them
            DeleteProcessedFiles();
            this._lastRunTime = DateTime.Now;

        }//end method


        /// <summary>
        /// Method that would update profiles for Moriis Daily Summary
        /// </summary>
        public bool UploadCsvForMorrisDailySummary()
        {
            bool b = true;
            //log.Debug("UpdateLinnworksForVendor:");
            UpdateToFTP uftp = new UpdateToFTP(this.VendorID);
            csvIsftp = uftp.Settings();

            MorrisDailySummaryService service = new MorrisDailySummaryService();
            string csvstring = service.GererateCsv();
            //upload order type
            uftp.csvfilename = service.csvfilename;
            if (csvstring.Length > 0)
                uftp.UploadFile(csvstring);
            else
                b = false;
            if (b)
            {
                //upload return type
                uftp.csvfolder = service.creditcsvfolder;
                uftp.csvfilename = service.csvfilename;
                if (service.CreditString.Length > 0)
                    uftp.UploadFile(service.CreditString);
                else
                    b = false;
            }
            this._lastRunTime = DateTime.Now;
            return b;
        }//end method

        /// <summary>
        /// Method that would update profiles for Morris Weekly Summary
        /// </summary>
        public bool UploadCsvForMorrisWeeklySummary()
        {
            bool b = true;
            UpdateToFTP uftp = new UpdateToFTP(this.VendorID);
            csvIsftp = uftp.Settings();

            MorrisWeeklySummaryService service = new MorrisWeeklySummaryService();
            string csvstring = service.GererateCsv();
            //upload order type
            uftp.csvfilename = service.csvfilename;
            if (csvstring.Length > 0)
                uftp.UploadFile(csvstring);
            else
                b = false;
            if (b)
            {
                //upload return type
                uftp.csvfolder = service.creditcsvfolder;
                uftp.csvfilename = service.creditcsvfilename;
                if (service.CreditString.Length > 0)
                    uftp.UploadFile(service.CreditString);
                else
                    b = false;
            }
            this._lastRunTime = DateTime.Now;
            return b;
        }//end method


        /// <summary>
        /// 
        /// </summary>
        private void DeleteProcessedFiles()
        {
            //loop files to delete
            for (int x = 0; x < this.FilesToDelete.Count; x++)
            {
                File.Delete(this.FilesToDelete[x]);
            }//end for
        }//end method

    }//end class

}//namespace
