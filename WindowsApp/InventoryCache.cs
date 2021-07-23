using System;
using System.Collections.Generic;
using System.IO;
using log4net;
//using ChannelAdvisor.LinnworksInventoryWS;
//using System.Configuration;

namespace ChannelAdvisor
{
    public static class InventoryCache
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(InventoryCache));
        private static List<StockItem> AllItems = new List<StockItem>();

        private static void GetPaths(out string path, out string inventoryPath)
        {
            path = Path.Combine(Path.GetTempPath(), "inventoryCache");
            inventoryPath = Path.Combine(path, "allItems");
        }

        public static void GetTempPaths(out string path, out string inventoryPath)
        {
            path = Path.Combine(Path.GetTempPath(), "inventoryCachetemp");
            inventoryPath = Path.Combine(path, "allItems");
        }

        public static void LoadCaches(out List<StockItem> allItems)
        {
            allItems = new List<StockItem>();

            string path;
            string inventoryPath;
            GetPaths(out path, out inventoryPath);

            if (!Directory.Exists(path))
            {
                return;
            }

            DirectoryInfo di = new DirectoryInfo(path);
            
            if (di.GetFiles().Length>0)
            {
                // check cache date expiration

                    try
                    {
                        int i = 0;
                        foreach (FileInfo file in di.GetFiles())
                        {
                            using (var stream = File.Open(inventoryPath+i.ToString(), FileMode.Open,FileAccess.ReadWrite ,FileShare.ReadWrite))
                            {
                                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                //Datas.AddRange(dataObjects);
                                allItems.AddRange( bformatter.Deserialize(stream) as List<StockItem>);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log.Debug(String.Format("Error occured while loading All Items. WinApp."), e);
                    }
            }

        }

        public static void SaveCaches(List<StockItem> allItems)
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
                int page = 40000;
                int del = cnt / page;
                int skip = 0;
            
                for (int i = 0; i < del; i++)
                {
                    //for (int j =del*i;)
                    skip = i * page;
                    alli = allItems.GetRange(skip, page);
                     SerializeList(alli,inventoryPath+i.ToString());
                    //using (Stream stream = File.Open(inventoryPath + i.ToString(), FileMode.Create))
                    //{
                    //    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    //    bformatter.Serialize(stream, alli);
                    //}+
                }

                if (del == 0)
                {
                    page = cnt;
                    skip = 0;
                    alli = allItems.GetRange(skip, page);
                    SerializeList(alli,inventoryPath+"0");
                }
                else
                {
                    skip = del * page;
                    page = cnt - del * page;
                    if (page > 0)
                    {
                         alli = allItems.GetRange(skip, page);
                        SerializeList(alli,inventoryPath+del.ToString());
                    }
                }
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