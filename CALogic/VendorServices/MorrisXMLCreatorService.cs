using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.ComponentModel;
using System.IO;
using System.Configuration;
using System.Net;
using log4net;
using ChannelAdvisor.Utility;

namespace ChannelAdvisor
{
    public class MorrisXMLCreatorService : IVendorService
    {
        public readonly ILog log = LogManager.GetLogger(typeof(MorrisXMLCreatorService));
        public Vendor VendorInfo { get; set; }

        private string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
        private string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
        private string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

        private string morrisLogin = ConfigurationManager.AppSettings["morrisLogin"].ToString();
        private string morrisPassword = ConfigurationManager.AppSettings["morrisPsw"].ToString();

        private string _urlInput = "";
        private string _urlProcessed = "";
        private string _urlReadyToUpload = "";
        private string _errorFolderName = "";
        private string host = "";
            //"/home/inventory1";

        string csvfolder = "";
        public string csvfilename = "";
       /// <summary>
        /// 
        /// </summary>
        public MorrisXMLCreatorService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            new DAL().GetMorrisXMLCreatorInfo(out _urlInput, out _urlProcessed, out _urlReadyToUpload, out _errorFolderName);
            VendorInfo = new DAL().GetVendor((int)VendorName.MorrisXMLCreator);

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
        /// 
        public void CreateXMLDoc()
        {
            // create email manager
            EmailManager em = new EmailManager();
            try
            {
                List<string> filesList = GetFilesList();
                foreach (string filename in filesList)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    string[] source = DownloadString(filename);
                    string[] fields;
                    string[] temp16;
                    string po = "";
                    string street2 = "";

                    Int32 lineCount = 1;
                    string fileNo = "";
                    Boolean invalidPo = false;
                    Boolean invalidFile = false;
                    Int32 pos = -1;

                    foreach (string str in source)
                    {
                        fields = str.Split(new string[] { "\"," }, StringSplitOptions.None);
                        // check if the order is the same
                        if (fields[2].Replace("\"", "") != po)
                        {
                            if (po != "") // if order# is changed the upload the xml. 
                            {
                                if (xmlDoc.ChildNodes.Count > 1)
                                {
                                    // if not po is invalid then  upload to ftp
                                    if (!invalidPo)
                                    {
                                        // Send email notification on Order upload error
                                        if (!UploadXMLToFTP(xmlDoc, po + ".xml"))
                                            em.sendEmail(em.morrisXmlUploadErrorBody(po + ".xml"));

                                        if (!PostXml(po + ".xml"))
                                        {
                                            //invalidPo = true;
                                            invalidFile = true;
                                            em.sendEmail(em.morrisXmlPostErrorBody(po));
                                            //po = fields[2].Replace("\"", "");
                                            //continue;
                                        }

                                    }
                                }

                            }

                            // create a new xml doc
                            po = fields[2].Replace("\"", "");
                            invalidPo = false;
                            xmlDoc = new XmlDocument();
                            XmlNode declarationNode = xmlDoc.CreateXmlDeclaration("1.0", "", "");
                            xmlDoc.AppendChild(declarationNode);
                            lineCount = 1;

                        }

                        /// check  if  xml elements are alredy created 
                        if (xmlDoc.GetElementsByTagName("otype").Count == 0)
                        {
                            XmlNode orderNode = xmlDoc.CreateElement("Order");
                            xmlDoc.AppendChild(orderNode);

                            XmlNode otypeNode = xmlDoc.CreateElement("otype");
                            otypeNode.InnerText = "O";
                            orderNode.AppendChild(otypeNode);

                            XmlNode pickmsgNode = xmlDoc.CreateElement("pickmsg");
                            pickmsgNode.InnerText = fields[1].Replace("\"", "");
                            orderNode.AppendChild(pickmsgNode);

                            XmlNode poNode = xmlDoc.CreateElement("po");
                            poNode.InnerText = fields[2].Replace("\"", "");
                            orderNode.AppendChild(poNode);

                            XmlNode viaNode = xmlDoc.CreateElement("via");
                            viaNode.InnerText = fields[3].Replace("\"", "");
                            orderNode.AppendChild(viaNode);

                            XmlNode termsNode = xmlDoc.CreateElement("terms");
                            termsNode.InnerText = fields[4].Replace("\"", "");
                            orderNode.AppendChild(termsNode);

                            XmlNode magicNode = xmlDoc.CreateElement("magic");
                            magicNode.InnerText = fields[5].Replace("\"", "");
                            orderNode.AppendChild(magicNode);

                            XmlNode countNode = xmlDoc.CreateElement("count");
                            countNode.InnerText = "1"; //fields[6];
                            orderNode.AppendChild(countNode);
                            // ship
                            XmlNode shipNode = xmlDoc.CreateElement("ShipTo");
                            XmlNode addressNode = xmlDoc.CreateElement("Address");

                            XmlNode Line1Node = xmlDoc.CreateElement("Line1");
                            Line1Node.InnerText = fields[7].Replace("\"", "");
                            if (Line1Node.InnerText.Length <26 )
                                addressNode.AppendChild(Line1Node);
                            else
                            {
                                em.sendEmail(em.morrisXmlLineCharacterlimitErrorBody(filename, po));
                                invalidFile = true;
                                invalidPo = true;
                            }

                            XmlNode Street1Node = xmlDoc.CreateElement("Street1");
                            Street1Node.InnerText = fields[8].Replace("\"", "");
                            if (Street1Node.InnerText.Length < 26)
                            {
                                addressNode.AppendChild(Street1Node);
                                XmlNode Street2Node = xmlDoc.CreateElement("Street2");
                                Street2Node.InnerText = fields[9].Replace("\"", "");
                                if (Street2Node.InnerText.Length < 26 )
                                addressNode.AppendChild(Street2Node);
                                else
                                {
                                    em.sendEmail(em.morrisXmlStreetCharacterlimitErrorBody(filename, po));
                                    invalidPo = true;
                                    invalidFile = true;
                                    //continue;
                                }
                            }
                            else if (Street1Node.InnerText.Length > 50)
                            {
                                em.sendEmail(em.morrisXmlStreetCharacterlimitErrorBody(filename, po));
                                invalidFile = true;
                                invalidPo = true;
                            }
                            else
                            {
                                if (fields[9].Replace("\"", "").Trim().Length == 0)
                                {
                                    pos = Pos25(Street1Node.InnerText.Trim().ToLower());
                                    street2 = Street1Node.InnerText.Substring(pos);
                                    Street1Node.InnerText = Street1Node.InnerText.Substring(0, pos);
                                    addressNode.AppendChild(Street1Node);
                                    // add to street2
                                    XmlNode Street2Node = xmlDoc.CreateElement("Street2");
                                    Street2Node.InnerText = street2;
                                    addressNode.AppendChild(Street2Node);
                                    em.sendEmail(em.morrisXmlMovePartOfLine1Body(filename, po));
                                }
                                else
                                {
                                    em.sendEmail(em.morrisXmlStreetCharacterlimitErrorBody(filename, po));
                                    invalidFile = true;
                                    invalidPo = true;
                                    //continue;
                                }
                            }

                            XmlNode CityNode = xmlDoc.CreateElement("City");
                            CityNode.InnerText = fields[10].Replace("\"", "");
                            addressNode.AppendChild(CityNode);

                            XmlNode StateNode = xmlDoc.CreateElement("State");
                            StateNode.InnerText = fields[11].Replace("\"", "");
                            addressNode.AppendChild(StateNode);

                            XmlNode ZipNode = xmlDoc.CreateElement("Zip");
                            ZipNode.InnerText = fields[12].Replace("\"", "");
                            addressNode.AppendChild(ZipNode);

                            XmlNode CountryNode = xmlDoc.CreateElement("Country");
                            CountryNode.InnerText = fields[13].Replace("\"", "");
                            addressNode.AppendChild(CountryNode);

                            XmlNode PhoneNode = xmlDoc.CreateElement("Phone");
                            PhoneNode.InnerText = fields[14].Replace("\"", "");
                            addressNode.AppendChild(PhoneNode);
                            shipNode.AppendChild(addressNode);
                            orderNode.AppendChild(shipNode);
                            // LineItems
                            XmlNode lineItemsNode = xmlDoc.CreateElement("LineItems");
                            orderNode.AppendChild(lineItemsNode);
                            XmlNode lineNode = xmlDoc.CreateElement("Line");
                            XmlNode partNode = xmlDoc.CreateElement("part");
                            partNode.InnerText = fields[15].Replace("\"", "");
                            lineNode.AppendChild(partNode);

                            XmlNode qtyNode = xmlDoc.CreateElement("qty");
                            temp16 = fields[16].Split(',');
                            qtyNode.InnerText = temp16[0].Replace("\"", "");
                            lineNode.AppendChild(qtyNode);
                            lineItemsNode.AppendChild(lineNode);
                        }
                        else
                        {
                            // if the order already exist then just add  a new line. 
                            XmlNode lineItemsNode = xmlDoc.SelectSingleNode("//LineItems");
                            XmlNode lineNode = xmlDoc.CreateElement("Line");
                            XmlNode partNode = xmlDoc.CreateElement("part");
                            partNode.InnerText = fields[15].Replace("\"", "");
                            lineNode.AppendChild(partNode);

                            XmlNode qtyNode = xmlDoc.CreateElement("qty");
                            temp16 = fields[16].Split(',');
                            qtyNode.InnerText = temp16[0].Replace("\"", "");
                            //qtyNode.InnerText = fields[16].Replace("\"", "");
                            lineNode.AppendChild(qtyNode);
                            lineItemsNode.AppendChild(lineNode);

                            xmlDoc.DocumentElement.AppendChild(lineItemsNode);

                            // change count 
                            lineCount++;
                            XmlNodeList countNode = xmlDoc.GetElementsByTagName("count");
                            countNode[0].InnerText = lineCount.ToString();
                        }

                    }
                    if (!invalidPo)
                    {
                        if (!UploadXMLToFTP(xmlDoc, po + ".xml"))
                            em.sendEmail(em.morrisXmlUploadErrorBody(po + ".xml"));
                    }
                    if (!invalidFile)
                    {

                        if (PostXml(po + ".xml"))
                            MoveCsvToProcessed(filename);
                        else
                        {
                            MoveCsvToError(filename);
                            em.sendEmail(em.morrisXmlPostErrorBody(po));
                        }
                    }
                    else
                    {
                        //em.sendEmail(em.PostErrorBody(po));
                        RenameInvalidCsv(filename);
                    }
                }
            }
            
