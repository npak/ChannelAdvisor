using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ChannelAdvisor
{
    public class OceanStarService : IVendorService
    {
        public Vendor VendorInfo { get; set; }

        public string FTPServer { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        private string LocalFolder { get; set; }

        public OceanStarService()
        {
            DAL dal = new DAL();

            FTPServer = dal.GetSettingValue("OceanStar_FTPServer");
            Login = dal.GetSettingValue("OceanStar_Login");
            Password = dal.GetSettingValue("OceanStar_Password");

            VendorInfo = dal.GetVendor((int)VendorName.OceanStar);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for OceanStar"));

            LocalFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";
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

            string filename = GetInventoryExcel();
            DataTable inventory = ExcelUtils.ReadExcelSheet(filename, "INVENTORY$", "ExcelConString");

            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(filename, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            InventoryUpdateService invService = new InventoryUpdateService();

            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();
            int qty = 0;
            foreach (DataRow row in inventory.Rows)
            {
                if ((row[1] == DBNull.Value) || (row[2] == DBNull.Value)
                    || (row[3] == DBNull.Value))
                {
                    continue;
                }

                // just pass along whatever quantity they have

               
                ////availability
                //if (row[3].ToString() != "50")
                //{
                //    qty = -50;
                //}
                if (row[3] != null)
                    qty = Convert.ToInt32(row[3].ToString());


                lstEMGInventory.Add(new Inventory()
                {
                    UPC = "",
                    SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, row[2].ToString().Trim()),
                    Description = "",
                    Qty = qty
                });
            }

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;
        }

        private string GetInventoryExcel()
        {
            FTP ftp = new FTP(FTPServer, Login, Password, true);
            // Get list of files
            IList<string> files = ftp.GetFiles();
            // Get inventory excel file
            string filename = files.Single(f => f.Contains("INVENTORY") && f.EndsWith(".xls"));
            // download inventory file
            ftp.DownloadFile(filename, LocalFolder);

            return string.Format(@"{0}{1}", LocalFolder, filename);
        }
    }
}
