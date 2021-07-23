using System;
using System.Collections.Generic;
using System.Text;
using ChannelAdvisor;
using System.Data;
using FileHelpers;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using EMGOrderService;

namespace EMGOrderService
{
    public class EMGOrderUpdater
    {
        //To-do
        private string _csvFile = "";//@"D:\Freelance\Bargains Delivered\Docs\Automate EMG orders\Test File\emgorders.csv";
        private string _sendOrderURL = "";//"https://www.ez-order-manager.com/om/lib/emg_dispatcher.php?cmd=sendOrder&format=xml&mode=test";
        private string _shipToID = "";//"107787";
        private string _customerID = "";//107773";
        private string _uomCode = "";//"EA";
        

        private DataTable _dtTransportCodes;

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
        public EMGOrderUpdater()
        {
            DAL dal = new DAL();
            _sleepTime = defaultSleepTime;

            //TO-DO
            //Get last run time 
            _lastRunTime = dal.GetLastEMGOrderUpdaterRunTime();

            if (_lastRunTime == null)
            {
                _lastRunTime = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            }//end if

            //Get other required values
            string getOrderStatusURL = "";
            string stoneedgeDBPath = "";
            dal.GetEMGOrderUpdateSettings(out _customerID,
                                                out _shipToID,
                                                out _sendOrderURL,
                                                out getOrderStatusURL,
                                                out _uomCode,
                                                out _csvFile,
                                                out stoneedgeDBPath);


            //Get transport codes
            _dtTransportCodes = dal.GetTransportCodes().Tables[0];

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
            dsWeekDays = dal.GetEMGFrequencyWeekDays((int)ServiceType.SendOrder); //dal.GetEMGOrderUpdaterFreqWeekDays();

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
            dsTimes = dal.GetEMGFrequencyTimes((int)ServiceType.SendOrder);//dal.GetEMGOrderUpdaterFreqTimes();

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
        public void UpdateEMG()
        {
            //Get Orders
            System.Diagnostics.Debug.WriteLine("Trying to read csv file...");
            FileOrder[] fileOrders = ReadFile();

            if (fileOrders == null)
            {
                System.Diagnostics.Debug.WriteLine("csv file does not exists...");
                return;
            }

            //Convert to EMG Orders
            System.Diagnostics.Debug.WriteLine("Creating EMG orders...");
            List<EMGOrder> emgOrders = ConvertToEMGOrders(fileOrders);
            System.Diagnostics.Debug.WriteLine(emgOrders.Count.ToString() + " EMG orders created...");

            //Generate guid for schedule
            Guid scheduleID = Guid.NewGuid();
            string requestXML = "";
            string result = "";
            string status = "";
            string errorMessage = "";

            //Save to schedule and no of orders in it
            System.Diagnostics.Debug.WriteLine("Saving schedule...");
            dal.SaveEMGOrderUpdateSchedule(scheduleID, emgOrders.Count);


            //loop EMG Orders and send order to EMG
            for (int x = 0; x < emgOrders.Count; x++)
            {
                //Generate xml for request
                System.Diagnostics.Debug.WriteLine("Creating request XML...");
                requestXML = GetOrderXML(emgOrders[x]);

                //get result
                System.Diagnostics.Debug.WriteLine("Sending order no. - " + emgOrders[x].OrderNo + " to emg");
                result = SendOrderToEMG(requestXML);

                //get status
                GetStatus(result, out status, out errorMessage);

                //Save status
                System.Diagnostics.Debug.WriteLine("Saving order status to EMG");
                dal.SaveEMGOrderUpdateStatus(scheduleID, emgOrders[x].OrderNo, status, errorMessage);
            }//end for

            //delete the file
            File.Delete(_csvFile);

            //--------end update code-----------------------
            this._lastRunTime = DateTime.Now;
            this._sleepTime = defaultSleepTime;//to-change make it 15 again

        }//end method

        #region Post Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestXML"></param>
        /// <returns></returns>
        private string SendOrderToEMG(string requestXML)
        {
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

            PostSubmitter post = new PostSubmitter();
            post.Url = _sendOrderURL;
            post.PostItems.Add("credentials", _customerID);
            post.PostItems.Add("data", requestXML);
            post.Type = PostSubmitter.PostTypeEnum.Post;
            string result = post.Post();

            return result;
        }//end method

        #endregion

        #region FileMethods


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FileOrder[] ReadFile()
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(FileOrder)); ;