            catch(Exception ex)
            {
                log.Info("MorrisXMLCreator error: " + ex.Message);
            }
}

        public  Int32 Pos25(string str)
        {
            Int32 pos = -1;
            //1 swithc to Lower: apt, ste, #, apartment, suite
            
            if (str.IndexOf(" apt ") > 0)
            {
                pos = str.IndexOf(" apt ");
            }
            else if (str.IndexOf(" ste ") > 0)
            {
                pos = str.IndexOf(" ste ");
            }
            else if (str.IndexOf(" # ") > 0)
            {
                pos = str.IndexOf(" # ");
            }
            else if (str.IndexOf(" apartment ") > 0)
            {
                pos = str.IndexOf(" apartment ");
            }
            else if (str.IndexOf(" unit ") > 0)
            {
                pos = str.IndexOf(" unit ");
            }
            else if (str.IndexOf(" suite ") > 0)
            {
                pos = str.IndexOf(" suite ");
            }

            if (pos>26)
            {
                pos = -1;
            }
            //
            if (pos == -1)
            {
                pos = 0;
                String[] words = str.Split(new char[] { ' ', '\t' }, StringSplitOptions.None);
                foreach (string word in words)
                {
                    pos += word.Length;
                    if (pos > 25)
                    {
                        pos -= word.Length+1;
                        break;
                    }
                    else
                        pos += 1;
                }
            }

            return pos;
        }
        private string[] DownloadString(string filename)
        {
            string ss = "";
            try
            {
                string url = "ftp://" + ftpAddress + _urlInput + filename; //account190821_2018-10-09_06_16_14.csv
                var reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.Credentials = new NetworkCredential(username, password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                //use the response like below
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                ss = reader.ReadToEnd().Split(Environment.NewLine.ToCharArray(), 2).Skip(1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                // log
            }

            string[] allLines = ss.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            return allLines;
        }

        private Boolean UploadXMLToFTP(XmlDocument xml, string filename)
        {
            try
            {
                // XmlDocument xml = ParceScvToXml(filename);
                string url = "ftp://" + ftpAddress + _urlReadyToUpload + "po_" + filename;
                var reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.Credentials = new NetworkCredential(username, password);
                //FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.ContentLength = xml.OuterXml.Length;

                Stream requestStream = reqFTP.GetRequestStream();
                xml.Save(requestStream);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Boolean PostXml(string filename)
        {
            System.Threading.Thread.Sleep(60000); 

            string url = "http://morris.morriscostumes.com/cgi-bin/doxml.cgi?userid=" + morrisLogin + "&password=" + morrisPassword  + "&xml_url=[xmlPath]&message=done";
            string xmlFile = "http://" + ftpAddress + "/" + username + _urlReadyToUpload.Replace(" ", "%20") + "po_" + filename;

            url = url.Replace("[xmlPath]", xmlFile); 

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "text/xml";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                string message = String.Format("POST failed. Received HTTP {0}",
                response.StatusCode);
                throw new ApplicationException(message);
            }
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
            StreamReader sr = new StreamReader(response.GetResponseStream(), encoding);

            string ss = sr.ReadToEnd();
            string[] lines = ss.Split( new[] { "\r\n", "\r", "\n" },  StringSplitOptions.None);

            string text = "";
            string status = "";
            Boolean isError = false;

            // check if error code =0 (OK)
            // if not succesfull then write message into log file 
            foreach (string line in lines)
            {
                if (line.StartsWith("ErrorCode"))
                {
                    if (!line.Contains("ErrorCode=0"))
                    {
                        isError = true;
                    }
                }
                else if (line.StartsWith("Text="))
                    text = line;
                else if (line.StartsWith("Status="))
                    status = line;
            }
            if (isError)
            {
                log.Info(filename + " : " + status + " " + text);
                return false;
            }
            else
                return true;
        }

        public XmlDocument PostXMLTransaction(string v_strURL, XmlDocument v_objXMLDoc)
        {
            //Declare XMLResponse document
            XmlDocument XMLResponse = null;

            //Declare an HTTP-specific implementation of the WebRequest class.
            HttpWebRequest objHttpWebRequest;

            //Declare an HTTP-specific implementation of the WebResponse class
            HttpWebResponse objHttpWebResponse = null;

            //Declare a generic view of a sequence of bytes
            Stream objRequestStream = null;
            Stream objResponseStream = null;

            //Declare XMLReader
            XmlTextReader objXMLReader;

            //Creates an HttpWebRequest for the specified URL.
            objHttpWebRequest = (HttpWebRequest)WebRequest.Create(v_strURL);

            try
            {
                //---------- Start HttpRequest 

                //Set HttpWebRequest properties
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(v_objXMLDoc.InnerXml);
                objHttpWebRequest.Method = "POST";
                objHttpWebRequest.ContentLength = bytes.Length;
                objHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";

                //Get Stream object 
                objRequestStream = objHttpWebRequest.GetRequestStream();

                //Writes a sequence of bytes to the current stream 
                objRequestStream.Write(bytes, 0, bytes.Length);

                //Close stream
                objRequestStream.Close();

                //---------- End HttpRequest

                //Sends the HttpWebRequest, and waits for a response.
                objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();

                //---------- Start HttpResponse
                if (objHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    //Get response stream 
                    objResponseStream = objHttpWebResponse.GetResponseStream();

                    //Load response stream into XMLReader
                    objXMLReader = new XmlTextReader(objResponseStream);

                    //Declare XMLDocument
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(objXMLReader);

                    //Set XMLResponse object returned from XMLReader
                    XMLResponse = xmldoc;

                    //Close XMLReader
                    objXMLReader.Close();
                }

                //Close HttpWebResponse
                objHttpWebResponse.Close();
            }
            catch (WebException we)
            {
                //TODO: Add custom exception handling
                throw new Exception(we.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //Close connections
                objRequestStream.Close();
                objResponseStream.Close();
                objHttpWebResponse.Close();

                //Release objects
                objXMLReader = null;
                objRequestStream = null;
                objResponseStream = null;
                objHttpWebResponse = null;
                objHttpWebRequest = null;
            }

            //Return
            return XMLResponse;
        }

        public void RenameInvalidCsv(string filename)
        {
            try
            {
                string url = "ftp://" + ftpAddress + _urlInput + filename; //account190821_2018-10-09_06_16_14.csv
                var reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.Credentials = new NetworkCredential(username, password);
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = host + _urlInput + "review_" + filename;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
                response = null;
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
            }
        }

        public void MoveCsvToProcessed(string filename)
        {
            try
            {
                string url = "ftp://" + ftpAddress + _urlInput + filename; //account190821_2018-10-09_06_16_14.csv
                var reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.Credentials = new NetworkCredential(username, password);
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = host + _urlProcessed + "processed_" + filename;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
                response = null;
            }
            catch (WebException wex)
            {
                String status = ((FtpWebResponse)wex.Response).StatusDescription;
            }
        }

        public void MoveCsvToError(string filename)
        {
            try
            {
                string url = "ftp://" + ftpAddress + _urlInput + filename; //account190821_2018-10-09_06_16_14.csv
                var reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.Credentials = new NetworkCredential(username, password);
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo =host + _errorFolderName + filename;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
                response = null;

            }
            catch (WebException wex)
            {
                String status = ((FtpWebResponse)wex.Response).StatusDescription;
            }
           
        }
        //        FtpWebRequest ftpRequest = null;
        //FtpWebResponse ftpResponse = null;
        //try
        //{
        //    ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://mysite.com/folder1/fileName.ext");
        //    ftpRequest.Credentials = new NetworkCredential("user", "pass");
        //    ftpRequest.UseBinary = true;
        //    ftpRequest.UsePassive = true;
        //    ftpRequest.KeepAlive = true;
        //    ftpRequest.Method = WebRequestMethods.Ftp.Rename;
        //    ftpRequest.RenameTo = "/folder2/fileName.ext";
        //    ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
        //    ftpResponse.Close();
        //    ftpRequest = null;
        //}

        //        ftpRequest.RenameTo = "/folder2/fileName.ext";

        //        A- change it to be

        //ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://mysite.com//folder1/fileName.ext");

        //B- ftpRequest.RenameTo = "ftp://mysite.com//folder2/fileName.ext";

        //Just add (/) before your file folder that allow you to delFile,renFile also downLoadFile


        private Boolean ValidateOrder(string line1, string street1, string street2, string city, string state)
        {
            // A name or company name must be provided (<Line>); (30 spaces).
            //2) An address street, city, state, (<-30 characters for street and city, 
            // 2 characters for state) and zip 
            // must be provided.

            if ((line1.Length > 30) || (street1.Length > 30) || street2.Length > 30 || city.Length > 30 || state.Length > 30)
                return false;
            return true;
        }

        private List<string> GetFilesList()
        {
            string url = "ftp://" + ftpAddress + _urlInput;  //+ "/";
            var reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
            reqFTP.Credentials = new NetworkCredential(username, password);
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string ss = reader.ReadToEnd();
            // ss =response.StatusDescription;
            string[] ll = ss.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            List<string> list = new List<string>();
            foreach (string r in ll)
                if (r.Contains(".csv") && !r.Contains("review_"))
                    list.Add(r);
            reader.Close();
            response.Close();

            return list;
        }

    }
}//end namespace
