using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace ChannelAdvisor
{
    public class RJTService : IVendorService
    {
        private string fileName;

        public Vendor VendorInfo { get; set; }

        public RJTService()
        {
            VendorInfo = Vendor.Load((int)VendorName.RJT);
        }

        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return GetInventoryListForRJT(false);
        }

        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileId)
        {
            InventoryUpdateServiceDTO invUpdateSvcDTO = GetInventoryListForRJT(true);

            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileId, invUpdateSvcDTO);

            return invUpdateSvcDTO;
        }

        private InventoryUpdateServiceDTO GetInventoryListForRJT(bool isWinForm)
        {
            if (String.IsNullOrEmpty(fileName))
                return null;

            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            IList<DataTable> tables = GetRJTTables();

            BindingList<Inventory> list = new BindingList<Inventory>();
            InventoryUpdateService invService = new InventoryUpdateService();

            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save excel file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(fileName, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            foreach (DataTable dt in tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["Item"].ToString()))
                    {
                        Inventory inv = new Inventory();
                        inv.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dr["Item"].ToString());
                        
                        //float price;
                        //if (float.TryParse(dr["Price"].ToString(), out price))
                        //    inv.Price = price;
                        
                        inv.RetailPrice = null;

                        int quantity;
                        if (int.TryParse(dr[4].ToString(), out quantity))
                            inv.Qty = quantity;
                        list.Add(inv);
                    }
                }
            }

            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID, list, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;
        }

        private IList<DataTable> GetRJTTables()
        {
            fileName = GetFileName();
            if (String.IsNullOrEmpty(fileName)) 
                return null;

            IList<DataTable> result = new List<DataTable>();
            int tabsCount = ExcelUtils.GetNumberOfTabs(fileName, "ExcelConStringIMEX");
            for (int i = 0; i < tabsCount; i++)
            {
                string sheetName = ExcelUtils.GetSheetNameByIndex(fileName, "ExcelConStringIMEX", i);

                if (!sheetName.EndsWith("$")) continue;

                if (sheetName.Contains("Sheet") || sheetName.Contains("QuickBooks Export Tips") || (sheetName.Contains("RJT Stand Inventory")))
                    continue;

                result.Add(ExcelUtils.ReadExcelSheet(fileName, sheetName, "ExcelConStringIMEX"));
            }

            return result;
        }

        private string GetFileName()
        {
            string folder = new DAL().GetSettingValue("RJT_Folder");
            return FileAndDirectoryUtils.GetNewestFile(folder, "xls");
        }
    }
}
