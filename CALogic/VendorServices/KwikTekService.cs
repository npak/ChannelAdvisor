using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace ChannelAdvisor
{
    public class KwikTekService : IVendorService
    {
        private readonly ILog log = LogManager.GetLogger(typeof(FTP));

        public Vendor VendorInfo { get; set; }

        public string FTPServer { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        private string LocalFolder { get; set; }

        public KwikTekService()
        {
            DAL dal = new DAL();

            FTPServer = dal.GetSettingValue("KwikTek_FTPServer");
            Login = dal.GetSettingValue("KwikTek_Login");
            Password = dal.GetSettingValue("KwikTek_Password");

            VendorInfo = dal.GetVendor((int)VendorName.KwikTek);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for KwikTek"));

            LocalFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";
        }

        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForKwikTek(false);
        }

        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileId)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForKwikTek(true);

            if (invUpdateSrcvDTO == null)
                return null;

            //Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileId, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;
        }

        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForKwikTek(bool isWinForm)
        {
            var filename = GetInventoryExcel();

            if (String.IsNullOrEmpty(filename))
            {
                if (isWinForm)
                    MessageBox.Show("File 'Kwik Tek Inventory*.xlsx' not exists of ftp.");
                
                return null;
            }
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            log.Info(String.Format("KwikTek inventory file name{0}", filename));
            
            DataTable inventory = ExcelUtils.ReadExcelSheet(filename, "Sheet1$", "Excel2007ConString");

            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(filename, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            var invService = new InventoryUpdateService();

            var lstEMGInventory = new BindingList<Inventory>();

            var inven = new Inventory();

           
            foreach (DataRow row in inventory.Rows)
            {
                if ((row[0] == DBNull.Value) || (row[4] == DBNull.Value)
                    || (row[1] == DBNull.Value) || (row[3] == DBNull.Value)) 
                    continue;

                    lstEMGInventory.Add(new Inventory()
                    {
                        UPC = row[3].ToString(),
                        SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, row[0].ToString()),
                        Description = row[1].ToString(),
                        Qty = Convert.ToInt32(row[4]) < 0 ? 0 : Convert.ToInt32(row[4])
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
            FTP ftp = new FTP(FTPServer, Login, Password);
            // Get list of files
            IList<string> files = ftp.GetFiles();
            // Get inventory excel file
            
            string filename = files.Where(f => f.Contains("Kwik Tek Inventory") && f.EndsWith(".xlsx")).FirstOrDefault();
            
            if (String.IsNullOrEmpty(filename))
                return null;

            // download inventory file
            ftp.DownloadFile(filename, LocalFolder);
            return string.Format(@"{0}{1}", LocalFolder, filename);
        }
    }
}
