using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Configuration;
using Newtonsoft.Json.Linq;
using ChannelAdvisor.Models;

namespace ChannelAdvisor
{

    public  class LWAPIs
    {
        private CookieContainer cookieContainer = new CookieContainer();
        public readonly ILog log = LogManager.GetLogger(typeof(ShipStationAPIs));

        private string strHost = "";
        private string strUrlGetOrder = "/api/Orders/GetOrder";
        private string strUrlGetOrderByNumOrderId = "/api/Orders/GetOrderDetailsByNumOrderId?token=";
        private string strUrlLocationData = "/api/Dashboards/GetInventoryLocationData?token=";
        private string strUrlInventoryItemPrices = "/api/Inventory/GetInventoryItemPrices?token=";
        private string strUrlInventoryItemExtendedProperties = "/api/Inventory/GetInventoryItemExtendedProperties?token=";

        private string apiKey = "";

        private string applicationId = "";
        private string applicationSecret = "";
        private string token = "";

        // ftp 
        private string localFolder;
        private string inputFilename;

        /// <summary>
        /// Constructor init GenerateParameters
        /// </summary>
        /// <param name="message"></param>
        public LWAPIs()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            initParameters();
            AuthorizeByApplication();
        }//end method

        private void initParameters()
        {
            DAL dal = new DAL(); 
            dal.GetLWAPIInfo(out applicationId, out applicationSecret, out token);          
        }

