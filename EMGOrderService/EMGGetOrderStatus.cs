using System;
using System.Collections.Generic;
using System.Text;
using ChannelAdvisor;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using log4net;
using log4net.Config;

namespace EMGOrderService
{
    public class EMGGetOrderStatus
    {
        public readonly ILog log = LogManager.GetLogger(typeof(EMGGetOrderStatus));

        private string _getOrderStatusURL = "";
        private string _customerID = "";

        private int _sleepTime;
        private DateTime _lastRunTime = DateTime.Now.Subtract(TimeSpan.FromDays(1));
        DAL dal = new DAL();

        int defaultSleepTime = 15; //to-change make it 15

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
        public EMGGetOrderStatus()
        {
            DAL dal = new DAL();
            _sleepTime = defaultSleepTime;

            //TO-DO
            //Get last run time 
            _lastRunTime = dal.GetLastEMGOrderStatusRunTime();

            if (_lastRunTime == null)
            {
                _lastRunTime = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            }//end if

            //Get other required values
            string shipToID = "";
            string sendOrderURL = "";
            string uomCode = "";
            string csvFile = "";
            string stoneedgeDBPath = "";
            dal.GetEMGOrderUpdateSettings(out _customerID,
                                            out shipToID,
                                            out sendOrderURL,
                                            out _getOrderStatusURL,
                                            out uomCode,
                                            out csvFile,
                                            out stoneedgeDBPath);

        }//end constructor


        #region Public Properties

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

        #region IsScheduled Methods


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
        /// Method that checks whether EMG Order Updater is scheduled
        /// at this time
        /// </summary>
        /// <returns></returns>
        public bool IsScheduled()
        {

            ////Check if auto update is disabled
            //if (dal.IsAutoUpdateDisabled())
            //{
            //    System.Diagnostics.Debug.WriteLine("Auto Update Disabled...");
            //    return profiles;
            //}


            if (IsWeekDay())
            {
                return IsTime();
            }

            return false;

        }//end method

        /// <summary>
        /// Check if current day is in frequency weekdays
        /// </summary>
        /// <returns></returns>
        private bool IsWeekDay()
        {
            DataSet dsWeekDays = new DataSet();

            //To DO
            dsWeekDays = dal.GetEMGFrequencyWeekDays((int)ServiceType.GetOrderStatus); //dal.GetEMGOrderUpdaterFreqWeekDays();

            //loop through datatable and find out whether current day
            //is in the weekday list
            if (dsWeekDays.Tables.Count > 0)
            {
                foreach (DataRow dr in dsWeekDays.Tables[0].Rows)
                {
                    if (DateTime.Today.DayOfWeek.ToString() == dr[0].ToString() && (bool)dr[1] == true)
                    {
                        return true;
                    }//end if
                }//end for each
            }//end if


            return false;
        }//end method


        /// <summary>
        /// Method to check whether its time to run EMG Order Updater
        /// </summary>
        /// <returns></returns>
        private bool IsTime()
        {
            DataSet dsTimes = new DataSet();

            //To-Do
            dsTimes = dal.GetEMGFrequencyTimes((int)ServiceType.GetOrderStatus); //dal.GetEMGOrderUpdaterFreqTimes();

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

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void GetStatus()
        {
            //Get pending orders
            DataTable dtPendingOrders = dal.GetPendingEMGOrders().Tables[0];

            if (dtPendingOrders.Rows.Count > 0)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Creating request XML for GetOrderStatus...");
                    //Create request xml
                    string requestXML = GetRequestXML(dtPendingOrders);
                    System.Diagnostics.Debug.WriteLine(requestXML);

                    System.Diagnostics.Debug.WriteLine("Sending GetOrderStatus request to EMG...");
                    //Send request to EMG
                    string result = GetOrderStatusFromEMG(requestXML);
                    System.Diagnostics.Debug.WriteLine(result);

                    //Get orders
                    List<OrderStatus> orderStatusList = GetOrderStatusList(result);
                    System.Diagnostics.Debug.WriteLine(orderStatusList.Count.ToString() + " orders recevied from GetOrderStatus...");

                    System.Diagnostics.Debug.WriteLine("Updating tracking nos to StoneEdge...");
                    //save to stone edge
                    UpdateStoneEdge(orderStatusList);
                    System.Diagnostics.Debug.WriteLine("StoneEdge Updated...");

                    System.Diagnostics.Debug.WriteLine("Saving information to logs...");
                    //save Order Status
                    dal.SaveEMGOrderStatusList(orderStatusList);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    throw ex;
                }

                

            }//end if

