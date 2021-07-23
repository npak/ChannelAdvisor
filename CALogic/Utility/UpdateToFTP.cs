using System;
using System.Collections.Generic;
using System.Text;
using Sgml;
using System.Xml;
using System.IO;
using FileHelpers;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using log4net;
using System.Linq;
using ChannelAdvisor.VendorServices;

namespace ChannelAdvisor
{
    public class UpdateToFTP
    {

        //npak
        //save csv files in FTP. The sane logic like in app. All parameters filename, folders, isFTP are managed in the application.
        // 
        public readonly ILog log = LogManager.GetLogger(typeof(UpdateToFTP));

        public int VendorID { get; set; }
        public int ProfileID { get; set; }
        //public string ProfileName { get; set; }

        public DataTable Profiles { get; set; }

        public InventoryUpdateServiceDTO invUpdSrvcDTO { get; set; }
        AdditionalHeaders addHeaders;

        public string csvfolder = "";
        public string csvfilename = "";
        public string Prefixname = "";
        public string csvfilenameConverted = "";
        public string fileArchive = "";

        public string inFolder = "";
        public string outFolder = "";

        string csvIsftp = "0";

        // for AZ and etra
        string csvfilenameToSaveOnServer = "";

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateToFTP(int inv, int prof, InventoryUpdateServiceDTO invDTO)
        {
            DAL dal = new DAL();
            Profiles = dal.GetProfiles().Tables[0];

            VendorID = inv;
            ProfileID = prof;
            invUpdSrvcDTO = invDTO;
            Settings();
            addHeaders = new AdditionalHeaders(VendorID.ToString());

        }//end constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateToFTP(int inv)
        {
            DAL dal = new DAL();
            Profiles = dal.GetProfiles().Tables[0];
            VendorID = inv;
            Settings();
            addHeaders = new AdditionalHeaders(VendorID.ToString());

        }//end constructor

        private string GetTempName()
        {
            return "\\Temp\\tempcsv" + VendorID.ToString() + ".csv";
        }

