using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Configuration;

namespace ChannelAdvisor
{
    //public static class excludeServices
    //{
    //    public const string mediumFRB = "USPS Priority Mail - Medium Flat Rate Box";
    //    public const string SmallFRB = "USPS Priority Mail - Small Flat Rate Box";
    //    public const string LageFRB = "USPS Priority Mail - Large Flat Rate Box";
    //    public const string FlatRateEnv = "USPS Priority Mail - Flat Rate Envelope";
    //    public const string FlatRatePaddedEnv = "USPS Priority Mail - Flat Rate Padded Envelope";
    //    public const string RegionalRateBoxA = "USPS Priority Mail - Regional Rate Box A";
    //    public const string RegionalRateBoxB = "USPS Priority Mail - Regional Rate Box B";
    //    public const string LegalFlatRateEnv = "USPS Priority Mail - Legal Flat Rate Envelope";
    //    public const string ExpressPackage = "USPS Priority Mail Express - Package";
    //    public const string ExpressFlatRateEnv = "USPS Priority Mail Express - Flat Rate Envelope";
    //    public const string ExpressFlatRatePaddedEnv = "USPS Priority Mail Express - Flat Rate Padded Envelope";
    //    public const string ExpressLegalFlatRateEnv = "USPS Priority Mail Express - Legal Flat Rate Envelope";
    //}


    public static class includeServices
    {
        public const string uspsPriorityMail = "usps_priority_mail";
        public const string uspsFirstClassmail = "usps_first_class_mail";
        public const string uspsParcelSelect = "usps_parcel_select";
        public const string uspGround = "ups_ground";
        public const string uspNextDayAirSaver = "ups_next_day_air_saver";

    }

    public  class ShipStationAPIs
    {
        public readonly ILog log = LogManager.GetLogger(typeof(ShipStationAPIs));
        
        public string DefaultCarrier { get; set; }
        public string DefaultShipFrom { get; set; }
        public string DefaultShipTo{ get; set; }

        public string OutputFileName = "";

        //public string Sku { get; set; }

        private string strUrl = "";
        private string strUrlcarriers = "https://ssapi.shipstation.com/carriers";
        private string strUrlservices = "https://ssapi.shipstation.com/carriers/listservices?carrierCode=";

        private string username = "";
        private string password = "";

        // templates to prepare data to send POST Request
        private string contentTemplate = "{  \"carrierCode\": \"TempcarrierCode\",  \"serviceCode\": TempserviceCode,  \"packageCode\": TemppackageCode,  \"fromPostalCode\": \"TempfromPostalCode\",  \"toState\": TemptoState,  \"toCountry\": TemptoCountry,  \"toPostalCode\": \"TemptoPostalCode\",  \"toCity\": TemptoCity,  \"weight\": {    \"value\": Tempvalue,    \"units\": \"TempWunits\"  },  [DM]  \"confirmation\": Tempconfirmation,  \"residential\": Tempresidential}";
        private string dimentionTemplate = "\"dimensions\": {    \"units\": \"TempDunits\",    \"length\": Templength,    \"width\": Temwidth,    \"height\": Tempheight  },";
       
        //init variables. In the future we can do it as setting variable.
        private string weightunit = "pound";
        private string dimunit = "inches";
        private string country = "\"US\"";
        public string confirmation = "delivery";
        public string conf_signature = "signature";
        public string conf_delivery = "delivery";

        public string residential = "true";
        public string shippingFolder = "";
        private RateParameters currentRP;
        private string postData = "";
        private string dimentionData = "";
        private double upsInsurance = 0;
        private double stampInsurance = 0;

        private double priorityMail = 0;
        private double firstClassMail = 0;
        private double parcelSelectMail = 0;
        private double uspsMarkup1 = 0;
        private double uspsMarkupParcel = 0;
        private double uspsMarkupPriority = 0;
        private double requiredSignatureAt = 0;

        // ftp 
        private string localFolder;
        private string inputFilename;

        /// <summary>
        /// Constructor init GenerateParameters
        /// </summary>
        /// <param name="message"></param>
        public  ShipStationAPIs()
        {
            InitCredentials();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        }//end method

