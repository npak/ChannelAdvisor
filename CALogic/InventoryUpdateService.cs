using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Data;
using System.ComponentModel;
using ChannelAdvisor.VendorServices;


namespace ChannelAdvisor
{
    public class InventoryUpdateServiceDTO
    {
        public string VendorFile = null;
        public string CAFile = null;
        public SortableBindingList<Inventory> InventoryDTO = new SortableBindingList<Inventory>();
        public List<ErrorLog> ErrorLogDTO = new List<ErrorLog>();
        public List<string> ToDeleteFiles = new List<string>();
        public string SuccesMessage;
        public bool WithoutResult = false;

        public void AddErrorLog(string errorDesc)
        {
            ErrorLog errorLog = new ErrorLog(0, errorDesc);
            ErrorLogDTO.Add(errorLog);
        }//end method

    }//end class

    [Serializable]
    public class ErrorLog
    {
        public string _errorDesc;
        public int _errorType = 0; //0 for local errors, 1 for CA errors
        /// <summary>
        /// 
        /// </summary>
        public string ErrorDesc
        {
            get
            {
                return _errorDesc;
            }
            set
            {
                _errorDesc = value;
            }
        }//end property

        public int ErrorType
        {
            get
            {
                return _errorType;
            }
            set
            {
                _errorType = value;
            }
        }//end property

        public ErrorLog(int errorType, string errorDesc)
        {
            _errorType = errorType;
            _errorDesc = errorDesc;
        }//end constructor

    }//end class

    public class InventoryUpdateService
    {
        DAL dal = new DAL();

        private int unavailableThreshold = -100;
        
        public List<String> errorLog;
        //InventoryUpdateServiceDTO invUpdDTO;

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="vendorID"></param>
        ///// <param name="profileID"></param>
        //public InventoryUpdateService(int vendorID, int profileID, InventoryUpdateServiceDTO inventoryUpdateServiceDTO)
        //{
        //    dal = new DAL();
        //    addedSKUs = new List<string>();
        //    errorLog = new List<string>();

        //    //Get Pricing Markups
        //    dtMarkups = dal.GetPricingMarkup(vendorID, profileID).Tables[0];
            
        //    //Duplicate SKU's
        //    dtDuplicateSKUs = dal.GetDuplicateSKUs(vendorID).Tables[0];

        //    invUpdDTO = inventoryUpdateServiceDTO;

        //    GetNegativeQtyCheck();
        //}//end constructor

        ///// <summary>
        ///// Method to get negative qty check from database
        ///// </summary>
        //private void GetNegativeQtyCheck()
        //{
        //    negativeQtyForZeroCheck = Int32.Parse(dal.GetSettingValue(SettingsConstant.Inventory_Negative_QTY_Check));

        //    negativeQtyForZero = Int32.Parse(dal.GetSettingValue(SettingsConstant.Inventory_Negative_QTY));

        //}//end method

        ///// <summary>
        ///// Method that would do all the validations, add markup price
        ///// and add invalid skus to the error log
        ///// </summary>
        ///// <param name="invDTO"></param>
        ///// <returns></returns>
        //public void CheckAndUpdateInventory(InventoryDTO invDTO)
        //{
        //    //Replace new SKU if found
        //    CheckForNewSKU(invDTO);

        //    //Check for duplicate SKU
        //    if (!IsDuplicateOrInvalidSKU(invDTO))
        //    {
        //        //Calculate the markup price
        //        GetMarkupPrice(invDTO);

        //        //Negative Qty
        //        SetNegativeQtyForZero(invDTO);

        //        invUpdDTO.InventoryDTO.Add(invDTO);
                
        //    }//end if
            

        //}//end method

        public List<StockItem> GetCache()
        {
            var linnworksService = new ChannelAdvisor.VendorServices.LinnworksService();
            //check cache to read linnworks catalog
            List<StockItem> linnwork_catalog = new List<StockItem>();
            InventoryCache.LoadCaches(out linnwork_catalog);
           
            return linnwork_catalog;
        }

