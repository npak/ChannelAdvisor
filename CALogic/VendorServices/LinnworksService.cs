using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
//using ChannelAdvisor.LinnworksInventoryWS;
using System.ComponentModel;
using System.IO;
using System.Xml;
using log4net;
using System.Configuration;
using System.Net;

namespace ChannelAdvisor.VendorServices
{
    public class LinnworksService
    {
        public readonly ILog log = LogManager.GetLogger(typeof(LinnworksService));
        public List<StockItem> AllItems = new List<StockItem>();
        private static Object sync = new Object();
        private static Object syncRun = new Object();
        string url;
        string token;
        public LinnworksService()
        {
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.Config.XmlConfigurator.Configure();
            var dal = new DAL();
            var linnworksProfile = (from row in dal.GetProfiles().Tables[0].AsEnumerable() where ((string)row["Profile"]) == "Linnworks" select row).FirstOrDefault();
            if (linnworksProfile == null)
                throw new Exception("Linnworks profile not found");
            token = (string)linnworksProfile["ProfileAPIKey"];
            var urlrow = (from row in dal.GetWebServices().Tables[0].AsEnumerable() where ((string)row["Service"]) == "Linnworks" select row).FirstOrDefault();
            if (urlrow == null)
                throw new Exception("Could not find url for linnworks inventory web service");
            url = (string)urlrow["ServiceURL"];
            if (string.IsNullOrEmpty(url))
                throw new Exception("linnworks service url is null or empty");

            //AllItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockItem>>(File.ReadAllText("linnworksallitems.txt"));

        }

        public void ExtendWithDataFromLinnworks(InventoryUpdateServiceDTO serviceDto, Inventory inventoryItem)
        {
            {
                //InventoryCache.LoadCaches(out AllItems);
                if (AllItems.Count == 0)
                {
                    //log.Debug("linnworks items count =0.  Extending.");
                    return;

                }
            }

            var items = (from item in AllItems where item.SKU == inventoryItem.SKU select item).ToArray();

            if (items == null || items.Length == 0)
            {
                serviceDto.AddErrorLog("Could not find item in cache with SKU: " + inventoryItem.SKU);
                return;
            }
            if (items.Length > 1)
            {
                serviceDto.AddErrorLog("Found more then one item in cache with SKU: " + inventoryItem.SKU + ". This is unexpected, nothing is done with vendor item in such sitation");
                return;
            }

            if (items[0].DomesticShipping != null)
                inventoryItem.DomesticShipping = items[0].DomesticShipping;

        }

        //  public List<StockItem> GetMissingItems(InventoryUpdateServiceDTO serviceDto, BindingList<Inventory> invDTOList, string category)
        public List<StockItem> GetMissingItems(BindingList<Inventory> invDTOList, string category)

        {
            //lock (AllItems)
            {
                if (AllItems.Count == 0)
                {
                    return null;
                }
            }

            var missitems =
              from item in AllItems
              where item.Category == category && !(
                  from item1 in invDTOList
                  select item1.SKU
                ).Contains(item.SKU)
              select item;

            return missitems.ToList();
        }

        private void TryRun(Action action)
        {
            lock (syncRun)
            {
                int attempt = 1;
                bool repeat = false;
                do
                {
                    if (repeat)
                        Thread.Sleep(61 * 1000);
                    try
                    {
                        //log.DebugFormat("Calling Linnworks from {0}.", Process.GetCurrentProcess().ProcessName);
                        action.Invoke();
                        repeat = false;
                        if (attempt > 1)
                            log.Debug(String.Format("{0} attempt was successfull!", attempt));
                    }
                    catch (InvalidOperationException e)
                    {
                        log.Info("Error not related to timing during API call, there will be no repeated attempts: ", e);
                        throw;
                    }
                    catch (Exception e)
                    {
                        if (repeat && attempt > 2)
                            throw;
                        repeat = true;
                        attempt++;
                        log.Debug(String.Format("Error during API call, trying {0} time.", attempt), e);
                    }
                } while (repeat);
            }
        }

        private List<StockItem> DownloadAllItems()
        {
            lock (sync)
            {
                var result = new List<StockItem>();
                List<ErrorLog> errorLogList = new List<ErrorLog>();
                try
                {
                    try
                    {
                        if (WaitDialogWithWork.Current != null)
                            WaitDialogWithWork.Current.ShowMessage(string.Format("Downloading cache, please wait...", result.Count));

                        result = GetCacheList(ref errorLogList);

                        //fores
                        // log.Debug("cache fetch count : " + result.Count.ToString());
                        // Log error items.
                        AddCacheBadItemsToLog(errorLogList);
                    }
                    catch (Exception ex)
                    {
                        log.Info("DownloadAllItems: error reading cache, continuing the processing!", ex);
                    }
                }
                catch (Exception e)
                {
                    log.Debug("DownloadAllItems", e);
                    throw;
                }


                InventoryCache.SaveCaches(result);
                return result;
            }
        }

        public List<StockItem> RunDownloadAllItems()
        {
            return DownloadAllItems();
        }

        private string GetTempName()
        {
            return "\\Temp\\";
        }

