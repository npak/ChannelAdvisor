using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Xml;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.ComponentModel;
using System.Threading;
using System.Web;

using log4net;
using log4net.Config;
using System.Data;

namespace ChannelAdvisor
{
    public class EMGService : IVendorService
    {
        public Vendor VendorInfo { get; set; }
        public string FTPServer { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        private string LocalFolder { get; set; }
        private string Filename { get; set; }
        public readonly ILog log = LogManager.GetLogger(typeof(EMGService));

        /// <summary>
        /// Default constructor
        /// </summary>
        public EMGService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            DAL dal = new DAL();

            FTPServer = dal.GetSettingValue("EMG_FTP");
            Login = dal.GetSettingValue("EMG_FtpUsername");
            Password = dal.GetSettingValue("EMG_FtpPassword");

            VendorInfo = new DAL().GetVendor((int)VendorName.EMG);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for EMG"));
            LocalFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";
        }//end constructor

        /// <summary>
        /// Method that calls the https address and get inventory as string
        /// </summary>
        /// <returns></returns>
        private string GetInventoryCSV()
        {
            FTP ftp = new FTP(FTPServer, Login, Password, true);
            // Get list of files
            IList<string> files = ftp.GetFiles();
            // Get inventory excel file
            Filename = files.Single(f => f.EndsWith(".csv"));
            // download inventory file
            ftp.DownloadFile(Filename, LocalFolder);

            return string.Format(@"{0}{1}", LocalFolder, Filename);
        }

