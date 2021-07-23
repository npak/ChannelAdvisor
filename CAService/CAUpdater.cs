using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.ServiceProcess;
using System.Text;
using System.Data.SqlClient;
using System.Threading;
using log4net;
using log4net.Config;
using ChannelAdvisor.VendorServices;
using System.IO;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class CAUpdater : ServiceBase
    {
        public readonly ILog log = LogManager.GetLogger(typeof(CAUpdater));

        public CAUpdater()
        {
            try
            {
                XmlConfigurator.Configure();
                log.Debug("Initializing");
                Debug.WriteLine("Initializing");
                InitializeComponent();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("CAService Error: " + ex.Message);
                log.Error(ex.Message);
            }

        }//end constructor

        public void Start()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture
                = System.Threading.Thread.CurrentThread.CurrentUICulture
                = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            Thread.Sleep(10000);
            log.Debug("Service started...");
            Debug.WriteLine("Service started...");
            int x = 1;

            while (x == 1)
            {
                try
                {
                    StartCacheThread();
                    log.Debug("Trying to load schedules...");
                    System.Diagnostics.Debug.Write("Trying to load schedules...");
                    LoadSchedules();
                    x = 0;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write("CAService Error: " + ex.Message);
                    System.Diagnostics.Debug.Write("Retrying after 2 minutes");
                    log.Error(ex.Message + ". Retrying after 2 minutes");
                    Thread.Sleep(TimeSpan.FromMinutes(2));
                }//end try catch
            }//end while

        }//end event

        protected override void OnStop()
        {
            Debug.WriteLine("Service stopped...");
        }

        /// <summary>
        /// This method gets the distinct vendors and creates seperate
        /// threads for each vendor
        /// </summary>
        private void LoadSchedules()
        {
            DAL dal = new DAL();

            DataSet dsVendors = dal.GetDistinctVendors();

            //log.Debug(String.Format("Processing {0} dsVendors.Tables[0].Rows", dsVendors.Tables[0].Rows.Count-2));
            //loop through vendors and create schedules
            int vendorID = 0;
            foreach (DataRow dr in dsVendors.Tables[0].Rows)
            {
                vendorID = Convert.ToInt32(dr["VendorID"]);
                // exclude EMG =1 and Benchmark=15
                if (vendorID > 1 && vendorID != 15)
                {
                    Schedule schedule = new Schedule(vendorID);
                    log.Debug("Shedule is created for Vendor ID:" + vendorID.ToString());

                    if (vendorID == 19)
                    {
                        log.Debug("Vendor is Morris Daily Summary");
                        Thread t19 = new Thread(new ParameterizedThreadStart(ExecuteMorrisDailySummary));
                        t19.Start(schedule);
                    }
                    else if (vendorID == 24)
                    {
                        log.Debug("Vendor is Morris XMLCreator");
                        Thread t24 = new Thread(new ParameterizedThreadStart(ExecuteMorrisXMLCreator));
                        t24.Start(schedule);
                    }
                    else if (vendorID == 29)
                    {
                        log.Debug("Vendor is Morris Weekly Summary");
                        Thread t29 = new Thread(new ParameterizedThreadStart(ExecuteMorrisWeeklySummary));
                        t29.Start(schedule);
                    }
                    else
                    {
                        Thread t = new Thread(new ParameterizedThreadStart(Execute));
                        t.Start(schedule);
                    }
                }
            }//end foreach

        }//end method

        private void LoadSchedulesOLD()
        {
            DAL dal = new DAL();

            DataSet dsVendors = dal.GetDistinctVendors();

            log.Debug(String.Format("Processing {0} dsVendors.Tables[0].Rows", dsVendors.Tables[0].Rows.Count));
            //loop through vendors and create schedules

            foreach (DataRow dr in dsVendors.Tables[0].Rows)
            {
                Schedule schedule = new Schedule(Convert.ToInt32(dr["VendorID"]));
                log.Debug("Shedule is created for Vendor ID:" + dr["VendorID"].ToString());

                Thread t = new Thread(new ParameterizedThreadStart(Execute));
                t.Start(schedule);

            }//end foreach

        }//end method

        /// <summary>
        /// Method which is executed inside each thread
        /// </summary>
        /// <param name="s"></param>
        /// <param name="sleep"></param>
        private void Execute(Object pSchedule)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture
                = System.Threading.Thread.CurrentThread.CurrentUICulture
                  = System.Globalization.CultureInfo.GetCultureInfo("en-US");

            //TODO---This method should not execute unless all the schedule objects
            //-------are created sucessfully.

            try
            {
                Schedule schedule = (Schedule)pSchedule;
                log.Debug("New Schedule Thread Started for Vendor: " + schedule.VendorID.ToString());
                System.Diagnostics.Debug.WriteLine("New Schedule Thread Started for Vendor: " + schedule.VendorID.ToString());
                while (1 == 1)
                {
                    //IsScheduled is will check whether there is a schedule at this time. If there is
                    //then the List<> will contain profiles which need to run
                    List<int> profiles = schedule.IsScheduled();

                    string cachepath = Path.Combine(Application.StartupPath, "inventoryCache");

                    if (profiles.Count > 0 && Directory.GetFiles(cachepath).Length > 0)
                    {

                        if (Interlocked.CompareExchange(ref InventoryCache.LoadAllItemsRunning, 0, 0) == 0)
                        {
                            //log.Debug("IsScheduled :" + schedule.VendorID.ToString());

                            try
                            {
                                Interlocked.Increment(ref InventoryCache.CacheReading);
                                schedule.UpdateLinnworks(profiles);
                                System.Diagnostics.Debug.WriteLine("Schedule Executing for vendor: " + schedule.VendorID.ToString());
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex.Message + ". Retrying after " + schedule.SleepTime.ToString() + " minutes.");
                                log.Error(string.Format("Vendor ID: {0}. Message: {1}. Stack trace: {2}",
                                    schedule.VendorID, ex.Message, ex.StackTrace));
                            }
                            finally
                            {
                                Interlocked.Decrement(ref InventoryCache.CacheReading);
                            }
                        }
                        else
                            log.Debug("Load All Items is running. The thread is sleeping. Vendor: " + schedule.VendorID.ToString());

                    }//end if

                    System.Diagnostics.Debug.WriteLine("Schedule will run again in " + schedule.SleepTime.ToString() + " minutes.");

                    Thread.Sleep(schedule.GetSleepTime());


                }//end while
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CAService Error: " + ex.Message);
                log.Error(ex.Message);
            }

        }//end method

        /// <summary>
        /// Method which is executed inside Morris Daily Summary thread
        /// </summary>
        /// <param name="pSchedule"></param>
        private void ExecuteMorrisDailySummary(Object pSchedule)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture
                = System.Threading.Thread.CurrentThread.CurrentUICulture
                  = System.Globalization.CultureInfo.GetCultureInfo("en-US");

            //TODO---This method should not execute unless all the schedule objects
            //-------are created sucessfully.

            try
            {
                Schedule schedule = (Schedule)pSchedule;
                log.Debug("New Schedule Thread Started for Morris Daily Summary: " + schedule.VendorID.ToString());
                System.Diagnostics.Debug.WriteLine("New Schedule Thread Started for Morris Daily Summary: " + schedule.VendorID.ToString());
                while (1 == 1)
                {
                    //IsScheduled is will check whether there is a schedule at this time. If there is
                    //then the List<> will contain profiles which need to run
                    List<int> profiles = schedule.IsScheduled();

                    if (profiles.Count > 0)
                    {
                            try
                            {
                                if (schedule.UploadCsvForMorrisDailySummary())
                                    log.Info("Uploaded Csv For Morris DailySummary ");
                                else
                                    log.Info("Didn't upload Csv For Morris DailySummary ");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex.Message + ". Retrying after " + schedule.SleepTime.ToString() + " minutes.");
                                log.Error(string.Format("Vendor ID: {0}. Message: {1}. Stack trace: {2}",
                                    schedule.VendorID, ex.Message, ex.StackTrace));
                            }

                    }//end if

                    Thread.Sleep(schedule.GetSleepTime());

                }//end while
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CAService Error: " + ex.Message);
                log.Error(ex.Message);
            }

        }//end method

        /// <summary>
        /// Method which is executed inside Morris Daily Summary thread
        /// </summary>
        /// <param name="pSchedule"></param>
        private void ExecuteMorrisWeeklySummary(Object pSchedule)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture
                = System.Threading.Thread.CurrentThread.CurrentUICulture
                  = System.Globalization.CultureInfo.GetCultureInfo("en-US");

            //TODO---This method should not execute unless all the schedule objects
            //-------are created sucessfully.

            try
            {
                Schedule schedule = (Schedule)pSchedule;
                log.Debug("New Schedule Thread Started for Morris Weekly Summary: " + schedule.VendorID.ToString());
                System.Diagnostics.Debug.WriteLine("New Schedule Thread Started for Morris Weekly Summary: " + schedule.VendorID.ToString());
                while (1 == 1)
                {
                    //IsScheduled is will check whether there is a schedule at this time. If there is
                    //then the List<> will contain profiles which need to run
                    List<int> profiles = schedule.IsScheduled();

                    if (profiles.Count > 0)
                    {
                        try
                        {
                            if (schedule.UploadCsvForMorrisWeeklySummary())
                                log.Info("Uploaded Csv For Morris Weekly Summary ");
                            else
                                log.Info("Didn't upload Csv For Morris Weekly Summary ");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message + ". Retrying after " + schedule.SleepTime.ToString() + " minutes.");
                            log.Error(string.Format("Vendor ID: {0}. Message: {1}. Stack trace: {2}",
                                schedule.VendorID, ex.Message, ex.StackTrace));
                        }

                    }//end if

                    Thread.Sleep(schedule.GetSleepTime());

                }//end while
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CAService Error: " + ex.Message);
                log.Error(ex.Message);
            }

        }//end method

        /// <summary>
        /// Method which is executed inside Morris Daily SummaryXML Creator thread
        /// </summary>
        /// <param name="pSchedule"></param>
        private void ExecuteMorrisXMLCreator(Object pSchedule)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture
                = System.Threading.Thread.CurrentThread.CurrentUICulture
                  = System.Globalization.CultureInfo.GetCultureInfo("en-US");

            //TODO---This method should not execute unless all the schedule objects
            //-------are created sucessfully.

            try
            {
                Schedule schedule = (Schedule)pSchedule;
                log.Debug("New Schedule Thread Started for Morris XMLCreator: " + schedule.VendorID.ToString());
                System.Diagnostics.Debug.WriteLine("New Schedule Thread Started for Morris XMLCreator: " + schedule.VendorID.ToString());
                while (1 == 1)
                {
                    //IsScheduled is will check whether there is a schedule at this time. If there is
                    //then the List<> will contain profiles which need to run
                    List<int> profiles = schedule.IsScheduled();
                    //test
                    log.Info("MorrisXMLCreator profile count = " + profiles.Count.ToString());
                    if (profiles.Count > 0)
                    {
                        try
                        {
                            ChannelAdvisor.MorrisXMLCreatorService serv = new MorrisXMLCreatorService();
                            //test
                            log.Info("MorrisXMLCreator start serv.CreateXMLDoc()");
                            serv.CreateXMLDoc();

                           log.Info("Converted Csv to XML For MorrisXMLCreator");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message + ". Retrying after " + schedule.SleepTime.ToString() + " minutes.");
                            log.Error(string.Format("Vendor ID: {0}. Message: {1}. Stack trace: {2}",
                                schedule.VendorID, ex.Message, ex.StackTrace));
                        }

                    }//end if

                    Thread.Sleep(schedule.GetSleepTime());

                }//end while
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CAService Error: " + ex.Message);
                log.Error(ex.Message);
            }

        }//end method

        /// <summary>
        /// Method which is executed inside RewriteCacheT thread
        /// </summary>
        private void StartCacheThread()
        {
            Thread thread = new Thread(RewriteCache);
            thread.IsBackground = true;
            thread.Name = "RewriteCacheThread";
            thread.Start();
        }


        private static void GetPaths(out string path, out string inventoryPath, out string pathtemp, out string messagefile)
        {
            //path = Path.Combine(Path.GetTempPath(), "inventoryCache");
            //inventoryPath = Path.Combine(path, "allItems"); CAServiceLog
            path = Path.Combine(Application.StartupPath, "inventoryCache");
            pathtemp = path + "temp";
            messagefile = Path.Combine(pathtemp, "message.txt");
            inventoryPath = Path.Combine(path, "allItems");
        }

        private void RewriteCache()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture
               = System.Threading.Thread.CurrentThread.CurrentUICulture
                 = System.Globalization.CultureInfo.GetCultureInfo("en-US");

            DateTime lastCacheDate = DateTime.MinValue; // the latest cache donload datetime. need to avoid run rewrite cache twice. 
            int errorCounter = 0;  //  Limit to try again rewrite cache. Hardcoded, set = 10. after 10 times the service will be sleeped. 
            string path;
            string inventoryPath;
            string pathtemp;
            string messagefile;
            //int dateresult = 1;
            bool datetoStart = true; // start recreate cache
            string scheduleValue;
            DirectoryInfo di;
            LinnworksService lin;
            //bool isLoading = false;
            try
            {
                log.Debug("Cache thread is started");
                DAL d = new DAL();
                // datetoStart = true;
                GetPaths(out path, out inventoryPath, out pathtemp, out messagefile);
                bool IsTempCacheUploaded = false; // Dwmladed or not cache to temp director
                int resultCount = 0;
                while (1 == 1)
                {
                    //In case of error set thread to sleep
                    // check datatime of the cache
                    // if i then rewrite
                    // else sleep 15 min;

                    if (errorCounter > 10)
                    {
                        if (File.Exists(messagefile))
                            File.Delete(messagefile);
                        errorCounter = 0;
                    }

                    //read if app sent the message to update cache.

                    if (!Directory.Exists(pathtemp))
                    {
                        Directory.CreateDirectory(pathtemp);
                    }


                    if (!File.Exists(messagefile))
                    {
                        // lastCacheDate = lastCacheDate.AddHours(2);
                        lastCacheDate = lastCacheDate.AddHours(2);

                        if (DateTime.Compare(lastCacheDate, DateTime.Now) < 0)
                        {
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            scheduleValue = d.GetSettingValue(SettingsConstant.Cache_Update_Interval);
                            datetoStart = GetScheduleTime(scheduleValue);
                        }
                        else
                            datetoStart = false;
                    }
                    else
                    {
                        datetoStart = true;
                    }

                    if (datetoStart)
                    {
                        // load cache to temp folder
                        //if (Interlocked.CompareExchange(ref InventoryCache.LoadAllItemsToTestFolder, 1, 0) == 0)
                        if (!IsTempCacheUploaded)
                        {
                            if (!File.Exists(messagefile))
                                CreateMessagefile(messagefile);

                            lin = new LinnworksService();
                            resultCount = lin.RunDownloadAllItems().Count;
                            if (resultCount > 0)
                            {
                                IsTempCacheUploaded = true;
                                lastCacheDate = DateTime.Now;
                                errorCounter = 0;
                                log.Debug("lin.RunDownloadAllItems to temp folder Complited");
                            }
                            else
                            {
                                errorCounter++;
                                log.Debug("Error lin.RunDownloadAllItems to temp folder return 0 records");
                            }
                        }

                        if (IsTempCacheUploaded)
                        {
                            Interlocked.CompareExchange(ref InventoryCache.LoadAllItemsRunning, 1, 0);

                            if (Interlocked.CompareExchange(ref InventoryCache.CacheReading, 0, 0) == 0)
                            {

                                try
                                {
                                    // Move the file.
                                    log.Debug("Movefile:" + path);
                                    ClearFolder(path);
                                    log.Debug("Clearpath :" + path);

                                    DirectoryInfo dir = new DirectoryInfo(pathtemp);
                                    foreach (FileInfo fi in dir.GetFiles())
                                    {
                                        File.Copy(fi.FullName, path + "\\" + fi.Name);
                                        log.Debug("Copy  to :" + path + "\\" + fi.Name);
                                    }
                                    foreach (FileInfo fi in dir.GetFiles())
                                    {
                                        fi.Delete();
                                    }
                                    if (File.Exists(messagefile))
                                        File.Delete(messagefile);

                                    IsTempCacheUploaded = false;
                                    log.Debug("FINISH!!!");
                                }
                                catch (Exception ex)
                                {
                                    log.Debug("Error while rewriting cache : " + ex.Message);
                                }
                                finally
                                {
                                    Interlocked.Exchange(ref InventoryCache.LoadAllItemsRunning, 0);
                                    // Interlocked.Exchange(ref InventoryCache.LoadAllItemsToTestFolder, 0);
                                }
                            }
                            else
                            {
                                Thread.Sleep(15000);
                            }
                        }
                    } // datetoStart

                    else
                    {
                        Thread.Sleep(15000);
                    }
                }//end while
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CAService rewrite error: " + ex.Message);
                log.Error(ex.Message);
            }
        }

        //private void RewriteCacheOLD()
        //{
        //    System.Threading.Thread.CurrentThread.CurrentCulture
        //       = System.Threading.Thread.CurrentThread.CurrentUICulture
        //         = System.Globalization.CultureInfo.GetCultureInfo("en-US");

        //    string path;
        //    string inventoryPath;
        //    string pathtemp;
        //    string messagefile;
        //    int dateresult = 1;
        //    bool datetoStart = true;
        //    string scheduleValue;
        //    DirectoryInfo di;
        //    LinnworksService lin;
        //    bool isLoading = false;
        //    try
        //    {
        //        log.Debug("Cache thread is started");
        //        DAL d = new DAL();
        //        while (1 == 1)
        //        {
        //            In case of error set thread to sleep
        //             check datatime of the cache
        //             if i then rewrite
        //             else sleep 15 min;

        //            dateresult = 1;
        //            datetoStart = true;
        //            GetPaths(out path, out inventoryPath, out pathtemp, out messagefile);
        //            log.Debug("Cache folder: " + path);

        //            read if app sent the message to update cache.

        //            if (!Directory.Exists(pathtemp))
        //            {
        //                Directory.CreateDirectory(pathtemp);
        //            }

        //            if (!File.Exists(messagefile))
        //            {
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }

        //                scheduleValue = d.GetSettingValue(SettingsConstant.Cache_Update_Interval);
        //                datetoStart = GetScheduleTime(scheduleValue);
        //            }
        //            else
        //            {
        //                log.Debug("App Message exists!");
        //            }

        //            if (datetoStart)
        //            {
        //                 load cache to temp folder
        //                if (Interlocked.CompareExchange(ref InventoryCache.LoadAllItemsToTestFolder, 1, 0) == 0)
        //                {
        //                    try
        //                    {
        //                        CreateMessagefile(messagefile);
        //                        lin = new LinnworksService();
        //                        lin.RunDownloadAllItems();

        //                        log.Debug("lin.RunDownloadAllItems() Complited");
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Interlocked.Exchange(ref InventoryCache.LoadAllItemsToTestFolder, 0);
        //                    }
        //                }
        //                log.Debug("Before LoadAllItemsRunning...");

        //                Interlocked.CompareExchange(ref InventoryCache.LoadAllItemsRunning, 1, 0);
        //                if (Interlocked.CompareExchange(ref InventoryCache.CacheReading, 0, 0) == 0)
        //                {
        //                    try
        //                    {
        //                         Move the file.
        //                        log.Debug("Movefile:" + path);
        //                        ClearFolder(path);
        //                        if (File.Exists(messagefile))
        //                            File.Delete(messagefile);
        //                        log.Debug("Clearpath :" + path);

        //                        DirectoryInfo dir = new DirectoryInfo(pathtemp);
        //                        foreach (FileInfo fi in dir.GetFiles())
        //                        {
        //                            File.Copy(fi.FullName, path + "\\" + fi.Name);
        //                            log.Debug("Copy  to :" + path + "\\" + fi.Name);
        //                        }
        //                        foreach (FileInfo fi in dir.GetFiles())
        //                        {
        //                            fi.Delete();
        //                        }
        //                        log.Debug("FINISH!!!");
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        log.Debug("Error while rewriting cache : " + ex.Message);
        //                    }
        //                    finally
        //                    {
        //                        Interlocked.Exchange(ref InventoryCache.LoadAllItemsRunning, 0);
        //                        Interlocked.Exchange(ref InventoryCache.LoadAllItemsToTestFolder, 0);
        //                    }

        //                }
        //                else
        //                {
        //                    log.Debug("Cache is in use. Waiting...60000");
        //                    Thread.Sleep(5000);
        //                }
        //            }
        //            else
        //            {
        //                log.Debug("Cache to sleep... 15 min");

        //                Thread.Sleep(5000); // 15 min
        //            }
        //        }//end while
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("CAService Error: " + ex.Message);
        //        log.Error(ex.Message);
        //    }
        //}

        private bool GetScheduleTime(string str)
        {
            bool ret = false;
            int day = (int)DateTime.Now.DayOfWeek;
            int hh = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int currentminutes = hh * 60 + minute;
            int scheduleminutes = -20;
            string[] rows = str.Split(',');
            string[] timerow;
            string[] time;
            foreach (string r in rows)
            {
                timerow = r.Split(';');
                if (day.ToString() == timerow[0])
                {
                    time = timerow[1].Split(':');
                    scheduleminutes = Convert.ToInt32(time[0]) * 60 + Convert.ToInt32(time[1]);
                    ret = Math.Abs(scheduleminutes - currentminutes) < 5;
                    if (ret)
                        break;
                }
            }
            return ret;

        }
        private void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }
        }

        private void CreateMessagefile(string filepath)
        {
            if (!System.IO.File.Exists(filepath))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(filepath))
                {
                    fs.WriteByte(1);
                }
            }
        }
    }//end class

}//end namespace