            this._lastRunTime = DateTime.Now;
            this._sleepTime = defaultSleepTime;//to-change make it 15 again
            
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderStatusList"></param>
        private void UpdateStoneEdge(List<OrderStatus> orderStatusList)
        {
            //loop order status
            for (int x = 0; x < orderStatusList.Count; x++)
            {
                if (orderStatusList[x].ShipReference.Trim() != "")
                {
                    System.Diagnostics.Debug.WriteLine("Updating Tracking No. -" + orderStatusList[x].ShipReference + " for Order - " + orderStatusList[x].OrderNo + " to StoneEdge");

                    bool isAccessUpdated = dal.UpdateTrackingNos(orderStatusList[x]);

                    if (isAccessUpdated)
                    {
                        //Set Order Status
                        orderStatusList[x].IsStoneEdgeUpdated = true;

                        dal.SaveEMGOrdersSent(orderStatusList[x].OrderNo, true);
                    }//end if
                }
                else if (orderStatusList[x].OrderStatusText.Trim() == "Order Cancelled")
                {
                    //if order is cancelled then set Complete to true
                    dal.SaveEMGOrdersSent(orderStatusList[x].OrderNo, true);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Tracking No. blank for Order - " + orderStatusList[x].OrderNo);
                }//end if
            }//end for
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private List<OrderStatus> GetOrderStatusList(string result)
        {
            using (StringReader reader = new StringReader(result))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);

                List<OrderStatus> orderStatusList = new List<OrderStatus>();

                //get node list
                XmlNodeList nodes = xmlDoc.SelectNodes("//getOrderStatus");

                foreach (XmlElement element in nodes)
                {
                    //XmlNodeList childNodes = element.SelectSingleNode(;

                    //if (childNodes.Count > 0)
                    //{
                    OrderStatus orderStatus = new OrderStatus();
                    orderStatus.OrderNo = element.SelectSingleNode("orderID").InnerText;
                    orderStatus.Status = element.SelectSingleNode("cmdStatus").InnerText;
                    orderStatus.ErrorMessage = element.SelectSingleNode("errMessage").InnerText;
                    orderStatus.OrderStatusText = element.SelectSingleNode("orderStatus").InnerText;
                    orderStatus.ShipReference = element.SelectSingleNode("orderShipReference").InnerText;
                    orderStatus.ShippingMethod = element.SelectSingleNode("orderShippingMethod").InnerText;
                    orderStatus.ShippingCost = float.Parse(element.SelectSingleNode("orderShippingCost").InnerText);
                    if (element.SelectSingleNode("orderShipDate").InnerText.Trim() != "")
                    {
                        orderStatus.ShipDate = Convert.ToDateTime(element.SelectSingleNode("orderShipDate").InnerText.Trim());
                    }
                    else
                    {
                        orderStatus.ShipDate = null;
                    }
                    orderStatus.NetAmount = float.Parse(element.SelectSingleNode("orderNetAmount").InnerText);
                    orderStatus.Payment = float.Parse(element.SelectSingleNode("orderPayment").InnerText);
                    if (element.SelectSingleNode("orderPaymentDate").InnerText.Trim() == "")
                    {
                        orderStatus.PaymentDate = null;
                    }
                    else
                    {
                        orderStatus.PaymentDate = Convert.ToDateTime(element.SelectSingleNode("orderPaymentDate").InnerText.Trim());
                    }//end if

                    orderStatusList.Add(orderStatus);
                    //}//end if
                }//end for each

                return orderStatusList;
            }//end using
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtPendingOrders"></param>
        /// <returns></returns>
        private string requestXML = "<getOrderStatusRequest><credentials>{credentials}</credentials>{Orders}</getOrderStatusRequest>";
        private string orderXML = "<orderID>{OrderNo}</orderID>";
        private string GetRequestXML(DataTable dtPendingOrders)
        {
            string tempRequestXML = requestXML;
            
            string orders="";
            //loop datatable and create orders
            foreach (DataRow dr in dtPendingOrders.Rows)
            {
                string tempOrderXML = orderXML;

                tempOrderXML = tempOrderXML.Replace("{OrderNo}", dr[0].ToString());

                orders += tempOrderXML;
            }//end for each

            //assign orders
            tempRequestXML = tempRequestXML.Replace("{Orders}", orders);

            //assign credentials
            tempRequestXML = tempRequestXML.Replace("{credentials}",_customerID);

            return tempRequestXML;
        }//end method

        #region Post Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestXML"></param>
        /// <returns></returns>
        private string GetOrderStatusFromEMG(string requestXML)
        {
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

            PostSubmitter post = new PostSubmitter();
            post.Url = _getOrderStatusURL;
            post.PostItems.Add("credentials", _customerID);
            post.PostItems.Add("data", requestXML);
            post.Type = PostSubmitter.PostTypeEnum.Post;
            string result = post.Post();

            return result;
        }//end method

        #endregion



    }//end class
}//end namespace
