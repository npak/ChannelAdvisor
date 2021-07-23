using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Linq;
using System.Net;
using System.Data.OleDb;
using System.ComponentModel;
using System.Data;
using Sgml;

namespace ChannelAdvisor
{

    public class PetraService:IVendorService
    {
        public Vendor VendorInfo { get; set; }
        private HttpWebRequest request;
        private CookieContainer cookieContainer = new CookieContainer();
       
        private string _ftpUrl = "";//"http://www.picnictime.com/dealers/memberLogin.php";
        //private string _excelUrl = "";//"http://www.picnictime.com/dealers/";
        private string _username = "";//"BAR1139";
        private string _password = "";//"63151";
        private string _ftpFilename = "";//"63151";

        private string _dropshipfee = "";
       
        private string _localFolder = "";
        private string _localFileName = "";
       
        /// <summary>
        /// 
        /// </summary>
        public PetraService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            new DAL().GetPetraInfo(out _ftpUrl,
                                        out _ftpFilename,
                                        out _username,
                                        out _password, out _dropshipfee);

            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";
            VendorInfo = new DAL().GetVendor((int)VendorName.Petra);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Petra"));
            _localFileName = "tempcsv" + VendorInfo.ID.ToString() + ".csv";

        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForPetra(false);
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForPetra(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForPetra(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();
            List<ErrorLog> errorList = new List<ErrorLog>();
            // upcomment after
            //DownloadCsvFile();
            GetInventoryCSV();
            DataTable dtProducts = GetPetraProductTable(ref errorList);

            invUpdateSrcvDTO.ErrorLogDTO = errorList;
            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_localFolder + _localFileName, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            // drop file on local server
            UpdateToFTP ftp = new UpdateToFTP(VendorInfo.ID);
            ftp.UploadFileAZ();

            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the datarows and create dto's
            float fee = 0;
            if (!string.IsNullOrWhiteSpace(_dropshipfee))
                fee = Convert.ToSingle(_dropshipfee);
           // string test = "";
            for(int x=0; x<dtProducts.Rows.Count; x++)
            {
                if (dtProducts.Rows[x][1] != null)
                {
                    if (!String.IsNullOrEmpty(dtProducts.Rows[x][1].ToString()))
                    {
                        Inventory invDTO = new Inventory();
                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[x][1].ToString());
                        invDTO.UPC = dtProducts.Rows[x][8].ToString();

                        invDTO.Qty = Convert.ToInt32(dtProducts.Rows[x]["AVAILABLE"]);//GetCorrectQty(dtProducts.Rows[x][3].ToString().Trim());

                        if (dtProducts.Rows[x]["Price"] == null)
                            invDTO.Price = null;
                        else
                        {
                            invDTO.Price = fee + float.Parse(dtProducts.Rows[x]["Price"].ToString());
                            invDTO.Price = (float)(Math.Round((double)invDTO.Price, 2));
                        }
                        invDTO.RetailPrice = null;
                        if (dtProducts.Rows[x][18] != null)
                        {
                            invDTO.MAP = Convert.ToSingle(Math.Round(double.Parse(dtProducts.Rows[x][18].ToString()), 2));
                        }
                        
                        lstEMGInventory.Add(invDTO);
                    }//end string empty check
                }//end null check

            }//end for each

            //0"VENDOR SKU",1"PETRA SKU",2"DESCRIPTION",3"AVAILABLE",4"PRICE",5"BRAND NAME",6"WEIGHT-UNPACKED",
            //7"PRODUCT CLASS",8"UPC",9"LONG DESC",10"KEYWORDS",11"SPECS",12"SUBCATEGORY",13"NOTES1",14"REFURB",15"LENGTH",
            //16"WIDTH",17"HEIGHT",18"MAP","RETURNABLE","ESTIMATED SHIP WEIGHT","NOTES2","SUBCATEGORY2","SUBCATEGORY3",
            //"MSRP","PO ETA DATE","WARRANTY","IMAGE URL" 

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        ////private bool doLogin(string uid, string pwd, string url)
        ////{
        ////    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        ////    // Create a request using a URL that can receive a post. 
        ////    request = (HttpWebRequest)HttpWebRequest.Create(url);
        ////    request.CookieContainer = cookieContainer;

        ////    // Set the Method property of the request to POST.
        ////    request.Method = "POST";

        ////    // Set the ContentType property of the WebRequest.
        ////    request.ContentType = "application/x-www-form-urlencoded";

        ////    // Create POST data and convert it to a byte array.
        ////    string postData = "username=" + uid + "&password=" + pwd;
        ////    byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        ////    // Set the ContentLength property of the WebRequest.
        ////    request.ContentLength = byteArray.Length;
        ////    request.KeepAlive = true;

        ////    // Get the request stream.
        ////    using (Stream dataStream = request.GetRequestStream())
        ////    {
        ////        dataStream.Write(byteArray, 0, byteArray.Length);
        ////    }

        ////    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        ////    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
        ////    response.Close();
        ////    return true;

        ////}

        ////private void downloadFile(string url, string fileName)
        ////{
        ////    // Create a request using a URL that can receive a post. 
        ////    request = (HttpWebRequest)HttpWebRequest.Create(url);
        ////    request.CookieContainer = cookieContainer;

        ////    // Set the Method property of the request to GET.
        ////    request.Method = "GET";

        ////    // Get the response.
        ////    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        ////    {
        ////        using (Stream responseStream = response.GetResponseStream())
        ////        {
        ////            using (StreamReader reader = new StreamReader(responseStream))
        ////            {
        ////                using (StreamWriter writer = new StreamWriter(fileName, false))
        ////                {
        ////                    writer.Write(reader.ReadToEnd());
        ////                    writer.Flush();
        ////                    writer.Close();
        ////                }
        ////            }
        ////            responseStream.Close();
        ////        }
        ////        response.Close();
        ////    }
        ////}

        /////// <summary>
        /////// Method to access Dress Up America website and download 
        /////// </summary>
        ////private void DownloadCsvFile()
        ////{

        ////    string saveTo = _localFolder + _localFileName;//"PicnicTime.xls";

        ////    if (doLogin(_username, _password, _url))
        ////        downloadFile(_excelUrl, saveTo);

        ////}//end method

        private bool GetInventoryCSV()
        {
            string url = "ftp://" + _ftpUrl;
            FTP ftp = new FTP(url, _username, _password, false);
            // Get list of files
            IList<string> files = ftp.GetFiles();
            try
            {
                // Get inventory excel file
                string filename = files.Single(f => f.Contains(_ftpFilename.Replace(".csv", "")) && f.EndsWith(".csv"));
                // download inventory file
                ftp.DownloadFile(filename, _localFileName, _localFolder);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable GetPetraProductTable(ref List<ErrorLog> errorlist)
        {
            //StreamReader sr = new StreamReader("E:\\papa\\2-15-15\\ChannelAdvisorSources\\output\\prodlist.csv");
            DataTable dt = new DataTable();

            using (StreamReader sr = new StreamReader(_localFolder + _localFileName))
            {
                // read headers
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header.Replace("\"", ""));
                }
                int cnt = 0;
                string[] stringSeparators = new string[] { "\",\"" };
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(stringSeparators, StringSplitOptions.None);
                    DataRow dr = dt.NewRow();
                    if (rows.Count() != headers.Length)
                    {
                        cnt++;

                        ErrorLog err = new ErrorLog(0, "Petra.csv record with Petra SKU:" + rows[1] + " is not properly formated: ");
                        errorlist.Add(err);
                        continue;
                    }
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i].Replace("\"", "");
                    }
                    dt.Rows.Add(dr);
                }
            } 

            return dt;
        }
   

    }//end class
}//end namespace
