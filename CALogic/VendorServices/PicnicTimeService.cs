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


    public class PicnicTimeService:IVendorService
    {
        public Vendor VendorInfo { get; set; }

        private string _csvurl = "";//"http://www.picnictime.com/dealers/memberLogin.php";
        private string _excelUrl = "";//"http://www.picnictime.com/dealers/";
        private string _isexcelfile = "";//"63151";
       // private int _qtyForAvailable;// = 100;
        //private int _qtyForDate;// = -50;

        private string _localFolder = "";
        private string _localFile = "PicnicTime.xlsx";
        private string _localcsvFile = "PicnicTime.csv";

        private string _excelConStringName = "ExcelConStringIMEXwithoutHeader";

        private string _dropshipfee = "";
        
        /// <summary>
        /// 
        /// </summary>
        public PicnicTimeService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string csvfolder ="";
            string csvfilename="";
            string csvIsftp = "";

            new DAL().GetPicnicTimeInfo(out _csvurl,
                                        out _excelUrl,
                                        out _isexcelfile,
                                        out csvfolder,
                                        out csvfilename,
                                        out csvIsftp,
                                        out _dropshipfee);

            
            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";

            VendorInfo = new DAL().GetVendor((int)VendorName.PicnicTime);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Picnic Time"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForPicnicTime(false);
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForPicnicTime(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForPicnicTime(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Download excel and return datatables
            DownloadExcelFile();

            List<ErrorLog> errorList = new List<ErrorLog>();
            //DataTable dtProducts = GetPetraProductTable(ref errorList);
           

            DataTable dtProducts;
            if (_isexcelfile == "1")
                dtProducts = GetPicnicTimeProductTable();
            else
            {
                dtProducts = GetProductTableFromCsv(ref errorList);
                invUpdateSrcvDTO.ErrorLogDTO = errorList;
            }
            // DataTable dtDiscontinued = GetDiscontinuedTable();

            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_localFolder + _localFile, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            float fee = 0;
            if (!string.IsNullOrWhiteSpace(_dropshipfee))
                fee = Convert.ToSingle(_dropshipfee);

            //loop through the datarows and create dto's
            string strSku;
            string strQty;
            string strPrice;
            for (int x = 2; x < dtProducts.Rows.Count; x++)
            {
                if (dtProducts.Rows[x][1] != null)
                {
                    if (!String.IsNullOrEmpty(dtProducts.Rows[x][1].ToString()))
                    {
                        Inventory invDTO = new Inventory();
                        invDTO.UPC = dtProducts.Rows[x][3].ToString();
                        //test
                        strSku = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[x][1].ToString());
                        strQty = dtProducts.Rows[x][5].ToString().Trim();
                        strPrice = dtProducts.Rows[x][11].ToString().Replace("$", "").Trim();
                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[x][1].ToString());
                        invDTO.Qty = Convert.ToInt32( dtProducts.Rows[x][5].ToString().Trim());
                        
                        //invDTO.Price = null;
                        if (dtProducts.Rows[x][11] == null)
                            invDTO.Price = null;
                        else
                        {
                            try
                            {
                                invDTO.Price = fee + float.Parse(dtProducts.Rows[x][11].ToString().Replace("$", "").Trim());
                                invDTO.Price = (float)(Math.Round((double)invDTO.Price, 2));

                            }
                            catch (Exception ex)
                            {
                                invDTO.Price = null ;
                            }

                           // invDTO.DomesticShipping = Convert.ToDecimal(fee);
                        }

                        invDTO.RetailPrice = null;

                        try
                        {
                            invDTO.MAP = Convert.ToSingle(dtProducts.Rows[x][12].ToString().Replace("$", "").Trim()); ;
                        }
                        catch (Exception ex)
                        {
                            invDTO.MAP  = 0;
                        }

                        invDTO.Description = dtProducts.Rows[x][4].ToString().ToString();

                        lstEMGInventory.Add(invDTO);
                    }//end string empty check
                }//end null check

            }//end for each

            // Loop through discontinued items
           

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

 
  

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForPicnicTimeOLD(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Download excel and return datatables
            DownloadExcelFile();
            DataTable dtProducts = GetPicnicTimeProductTable();
            DataTable dtDiscontinued = GetDiscontinuedTable();

            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_localFolder + _localFile, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }


            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the datarows and create dto's
            for(int x=10; x<dtProducts.Rows.Count; x++)
            {
                if (dtProducts.Rows[x][0] != null)
                {
                    if (!String.IsNullOrEmpty(dtProducts.Rows[x][0].ToString()))
                    {
                        Inventory invDTO = new Inventory();
                        invDTO.UPC = "";
                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[x][0].ToString());
                        invDTO.Qty = Convert.ToInt32(dtProducts.Rows[x][3].ToString().Trim());
                        invDTO.Price = null;
                        invDTO.RetailPrice = null;
                        invDTO.MAP = 0;
                        invDTO.Description = dtProducts.Rows[x][2].ToString() +
                                               " - " + dtProducts.Rows[x][1].ToString();

                        lstEMGInventory.Add(invDTO);
                    }//end string empty check
                }//end null check

            }//end for each

            // Loop through discontinued items
            for (int i = 2; i < dtDiscontinued.Rows.Count; i++)
            {
                if (dtDiscontinued.Rows[i][0] != null)
                {
                    if (!String.IsNullOrEmpty(dtDiscontinued.Rows[i][0].ToString()))
                    {
                        
                        Inventory invDTO = new Inventory();
                        invDTO.UPC = "";
                        string discontinuedSku = dtDiscontinued.Rows[i][0].ToString();
                        if(discontinuedSku.EndsWith("-000-0"))
                            discontinuedSku = discontinuedSku.Substring(0, discontinuedSku.Length - "-000-0".Length);
                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, discontinuedSku);//.Replace("-","");
                        invDTO.Qty = -5000;
                        invDTO.Price = null;
                        invDTO.RetailPrice = null;
                        invDTO.MAP = 0;
                        invDTO.Description = dtDiscontinued.Rows[i][2].ToString() +
                                               " - " + dtDiscontinued.Rows[i][1].ToString();

                        lstEMGInventory.Add(invDTO);
                    }
                }
            }

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

 
  
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="doc"></param>
        ///// <returns></returns>
        //private string ExtractProductExcelFileURL(HtmlAgilityPack.HtmlNode bodyNode)
        //{
        //    XPathNavigator nav = bodyNode.CreateNavigator();
        //    XPathNodeIterator nodes = nav.Select("//a");

        //    string xlsPath = "";
        //    while (nodes.MoveNext())
        //    {
        //        string href = nodes.Current.GetAttribute("href", "");
        //        if (href.Length > 28 && href.Substring(0, 28) == "docs/stock_status_picnictime")
        //        {
        //            xlsPath = href;
                    
        //        }
        //    }//end while

        //    xlsPath = _excelUrl + xlsPath;
        //    return xlsPath;
        //}//end method

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="result"></param>
        ///// <returns></returns>
        //private HtmlAgilityPack.HtmlNode GetBodyNode(string result)
        //{
        //    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

        //    // There are various options, set as needed
        //    htmlDoc.OptionFixNestedTags = true;
        //    htmlDoc.LoadHtml(result);  
        //    if (htmlDoc.DocumentNode != null)
        //    {
        //       return htmlDoc.DocumentNode.SelectSingleNode("//body");
                
        //    }
        //    return null;
        //    //Sgml.SgmlReader sgmlReader = new SgmlReader();
        //    //sgmlReader.DocType = "HTML";
        //    //StringReader s = new StringReader(result);

        //    //sgmlReader.InputStream = s;

        //    //XmlDocument doc = new XmlDocument();
        //    //doc.PreserveWhitespace = true;
        //    //doc.XmlResolver = null;
        //    //doc.Load(sgmlReader);

        //    //return doc;
        //}//end method
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetPicnicTimeProductTable()
        {
            string excelConString = System.Configuration.ConfigurationManager.AppSettings[_excelConStringName];

            // create a connection to your excel file
            //OleDbConnection con = new OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", "Temp\\DressUpAmerica.xls"));
            OleDbConnection con = new OleDbConnection(String.Format(excelConString, _localFolder + _localFile));//_localFolder

            // create a new blank datatable
            DataTable dtSheets = new DataTable();

            // create a data adapter to select everything from the worksheet you want
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from [AllSKUs$]", con);
            
            // use the data adapter to fill the datatable
            da.Fill(dtSheets);

            return dtSheets;

        }//end method

        private DataTable GetDiscontinuedTable()
        {
            //get excel connection string
            string excelConString = System.Configuration.ConfigurationManager.AppSettings["ExcelConStringIMEX"];

            //Get datatable
            // create a connection to your excel file
            //OleDbConnection con = new OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", "Temp\\DressUpAmerica.xls"));
            OleDbConnection con = new OleDbConnection(String.Format(excelConString, _localFolder + _localFile));//_localFolder

            // create a new blank datatable
            DataTable dtSheets = new DataTable();

            // create a data adapter to select everything from the worksheet you want
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from [Discontinued$]", con);

            // use the data adapter to fill the datatable
            da.Fill(dtSheets);

            return dtSheets;
        }

        /// <summary>
        /// Method to access Dress Up America website and download 
        /// </summary>
        private void DownloadExcelFile()
        {
            string FileURL =_csvurl;
            if (_isexcelfile == "1")
                FileURL = _excelUrl;
            //test
            //excelFileURL = "https://s3.amazonaws.com/picnictime-resources/dealer-documents/product-information/Stock_Status_2016.csv";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(FileURL);
            req.Method = "GET";

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream responseStream = res.GetResponseStream();

            string saveTo = _localFolder;//"PicnicTime.xls";
            if (_isexcelfile == "1")
                saveTo += _localFile;
            else
                saveTo += _localcsvFile;

            //// create a write stream
            FileStream writeStream = new FileStream(saveTo, FileMode.Create, FileAccess.Write);
            //// write to the stream
            ReadWriteStream(responseStream, writeStream);


        }//end method

        public DataTable GetProductTableFromCsv(ref List<ErrorLog> errorlist)
        {
            // upcomment after
            //StreamReader sr = new StreamReader("E:\\papa\\2-15-15\\ChannelAdvisorSources\\output\\prodlist.csv");
            StreamReader sr = new StreamReader(_localFolder + _localcsvFile);
            // skip two ffirst lines
            sr.ReadLine();
            
            
            string[] stringSeparators = new string[] { "," };
            
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header.Replace("\"", ""));
            }

            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(stringSeparators, StringSplitOptions.None);
                DataRow dr = dt.NewRow();
                if (rows.Count() != headers.Length)
                {
                    ErrorLog err = new ErrorLog(0, "PicnicTime.csv record with PicnicTime SKU:" + rows[1] + " is not properly formated.");
                    errorlist.Add(err);
                    continue;
                }

                for (int i = 0; i < rows.Length; i++)
                {
                    dr[i] = rows[i].Replace("\"", "");
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
   

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //private DataTable GetRocklineProductTableOLD()
        //{
        //    //Download file first
        //    //DownloadExcelFile();
        //    GetInventoryExcel();
        //    //get excel connection string
        //    string excelConString = System.Configuration.ConfigurationManager.AppSettings["ExcelConStringIMEXwithoutHeader"];

        //    //Get datatable
        //    // create a connection to your excel file
        //    //OleDbConnection con = new OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", "Temp\\DressUpAmerica.xls"));
        //    OleDbConnection con = new OleDbConnection(String.Format(excelConString, _localFolder + "Inventory.csv"));

        //    // create a new blank datatable
        //    DataTable dtSheets = new DataTable();

        //    // create a data adapter to select everything from the worksheet you want
        //    OleDbDataAdapter da = new OleDbDataAdapter("Select * from Inventory.csv", con);

        //    // use the data adapter to fill the datatable
        //    da.Fill(dtSheets);

        //    return dtSheets;

        //}//end method



        /// <summary>
        /// 
        /// </summary>
        /// <param name="readStream"></param>
        /// <param name="writeStream"></param>
        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            //int Length = 256;
            int Length = 1256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }//end method

    }//end class
}//end namespace