        /// <summary>
        /// This method will loop through invDTOList. It will add valid SKU's to invUpdateSrcvDTO
        /// and the invalid ones to Error Log
        /// </summary>
        /// <param name="invDTOList"></param>
        /// <param name="invUpdateSrcvDTO"></param>
        public void CreateInventoryDTOListOfValidSKUs(int vendorID,
                                                BindingList<Inventory> invDTOList, 
                                                InventoryUpdateServiceDTO invUpdateSrcvDTO)
        {
            Vendor vendorInfo = dal.GetVendor(vendorID);

            //Get Blocked SKU's table
            DataTable dtBlockedSKUs = dal.GetBlockedSKUs().Tables[0];

            //Get Duplicate SKU's table
            DataTable dtDuplicateSKUs = dal.GetDuplicateSKUs(vendorID).Tables[0];

            //List to maintain already added SKU's
            List<String> addedSKUs = new List<string>();

            //Negative Qty check
            int negativeQtyForZero = vendorInfo.OutOfStockQuantity.HasValue ? vendorInfo.OutOfStockQuantity.Value :
                Int32.Parse(dal.GetSettingValue(SettingsConstant.Inventory_Negative_QTY));
            int negativeQtyForZeroCheck = vendorInfo.OutOfStockThreshold.HasValue ? vendorInfo.OutOfStockThreshold.Value :
                Int32.Parse(dal.GetSettingValue(SettingsConstant.Inventory_Negative_QTY_Check));
            

            //loop the InventoryDTO list
            for (int x = 0; x < invDTOList.Count; x++)
            {
                //Replace new SKU if found
                CheckForNewSKU(invDTOList[x], dtDuplicateSKUs);

                //Check for duplicate and blank SKU and add to error log if found
                if (!IsDuplicateOrBlankSKU(invDTOList[x], addedSKUs, invUpdateSrcvDTO))
                {
                    //Negative Qty if less than 10 or as configured by the user
                    if (vendorID !=53)
                        SetNegativeQtyForZero(invDTOList[x], negativeQtyForZeroCheck, negativeQtyForZero);

                    // if SetOutOfStockIfNotPresented = true
                    //if (vendorInfo.SetOutOfStockIfNotPresented && string.IsNullOrWhiteSpace(invDTOList[x].SKU))
                    //{
                    //    invDTOList[x].Qty = 0;
                    //}
                    //Everything checks out. Add the DTO to InventoryUpdateServiceDTO
                    invUpdateSrcvDTO.InventoryDTO.Add(invDTOList[x]);

                }//end if

            }//end for

            // blocked SKUs
            CheckForBlockedSKUs(invDTOList, dtBlockedSKUs);

        }//end method


        /// <summary>
        /// This will check whether there is a new sku for UPC-SKU combination 
        /// and if found update the new sku
        /// </summary>
        /// <param name="invDTO"></param>
        private void CheckForNewSKU(Inventory invDTO, DataTable dtDuplicateSKUs)
        {
            //Search datatable
            DataRow[] dr = dtDuplicateSKUs.Select("UPC = '" + invDTO.UPC + "' AND SKU='" + invDTO.SKU.Replace("'","''") + "'");

            if (dr.GetLength(0) > 0)
            {
                invDTO.SKU = dr[0]["NewSKU"].ToString();
            }//end if

        }//end method

        /// <summary>
        /// This will check whether there is a blocked sku
        /// and if found update the quantity
        /// </summary>
        /// <param name="invDTOList"></param>
        /// <param name="dtBlockedSKUs"></param>
        private void CheckForBlockedSKUs(BindingList<Inventory> invDTOList, DataTable dtBlockedSKUs)
        {
            //Search wildcard SKUs
            DataRow[] dr = dtBlockedSKUs.Select("IsWildcard = 1");

            if (dr.GetLength(0) > 0)
            {
                foreach (DataRow row in dr)
                    foreach (Inventory inv in invDTOList)
                    {
                        if (inv.SKU.StartsWith(row["SKU"].ToString().Replace("*", "")))
                            inv.Qty = 0;
                    }  
            }//end if

            //search not wildcard
            dr = dtBlockedSKUs.Select("IsWildcard = 0");
            if (dr.GetLength(0) > 0)
            {
                foreach (DataRow row in dr)
                    foreach (Inventory inv in invDTOList)
                    {
                        if (inv.SKU == row["SKU"].ToString())
                            inv.Qty = 0;
                    }
            }//end if

        }//end method

