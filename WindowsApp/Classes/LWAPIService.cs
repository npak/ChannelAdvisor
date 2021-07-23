using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net;
using System.IO;
using System.Linq;

namespace ChannelAdvisor
{
    /// <summary>
    /// class to manage with LW APIs data
    /// </summary>
    public  class LWAPIService
    {
        private const string CONST_Ebay = "EBAY0_US"; // and tag is empty
        private const string CONST_Amazon = "Bargains Delivered";
        private const string CONST_Walmar = "Walmart";
        private const string CONST_Shopify = "Bargains Delivered";

        private LWAPIs lw = new LWAPIs();
        private string csvfolder = "";
        private string csvfilename = "";

        /// <summary>
        /// Constructor init GenerateParameters
        /// </summary>
        /// <param name="message"></param>
        public LWAPIService()
        {
            initParameters();
        }//end method

        private void initParameters()
        {
            DAL dal = new DAL();
            dal.GetLWAPIcsvfolder (out csvfolder, out csvfilename);
        }

        /// <summary>
        /// Get StockItem Location
        /// </summary>
        public List<Models.StockItemLocation> GetLocations()
        {
            List<Models.StockItemLocation> list = JsonConvert.DeserializeObject<List<Models.StockItemLocation>>(lw.GetInventoryLocationData());
            return list;
        }

        /// <summary>
        /// Get Order By NumericID
        /// </summary>
        public string GetOrderByNumericID(string ordernumber)
        {
            return lw.GetOrderByNumericID(ordernumber);
        }

        /// <summary>
        /// Get StockItem Location
        /// </summary>
        public string GetInventoryItemPrice(string itemId, string source)
        {
            //string strSource = lw.GetInventoryItemPrices(itemId);
            string price = "";
            string sstr = lw.GetInventoryItemPrices(itemId);
            JArray tokens = JArray.Parse(sstr);                 // Parse the object graph

            foreach (JToken tok in tokens)
            {
                if (tok["Source"].ToString()== source)
                {
                    switch (source)
                    {
                        case "EBAY":
                            if (tok["SubSource"].ToString() == CONST_Ebay &&  string.IsNullOrWhiteSpace(tok["Tag"].ToString()))
                                price = tok["Price"].ToString();
                            break;
                        case "AMAZON":
                            if (tok["SubSource"].ToString() == CONST_Amazon)
                                price = tok["Price"].ToString();
                            break;
                        case "WALMART":
                            if (tok["SubSource"].ToString() == CONST_Walmar)
                                price = tok["Price"].ToString();
                            break;
                        case "SHOPIFY":
                            if (tok["SubSource"].ToString() == CONST_Shopify)
                                price = tok["Price"].ToString();
                            break;
                    }

                    if (!string.IsNullOrWhiteSpace(price))
                        break;
                }
            }

            return price;
        }