        public DataTable GetEMGProductTable(ref List<ErrorLog> errorlist)
        {
            // upcomment after
            GetInventoryCSV();

            //StreamReader sr = new StreamReader("E:\\papa\\2-15-15\\ChannelAdvisorSources\\output\\prodlist.csv");
            StreamReader sr = new StreamReader(LocalFolder + Filename);
            
            // skip two ffirst lines
            //sr.ReadLine();
            //sr.ReadLine();

            // read headers
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header.Replace("\"", ""));
            }
            int cnt = 0;
            string[] stringSeparators = new string[] { "," };
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(stringSeparators, StringSplitOptions.None);
                DataRow dr = dt.NewRow();
                if (rows.Count() != headers.Length)
                {
                    cnt++;

                    ErrorLog err = new ErrorLog(0, "EMG.csv record with EMG SKU:" + rows[1] + " is not properly formated: ");
                    errorlist.Add(err);
                    continue;
                }
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i].Replace("\"", "");
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
   
        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForEMG(true);

            //Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForEMG(false);
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForEMG(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //XmlDocument xmlInventory = new XmlDocument();
            ////xmlInventory.Load("C:\\1.xml"); //for debug only
            //xmlInventory = GetInventoryXML(isWinForm); //To Un-Comment
             List<ErrorLog> errorList = new List<ErrorLog>();
            DataTable dtProducts = GetEMGProductTable(ref errorList);
            invUpdateSrcvDTO.ErrorLogDTO = errorList;

            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                List<string> csvList = new List<string>();
                csvList.Add(LocalFolder + Filename);
                string vendorFile = CAUtil.SaveMultipleCSVFilesAsVendorFile(csvList, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }


            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            //XmlNodeList nodes = xmlInventory.SelectNodes("getFullInvResponse/item");

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the xml nodes and create dto
            for(int x=0; x<dtProducts.Rows.Count; x++)
            {
                Inventory invDTO = new Inventory();
                //invDTO.UPC = dtProducts.Rows[x]["supUPC"].ToString();
                invDTO.SKU = dtProducts.Rows[x]["supUPC"].ToString();
                    //string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[x]["supUPC"].ToString());
                invDTO.Qty = Convert.ToInt32(dtProducts.Rows[x]["supQty"]);
                //test
                //invDTO.Price = float.Parse(node["supPrice"].InnerXml);
                //invDTO.Price = (float)(Math.Round((double)invDTO.Price, 2));
                invDTO.Price = (float)(Math.Round(Convert.ToDouble(dtProducts.Rows[x]["supPrice"].ToString()), 2));

                //invDTO.MAP = float.Parse(node["supMinAdvPrice"].InnerXml, new CultureInfo("en-US"));
                invDTO.Description = dtProducts.Rows[x]["supDesc"].ToString();

                lstEMGInventory.Add(invDTO);
            }//end for each

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        #region EMG Extractor
        public void Extract(EMGExtractorSettings aSettings, List<string> aSKUs, bool aIsSKUUsed, string aFolderName, bool IsWholeFile)
        {
            XmlDocument lXmlDocument;
            try
            {
                lXmlDocument = LoadXML(aSettings.URLToLoadXML);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot load XML file", ex);
            }

            List<EMGExportRow> lResult = new List<EMGExportRow>();
            lResult.Add(EMGExportRow.GetHeaderRow());

            EMGExportRow lRow;
            XmlNodeList lNodeList = lXmlDocument.SelectNodes("getFullInvResponse/item");

            foreach (XmlNode lNode in lNodeList)
            {
                string lSku = aIsSKUUsed ? lNode["supMfgrModel"].InnerText : lNode["supUPC"].InnerText;
                if (IsWholeFile || (!(string.IsNullOrEmpty(lSku)) && (aSKUs.IndexOf(lSku) > -1) && 
                    (lNode["supComment"].InnerText != "Added from EMG daily inventory file")))
                {
                    lRow = new EMGExportRow();
                    string supDesc = ToLowerCase(lNode["supDesc"].InnerText);

                    lRow.AuctionTitle = supDesc.Length > aSettings.AuctionTitleMaxLength ?
                        supDesc.Substring(0, aSettings.AuctionTitleMaxLength) : supDesc;
                    lRow.InventoryNumber = lSku;
                    lRow.Weight = lNode["supShipWt"].InnerText;
                    lRow.UPC = lNode["supUPC"].InnerText;
                    lRow.MPN = lNode["supMfgrModel"].InnerText;

                    List<string> lDescriptions = new List<string>();
                    for (int i = 1; i <= 5; i++)
                        if (lNode["supInfo" + i].InnerText.Length > 0)
                            lDescriptions.Add(HttpUtility.HtmlEncode(lNode["supInfo" + i].InnerText));
                    string IntroParagraph = HttpUtility.HtmlEncode(lNode["supComment"].InnerText).Replace("#", "");
                    lRow.Description = GetHtml(IntroParagraph, lDescriptions).Replace("\n"," ");
                    
                    lRow.SupplierCode = aSettings.SupplierCode;
                    lRow.WarehouseLocation = aSettings.WarehouseLocation;
                    lRow.DCCode = aSettings.DCCode;
                    lRow.ChannelAdvisorStoreTitle = supDesc.Length > aSettings.CAStoreTitleMaxLength ?
                        supDesc.Substring(0, aSettings.CAStoreTitleMaxLength) : supDesc;
                    lRow.ChannelAdvisorStoreDescription = GetHtml(IntroParagraph, lDescriptions);
                    lRow.Classification = lNode["supSubCat"].InnerText.Length > aSettings.ClassificationMaxLength ?
                        lNode["supSubCat"].InnerText.Substring(0, aSettings.ClassificationMaxLength) : lNode["supSubCat"].InnerText;
                    lRow.Attribute1Name = "Brand";
                    lRow.Attribute1Value = lNode["supMfgr"].InnerText;
                    lRow.Attribute2Name = "Last Update";
                    lRow.Attribute2Value = lNode["lastUpdate"].InnerText;
                    lRow.Attribute3Name = aSettings.WarrantyLabel;
                    if (lNode["supInfo5"].InnerText.ToLower().Contains("warranty"))
                        lRow.Attribute3Value = lNode["supInfo5"].InnerText;
                    else
                        lRow.Attribute3Value = aSettings.WarrantyDefaultValue;
                    lRow.Attribute4Name = "Backorder Date";
                    lRow.Attribute4Value = lNode["supBackorderDate"].InnerText;
                    lRow.Attribute5Name = "L";
                    lRow.Attribute5Value = lNode["supShipLen"].InnerText;
                    lRow.Attribute6Name = "W";
                    lRow.Attribute6Value = lNode["supShipWidth"].InnerText;
                    lRow.Attribute7Name = "H";
                    lRow.Attribute7Value = lNode["supShipHeight"].InnerText;

                    lResult.Add(lRow);
                }
            }

            string lFileName = aFolderName + aSettings.OutputFilePrefix + DateTime.Now.ToString("MMddyyyy") + ".txt";
            //EMGExporter.ExportEMGDataToTextFile(lResult, lFileName);
            ExportToCSV(lFileName, lResult);
        }

        //private string GetColumNames()
        //{
        //    var columnNames = new [] {"Auction Title",
        //                       "Inventory Number",
        //                       "Weight",
        //                       "UPC",
        //                       "MPN",
        //                       "Description",
        //                       "Supplier Code",
        //                       "Warehouse Location",
        //                       "DC Code",
        //                       "ChannelAdvisor Store Title",
        //                       "ChannelAdvisor Store Description",
        //                       "Classification",
        //                       "Warranty",
        //                       "Labels",
        //                       "Attribute1Name",
        //                       "Attribute1Value",
        //                       "Attribute2Name",
        //                       "Attribute2Value",
        //                       "Attribute3Name",
        //                       "Attribute3Value",
        //                       "Attribute4Name",
        //                       "Attribute4Value",
        //                       "Attribute5Name",
        //                       "Attribute5Value",
        //                       "Attribute6Name",
        //                       "Attribute6Value",
        //                       "Attribute7Name",
        //                       "Attribute7Value"};

        //    var res = String.Join("\t", columnNames);
        //    return res;
        //}

        private string GetRowStr(EMGExportRow row)
        {
            var sep = "\"\t\"";
            var res = 
             "\"" + row.AuctionTitle + sep +
            row.InventoryNumber + sep +
            row.Weight + sep +
            row.UPC + sep +
            row.MPN + sep +
            row.Description + sep +
            row.SupplierCode + sep +
            row.WarehouseLocation + sep +
            row.DCCode + sep +
            row.ChannelAdvisorStoreTitle + sep +
            row.ChannelAdvisorStoreDescription + sep +
            row.Classification + sep +
            row.Warranty + sep +
            row.Label + sep +
            row.Attribute1Name + sep +
            row.Attribute1Value + sep +
            row.Attribute2Name + sep +
            row.Attribute2Value + sep +
            row.Attribute3Name + sep +
            row.Attribute3Value + sep +
            row.Attribute4Name + sep +
            row.Attribute4Value + sep +
            row.Attribute5Name + sep +
            row.Attribute5Value + sep +
            row.Attribute6Name + sep +
            row.Attribute6Value + sep +
            row.Attribute7Name + sep +
            row.Attribute7Value + "\"";
            return res;
        }

        private void ExportToCSV(string fileName, IEnumerable<EMGExportRow> rows)
        {
            var sb = new StringBuilder();
            //sb.AppendLine(GetColumNames());
            foreach (var row in rows)
            {
                var rowStr = GetRowStr(row).Replace("\r", "").Replace("\n", "") ;
                sb.AppendLine(rowStr);
            }
            File.WriteAllText(fileName, sb.ToString());
        }


        private string ToLowerCase(string source)
        {
            StringBuilder sb = new StringBuilder();
            string[] parts = source.Split(new char[] { ' ', '-' }, StringSplitOptions.None);

            for (int i = 0; i < parts.Length; i++)
            {
                if ((i > 0) && (i < parts.Length)) sb.Append(source[sb.Length]);
                if (parts[i].Length >= 2)
                {
                    sb.Append(string.Format("{0}{1}", parts[i][0], parts[i].Substring(1).ToLower()));
                }
                else
                    sb.Append(parts[i]);
            }

            return sb.ToString();
        }

        private XmlDocument LoadXML(string aURL)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, cert, chain, Errors) => true);
            //string emgURL = "https://www.ez-om.com/home.php";
            HttpWebRequest lHttpWebRequest;
            HttpWebResponse lHttpWebResponse;

            // First request, Login and get cookies
            lHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create("https://www.ez-om.com/home.php");
            SetCommonHeaders(lHttpWebRequest);
            lHttpWebRequest.Method = "POST";
            lHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            string lData = "login=bdentry&password=bdentry1&login_user=login_user&submit.x=21&submit.y=7";
            byte[] lBytes = System.Text.Encoding.UTF8.GetBytes(lData);
            lHttpWebRequest.ContentLength = lBytes.Length;
            lHttpWebRequest.GetRequestStream().Write(lBytes, 0, lBytes.Length);

            lHttpWebResponse = (HttpWebResponse)lHttpWebRequest.GetResponse();
            string lCookies = String.IsNullOrEmpty(lHttpWebResponse.Headers["Set-Cookie"]) ? "" : lHttpWebResponse.Headers["Set-Cookie"];
            lHttpWebResponse.Close();


            // Second request, without this request server return 302 on the next one
            lHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create("https://www.ez-om.com/home.php");
            SetCommonHeaders(lHttpWebRequest);
            lHttpWebRequest.Headers.Add("Cookie", lCookies);
            lHttpWebRequest.Method = "POST";
            lHttpWebRequest.AllowAutoRedirect = false;
            lHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            lData = "coID=25046&setActiveCo=1";
            lBytes = System.Text.Encoding.UTF8.GetBytes(lData);
            lHttpWebRequest.ContentLength = lBytes.Length;
            lHttpWebRequest.GetRequestStream().Write(lBytes, 0, lBytes.Length);

            lHttpWebResponse = (HttpWebResponse)lHttpWebRequest.GetResponse();
            lHttpWebResponse.Close();

            // Third request, get XML file
            lHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(aURL);
            SetCommonHeaders(lHttpWebRequest);
            lHttpWebRequest.Headers.Add("Cookie", lCookies);
            lHttpWebRequest.Method = "POST";
            lHttpWebRequest.AllowAutoRedirect = false;
            lBytes = System.Text.Encoding.UTF8.GetBytes(GetPostDataForOrderXMLRequest().ToCharArray());
            lHttpWebRequest.ContentLength = lBytes.Length;
            lHttpWebRequest.ContentType = "multipart/form-data; boundary=---------------------------7d936812a09f2";
            lHttpWebRequest.GetRequestStream().Write(lBytes, 0, lBytes.Length);

            lHttpWebResponse = (HttpWebResponse)lHttpWebRequest.GetResponse();

            XmlDocument XMLDoc = new XmlDocument();
            using (Stream responseStream = lHttpWebResponse.GetResponseStream())
            {
                if (lHttpWebResponse.ContentEncoding.Contains("gzip"))
                {                    
                    using (var gzip = new GZipStream(responseStream, CompressionMode.Decompress))
                    {
                        using (StreamReader streamReader = new StreamReader(gzip, Encoding.UTF8))
                        {
                            XMLDoc.Load(streamReader);
                        }
                    }
                }
                else
                {
                    using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        XMLDoc.Load(readStream);
                    }
                }
            }
            lHttpWebResponse.Close();

            return XMLDoc;
        }

        private void SetCommonHeaders(HttpWebRequest aHttpWebRequest)
        {
            aHttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1; .NET CLR 2.0.50727)";
            aHttpWebRequest.Headers.Add("Accept-Language", "en");
            aHttpWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            aHttpWebRequest.Accept = @"*/*";
        }

        private string GetPostDataForOrderXMLRequest()
        {
            StringBuilder sb = new StringBuilder("-----------------------------7d936812a09f2\n");
            sb.Append("Content-Disposition: form-data; name=\"MAX_FILE_SIZE\"\n\n");

            sb.Append("2097152\n");
            sb.Append("-----------------------------7d936812a09f2\n");
            sb.Append("Content-Disposition: form-data; name=\"csvFile\"; filename=\"\"\n");
            sb.Append("Content-Type: application/octet-stream\n\n\n");

            sb.Append("-----------------------------7d936812a09f2\n");
            sb.Append("Content-Disposition: form-data; name=\"profile\"\n\n");

            sb.Append("11\n");
            sb.Append("-----------------------------7d936812a09f2\n");
            sb.Append("Content-Disposition: form-data; name=\"showInv\"\n\n");

            sb.Append("0\n");
            sb.Append("-----------------------------7d936812a09f2\n");
            sb.Append("Content-Disposition: form-data; name=\"downloadFile\"\n\n");

            sb.Append("fullInv.xml\n");
            sb.Append("-----------------------------7d936812a09f2--\n");
            return sb.ToString();
        }

        private string GetHtml(string aIntroParagraph, List<string> aDescriptions)
        {
            StringBuilder lSB = new StringBuilder();
            lSB.Append(@"<!DOCTYPE HTML PUBLIC """"-//W3C//DTD HTML 3.2//EN"""">");
            lSB.Append("<html>");
            lSB.Append("<head>");
            lSB.Append("</head>");
            lSB.Append(@"<body bgcolor=""""#ffffff"""">");
            lSB.Append(@"<p style=""""font-family: Arial;""""><span ");
            lSB.Append(@"style=""""FONT-SIZE: 12pt;""""><font");
            lSB.Append(String.Format(@" size=""""2"""">{0}</font></span></p>", aIntroParagraph));            
            lSB.Append(@"<p style=""""font-family: Arial;""""><font size=""""2""""><span style=""""font-weight:bold;"""">Features:</span></font> </p>");
            lSB.Append(@"<ul style=""""font-family: Arial;"""">");
            foreach (string lDescr in aDescriptions)
            {
                lSB.Append("<li><font");
                lSB.Append(string.Format(@" size=""""2"""">{0}</font></li>", HttpUtility.HtmlEncode(lDescr)));
            }
            lSB.Append("</ul>");
            lSB.Append("</body>");
            lSB.Append("</html>");
            var res =  lSB.ToString();            
            return res;;
        }
        #endregion
    }

}//end namespace