        public List<ErrorLog> ExportCSV()
        {
            List<ErrorLog> errorLogList = new List<ErrorLog>();
            string tempPath = Application.StartupPath + GetTempName(); // "\\tempcsv.csv";
             
            try
            {
                int vendorId = this.VendorID;
                Vendor VendorInfo = new DAL().GetVendor(vendorId);
                StringBuilder sb = new StringBuilder();
                //Unsert header
                sb.Append(CSVExportRow.GetHeaderRow().UPC).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().Sku).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().Qty).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().Price).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().MarkupPercentage).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().MAP).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().DomesticShipping).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().MarkupPrice).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().RetailPrice).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().AvgShipCost).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().IsSetQtyT0Zero);
                // if we have additional fields
                if (addHeaders.HeadersList.Count >0)
                {
                    foreach(string  header in addHeaders.HeadersList)
                    {
                        sb.Append(",").Append(header);
                    } 
                }
                sb.AppendLine();

                if (VendorInfo.SetOutOfStockIfNotPresented && !string.IsNullOrWhiteSpace(VendorInfo.Label))
                {
                    if (WaitDialogWithWork.Current != null)
                        WaitDialogWithWork.Current.ShowMessage("Fetching data, please wait...");

                    var query =
                       (from inv in invUpdSrvcDTO.InventoryDTO
                        //where inv.LinnworksStockItemId != Guid.Empty
                        select new CSVExportRow()
                        {
                            UPC = inv.UPC,
                            Sku = inv.SKU,
                            Qty = inv.Qty == null ? "" : inv.Qty.ToString(),
                            Price = inv.Price == null ? "" : inv.Price.ToString(),
                            MarkupPercentage = inv.MarkupPercentage.ToString(),
                            MAP = inv.MAP.ToString(),
                            DomesticShipping = inv.DomesticShipping == null ? "" : inv.DomesticShipping.ToString(),
                            MarkupPrice = inv.MarkupPrice.ToString(),
                            RetailPrice = inv.RetailPrice == null ? "" : inv.RetailPrice.ToString(),
                            AvgShipCost = inv.AvrShiftCost == null ? "" : inv.AvrShiftCost.ToString(),
                            IsSetQtyT0Zero = inv.Category == "Discontinued" ? "IsSetQtyT0Zero" : "",
                            AdditionalFields = inv.AdditionalFields 
                        }).ToList<CSVExportRow>();

                    //Get missing itemsfrom the cache
                    var linnworksService = new ChannelAdvisor.VendorServices.LinnworksService();
                    //check cache to read linnworks catalog
                    List<StockItem> linnwork_catalog;
                    InventoryCache.LoadCaches(out linnwork_catalog);
                    linnworksService.AllItems = linnwork_catalog;
                    List<StockItem> missItems = linnworksService.GetMissingItems(invUpdSrvcDTO.InventoryDTO, VendorInfo.Label);

                    var query2 =
                       (from miss in missItems
                        select new CSVExportRow()
                        {
                            Sku = miss.SKU,
                            Qty = "0",
                            Price = "",
                            MarkupPercentage = "",
                            MAP = "",
                            DomesticShipping = "",
                            MarkupPrice = "",
                            RetailPrice = "",
                            AvgShipCost ="0",
                            IsSetQtyT0Zero = "Discontinued"
                        }).ToList<CSVExportRow>();

                    foreach (CSVExportRow row in query)
                    {
                        sb.Append(row.UPC).Append(',');
                        sb.Append(String.Format("\"{0}\"", row.Sku)).Append(',');
                        sb.Append(row.Qty).Append(',');
                        sb.Append(row.Price).Append(',');
                        sb.Append(row.MarkupPercentage).Append(',');
                        sb.Append(row.MAP).Append(',');
                        sb.Append(row.DomesticShipping).Append(',');
                        sb.Append(row.MarkupPrice).Append(',');
                        sb.Append(row.RetailPrice).Append(',');
                        sb.Append(row.AvgShipCost).Append(',');
                        sb.Append(row.IsSetQtyT0Zero) ;
                         
                        // if we have additional fields
                        if (addHeaders.HeadersList.Count > 0)
                        {
                            foreach (string field in row.AdditionalFields)
                            {
                                sb.Append(",").Append(String.Format("\"{0}\"", field));
                            }
                        }
                        sb.AppendLine();
                    }

                    foreach (CSVExportRow row in query2)
                    {
                        sb.Append(row.UPC).Append(',');
                        sb.Append(String.Format("\"{0}\"", row.Sku)).Append(',');
                        sb.Append(row.Qty).Append(',');
                        sb.Append(row.Price).Append(',');
                        sb.Append(row.MarkupPercentage).Append(',');
                        sb.Append(row.MAP).Append(',');
                        sb.Append(row.DomesticShipping).Append(',');
                        sb.Append(row.MarkupPrice).Append(',');
                        sb.Append(row.RetailPrice).Append(',');
                        sb.Append(row.AvgShipCost).Append(',');
                        sb.Append(row.IsSetQtyT0Zero);

                        if (addHeaders.HeadersList.Count > 0)
                        {
                            foreach (string field in row.AdditionalFields)
                            {
                                sb.Append(",").Append(String.Format("\"{0}\"", field));
                            }
                        }
                        sb.AppendLine();
                    }

                    // save file in temp

                    File.WriteAllText(tempPath, sb.ToString());
                }
                else
                {
                    string category = "";
                    foreach (Inventory inv in invUpdSrvcDTO.InventoryDTO)
                    {
                        sb.Append(inv.UPC).Append(',');
                        sb.Append(String.Format("\"{0}\"", inv.SKU)).Append(',');
                        sb.Append(inv.Qty).Append(',');
                        sb.Append(inv.Price).Append(',');
                        sb.Append(inv.MarkupPercentage).Append(',');
                        sb.Append(inv.MAP).Append(',');
                        sb.Append(inv.DomesticShipping).Append(',');
                        sb.Append(inv.MarkupPrice).Append(',');
                        sb.Append(inv.RetailPrice).Append(',');
                        //added avr
                        sb.Append("").Append(',');
                        category = inv.Category == "Discontinued" ? "Discontinued" : "";
                        sb.Append(category);

                        if (addHeaders.HeadersList.Count > 0)
                        {
                            foreach (string field in inv.AdditionalFields)
                            {
                                sb.Append(",").Append(String.Format("\"{0}\"", field));
                            }
                        }
                        sb.AppendLine();

                    }
                    // save file in temp
                    File.WriteAllText(tempPath, sb.ToString());
                }


                if (!UploadFile())
                    errorLogList.Add(new ErrorLog(1, "Error while trasfering csv file to FTP server. VendorId " + VendorID.ToString()));
 //               log.Info("The file was uploaded to FTP. VendorID " + VendorID.ToString());

            }
            catch (ArgumentOutOfRangeException ex)
            {
                log.Debug("Error while converting csv file to FTP(rangeex). Vendorid " + VendorID.ToString() +" : "+ ex.Message);
                errorLogList.Add(new ErrorLog(1, "Error while converting csv file to FTP(rangeex) : VendorID " + VendorID.ToString() + " : " + ex.Message));
            }
            catch (Exception ex)
            {
                log.Debug("Error while converting csv file to FTP(common). VendorID " + VendorID.ToString() + " : " + ex.Message);
                errorLogList.Add(new ErrorLog(1, "Error while converting csv file to FTP(common). VendorID  " + VendorID.ToString() + " : " + ex.Message));
            }
            finally
            {
                //delete temp file
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
            return errorLogList;
        }

        /// <summary>
        /// method that would set foldername, filename, is Saved on FTP
        /// </summary>
        public string Settings()
        {
            DAL dal = new DAL();
            csvIsftp = "0";
            switch (this.VendorID.ToString())
            {
                case "1":
                    csvfolder = dal.GetSettingValue("EMG_CSVFolder");
                    csvfilename = dal.GetSettingValue("EMG_CSVFIle");
                    csvIsftp = dal.GetSettingValue("EMG_CSVIsFTP") == "1" ? "1" : "0";
                    break;

                case "3":
                    csvfolder = dal.GetSettingValue("PCycle_CSVFolder");
                    csvfilename = dal.GetSettingValue("PCycle_CSVFIle");
                    csvIsftp = dal.GetSettingValue("PCycle_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "4":
                    csvfolder = dal.GetSettingValue("DUpAmerica_CSVFolder");
                    csvfilename = dal.GetSettingValue("DUpAmerica_CSVFIle");
                    csvIsftp = dal.GetSettingValue("DUpAmerica_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "5":
                    csvfolder = dal.GetSettingValue("PTime_CSVFolder");
                    csvfilename = dal.GetSettingValue("PTime_CSVFIle");
                    csvIsftp = dal.GetSettingValue("PTime_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "6":
                    csvfolder = dal.GetSettingValue("Sumdex_CSVFolder");
                    csvfilename = dal.GetSettingValue("Sumdex_CSVFile");
                    csvIsftp = dal.GetSettingValue("Sumdex_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "7":
                    csvfolder = dal.GetSettingValue("CWR_CSVFolder");
                    csvfilename = dal.GetSettingValue("CWR_CSVFIle");
                    csvIsftp = dal.GetSettingValue("CWR_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "10":
                    csvfolder = dal.GetSettingValue("KwikTek_CSVFolder");
                    csvfilename = dal.GetSettingValue("KwikTek_CSVFIle");
                    csvIsftp = dal.GetSettingValue("KwikTek_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "11":
                    csvfolder = dal.GetSettingValue("Rockline_CSVFolder");
                    csvfilename = dal.GetSettingValue("Rockline_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Rockline_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "12":
                    csvfolder = dal.GetSettingValue("Morris_CSVFolder");
                    csvfilename = dal.GetSettingValue("Morris_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Morris_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "13":
                    csvfolder = dal.GetSettingValue("Petra_CSVFolder");
                    csvfilenameToSaveOnServer = dal.GetSettingValue("Petra_CSVFIleToServer");
                    csvfilename = dal.GetSettingValue("Petra_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Petra_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "14":
                    csvfolder = dal.GetSettingValue("MorrisComplete_CSVFolder");
                    csvfilename = dal.GetSettingValue("MorrisComplete_CSVFIle");
                    csvIsftp = dal.GetSettingValue("MorrisComplete_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "15":
                    csvfolder = dal.GetSettingValue("Benchmark_CSVFolder");
                    csvfilename = dal.GetSettingValue("Benchmark_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Benchmark_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "16":
                    csvfolder = dal.GetSettingValue("MorrisNightly_CSVFolder");
                    csvfilename = dal.GetSettingValue("MorrisNightly_CSVFIle");
                    csvIsftp = dal.GetSettingValue("MorrisNightly_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "17":
                    csvfolder = dal.GetSettingValue("MorrisChanges_CSVFolder");
                    csvfilename = dal.GetSettingValue("MorrisChanges_CSVFIle");
                    csvIsftp = dal.GetSettingValue("MorrisChanges_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "18":
                    csvfolder = dal.GetSettingValue("AZ_CSVFolder");
                    csvfilename = dal.GetSettingValue("AZ_CSVFIle");
                    csvfilenameToSaveOnServer = dal.GetSettingValue("AZ_CSVFIleImage");
                    csvIsftp = dal.GetSettingValue("AZ_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "19":
                    csvfolder = dal.GetSettingValue("MorrisDailySummary_CSVFolder");
                    csvfilename = dal.GetSettingValue("MorrisDailySummary_CSVFIle");
                    csvIsftp = "1";
                    break;
                case "20":
                    csvfolder = dal.GetSettingValue("GreenSupply_CSVFolder");
                    csvfilename = dal.GetSettingValue("GreenSupply_CSVFIle");
                    csvIsftp = dal.GetSettingValue("GreenSupply_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "21":
                    csvfolder = dal.GetSettingValue("Viking_CSVFolder");
                    csvfilename = dal.GetSettingValue("Viking_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Viking_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "22":
                    csvfolder = dal.GetSettingValue("NearlyNatural_CSVFolder");
                    csvfilenameConverted = dal.GetSettingValue("NearlyNatural_CSVFIleOriginal");
                    csvfilename = dal.GetSettingValue("NearlyNatural_CSVFIle");
                    csvIsftp = dal.GetSettingValue("NearlyNatural_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "23":
                    csvfolder = dal.GetSettingValue("Moteng_CSVFolder");
                    csvfilenameConverted = dal.GetSettingValue("Moteng_CSVFIleConverted");
                    csvfilename = dal.GetSettingValue("Moteng_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Moteng_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "25":
                    csvfolder = dal.GetSettingValue("Petra_Order_Folder");
                    fileArchive = dal.GetSettingValue("Petra_Order_Archive");
                    //csvfilename = dal.GetSettingValue("Moteng_CSVFIle");
                    //csvIsftp = dal.GetSettingValue("Moteng_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "26":
                    inFolder = dal.GetSettingValue(DAL.CONST_PetraReformat_InFolder);
                    csvfolder = dal.GetSettingValue(DAL.CONST_PetraReformat_InFolder);
                    outFolder = dal.GetSettingValue(DAL.CONST_PetraReformat_OutFolder);
                    break;
                case "27":
                    csvfolder = dal.GetSettingValue(DAL.CONST_Seawide_CSVFolder);
                    csvfilename = dal.GetSettingValue(DAL.CONST_Seawide_CSVFIle);
                    csvIsftp = dal.GetSettingValue(DAL.CONST_Seawide_IsFTP);
                    break;
                case "28":
                    csvfolder = dal.GetSettingValue(DAL.CONST_TWH_CSVFolder);
                    csvfilename = dal.GetSettingValue(DAL.CONST_TWH_CSVFIle);
                    csvIsftp = dal.GetSettingValue(DAL.CONST_TWH_IsFTP) == "1" ? "1" : "0";
                    break;
                case "29":
                    csvfolder = dal.GetSettingValue("MorrisWeeklySummary_CSVFolder");
                    csvfilename = dal.GetSettingValue("MorrisWeeklySummary_CSVFIle");
                    csvIsftp = "1";
                    break;
                case "53":
                    csvfolder = dal.GetSettingValue("PetGear_CSVFolder");
                    csvfilename = dal.GetSettingValue("PetGear_CSVFIle");
                    csvIsftp = dal.GetSettingValue("PetGear_CSVIsFTP") == "1" ? "1" : "0";
                    break;

                case "61":
                    csvfolder = dal.GetSettingValue("OceanStar_CSVFolder");
                    csvfilename = dal.GetSettingValue("OceanStar_CSVFIle");
                    csvIsftp = dal.GetSettingValue("OceanStar_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                default:
                    //Other Vendors
                    csvfolder = dal.GetSettingValue("OtherVendors_CSVFolder");
                    csvfilename = dal.GetSettingValue("OtherVendors_CSVFIle");
                    csvIsftp = dal.GetSettingValue("OtherVendors_CSVIsFTP") == "1" ? "1" : "0";

                    break;
            }
            return csvIsftp;
        }//end method

        /// <summary>
        /// method  to export csv to ftp
        /// </summary>
        /// <returns></returns>
        public bool UploadFile()
        {

            try
            {
                string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
                string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
                string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();
               
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var tempPath = Path.GetDirectoryName(location) + GetTempName(); // "\\tempcsv.csv";

                using (StreamReader stream = new StreamReader(tempPath))
                {
                    byte[] buffer = Encoding.Default.GetBytes(stream.ReadToEnd());
                    var uur = "ftp://" + ftpAddress + "/" + csvfolder + "/" + GetPrefix() + csvfilename;
                    WebRequest request = WebRequest.Create(uur);
                        //("ftp://" + ftpAddress + "/" + csvfolder + "/" + GetPrefix()+ csvfilename);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(username, password);
                    Stream reqStream = request.GetRequestStream();
                    reqStream.Write(buffer, 0, buffer.Length);
                    reqStream.Close();
                }
                log.Info("The file " + GetPrefix() + csvfilename + " was uploaded to FTP. VendorID " + VendorID.ToString());

                return true;
            }
            catch (Exception ex)
            {
                log.Debug("Error uploading csv file for VendorID:" + this.VendorID.ToString() + " to FTP :" + ex.Message);
                return false;
            }
        }

        private string GetPrefix()
        {
            string value = "";
            string filter = "ID = " + ProfileID.ToString();
            DataRow[] dr = Profiles.Select(filter);
            if (dr.Length > 0 && dr[0]["Profile"].ToString() != "Linnworks")
                value = dr[0]["Profile"].ToString()+"-";
            return value; 
        }

        public bool UploadFileAZ()
        {
            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            var tempPath = Path.GetDirectoryName(location) + GetTempName(); // "\\tempcsv.csv";
            
            try
            {
                string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
                string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
                string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

                using (StreamReader stream = new StreamReader(tempPath))
                {
                    byte[] buffer = Encoding.Default.GetBytes(stream.ReadToEnd());
                    WebRequest request = WebRequest.Create("ftp://" + ftpAddress + "/" + csvfolder + "/" + GetPrefix()+ csvfilenameToSaveOnServer);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(username, password);
                    Stream reqStream = request.GetRequestStream();
                    reqStream.Write(buffer, 0, buffer.Length);
                    reqStream.Close();
                }
                log.Info("The file " + GetPrefix() + csvfilenameToSaveOnServer + " was uploaded to FTP. VendorID " + VendorID.ToString());

                return true;
            }
            catch (Exception ex)
            {
                log.Debug("Error uploading csv file for VendorID:" + this.VendorID.ToString() + " to FTP :" + ex.Message);
                return false;
            }
            finally
            {
                //delete temp file
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }

        /// <summary>
        /// method  to export csv to ftp
        /// </summary>
        /// <returns></returns>
        public bool UploadOriginalFile(string csvTempName, string csvName)
        {
            try
            {
                string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
                string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
                string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var tempPath = csvTempName;
                using (StreamReader stream = new StreamReader(tempPath))
                {
                    byte[] buffer = Encoding.Default.GetBytes(stream.ReadToEnd());
                    WebRequest request = WebRequest.Create("ftp://" + ftpAddress + "/" + csvfolder + "/" + csvName); // "AZ_UpdatedColumns.csv");
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(username, password);
                    Stream reqStream = request.GetRequestStream();
                    reqStream.Write(buffer, 0, buffer.Length);
                    reqStream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Debug("Error uploading original csv file for VendorID:" + this.VendorID.ToString() + " to FTP :" + ex.Message);
                return false;
            }
        }

        public bool UploadFile(string data)
        {

            try
            {
                string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
                string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
                string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

                byte[] buffer = Encoding.UTF8.GetBytes(data);

                //byte[] buffer = Encoding.Default.GetBytes(stream.ReadToEnd());
                WebRequest request = WebRequest.Create("ftp://" + ftpAddress + "/" + csvfolder + "/" + csvfilename);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();

                return true;
            }
            catch (Exception ex)
            {
                //log.Debug("Error uploading csv file for VendorID:" + this.VendorID.ToString() + " to FTP :" + ex.Message);
                return false;
            }
        }

        public void UploadFileFromString(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(str);
            string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
            string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
            string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

            //WebRequest request = WebRequest.Create("ftp://" + ftpAddress + "/" + csvfolder + "/" + csvfilename);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpAddress + "/" + csvfolder + "/" + csvfilenameConverted);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UsePassive = true;
            request.KeepAlive = true;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();

        }

    
        public List<ErrorLog> ExportAZCSV(DataTable dt, string skuPrefix)
        {
            List<ErrorLog> errorLogList = new List<ErrorLog>();

            string tempPath = Application.StartupPath + GetTempName(); // "\\tempcsv.csv";
            int colCount = 0;
            string filterValue = "";
            try
            {
                int vendorId = this.VendorID;
                Vendor VendorInfo = new DAL().GetVendor(vendorId);
                StringBuilder sb = new StringBuilder();
                //Unsert header
                sb.Append(CSVExportRow.GetHeaderRow().UPC).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().Sku).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().Qty).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().Price).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().MarkupPercentage).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().MAP).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().DomesticShipping).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().MarkupPrice).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().RetailPrice).Append(',');
                sb.Append(CSVExportRow.GetHeaderRow().IsSetQtyT0Zero);
                // add file headers
                colCount = dt.Columns.Count;
                for (int i = 0; i < colCount; i++)
                {
                    sb.Append(',').Append(dt.Columns[i].Caption);
                }
                sb.AppendLine();

                if (VendorInfo.SetOutOfStockIfNotPresented && !string.IsNullOrWhiteSpace(VendorInfo.Label))
                {
                    if (WaitDialogWithWork.Current != null)
                        WaitDialogWithWork.Current.ShowMessage("Fetching data, please wait...");

                    var query =
                       (from inv in invUpdSrvcDTO.InventoryDTO
                        //where inv.LinnworksStockItemId != Guid.Empty
                        select new CSVExportRow()
                        {
                            UPC = inv.UPC,
                            Sku = inv.SKU,
                            Qty = inv.Qty == null ? "" : inv.Qty.ToString(),
                            Price = inv.Price == null ? "" : inv.Price.ToString(),
                            MarkupPercentage = inv.MarkupPercentage.ToString(),
                            MAP = inv.MAP.ToString(),
                            DomesticShipping = inv.DomesticShipping == null ? "" : inv.DomesticShipping.ToString(),

                            MarkupPrice = inv.MarkupPrice.ToString(),
                            RetailPrice = inv.RetailPrice == null ? "" : inv.RetailPrice.ToString(),
                            IsSetQtyT0Zero = inv.Category == "Discontinued" ? "IsSetQtyT0Zero" : ""
                        }).ToList<CSVExportRow>();

                    //Get missing itemsfrom the cache
                    var linnworksService = new ChannelAdvisor.VendorServices.LinnworksService();
                    //check cache to read linnworks catalog
                    List<StockItem> linnwork_catalog;
                    InventoryCache.LoadCaches(out linnwork_catalog);
                    linnworksService.AllItems = linnwork_catalog;
                    List<StockItem> missItems = linnworksService.GetMissingItems(invUpdSrvcDTO.InventoryDTO, VendorInfo.Label);

                    var query2 =
                       (from miss in missItems
                        select new CSVExportRow()
                        {
                            Sku = miss.SKU,
                            Qty = "0",
                            Price = "",
                            MarkupPercentage = "",
                            MAP = "",
                            DomesticShipping = "",
                            MarkupPrice = "",
                            RetailPrice = "",
                            IsSetQtyT0Zero = "Discontinued"
                        }).ToList<CSVExportRow>();

                    foreach (CSVExportRow row in query)
                    {
                        sb.Append(row.UPC).Append(',');
                        sb.Append(String.Format("\"{0}\"", row.Sku)).Append(',');
                        sb.Append(row.Qty).Append(',');
                        sb.Append(row.Price).Append(',');
                        sb.Append(row.MarkupPercentage).Append(',');
                        sb.Append(row.MAP).Append(',');
                        sb.Append(row.DomesticShipping).Append(',');
                        sb.Append(row.MarkupPrice).Append(',');
                        sb.Append(row.RetailPrice).Append(',');
                        sb.Append(row.IsSetQtyT0Zero);

                        //find a row from csv 
                        filterValue = row.Sku.Substring(skuPrefix.Length).Replace("'","''").Trim();
                        if (filterValue.Length > 0)
                        {
                            DataRow[] dr = dt.Select("[Item Sku]='" + filterValue + "'");

                            if (dr.GetLength(0) > 0)
                            {
                                for (int i = 0; i < colCount; i++)
                                {
                                    if (i == 1 || i == 2 || i == 23 || i == 24)
                                        sb.Append(',').Append(String.Format("\"{0}\"", dr[0][i].ToString().Replace("\"", "\"\"")));
                                    else
                                        sb.Append(',').Append(dr[0][i].ToString());

                                }
                            }//end if
                            sb.AppendLine();
                        }
                    }

                    foreach (CSVExportRow row in query2)
                    {
                        sb.Append(row.UPC).Append(',');

                        sb.Append(String.Format("\"{0}\"", row.Sku)).Append(',');
                        // sb.Append(row.LinnworksStockItemId).Append(',');
                        sb.Append(row.Qty).Append(',');
                        sb.Append(row.Price).Append(',');
                        sb.Append(row.MarkupPercentage).Append(',');
                        sb.Append(row.MAP).Append(',');
                        sb.Append(row.DomesticShipping).Append(',');
                        sb.Append(row.MarkupPrice).Append(',');
                        sb.Append(row.RetailPrice).Append(',');
                        sb.Append(row.IsSetQtyT0Zero);

                        //find a row from csv
                         filterValue = row.Sku.Substring(skuPrefix.Length).Replace("'","''").Trim();
                         if (filterValue.Length > 0)
                         {
                             DataRow[] dr = dt.Select("[Item Sku]='" + filterValue + "'");
                   
                             if (dr.GetLength(0) > 0)
                             {
                                 //lstWeekDays.Items[x].Checked = Convert.ToBoolean(dr[0]["IsEnabled"]);
                                 for (int i = 0; i < colCount; i++)
                                 {
                                     if (i == 1 || i == 2 || i == 23 || i == 24)
                                         sb.Append(',').Append(String.Format("\"{0}\"", dr[0][i].ToString().Replace("\"", "\"\"")));
                                     else
                                         sb.Append(',').Append(dr[0][i].ToString());
                                 }
                             }//end if

                             sb.AppendLine();
                         }
                    }

                    File.WriteAllText(tempPath, sb.ToString());
                }
                else
                {
                    string category = "";
                    foreach (Inventory inv in invUpdSrvcDTO.InventoryDTO)
                    {
                        sb.Append(inv.UPC).Append(',');
                        sb.Append(String.Format("\"{0}\"", inv.SKU)).Append(',');
                        sb.Append(inv.Qty).Append(',');
                        sb.Append(inv.Price).Append(',');
                        sb.Append(inv.MarkupPercentage).Append(',');
                        sb.Append(inv.MAP).Append(',');
                        sb.Append(inv.DomesticShipping).Append(',');
                        sb.Append(inv.MarkupPrice).Append(',');
                        sb.Append(inv.RetailPrice).Append(',');
                        category = inv.Category == "Discontinued" ? "Discontinued" : "";
                        sb.Append(category);

                        //find a row from csv
                        filterValue = inv.SKU.Substring(skuPrefix.Length).Replace("'", "''").Trim();
                         if (filterValue.Length > 0)
                         {
                             DataRow[] dr = dt.Select("[Item Sku]='" + filterValue + "'");
                             if (dr.GetLength(0) > 0)
                             {
                                 for (int i = 0; i < colCount; i++)
                                 {
                                     if (i == 1 || i == 2 || i == 23 || i == 24)
                                         sb.Append(',').Append(String.Format("\"{0}\"", dr[0][i].ToString().Replace("\"", "\"\"")));
                                     else
                                         sb.Append(',').Append(dr[0][i].ToString());
                                 }
                             }//end if

                             sb.AppendLine();
                         }
                    }
                    // save file in temp
                    File.WriteAllText(tempPath, sb.ToString());
                }


                if (!UploadFileAZ())
                    errorLogList.Add(new ErrorLog(1, "Error while trasfering csv file to FTP server. VendorID "+VendorID.ToString() ));

            }
            catch (ArgumentOutOfRangeException ex)
            {
                log.Debug("Error while converting csv file to FTP(rangeex). VendorID " + VendorID.ToString() + " : " + ex.Message);
                errorLogList.Add(new ErrorLog(1, "Error while converting csv file to FTP(rangeex). VendorID " + VendorID.ToString() + ". SKU =" + filterValue+ " "  + ex.Message));
            }
            catch (Exception ex)
            {
                log.Debug("Error while converting csv file to FTP(common). VendorID " + VendorID.ToString() + ". SKU =" + filterValue+ " " + ex.Message);
                errorLogList.Add(new ErrorLog(1, "Error while converting csv file to FTP(common). VendorID " + VendorID.ToString() + " : " + ex.Message));
            }
            finally
            {
                //delete temp file
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
            return errorLogList;
        }

    }//end class

}//end namespace