            FileOrder[] fileOrders = File.Exists(_csvFile) ? (FileOrder[])engine.ReadFile(_csvFile) : null;

            return fileOrders;
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        private List<EMGOrder> ConvertToEMGOrders(FileOrder[] fileOrders)
        {
            List<EMGOrder> emgOrders = new List<EMGOrder>();

            bool isFound = false;
            //loop orders
            for (int x = 0; x < fileOrders.GetLength(0); x++)
            {
                isFound = false;
                //loop EMGOrders
                for (int x1 = 0; x1 < emgOrders.Count; x1++)
                {
                    if (emgOrders[x1].OrderNo == fileOrders[x].OrderNo)
                    {
                        isFound = true;
                        AddItemToEMGOrder(emgOrders[x1], fileOrders[x]);
                    }//end if

                }//end emgOrders loop

                if (!isFound)
                {
                    emgOrders.Add(CreateNewEMGOrder(fileOrders[x]));
                }//end if


            }//end for

            return emgOrders;
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private EMGOrder CreateNewEMGOrder(FileOrder order)
        {
            EMGOrder emgOrder = new EMGOrder();

            emgOrder.OrderNo = order.OrderNo;
            emgOrder.OrderDate = order.OrderDate;

            //Transport Codes
            GetTransportCodes(order.ShippingMethod,
                                out emgOrder.TransportID,
                                out emgOrder.TransportCode);

            emgOrder.DestinationName = order.Name;
            emgOrder.Street = order.Street;
            emgOrder.StreetSupplement = order.StreetSupplement;
            emgOrder.City = order.City;
            emgOrder.RegionCode = order.RegionCode;
            
            //Change Vinay - 05/04/09
            //Remove characters after dash
            int pos = order.PostalCode.IndexOf("-");
            if (pos > -1)
            {
                emgOrder.PostalCode = order.PostalCode.Substring(0, pos);
            }
            else
            {
                emgOrder.PostalCode = order.PostalCode;
            }
            
            //emgOrder.PostalCode = order.PostalCode;
            
            emgOrder.CountryCode = order.CountryCode;
            emgOrder.Email = "";
            emgOrder.Phone = order.Phone;

            EMGItem emgItem = new EMGItem(order.PartID, order.Quantity, _uomCode);
            emgOrder.Items.Add(emgItem);

            return emgOrder;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emgOrder"></param>
        /// <param name="order"></param>
        private void AddItemToEMGOrder(EMGOrder emgOrder, FileOrder order)
        {
            EMGItem emgItem = new EMGItem(order.PartID, order.Quantity, _uomCode);

            emgOrder.Items.Add(emgItem);
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippingMethod"></param>
        /// <param name="transportID"></param>
        /// <param name="transportCode"></param>
        private void GetTransportCodes(string shippingMethod,
                                        out string transportID,
                                        out string transportCode)
        {
            //to-do

            DataRow[] dr = _dtTransportCodes.Select("Code ='" + shippingMethod + "'");

            if (dr.GetLength(0) > 0)
            {
                transportID = dr[0]["CarrierID"].ToString();
                transportCode = dr[0]["TransportID"].ToString();
            }
            else
            {
                throw new Exception("Transport Codes not found for Shipping Code '" + shippingMethod + "'");
            }//end if

        }//end method

        #endregion

        #region XML Methods

        private string orderXML = "<sendOrderRequest><credentials>{Credentials}</credentials><buyer><CustomerId>{CustomerId}</CustomerId><ShiptoId>{ShiptoId}</ShiptoId></buyer><order><BuyerOrderNumber>{BuyerOrderNumber}</BuyerOrderNumber><OrderIssueDate>{OrderIssueDate}</OrderIssueDate><transport><TransportID>{TransportID}</TransportID><TransportIDCode>{TransportIDCode}</TransportIDCode></transport><destination><Name1>{Name1}</Name1><Street>{Street}</Street><StreetSupplement1>{StreetSupplement1}</StreetSupplement1><City>{City}</City><RegionCoded>{RegionCoded}</RegionCoded><PostalCode>{PostalCode}</PostalCode><CountryCoded>{CountryCoded}</CountryCoded><Email>{Email}</Email><Phone>{Phone}</Phone></destination><items>{Items}</items></order></sendOrderRequest>";
        private string orderItemXML = "<item><PartId>{PartId}</PartId><QuantityValue>{QuantityValue}</QuantityValue><UOMCoded>{UOMCoded}</UOMCoded></item>";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emgOrder"></param>
        /// <returns></returns>
        private string GetOrderXML(EMGOrder emgOrder)
        {
            string tempOrderXML = orderXML;

            //Assign values to placeholder
            tempOrderXML = tempOrderXML.Replace("{Credentials}", _customerID);
            tempOrderXML = tempOrderXML.Replace("{CustomerId}", _customerID);
            tempOrderXML = tempOrderXML.Replace("{ShiptoId}", _shipToID);
            tempOrderXML = tempOrderXML.Replace("{BuyerOrderNumber}", emgOrder.OrderNo);
            tempOrderXML = tempOrderXML.Replace("{OrderIssueDate}", emgOrder.OrderDate);
            tempOrderXML = tempOrderXML.Replace("{TransportID}", emgOrder.TransportID);
            tempOrderXML = tempOrderXML.Replace("{TransportIDCode}", emgOrder.TransportCode);
            tempOrderXML = tempOrderXML.Replace("{Name1}", emgOrder.DestinationName);
            tempOrderXML = tempOrderXML.Replace("{Street}", emgOrder.Street);
            tempOrderXML = tempOrderXML.Replace("{StreetSupplement1}", emgOrder.StreetSupplement);
            tempOrderXML = tempOrderXML.Replace("{City}", emgOrder.City);
            tempOrderXML = tempOrderXML.Replace("{RegionCoded}", emgOrder.RegionCode);
            tempOrderXML = tempOrderXML.Replace("{PostalCode}", emgOrder.PostalCode);
            tempOrderXML = tempOrderXML.Replace("{CountryCoded}", emgOrder.CountryCode);
            tempOrderXML = tempOrderXML.Replace("{Email}", "");
            tempOrderXML = tempOrderXML.Replace("{Phone}", emgOrder.Phone);

            string itemXML = "";
            string tempItemXML = "";
            //Create items xml
            //loop items
            for (int x = 0; x < emgOrder.Items.Count; x++)
            {
                tempItemXML = orderItemXML;
                tempItemXML = tempItemXML.Replace("{PartId}", emgOrder.Items[x].PartID);
                tempItemXML = tempItemXML.Replace("{QuantityValue}", emgOrder.Items[x].Quantity);
                tempItemXML = tempItemXML.Replace("{UOMCoded}", _uomCode);

                itemXML += tempItemXML;
            }//end for

            tempOrderXML = tempOrderXML.Replace("{Items}", itemXML);

            return tempOrderXML;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="status"></param>
        /// <param name="errorMessage"></param>
        private void GetStatus(string result,
                                out string status,
                                out string errorMessage)
        {

            status = "";
            errorMessage = "";

            //filter our uneeded stuff from result
            int position = result.IndexOf("<sendOrderResponse>");
            if (position != 0)
            {
                result = result.Substring(position);
            }//end if

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(result));

            XPathNavigator nav = xmlDoc.CreateNavigator();

            //Get Status
            XPathNodeIterator nodes = nav.Select("//sendStatus");

            if (nodes.MoveNext())
            {
                status = nodes.Current.Value;
            }//end if

            //Get Error Message
            nodes = nav.Select("//errorMessage");

            if (nodes.MoveNext())
            {
                errorMessage = nodes.Current.Value;
            }//end if


        }//end method

        #endregion


    }//end class

}//end namespace
