using System;
using System.Collections.Generic;
using System.IO;
using log4net;
//using ChannelAdvisor.LinnworksInventoryWS;
using System.Windows.Forms;


namespace ChannelAdvisor.VendorServices
{
    
    public static class InventoryCache
    {
        public static int LoadAllItemsToTestFolder;
        public static int LoadAllItemsRunning;
        public static int CacheReading;

        public static readonly ILog log = LogManager.GetLogger(typeof(InventoryCache));
        private static List<StockItem> AllItems = new List<StockItem>();

        private static void GetPaths(out string path, out string inventoryPath)
        {
            //path = Path.Combine(Path.GetTempPath(), "inventoryCache");
            //inventoryPath = Path.Combine(path, "allItems");
            path = Path.Combine(Application.StartupPath, "inventoryCache");
            inventoryPath = Path.Combine(path, "allItems");
        }
        private static void GetTempPaths(out string path, out string inventoryPath)
        {
            path = Path.Combine(Application.StartupPath, "inventoryCachetemp");
            inventoryPath = Path.Combine(path, "allItems");
        }

        public static void LoadCaches(out List<StockItem> allItems)
        {
            allItems = new List<StockItem>();

            string path;
            string inventoryPath;
            GetPaths(out path, out inventoryPath);
            log.Debug("Cache folder: "+ path) ;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DirectoryInfo di = new DirectoryInfo(path);

            if (di.GetFiles().Length > 0)
            {
                // check cache date expiration
                List<StockItem> temp = new List<StockItem>();

                        try
                        {
                            foreach (FileInfo file in di.GetFiles())
                            {
                               
                                using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                                {
                                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                    allItems.AddRange(bformatter.Deserialize(stream) as List<StockItem>);
                                    log.Debug("Deserialized : " + allItems.Count.ToString());

                                }
                            }
                        }
                        catch (Exception e)
                        {
                            log.Debug(String.Format("Error occured while loading All Items"), e);
                        }

            }
            else
                log.Debug("Not found Cache file in the forder: " + path);

        }

        public static void SaveCaches(List<StockItem> allItems)
        {
            try
            {
                string path;
                string inventoryPath;
                // gwt pathh
                GetTempPaths(out path, out inventoryPath);
                // clear directory
                ClearFolder(path);

                int cnt = allItems.Count;
                List<StockItem> alli = new List<StockItem>();

                if (cnt > 0)
                {
                    int page = 1000000;
                    int del = cnt / page;
                    //int lastPage = cnt - del * page;
                    //if (lastPage > 0)
                    //    del++;

                    int skip = 0;

                    for (int i = 0; i < del; i++)
                    {
                        //for (int j =del*i;)
                        skip = i * page;
                        alli = allItems.GetRange(skip, page);
                        SerializeList(alli, inventoryPath + i.ToString());
                        //using (Stream stream = File.Open(inventoryPath + i.ToString(), FileMode.Create))
                        //{
                        //    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                        //    bformatter.Serialize(stream, alli);
                        //}
                    }

                    if (del == 0)
                    {
                        page = cnt;
                        skip = 0;
                        alli = allItems.GetRange(skip, page);
                        SerializeList(alli, inventoryPath + "0");
                    }
                    else
                    {
                        skip = del * page;
                        int pagelast = cnt - del * page;
                        if (pagelast > 0)
                        {
                            alli = allItems.GetRange(skip, pagelast);
                            SerializeList(alli, inventoryPath + del.ToString());
                        }
                    }

                    log.Debug("Cache saved in the folder  " + path);
                }
                else
                    log.Debug("While saving cache, linnworks items count = 0. ");
            }
            catch (Exception ex)
            {
                log.Debug("Saving cache error: " + ex.Message);

            }
        }

        private static void SerializeList(List<StockItem> alli, string inventoryPath)
        {

            using (var stream = File.Open(inventoryPath, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, alli);
            }

        }

        private static void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }
        }
      
    }
}