        /// <summary>
        /// Get StockItem Location
        /// </summary>
        public string GetInventoryItemExtendedProperties(string itemId)
        {
            string domesticSheeping = "";
            string sstr = lw.GetInventoryItemExtendedProperties(itemId);
            JToken obj = JToken.Parse(sstr);                
            if (obj.HasValues) 
                domesticSheeping = obj[0]["PropertyValue"].ToString(); 
            return domesticSheeping;
        }
        public string GetCSVString()
        {
            string fromID = "";
            string toID = "";
            // get order id interval
            DAL dal = new DAL();
            dal.GetLWAPIInfo(out fromID, out toID);

            int intFrom = Convert.ToInt32(fromID);
            int intTo = intFrom;
            if (toID != "")
                intTo = Convert.ToInt32(toID);

            //LWAPIService serv = new LWAPIService();
            string ReadWebsite_JSON;

            JObject jObj;  // joson object
            JObject generalInfo;
            JObject items;
            JToken itemsList;
            JObject customerInfo;
            JObject shippingInfo;
            JObject totalsInfo;
            JArray extendedProperties;
            
            string itemId = "";
            string source = "";
            string csvHeader = "Order Id, Created Date, Status, SKU, Barcode, Source, Marketplace ID, Buyer Name, Shipped Name, State, ";
            csvHeader += "Zip Code , Category Name, PricePerUnit, Unit cost,Quantity, Line discount, Cost, CostIncTax, Subtotal, Shipping cost, ";
            csvHeader += "Tax rate, Order tax, Order total, Channel Price, Domestic Shipping,Total Weight, Tracking Number";


            string strResult = csvHeader;
            strResult += Environment.NewLine;

            for (int i = intFrom; i <= intTo; i++)
            {
                if (i == 129358)
                {
                    ;
                }
                ReadWebsite_JSON = GetOrderByNumericID(i.ToString());
                // delay to avoid Error# 429
                System.Threading.Thread.Sleep(1000);  
                if (ReadWebsite_JSON == "null")
                    continue;
                // order details
                jObj = JObject.Parse(ReadWebsite_JSON);

                JToken toks = jObj.SelectToken("Items");

                // generalInfo
                generalInfo = JObject.Parse(jObj["GeneralInfo"].ToString());

                // Items
                //items = JObject.Parse(jObj.SelectToken("Items")[0].ToString());

                // CustomerInfo Address
                customerInfo = JObject.Parse(jObj["CustomerInfo"].ToString());

                // ShippingInfo
                shippingInfo = JObject.Parse(jObj["ShippingInfo"].ToString());

                // TotalsInfo
                totalsInfo = JObject.Parse(jObj["TotalsInfo"].ToString());

                // ExtendedProperties
                extendedProperties = JArray.Parse(jObj["ExtendedProperties"].ToString());

                for (int j=0; j<toks.Count(); j++)
                {
                    //strResult += items["OrderId"].ToString() +",";
                    //JObject.Parse(jObj.SelectToken("Items")[0].ToString());
                    items = JObject.Parse(toks[j].ToString());
                    items = (JObject) toks[j];
                    strResult += i.ToString() + ",";
                    strResult += generalInfo["ReceivedDate"].ToString() + ",";
                    strResult += generalInfo["Status"].ToString() + ",";
                    strResult += items["SKU"].ToString() + ",";
                    strResult += items["BarcodeNumber"].ToString() + ",";
                    itemId = items["ItemId"].ToString();
                    source = generalInfo["Source"].ToString();
                    strResult += source + ",";
                    strResult += generalInfo["ReferenceNum"].ToString().Replace(",", ".") + ",";
                    strResult += customerInfo["ChannelBuyerName"].ToString() + ",";
                    strResult += customerInfo["Address"]["FullName"].ToString().Replace(",", ".") + ",";
                    strResult += customerInfo["Address"]["Region"].ToString() + ",";
                    strResult += customerInfo["Address"]["PostCode"].ToString() + ",";
                    strResult += items["CategoryName"].ToString() + ",";
                    strResult += items["PricePerUnit"].ToString() + ",";
                    strResult += items["UnitCost"].ToString() + ",";
                    strResult += items["Quantity"].ToString() + ",";
                    strResult += items["Discount"].ToString() + ",";
                    strResult += items["Cost"].ToString() + ",";
                    strResult += items["CostIncTax"].ToString() + ",";
                    strResult += totalsInfo["Subtotal"].ToString() + ",";

                    strResult += shippingInfo["PostageCost"].ToString() + ",";
                    strResult += items["TaxRate"].ToString() + ",";
                    strResult += totalsInfo["Tax"].ToString() + ",";
                    strResult += totalsInfo["TotalCharge"].ToString() + ",";
                    // price
                    strResult += GetInventoryItemPrice(itemId, source) + ",";
                    // domestic sheeping
                    strResult += GetInventoryItemExtendedProperties(itemId) + ",";

                    strResult += shippingInfo["TotalWeight"].ToString() + ",";
                    strResult += shippingInfo["TrackingNumber"].ToString();

                    strResult += Environment.NewLine;
                }
            }
            return strResult;
        }

        public void UploadFileFromString(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(str);
            string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
            string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
            string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

            //WebRequest request = WebRequest.Create("ftp://" + ftpAddress + "/" + csvfolder + "/" + csvfilename);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpAddress + "/" + csvfolder + "/" + csvfilename);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UsePassive = true;
            request.KeepAlive = true;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();

        }
    }//end class

}//end namespace