        /// <summary>
        /// method to Authorize By Application credentials
        /// </summary>
        /// <returns>string in session token</returns>
        public void AuthorizeByApplication()
        {
            string postData = "applicationId=" + applicationId +"&applicationSecret=" + applicationSecret + "&token=" + token;
            try
            {
                HttpWebResponse LoginRes;
                StreamReader sr;
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create("https://api.linnworks.net//api/Auth/AuthorizeByApplication");

                byte[] psw = Encoding.UTF8.GetBytes(apiKey);
                String encoded = Convert.ToBase64String(psw);

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                objRequest.Method = "POST";
                objRequest.CookieContainer = cookieContainer;
                objRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0";
                objRequest.Accept = "application/json, text/plain, */*";
                objRequest.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                objRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                //objRequest.Headers.Add("Authorization", encoded);
                objRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

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
    
                    this.cookieContainer = objRequest.CookieContainer;
                    sr = new StreamReader(LoginRes.GetResponseStream());
                    string ReadWebsite_JSON = sr.ReadToEnd();
                    sr.Close();
                    sr = null;
                    LoginRes.Close();

                    LoginRes = null;
                    objRequest = null;

                    JObject jObj = JObject.Parse(ReadWebsite_JSON);                 // Parse the object and get session token and host
                    apiKey = jObj["Token"].ToString();
                    strHost = jObj["Server"].ToString();
                    
                }
                else
                {
                    log.Error("API retuns status code: " + LoginRes.StatusCode.ToString() + " Post data: " + postData);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " Post data: " + postData);
            }

        }

        public string GetOrderByNumericID(string ordernumber)
        {
            string ss = strHost + strUrlGetOrderByNumOrderId + apiKey + "&OrderId=" + ordernumber;
            try
            {
                HttpWebResponse LoginRes;
                StreamReader sr;
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(strHost + strUrlGetOrderByNumOrderId + apiKey + "&OrderId=" + ordernumber);

                byte[] psw = Encoding.UTF8.GetBytes(apiKey);
                String encoded = Convert.ToBase64String(psw);

                objRequest.Method = "GET";
                //objRequest.Headers.Add("Authorization", "token " + encoded);
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
                if (string.IsNullOrWhiteSpace(ReadWebsite_JSON) || string.IsNullOrEmpty(ReadWebsite_JSON) || ReadWebsite_JSON=="null")
                {
                    //test
                    string sad = "";
                }
                return ReadWebsite_JSON;
            }
            catch (Exception ex)
            {
                log.Error("URL " + ss + " " + ex.Message);
                return "";
            }

        }

        /// <summary>
        /// method to GetInventoryLocationData
        /// </summary>
        /// <returns>string of Json </returns>
        public string GetInventoryLocationData()
        {
            try
            {
                HttpWebResponse LoginRes;
                StreamReader sr;
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(strHost + strUrlLocationData + apiKey);

                byte[] psw = Encoding.UTF8.GetBytes(apiKey);
                String encoded = Convert.ToBase64String(psw);

                objRequest.Method = "GET";
                //objRequest.Headers.Add("Authorization", "token " + encoded);
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
        ///  Get InventoryItemPrices
        /// </summary>
        /// <returns>string of Json </returns>
        public string GetInventoryItemPrices(string itemId)
        {
            string url = "";
            try
            {
                HttpWebResponse LoginRes;
                StreamReader sr;
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(strHost + strUrlInventoryItemPrices + apiKey + "&inventoryItemId=" +itemId);
                url = strHost + strUrlInventoryItemPrices + apiKey + "&inventoryItemId=" + itemId;
                objRequest.Method = "GET";
                objRequest.ContentType = "text/html";
                objRequest.KeepAlive = true;

                LoginRes = (HttpWebResponse)objRequest.GetResponse();
                sr = new StreamReader(LoginRes.GetResponseStream());
                string ReadWebsite_JSON = sr.ReadToEnd();
                sr.Close();
                sr = null;
                LoginRes.Close();
                LoginRes = null;
                objRequest = null;

                return ReadWebsite_JSON;
            }
            catch (Exception ex)
            {
                log.Error("GetInventoryItemPrices url: " + url + ". " + ex.Message);
                return "";
            }

        }

        public string GetInventoryItemExtendedProperties(string itemId)
        {
            try
            {
                string strDS = "&propertyParams ={ \"PropertyName\":\"Domestic Shipping\",\"PropertyType\":\"Attribute\"}";
                
                HttpWebResponse LoginRes;
                StreamReader sr;
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(strHost + strUrlInventoryItemExtendedProperties + apiKey + "&inventoryItemId=" + itemId + strDS);

                objRequest.Method = "GET";
                objRequest.ContentType = "text/html";
                objRequest.KeepAlive = true;

                LoginRes = (HttpWebResponse)objRequest.GetResponse();
                sr = new StreamReader(LoginRes.GetResponseStream());
                string ReadWebsite_JSON = sr.ReadToEnd();
                sr.Close();
                sr = null;
                LoginRes.Close();
                LoginRes = null;
                objRequest = null;

                return ReadWebsite_JSON;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return "";
            }

        }

        /// <summary>
        /// method to call GetRate API
        /// </summary>
        /// <param name="message"></param>
        /// <returns>string in Json format</returns>
        public string ReadAPI_GetGetOrder()
        {
            string postData = "orderId=[orderid]&fulfilmentLocationId=&loadItems=true&loadAdditionalInfo=true";
            //GenerateParameters();
            try
            {
                HttpWebResponse LoginRes;
                StreamReader sr;
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(strHost + strUrlGetOrder);

                byte[] psw = Encoding.UTF8.GetBytes(apiKey);
                String encoded = Convert.ToBase64String(psw);

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                objRequest.Method = "POST";
                // objRequest.CookieContainer = cookieContainer;
                objRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0";
                objRequest.Accept = "application/json, text/plain, */*";
                objRequest.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                objRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                objRequest.Headers.Add("Authorization", "Basic " + encoded); // "Bearer Basic
                objRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";  //"application/json;charset=utf-8";
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
                    //remaining = LoginRes.Headers["X-Rate-Limit-Remaining"];
                    //reset = LoginRes.Headers["x-rate-limit-reset"];

                    //if (Convert.ToInt32(remaining) < 1)
                    //    System.Threading.Thread.Sleep(Convert.ToInt32(reset) * 1000);

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
                    log.Error("API retuns status code: " + LoginRes.StatusCode.ToString() + " Post data: " + postData);
                    return "";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " Post data: " + postData);
                return "";
            }

        }

        /// <summary>
        /// method to SetExtendedProperties
        /// </summary>
        /// <param name="message"></param>
        /// <returns>string in Json format</returns>
        public string SetExtendedProperties()
        {
            string postData = "orderId=[orderid]&fulfilmentLocationId=&loadItems=true&loadAdditionalInfo=true";
            //GenerateParameters();
            try
            {
                HttpWebResponse LoginRes;
                StreamReader sr;
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(strHost + strUrlGetOrder);

                byte[] psw = Encoding.UTF8.GetBytes(apiKey);
                String encoded = Convert.ToBase64String(psw);

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                objRequest.Method = "POST";
                // objRequest.CookieContainer = cookieContainer;
                objRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0";
                objRequest.Accept = "application/json, text/plain, */*";
                objRequest.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                objRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                objRequest.Headers.Add("Authorization", "Basic " + encoded); // "Bearer Basic
                objRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";  //"application/json;charset=utf-8";
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
                    //remaining = LoginRes.Headers["X-Rate-Limit-Remaining"];
                    //reset = LoginRes.Headers["x-rate-limit-reset"];

                    //if (Convert.ToInt32(remaining) < 1)
                    //    System.Threading.Thread.Sleep(Convert.ToInt32(reset) * 1000);

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
                    log.Error("API retuns status code: " + LoginRes.StatusCode.ToString() + " Post data: " + postData);
                    return "";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " Post data: " + postData);
                return "";
            }

        }

        private string GenerateProerties(string orderid, List<ExtendedProperty> list)
        {
            string result = "orderId=" +orderid;
            //            orderId = bd381976 - 62e8 - 43f8 - aa2f - 50527a25e010 & extendedProperties =[
            //  {
            //                "RowId": "5456111d-9c18-4f40-a1dc-aede557800ec",
            //    "Name": "sample string 2",
            //    "Value": "sample string 3",
            //    "Type": "sample string 4"
            //  },
            //  {
            //                "RowId": "5456111d-9c18-4f40-a1dc-aede557800ec",
            //    "Name": "sample string 2",
            //    "Value": "sample string 3",
            //    "Type": "sample string 4"
            //  }
            //]
            return result;
        }

    }//end class

}//end namespace
