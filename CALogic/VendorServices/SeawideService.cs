using System;
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
    public class SeawideService : IVendorService
    {
        public Vendor VendorInfo { get; set; }
        public readonly ILog log = LogManager.GetLogger(typeof(SeawideService));

        string FTPServer;
        string login;
        string password;
        string ftpfilename;
        string csvfolder;
        string csvfilename;
        string csvIsftp;

        string localFolder;
        public SeawideService()
        {
            DAL dal = new DAL();
            dal.GetSeawideSettings(out FTPServer,
                                      out login,
                                      out password, out ftpfilename, out csvfolder, out csvfilename, out csvIsftp);


            VendorInfo = dal.GetVendor((int)VendorName.Seawide);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Seawide"));

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

                string vendorFile = CAUtil.SaveScvFileAsVendorFile(localFolder + ftpfilename, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            InventoryUpdateService invService = new InventoryUpdateService();

            BindingList<Inventory> lstSeawideInventory = new BindingList<Inventory>();
            int qty = 0;
            foreach (DataRow row in inventory.Rows)
            {

                Inventory invDTO = new Inventory();
                invDTO.UPC = row["UPC"].ToString();
                invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, row["DEALER_SKU"].ToString());

                //check for blank
                if (row["TOTAL_AVAILABLE"].ToString().Replace("\"", "") != "")
                    invDTO.Qty = Convert.ToInt32(row["TOTAL_AVAILABLE"].ToString().Replace("\"", ""));
                else
                    invDTO.Qty = 0;
 
                if (row["COST"].ToString().Replace("\"", "") != "")
                    invDTO.Price = (float)(Math.Round(Convert.ToDouble(row["COST"].ToString().Replace("\"", "")), 2));
                else
                    invDTO.Price = 0;

                if (row["MAP"].ToString().Replace("\"", "") != "")
                    invDTO.MAP = (float)(Math.Round(Convert.ToDouble(row["MAP"].ToString().Replace("\"", "")), 2));
                else
                    invDTO.MAP = 0;
                float invetoryMRP = row["MRP"].ToString().Replace("\"", "") == "" ? 0 : float.Parse(row["MRP"].ToString().Replace("\"", ""));
                if (invetoryMRP > invDTO.MAP)
                    invDTO.MAP = invetoryMRP;

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
                StreamReader sr = new StreamReader(localFolder + ftpfilename);
                
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
            FTP ftp = new FTP("ftp://" + FTPServer, login, password, true);
            // Get list of files
            IList<string> files = ftp.GetFiles();
            try
            {
                // Get inventory excel file
                string filename = files.Single(f => f.Contains(ftpfilename.Replace(".csv","")) && f.EndsWith(".csv"));
                // download inventory file
                ftp.DownloadFile(filename, localFolder);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
