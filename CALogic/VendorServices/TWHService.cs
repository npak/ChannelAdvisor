﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using log4net;
using System.IO;

namespace ChannelAdvisor
{
    public class TWHService : IVendorService
    {
        public Vendor VendorInfo { get; set; }
        public readonly ILog log = LogManager.GetLogger(typeof(TWHService));

        string FTPServer;
        string login;
        string password;
        //string ftpfilename;
        string csvfolder;
        string csvfilename;
        string csvIsftp;
        private string dropshipfee;

        string localFolder;
        string filename;
        public TWHService()
        {
            DAL dal = new DAL();
            dal.GetTWHSettings(out FTPServer,
                                      out login,
                                      out password, out csvfolder, out csvfilename, out csvIsftp, out dropshipfee);

            VendorInfo = dal.GetVendor((int)VendorName.TWH);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for TWH"));

            localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";
        }

        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForWynit(false);
        }

        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileId)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForWynit(true);

            //Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileId, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;
        }

        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForWynit(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //if  GetInventoryExcel();
            List<ErrorLog> errorList = new List<ErrorLog>();
            DataTable inventory = GetProductTable(ref errorList);

            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO

                string vendorFile = CAUtil.SaveScvFileAsVendorFile(localFolder + filename, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            InventoryUpdateService invService = new InventoryUpdateService();

            BindingList<Inventory> lstSeawideInventory = new BindingList<Inventory>();
            int qty = 0;
            float fee = 0;
            if (!string.IsNullOrWhiteSpace(dropshipfee))
                fee = Convert.ToSingle(dropshipfee);
            foreach (DataRow row in inventory.Rows)
            {

                Inventory invDTO = new Inventory();
                invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, row["itemcode"].ToString());
                //upc
                if (row["upc"].ToString() != "")
                    invDTO.UPC  = row["upc"].ToString();
                
                //check for blank
                if (row["totalQty"].ToString()  != "")
                    invDTO.Qty = Convert.ToInt32(row["totalQty"].ToString());
                else
                    invDTO.Qty = 0;
 
                if (row["price"].ToString() != "")
                    invDTO.Price = (float)(Math.Round(Convert.ToDouble(row["price"].ToString()), 2)) + fee;
                else
                    invDTO.Price = 0;

                if (row["onlinemap"].ToString() != "")
                    invDTO.MAP = (float)(Math.Round(Convert.ToDouble(row["onlinemap"].ToString()), 2));
                else
                    invDTO.MAP = 0;
                //additional 
                invDTO.AdditionalFields.Add(row["fullVendor"].ToString());
                invDTO.AdditionalFields.Add(row["ohioQty"].ToString());
                invDTO.AdditionalFields.Add(row["floridaQty"].ToString());
                invDTO.AdditionalFields.Add(row["ean"].ToString());
                invDTO.AdditionalFields.Add(row["weight"].ToString());
                invDTO.AdditionalFields.Add(row["dimweight"].ToString());
                invDTO.AdditionalFields.Add(row["description"].ToString());
                invDTO.AdditionalFields.Add(row["vendorItemcode"].ToString());
                invDTO.AdditionalFields.Add(row["length"].ToString());
                invDTO.AdditionalFields.Add(row["width"].ToString());
                invDTO.AdditionalFields.Add(row["height"].ToString());
                invDTO.AdditionalFields.Add(row["image1"].ToString());

                lstSeawideInventory.Add(invDTO);
            }

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstSeawideInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;
        }

        public DataTable GetProductTable(ref List<ErrorLog> errorlist)
        {
            DataTable dt = new DataTable();
            if (GetInventoryCSV())
                {
                StreamReader sr = new StreamReader(localFolder + filename);
                
                // read headers
                string[] headers = sr.ReadLine().Split(',');
                
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

                        ErrorLog err = new ErrorLog(0, "Seawide.csv record with  SKU:" + rows[1] + " is not properly formated: ");
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
            else
                return dt;
        }

        private bool GetInventoryCSV()
        {
            FTP ftp = new FTP(FTPServer, login, password);
            // Get list of files
            IList<string> files = ftp.GetFiles();
            try
            {
                // Get inventory excel file
                //string filename = files.Single(f => f.Contains(ftpfilename.Replace(".csv","")) && f.EndsWith(".csv"));
                filename = files.Single(f => f.EndsWith(".csv"));
                // download inventory file
                ftp.DownloadFile(filename, localFolder);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //// this works, because the protocol is included in the string
        //Uri serverUri = new Uri(server);

        //// needs UriKind arg, or UriFormatException is thrown
        //Uri relativeUri = new Uri(relativePath, UriKind.Relative);

        //// Uri(Uri, Uri) is the preferred constructor in this case
        //Uri fullUri = new Uri(serverUri, relativeUri);
    }
}
