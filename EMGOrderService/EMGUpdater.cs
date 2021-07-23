using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Data.SqlClient;
using System.Threading;
using log4net;
using log4net.Config;

namespace EMGOrderService
{
    public partial class EMGUpdater : ServiceBase
    {
        public readonly ILog log = LogManager.GetLogger(typeof(EMGUpdater));

        /// <summary>
        /// 
        /// </summary>
        public EMGUpdater()
        {
            try
            {
                XmlConfigurator.Configure();
                Debug.WriteLine("Initializing");
                InitializeComponent();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("EMGUpdater Error: " + ex.Message);
                log.Error(ex.Message);
            }
        }//end constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            Debug.WriteLine("Service started...");
            int x = 1;

            while (x == 1)
            {
                try
                {
                    System.Diagnostics.Debug.Write("Trying to load Order and Tracking services...");
                    StartServices();
                    x = 0;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write("EMGUpdater Error: " + ex.Message);
                    System.Diagnostics.Debug.Write("Retrying after 2 minutes");
                    log.Error(ex.Message + ". Retrying after 2 minutes");
                    Thread.Sleep(TimeSpan.FromMinutes(2));
                }//end try catch
            }//end while

        }//end event


        /// <summary>
        /// Method that would try to start OrderUpdate & Tracking objects
        /// </summary>
        private void StartServices()
        {
            Thread thrdOrderUpdate = new Thread(new ThreadStart(ExecuteEMGOrderUpdater));
            thrdOrderUpdate.Start();

            Thread thrdOrderStatus = new Thread(new ThreadStart(ExecuteEMGGetOrderStatus));
            thrdOrderStatus.Start();

        }//end method

        /// <summary>
        /// 
        /// </summary>
        private void ExecuteEMGOrderUpdater()
        {
            try
            {
                EMGOrderUpdater emgOrderUpdater = new EMGOrderUpdater();
                System.Diagnostics.Debug.WriteLine("Trying to start EMG Order Updater");
                while (true)
                {
                    if (emgOrderUpdater.IsScheduled())
                    {
                        System.Diagnostics.Debug.WriteLine("EMG Order Updater Executing...");

                        //In case of error set thread to sleep
                        try
                        {
                            emgOrderUpdater.UpdateEMG();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message + ". Retrying after " + emgOrderUpdater.SleepTime.ToString() + " minutes.");
                            log.Error(ex.Message);
                        }//end try/catch

                    }//end if

                    System.Diagnostics.Debug.WriteLine("EMG Order Updater will run again in " + emgOrderUpdater.SleepTime.ToString() + " minutes.");
                    Thread.Sleep(emgOrderUpdater.GetSleepTime());

                }//end while
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("EMGUpdater Error: " + ex.Message);
                log.Error(ex.Message);
            }
        }//end method


        /// <summary>
        /// 
        /// </summary>
        private void ExecuteEMGGetOrderStatus()
        {
            try
            {
                EMGGetOrderStatus emgOrderStatus = new EMGGetOrderStatus();
                System.Diagnostics.Debug.WriteLine("Trying to start EMG Order Status");
                while (1 == 1)
                {
                    if (emgOrderStatus.IsScheduled())
                    {
                        System.Diagnostics.Debug.WriteLine("EMG Order Status Executing...");

                        //In case of error set thread to sleep
                        try
                        {
                            emgOrderStatus.GetStatus();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message + ". Retrying after " + emgOrderStatus.SleepTime.ToString() + " minutes.");
                            log.Error(ex.Message);
                        }//end try/catch

                    }//end if

                    System.Diagnostics.Debug.WriteLine("EMG Order Status will run again in " + emgOrderStatus.SleepTime.ToString() + " minutes.");
                    Thread.Sleep(emgOrderStatus.GetSleepTime());

                }//end while
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("EMG Order Status Error: " + ex.Message);
                log.Error(ex.Message);
            }
        }//end method

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStop()
        {
            Debug.WriteLine("Service stopped...");
        }//end event

    }//end class

}//end namespace