        private void AddCacheBadItemsToLog(List<ErrorLog> errlist)
        {
            //var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            //var tempPath = Path.GetDirectoryName(location) + GetTempName()+"CAServiceCacheErrorItems.txt"; // "\\tempcsv.csv";
            ////pathtemp = path + "temp";
            log.Debug("List of Bad formatted items from ftp cache.csv");
            foreach (var err in errlist)
            {
                log.Debug(err.ErrorDesc);
            }
            log.Debug("End of List of Bad formatted items from ftp cache.csv");
        }
        /// <summary>
        /// method  to export csv to ftp
        /// </summary>
        /// <returns></returns>
        public bool GetCacheCsvFile()
        {
            try
            {
                string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
                string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
                string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

                string csvfolder = ConfigurationManager.AppSettings["cachepath"].ToString();
                string csvfilename = ConfigurationManager.AppSettings["cacheFileName"].ToString();

                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var tempPath = Path.GetDirectoryName(location) + GetTempName(); // "\\tempcsv.csv";
                //test

                FTP ftp = new FTP("ftp://" + ftpAddress + "/" + csvfolder + "/", username, password, true);

                // download inventory file
                ftp.WebClientDownloadFile(csvfilename, tempPath);
                //.DownloadFile(csvfilename, tempPath);
                log.Debug("Has downloaded cache csv file from FTP");
                return true;
            }
            catch (Exception ex)
            {
                log.Debug("Error downloading cache csv file from FTP :" + ex.Message);
                return false;
            }
        }

        public List<StockItem> GetCacheList(ref List<ErrorLog> errorLogList)
        {
            List<StockItem> result = new List<StockItem>();

            if (GetCacheCsvFile())
            {
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                string csvfilename = ConfigurationManager.AppSettings["cacheFileName"].ToString();

                var tempPath = Path.GetDirectoryName(location) + GetTempName() + csvfilename;
                log.Debug("Start reading downloaded cache csv.tempPath: " + tempPath);
                using (StreamReader sr = new StreamReader(tempPath))
                {
                    StockItem row;

                    try
                    {
                        // read headers
                        string[] headers = sr.ReadLine().Split(',');

                        int cnt = 0;

                        string[] stringSeparators = new string[] { "\",\"" };
                        log.Debug("Start reading downloaded cache csv.");
                        string[] rows;
                        decimal number3 = 0;

                        while (!sr.EndOfStream)
                        {
                            rows = sr.ReadLine().Split(stringSeparators, StringSplitOptions.None);
                            cnt++;
                            if (rows.Count() != headers.Length)
                            {
                                ErrorLog err = new ErrorLog(1, "cache.csv  line:" + cnt.ToString() + " is not properly formated: " + string.Join(",", rows));
                                errorLogList.Add(err);
                                continue;
                            }

                            string strDomestic = "";

                            row = new StockItem();
                            if (string.IsNullOrWhiteSpace(rows[0]))
                                row.SKU = "";
                            else
                                row.SKU = (rows[0].Substring(1));

                            if (string.IsNullOrWhiteSpace(rows[1]))
                                row.Title = "";
                            else
                                row.Title = rows[1];

                            if (string.IsNullOrWhiteSpace(rows[2]))
                                row.Category = "";
                            else
                                row.Category = rows[2];

                            if (rows[3] != null)
                            {
                                strDomestic = rows[3].Replace("\"", "").Replace("$", "");

                                if (string.IsNullOrWhiteSpace(strDomestic))
                                    row.DomesticShipping = null;
                                else
                                {
                                    if (decimal.TryParse(strDomestic, out number3))
                                        row.DomesticShipping = number3;
                                    else
                                    {
                                        ErrorLog err = new ErrorLog(1, "cache.csv  line:" + cnt.ToString() + " is not properly formated: " + string.Join(",", rows));
                                        errorLogList.Add(err);
                                        row.DomesticShipping = null;
                                    }
                                }
                            }
                            result.Add(row);
                        }

                        log.Debug("Items count with dublicate :" + result.Count.ToString());
                        // remove dublicate
                        return RemoveDublicateSku(result);
                    }
                    catch (Exception ex)
                    {
                        log.Debug("Error reading cache csv file from temp :" + ex.Message);
                        return result;
                    }
                }
            }
            return result;
        }

        private List<StockItem> RemoveDublicateSku(List<StockItem> dublist)
        {
            return dublist.GroupBy(s => s.SKU).Select(x => x.First()).ToList();
        }

        private List<StockItem> RemoveDublicateSkuOLd(List<StockItem> dublist)
        {
            Dictionary<string, StockItem> dic = new Dictionary<string, StockItem>();
            foreach (var item in dublist)
            {
                if (item.SKU == "\"mrs-82001\"")
                {
                    log.Debug("Find TEST TEST sku :" + item.SKU);
                    if (dic.ContainsKey(item.SKU))
                        log.Debug("Contains TEST TEST sku :" + item.SKU);
                    else
                        log.Debug("Not contains TEST TEST sku :" + item.SKU);
                }
                if (dic.ContainsKey(item.SKU))
                    continue;
                dic[item.SKU] = item;

            }

            List<StockItem> filteredList = new List<StockItem>(dic.Values);
            return filteredList;
        }

    }
}
