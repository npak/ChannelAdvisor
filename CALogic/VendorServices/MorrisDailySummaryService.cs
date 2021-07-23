using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.IO;

namespace ChannelAdvisor
{
    public class MorrisDailySummaryService : IVendorService
    {
        public Vendor VendorInfo { get; set; }
        public string CreditString { get; set; }
        public int Datelag { get; set; }

        private string _url = "";
        
        string csvfolder = "";
        public string csvfilename = "";
        public string creditcsvfolder = "";
        public string creditcsvfilename = "";

        /// <summary>
        /// 
        /// </summary>
        public MorrisDailySummaryService()
        {
            Datelag = 1;
            new DAL().GetMorrisDailySummarySettings(out _url,out csvfolder, out csvfilename, out creditcsvfolder, out creditcsvfilename);
            _url = GetCurrentUrl();
            
            VendorInfo = new DAL().GetVendor((int)VendorName.MorrisDailySummary);

            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Morris"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return null;
        }//end method


        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
               return null;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GererateCsv()
        {
            _url = GetCurrentUrl();            
            csvfilename = GetCurrentFileName();
            creditcsvfilename = GetCurrentCreditFileName();
            //Header
            string orderNum = "";
            string po = "";
            string InvAmt = "";
            string Shipping = "";
            string via = "";


            //LineItems
            string No = "";
            string Sku = "";
            string Part = "";
            string Ordered = "";
            string Ship = "";
            string Price = "";
            string Net = "";

            //BillTo/Address
            string Line1 = "";
            string Street1 = "";
            string City = "";
            string State = "";
            string Zip = "";
            string Address = "";

            //Boxes
            string TrackNum = "";

            XmlDocument xmlInventory = new XmlDocument();
            XmlNodeList nodes;
            XmlNode tempnode;
            string headers = "Order #, Our order #,Invoice Total,All Shipping, Shipping service, Address, Sku, Part, isOrdered, isShip, Price, Net,TrackNum";

            StringBuilder sb = new StringBuilder();
            sb.Append(headers);
            sb.AppendLine();

            StringBuilder returnBuilder = new StringBuilder();
            returnBuilder.Append(headers);
            returnBuilder.AppendLine();

            try
            {
                xmlInventory = GetInventoryXML(_url);

                if (xmlInventory != null)
                {
                    nodes = xmlInventory.SelectNodes("//Order"); // get all <AvailableBatch> nodes

                    foreach (XmlNode node in nodes)
                    {
                      
                        if ((node["otype"].InnerXml.Trim() == "Order" && node["Status"].InnerXml.Trim() == "Shipped" ) ||
                            (node["otype"].InnerXml.Trim() == "Order" && node["Status"].InnerXml.Trim() == "Invoiced") ||
                           (node["otype"].InnerXml.Trim() == "Return" && node["Status"].InnerXml.Trim() == "Invoiced"))
                        {
                            tempnode =node.SelectSingleNode("./Boxes/Box/TrackNum");
                            TrackNum = tempnode==null ? "": tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./Header/OrderNum");
                            orderNum = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./Header/po");
                            po = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./Header/InvAmt");
                            InvAmt = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./Header/Shipping");
                            Shipping = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("Header/Via");
                            via = tempnode == null ? "" : tempnode.InnerText;
                            //rename shipping service
                            if (via == "313-SUREPOST USPS DELIVERY")
                                via = "UPS Surepost";
                            else if (via == "385-ENDICIA PRIORITY")
                                via = "USPS Priority Mail";
                            else if (via == "386-ENDICIA FIRST CLASS")
                                via = "USPS First Class Mail";

                            else if (via == "322-3 P UPS GRD(R)W/SIGNATURE")
                                via = "UPS Ground Signature";
                            else if (via == "45-3RD P UPS GRND RES")
                                via = "UPS Ground";
                            else if (via == "384-ENDICIA PARCEL POST")
                                via = "USPS Parcel Select Ground";
                            else if (via == "316-FEDEX SMARTPOST")
                                via = "FedEx SmartPost";

                            tempnode = node.SelectSingleNode("./Header/BillTo/Address/Line1");
                            Line1 = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./Header/BillTo/Address/Street1");
                            Street1 = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./Header/BillTo/Address/City");
                            City = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./Header/BillTo/Address/State");
                            State = tempnode == null ? "" : tempnode.InnerText;

                            Zip = node.SelectSingleNode("Header/BillTo/Address/Zip").InnerText;
                            Address = Line1 + " " + Street1 + " " + City + " " + State + "-" + Zip;

                            tempnode = node.SelectSingleNode("./LineItems/Line/No");
                            No = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./LineItems/Line/Sku");
                            Sku = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./LineItems/Line/Part");
                            Part = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./LineItems/Line/Ordered");
                            Ordered = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./LineItems/Line/Ship");
                            Ship = tempnode == null ? "" : tempnode.InnerText;

                            tempnode =  node.SelectSingleNode("./LineItems/Line/Price");
                            Price = tempnode == null ? "" : tempnode.InnerText;

                            tempnode = node.SelectSingleNode("./LineItems/Line/Net");
                            Net = tempnode == null ? "" : tempnode.InnerText;

                            if (node["otype"].InnerXml.Trim() == "Order")
                            {
                                sb.Append(orderNum).Append(",");
                                sb.Append(po).Append(",");
                                sb.Append(InvAmt).Append(",");
                                sb.Append(Shipping).Append(",");
                                sb.Append(via).Append(",");

                                sb.Append("\"" + Address + "\"").Append(",");
                                sb.Append(Sku).Append(",");
                                sb.Append(Part).Append(",");
                                sb.Append(Ordered).Append(",");
                                sb.Append(Ship).Append(",");
                                sb.Append(Price).Append(",");
                                sb.Append(Net).Append(",");
                                sb.Append(TrackNum);
                                sb.AppendLine();
                            }
                            else if (node["otype"].InnerXml.Trim() == "Return")
                            {
                                returnBuilder.Append(orderNum).Append(",");
                                returnBuilder.Append(po).Append(",");
                                returnBuilder.Append(InvAmt).Append(",");
                                returnBuilder.Append(Shipping).Append(",");
                                returnBuilder.Append(via).Append(",");

                                returnBuilder.Append("\"" + Address + "\"").Append(",");
                                returnBuilder.Append(Sku).Append(",");
                                returnBuilder.Append(Part).Append(",");
                                returnBuilder.Append(Ordered).Append(",");
                                returnBuilder.Append(Ship).Append(",");
                                returnBuilder.Append(Price).Append(",");
                                returnBuilder.Append(Net).Append(",");

                                returnBuilder.Append(TrackNum);
                                returnBuilder.AppendLine();
                            }

                        } // if 
                       
                    } // for
                }
                // set ReturnType string
                if (!string.IsNullOrWhiteSpace(returnBuilder.ToString()) )
                {
                    CreditString = returnBuilder.ToString();
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }

        }//end method

        private XmlDocument GetInventoryXML(string url)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(url);
                return xd;
            }
            catch
            {
                return null;
            }

        }//end method

        private string GetCurrentUrl()
        {
            DateTime dt = DateTime.Today.AddDays(-Datelag);
            string sdd = dt.Month.ToString("00") + dt.Day.ToString("00") + dt.Year.ToString("0000");
            string myString = _url.Remove(_url.Length - 12) + sdd + ".xml";
            return myString;
        }

        private string GetCurrentFileName()
        {
            DateTime dt = DateTime.Today.AddDays(-Datelag);
            string sdd = dt.Month.ToString("00") + dt.Day.ToString("00") + dt.Year.ToString("0000");
            string myString = csvfilename.Remove(csvfilename.Length - 12) + sdd + ".csv";
            return myString;
        }

        private string GetCurrentCreditFileName()
        {
            DateTime dt = DateTime.Today.AddDays(-Datelag);
            string sdd = dt.Month.ToString("00") + dt.Day.ToString("00") + dt.Year.ToString("0000");
            string myString = creditcsvfilename.Remove(creditcsvfilename.Length - 12) + sdd + ".csv";
            return myString;
        }
    }
}//end namespace