        /// <summary>
        /// This will check if the SKU is duplicate or blank. If it is, it will update to error log.
        /// </summary>
        /// <param name="invDTO"></param>
        private bool IsDuplicateOrBlankSKU(Inventory invDTO, 
                                                List<String> addedSKUs, 
                                                InventoryUpdateServiceDTO invUpdDTO)
        {
            //Check whether SKU is in added SKU's
            if (addedSKUs.Contains(invDTO.SKU))
            {
                invUpdDTO.AddErrorLog("Duplicate SKU: " + invDTO.SKU + " found for UPC: " + invDTO.UPC);
                return true;
            }//end if
            else if (invDTO.SKU.Trim() == "")
            {
                invUpdDTO.AddErrorLog("SKU blank for UPC: " + invDTO.UPC);
                return true;
            }
            else
            {
                addedSKUs.Add(invDTO.SKU);
                return false;
            }
        }//end method

        /// <summary>
        /// Method that accepts price and calculates markup price
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public void UpdateMarkupPrice(int vendorID, int profileID, InventoryUpdateServiceDTO invUpdateSrvDto )
        {
            //bool isok = true;
            BindingList<Inventory> invDTOList = invUpdateSrvDto.InventoryDTO;
            //Get Pricing Markups for vendor-profile combination
            DataTable dtMarkups = dal.GetPricingMarkup(vendorID, profileID).Tables[0];

            var linnworksService = new ChannelAdvisor.VendorServices.LinnworksService();
            
            //check cache to read linnworks catalog
            List<StockItem> cache_catalog;
            InventoryCache.LoadCaches(out cache_catalog);
            
            linnworksService.AllItems = cache_catalog;

            //loop the inventory DTO
            foreach(var invDto in invDTOList)
            {

                if (WaitDialogWithWork.Current != null)
                    WaitDialogWithWork.Current.ShowMessage("Fetching data from Linnworks for SKU:" + invDto.SKU);

                linnworksService.ExtendWithDataFromLinnworks(invUpdateSrvDto, invDto);
                //only process if not null
                if (invDto.Price == null)
                    continue;
                if (invDto.DomesticShipping == null)//could not find entry in linnworks, error logged, no calculations
                    invDto.DomesticShipping=0;
                //Get range
                string lWhereStatement = "PriceFrom <= " + invDto.Price.Value.ToString(new CultureInfo("en-US")) + " AND PriceTo >=" + invDto.Price.Value.ToString(new CultureInfo("en-US"));
                DataRow[] dr = dtMarkups.Select(lWhereStatement);
               
                invDto.MarkupPercentage = dr.GetLength(0) == 0 ? SettingsConstant.Default_Markup_Percentage : float.Parse(dr[0]["Markup"].ToString()); 

                invDto.MarkupPrice = float.Parse(Math.Round((double)((invDto.Price + (float)invDto.DomesticShipping.Value) * (invDto.MarkupPercentage / 100)), 2).ToString());
                //Check if markup price is less than MAP
                if (invDto.MarkupPrice < invDto.MAP)
                {
                    invDto.MarkupPrice = invDto.MAP;
                }//end if

                

            }//end for

            //npak added
            Vendor VendorInfo = new DAL().GetVendor(vendorID);

            if (VendorInfo.SetOutOfStockIfNotPresented && !string.IsNullOrWhiteSpace(VendorInfo.Label))
            {
                //Get missing itemsfrom the cache
                List<StockItem> missItems = linnworksService.GetMissingItems(invDTOList, VendorInfo.Label);
                // send Qty =0 to linnwork
               // List<ErrorLog> err = linnworksService.SetQuantityOfZeroToLinnworks(missItems);
                ErrorLog err;
                foreach(var item in missItems)
                {
                    err = new ErrorLog(0, "SKU:" + item.SKU + " is present in linnworks but not vendor");
                    invUpdateSrvDto.ErrorLogDTO.Add(err);
                }
            }

            //add Discontinued 

        }//end method

        /// <summary>
        /// Set the qty as -50 for negative qty (but greater than unavailable threshold)
        /// </summary>
        /// <param name="invDTO"></param>
        private void SetNegativeQtyForZero(Inventory invDTO, 
                                            int negativeQtyForZeroCheck, 
                                            int negativeQtyForZero)
        {
            if ((invDTO.Qty <= negativeQtyForZeroCheck) && (invDTO.Qty > unavailableThreshold))
            {
                invDTO.Qty = negativeQtyForZero;
            }//end if
        }//end method

    }//end class

}//end namespace