        /// <summary>
        /// method to call GetRate API
        /// </summary>
        /// <param name="message"></param>
        /// <returns>string in Json format</returns>
        private string ReadAPI_GetRate()
        {
            try
            {
                HttpWebResponse LoginRes;
                StreamReader sr;
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(strUrl);

                // MzAxOGVlY2MxODQ2NDQ5OTgzOTcwNTRkNTM0NTczMjc6YTBjZDgzZmFiZmZlNGQzNmEyOWI1MGNkYmUxYTBkMDc=

                byte[] psw = Encoding.UTF8.GetBytes(username + ":" + password);
                String encoded = Convert.ToBase64String(psw);

                string postData = GenerateParameters(currentRP);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                string remaining;
                string reset;
                objRequest.Method = "POST";
                // objRequest.Host = "https://ssapi.shipstation.com";
                objRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0";
                objRequest.Accept = "application/json, text/plain, */*";
                objRequest.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                objRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                // objRequest.Headers.Add("Origin", "http://www.cse.lk");
                objRequest.Headers.Add("Authorization", "Basic " + encoded);
                objRequest.ContentType = "application/json;charset=utf-8";
                //objRequest.Referer = "http://www.cse.lk/home/announcement-details/";
                objRequest.KeepAlive = true;
                objRequest.ContentLength = byteArray.Length;

                // Get the request stream.
                using (Stream dataStream = objRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                LoginRes = (HttpWebResponse)objRequest.GetResponse();
                if (LoginRes.StatusCode == HttpStatusCode.OK)
                {
                    remaining = LoginRes.Headers["X-Rate-Limit-Remaining"];
                    reset = LoginRes.Headers["x-rate-limit-reset"];

                    if (Convert.ToInt32(remaining) < 1)
                        System.Threading.Thread.Sleep(Convert.ToInt32(reset) * 1000);

                    sr = new StreamReader(LoginRes.GetResponseStream());
                    string ReadWebsite_JSON = sr.ReadToEnd();
                    sr.Close();
                    sr = null;
                    LoginRes.Close();

                    LoginRes = null;
                    objRequest = null;
                    return ReadWebsite_JSON.Replace("®", "");
                }
                else
                {
                    log.Error("API retuns status code: " + LoginRes.StatusCode.ToString() + " Post data: "+ postData);
                    return "";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message +" Post data: " + postData);
                return "";
            }

        }

        /// <summary>
        /// method to get List of Carriers
        /// </summary>
        /// <param name="message"></param>
        /// <returns>string of Json </returns>
        public string ReadAPI_ListCarriers()
        {
            try
            {
                HttpWebResponse LoginRes;
                StreamReader sr;
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(strUrlcarriers);

                byte[] psw = Encoding.UTF8.GetBytes(username + ":" + password);
                String encoded = Convert.ToBase64String(psw);

                objRequest.Method = "GET";
                objRequest.Headers.Add("Authorization", "Basic " + encoded);
                objRequest.ContentType = "text/html";
                //"application/json;charset=utf-8";
                objRequest.KeepAlive = true;

                LoginRes = (HttpWebResponse)objRequest.GetResponse();
                sr = new StreamReader(LoginRes.GetResponseStream());
                string ReadWebsite_JSON = sr.ReadToEnd();
                sr.Close();
                sr = null;
                LoginRes.Close();
                LoginRes = null;
                objRequest = null;
                // TODO: Exit Function: Warning!!! Need to return the value
                return ReadWebsite_JSON;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return "";
            }

        }

        /// <summary>
        /// method to get List of Carriers
        /// </summary>
        /// <param name="message"></param>
        /// <returns>string of Json </returns>
        public string ReadAPI_ListServices(string carrier)
        {
            try
            {
                HttpWebResponse LoginRes;
                StreamReader sr;
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(strUrlservices + carrier);

                byte[] psw = Encoding.UTF8.GetBytes(username + ":" + password);
                String encoded = Convert.ToBase64String(psw);

                objRequest.Method = "GET";
                objRequest.Headers.Add("Authorization", "Basic " + encoded);
                objRequest.ContentType = "text/html";
                //"application/json;charset=utf-8";
                objRequest.KeepAlive = true;

                LoginRes = (HttpWebResponse)objRequest.GetResponse();
                sr = new StreamReader(LoginRes.GetResponseStream());
                string ReadWebsite_JSON = sr.ReadToEnd();
                sr.Close();
                sr = null;
                LoginRes.Close();
                LoginRes = null;
                objRequest = null;
                // TODO: Exit Function: Warning!!! Need to return the value
                return ReadWebsite_JSON;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return "";
            }

        }

        /// <summary>
        /// Generate RateParameters object for Get rate and display parameters 
        /// </summary>
        /// <param name="rp">object of RateParameters type </param>
        /// <returns>string of parameters for Post Reques</returns>
        private string GenerateParameters(RateParameters rp)
        {
            //carrierCode
            string postData = contentTemplate.Replace("TempcarrierCode", rp.carrierCode);
            
            postData = postData.Replace("TempserviceCode", rp.serviceCode=="null"? rp.serviceCode : "\""+rp.serviceCode +"\"");
            postData = postData.Replace("TemppackageCode", rp.packageCode == "null" ? rp.packageCode : "\"" + rp.packageCode + "\"");
            //get TempfromPostalCode
            postData = postData.Replace("TempfromPostalCode", rp.fromPostalCode);
            postData = postData.Replace("TemptoState", rp.toState == "null" ? rp.toState : "\"" + rp.toState + "\"");
            postData = postData.Replace("TemptoCountry", rp.toCountry);
            //get TemptoPostalCode
            postData = postData.Replace("TemptoPostalCode", rp.toPostalCode);
            postData = postData.Replace("TemptoCity", rp.toCity == "null" ? rp.toCity : "\"" + rp.toCity + "\"");
            //get weight value
            postData = postData.Replace("Tempvalue", rp.wvalue);
            //get weight units
            postData = postData.Replace("TempWunits", rp.wunits);


            // get dimmention units
            if (rp.dlength != "0" && rp.dwidth != "0" && rp.dheight != "0")
            {
                dimentionData = dimentionTemplate.Replace("TempDunits", rp.dunits);
                dimentionData = dimentionData.Replace("Templength", rp.dlength);
                dimentionData = dimentionData.Replace("Temwidth", rp.dwidth);
                dimentionData = dimentionData.Replace("Tempheight", rp.dheight);
                postData = postData.Replace("[DM]", dimentionData);
            }
            else
                postData = postData.Replace("[DM]", "");

            postData = postData.Replace("Tempconfirmation", rp.confirmation == "null" ? rp.confirmation : "\"" + rp.confirmation + "\"");
            postData = postData.Replace("Tempresidential", rp.residential);
            return postData;
        }

        /// <summary>
        /// method to Get an object of Ratearameers type
        /// </summary>
        /// <param name="message"></param>
        /// <returns>string in Json format</returns>
        /// 
        public RateParameters GetRateParametersObject(string[] row )
        {
            // get weight and dimention
  
            RateParameters rp = new RateParameters();
     
            rp.carrierCode = DefaultCarrier;
            rp.serviceCode = "null";
            rp.packageCode = "null";
            rp.fromPostalCode = DefaultShipFrom ;
            rp.toState = "null";
            rp.toCountry = country;
            rp.toPostalCode = DefaultShipTo ;
            rp.toCity = "null";
            rp.wvalue = row[1];
            rp.wunits = weightunit ;

            rp.dunits = dimunit;
            if ((string.IsNullOrWhiteSpace(row[2])) || (string.IsNullOrWhiteSpace(row[3])) || (string.IsNullOrWhiteSpace(row[4])))
            {
                rp.dlength = "0";
                rp.dwidth = "0";
                rp.dheight = "0";
            }
            else
            {
                rp.dlength = row[2];
                rp.dwidth = row[3];
                rp.dheight = row[4];
            }
            rp.confirmation = confirmation;
            rp.residential = residential;

            return rp;
        }


        /// <summary>
        /// method to Get list of RateToOutput objects
        /// 1 read csv file with list of SKUs and dimentions
        /// 2 read carriers list (call API)
        /// for each sku   
        ///  and for each carriers
        ///   call GetRateAPI
        ///   transform columns(services) to rows
        ///   calculate the best Cost
        /// </summary>
        /// <param name="message"></param>
        /// <returns>list of RateToOutput</returns>
        /// 
        public List<RateToOutput> GetRateListOutput()
        {
            try
            {
                List<RateToOutput> RateToOutput = new List<RateToOutput>();

                // read selected services into sring
                DAL dal = new DAL();
                string strServices = "";
                dal.GetServicesString(out strServices);

                List<Rate> listRate = new List<Rate>();
                List<Rate> tempList = new List<Rate>();

                //read csv with weight and dimentions from ftp 
                string path = GetFTPData();

                // parce csv file with dimmenion and weight
                //format: sku,weight,length,width,height,purchase price,Existing Domestic Shipping
                Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
                StreamReader sr = new StreamReader(path, encoding);

                //read carriers list. call API
                List<Carrier> listCarrier = JsonConvert.DeserializeObject<List<Carrier>>(ReadAPI_ListCarriers());

                string strheader = sr.ReadLine();
                // read headers
                string[] headers = strheader.Replace("\"", "").Split(',');
                string[] row;
                List<RateToOutput> templistToOutput = new List<RateToOutput>();
               
                //RateToDisplay rtd;
                RateToOutput outrtd;
                double bestCost = 0;
                string bestService = "";
                double tempSum = 0;
                double insuranceCost = 0;
                double costWithuspsMarkup = 0;
               
                string line = "";

                while (!sr.EndOfStream) // parce SKUs with dimentions
                {
                    //format: 0 sku, 1 weight, 2 length, 3 width, 4 height, 5 purchase price, 6 Existing Domestic Shipping

                    line = sr.ReadLine();
                    row = line.Split(',');
                    if (row.Length == 7 && row[1]!="0")
                    {
                        bestCost = 1000000;
                        double row5 = 0;
                        double row6 = 0;
                        row5 = string.IsNullOrWhiteSpace(row[5]) ? 0 : Convert.ToDouble(row[5]);
                        row6 = string.IsNullOrWhiteSpace(row[6]) ? 0 : Convert.ToDouble(row[6]);
                  
                        //if we enter 150, then when purchase price 150 or 
                        //above it does the confirmation type Signature instead of Delivery or Online

                        if (row5 >= requiredSignatureAt)
                            confirmation = conf_signature;
                        else
                            confirmation = conf_delivery;

                        currentRP = GetRateParametersObject(row);
                       
                        // new RateToOutput
                        outrtd = new RateToOutput();
                        outrtd.sku = row[0];
                        outrtd.purchasePrice = row5;
                        outrtd.existingDomesticShiping = row6;
                        //outrtd.shipmentCost = costWithuspsMarkup;
                        //outrtd.otherCost = r.OtherCost;

                        string apiresult = "";
                        foreach (Carrier carr in listCarrier)
                        {
                            currentRP.carrierCode = carr.code;
                            apiresult = ReadAPI_GetRate();
                            if (apiresult.Length > 0)
                                tempList = JsonConvert.DeserializeObject<List<Rate>>(apiresult);
                            else
                                tempList.Clear();

                            foreach (Rate r in tempList)
                            {
                                if ((carr.code == "stamps_com" && r.ServiceName.Contains("- Package") && strServices.Contains(r.ServiceCode + ",")) || (carr.code != "stamps_com" && strServices.Contains(r.ServiceCode + ",")))
                                {
                                    if (carr.code == "stamps_com")
                                    {
                                        switch (r.ServiceCode)
                                        {
                                            case includeServices.uspsFirstClassmail:
                                                costWithuspsMarkup = r.ShipmentCost * uspsMarkup1;
                                                break;
                                            case includeServices.uspsParcelSelect:
                                                costWithuspsMarkup = r.ShipmentCost * uspsMarkupParcel;
                                                break;
                                            case includeServices.uspsPriorityMail:
                                                costWithuspsMarkup = r.ShipmentCost * uspsMarkupPriority;
                                                break;
                                        }
                                        
                                    }
                                    else
                                        costWithuspsMarkup = r.ShipmentCost;

                                    tempSum = 0;
                                    ////row5 = string.IsNullOrWhiteSpace(row[5]) ? 0 : Convert.ToDouble(row[5]);
                                    ////row6 = string.IsNullOrWhiteSpace(row[6]) ? 0 : Convert.ToDouble(row[6]);

                                    insuranceCost = CalculateInsuranceCost(carr.code,r.ServiceCode, row5);
                                    //tempSum = Math.Round((r.ShipmentCost + r.OtherCost + insuranceCost), 2);
                                    tempSum = Math.Round((costWithuspsMarkup + r.OtherCost + insuranceCost), 2);

                                    if (bestCost > tempSum)
                                    {
                                        bestCost = tempSum;
                                        bestService = r.ServiceName.Replace("- Package","").Trim() ;
                                        if (confirmation == conf_signature)
                                            bestService += " Signature";
                                        // signature
                                    }

                                    switch (r.ServiceCode)
                                    {
                                        case includeServices.uspGround:
                                            outrtd.UPSGround = tempSum;
                                            outrtd.Groundinsurance = insuranceCost;
                                            break;
                                        case includeServices.uspNextDayAirSaver:
                                            outrtd.UPSNextDayAirSaver = tempSum;
                                            outrtd.NextDayAirSaverinsurance = insuranceCost;
                                            break;
                                        case includeServices.uspsFirstClassmail:
                                            outrtd.USPSFirstClassMail = tempSum;
                                            outrtd.FirstClassMailinsurance = insuranceCost;
                                            break;
                                        case includeServices.uspsParcelSelect:
                                            outrtd.USPSParcelSelect = tempSum;
                                            outrtd.ParcelSelectinsurance = insuranceCost;
                                            break;
                                        case includeServices.uspsPriorityMail:
                                            outrtd.USPSPriorityMail = tempSum;
                                            outrtd.PriorityMailinsurance = insuranceCost;
                                            //outrtd.shipmentCost = r.ShipmentCost;

                                            break;

                                    }
                                }
                            }

                            //listToDislay.AddRange(templistToDislay);

                        }
                        //lisst.Add(out object)
                        if (!string.IsNullOrEmpty(outrtd.sku))
                        {
                            outrtd.BestRate = bestCost;
                            outrtd.BestService = bestService;
                            RateToOutput.Add(outrtd);
                        }

                    }//e
                    else
                    {
                        log.Error("Csv string is not properly formatted: " + line);
                    }
                }
                sr.Close();
                sr.Dispose();

                return RateToOutput;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Calculate service cost
        /// </summary>
        /// <param name="message"></param>
        /// <returns>double or 0 </returns>
        /// 
        public double CalculateInsuranceCost(string carrierCode, string serviceCode, double price)
        {
            double result = 0;
            double uspsPrice = 0;
            if (carrierCode == "ups" || carrierCode == "fedex" )
            { 
                if (price>100)
                    result = (Math.Ceiling( price/100)-1)* upsInsurance;
            }
            else
            {
                if (serviceCode == includeServices.uspsPriorityMail)
                    uspsPrice = priorityMail;
                else if (serviceCode == includeServices.uspsFirstClassmail)
                    uspsPrice = firstClassMail;
                else if (serviceCode == includeServices.uspsParcelSelect)
                    uspsPrice = parcelSelectMail;
                else
                    uspsPrice = 50;

                if (price > uspsPrice)
                {
                    result=1 +(Math.Ceiling( price/100)-1)*stampInsurance;
                }
            }
            return Math.Round(result,2);
        }

        /// <summary>
        /// method to call GetRate API by search arameers
        /// </summary>
        /// <param name="message"></param>
        /// <returns>List of objects of Rate type</returns>
        public List<Rate> SearchRateListByParams (RateParameters rp)
        {
            List<Rate> listRate = new List<Rate>();   
            currentRP = rp;
            listRate = JsonConvert.DeserializeObject<List<Rate>>(ReadAPI_GetRate());
            return listRate;
        }

        private void InitCredentials()
        {
            strUrl = ConfigurationManager.AppSettings["apiurl"].ToString();
            username = ConfigurationManager.AppSettings["user"].ToString();
            password = ConfigurationManager.AppSettings["secret"].ToString();
            DefaultCarrier = "ups";
            InitProperties();
           
        }//end method

        ///// <summary>
        ///// Method to Read csv file from 
        ///// </summary>
        private string GetFTPData()
        {
            string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
            string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
            string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            localFolder = Path.GetDirectoryName(location) + "\\Temp\\";

            FTP ftp = new FTP("ftp://" + ftpAddress + "/" +shippingFolder, username, password, true);
           
            // download csv file
            ftp.DownloadFile(inputFilename, localFolder);

            return string.Format(@"{0}{1}", localFolder, inputFilename);
        }

        /// <summary>
        /// Convert list of objects to csv and Upload on FTp
        /// </summary>
        /// <param name="list"> List of object (RateToOutput type)</param>
        /// <param name="csvName">name of csv file</param>
        /// <returns>string in Json format</returns>
        public bool UploadRateCsvFile(List<RateToOutput> list, string csvName)
        {
            try
            {
                string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
                string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
                string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

                var sb = new StringBuilder();
                sb.AppendLine("SKU,PurchasePrice ,Existing Domestic Shipping,USPS Priority Mail,Priority Mail Insurance,USPS Parcel Select, Parcel Select Insurance, USPS First Class, First Class Insurance,UPS Ground,Ground insurance,UPS Next Day Air Saver,Next Day Air Saver Insurance,Best Service,Best Rate");

                foreach (var data in list)
                {
                    sb.AppendLine(data.sku + "," + data.purchasePrice + "," + data.existingDomesticShiping + "," + data.USPSPriorityMail + "," + data.PriorityMailinsurance + "," + data.USPSParcelSelect + "," + data.ParcelSelectinsurance + "," + data.USPSFirstClassMail + "," + data.FirstClassMailinsurance + "," + data.UPSGround + "," + data.Groundinsurance + "," + data.UPSNextDayAirSaver + "," + data.NextDayAirSaverinsurance + "," + data.BestService + "," + data.BestRate);
                }

                byte[] buffer = Encoding.Default.GetBytes(sb.ToString());
                WebRequest request = WebRequest.Create("ftp://" + ftpAddress + "/" + csvName); // "AZ_UpdatedColumns.csv");
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();

                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Read setting value of from/to postal codes from DB
        /// </summary>
        private bool InitProperties()
        {
            string fromZip = "";
            string toZip = "";
            string _stamInsurance = "";
            string _upsInsurance = "";
            string _priorityMail = "";
            string _firstClassMail = "";
            string _parcelSelectMail = "";
            string _uspsMarkup1 = "";
            string _uspsMarkupParcel = "";
            string _uspsMarkupPriority = "";
            string _requiredSignatureAt = "";

            new DAL().GetShipstationInfo(out fromZip, out toZip, out _stamInsurance, out _upsInsurance, out inputFilename, out OutputFileName, out _priorityMail, out _firstClassMail, out _parcelSelectMail, out _uspsMarkup1, out _uspsMarkupParcel, out _uspsMarkupPriority, out _requiredSignatureAt);

            if (string.IsNullOrWhiteSpace(fromZip) || string.IsNullOrWhiteSpace(toZip) ||  string.IsNullOrWhiteSpace(inputFilename) || string.IsNullOrWhiteSpace(OutputFileName) ||
                        string.IsNullOrWhiteSpace(_priorityMail) || string.IsNullOrWhiteSpace(_firstClassMail) || string.IsNullOrWhiteSpace(_parcelSelectMail))
                return false;

            DefaultShipFrom = fromZip;
            DefaultShipTo = toZip;

            int ind = inputFilename.LastIndexOf("/");
            shippingFolder = inputFilename.Substring(0, ind + 1);
            inputFilename = inputFilename.Substring(ind + 1);
            

            stampInsurance = string.IsNullOrWhiteSpace(_stamInsurance) ?  0 : Convert.ToDouble(_stamInsurance);
            upsInsurance = string.IsNullOrWhiteSpace(_upsInsurance) ? 0:Convert.ToDouble(_upsInsurance);

            priorityMail = string.IsNullOrWhiteSpace(_priorityMail) ? 0 : Convert.ToDouble(_priorityMail);
            firstClassMail = string.IsNullOrWhiteSpace(_firstClassMail) ? 0 : Convert.ToDouble(_firstClassMail);
            parcelSelectMail = string.IsNullOrWhiteSpace(_parcelSelectMail) ? 0 : Convert.ToDouble(_parcelSelectMail);
            uspsMarkup1 = string.IsNullOrWhiteSpace(_uspsMarkup1) ? 1 : Convert.ToDouble(_uspsMarkup1)/100;
            uspsMarkupParcel = string.IsNullOrWhiteSpace(_uspsMarkupParcel) ? 1 : Convert.ToDouble(_uspsMarkupParcel) / 100;
            uspsMarkupPriority = string.IsNullOrWhiteSpace(_uspsMarkupPriority) ? 1 : Convert.ToDouble(_uspsMarkupPriority) / 100;
            requiredSignatureAt = string.IsNullOrWhiteSpace(_requiredSignatureAt) ? 0 : Convert.ToDouble(_requiredSignatureAt);

            return true;

        }//end method     

    }//end class

}//end namespace
