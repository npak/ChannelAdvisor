//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.ComponentModel;
//using System.Data;

//namespace ChannelAdvisor
//{
//    /// <summary>
//    /// Class that 
//    /// </summary>
//    public class UpdateCA
//    {
//        private int _outOfStockQuantity = -5000;

//        DAL dal = new DAL();

//        public List<ErrorLog> UpdateInventoryToCA(int vendorID,
//                                                    int profileID,
//                                                    BindingList<Inventory> inventoryDTO,
//                                                    bool showErrorsForInstockOnly)
//        {
//            //Get list of selected profiles for vendor
//            DataTable dtVendorProfiles = dal.GetProfilesForVendor(vendorID).Tables[0];
//            DataTable dtProfiles = dal.GetProfiles().Tables[0];

//            //Create web service object
//            CA.InventoryService.InventoryService invService = new CA.InventoryService.InventoryService();

//            //Get web service URL
//            invService.Url = dal.GetWebServiceURL(WebServiceConstants.Inventory_Service);
//            invService.Timeout = 180000;//set timeout to 3 minutes

//            CA.InventoryService.APICredentials cred = new CA.InventoryService.APICredentials();
//            List<ErrorLog> errorLogList = new List<ErrorLog>();

//            cred.DeveloperKey = "2a980ebc-63e1-4b2f-a1e1-3d68925d4db1";
//            cred.Password = "steZaph4";
//            invService.APICredentialsValue = cred;


//            //Get Max Sku to update each time
//            int maxSKUs = 0;
//            int.TryParse(dal.GetSettingValue("Max_SKU_Update"), out maxSKUs);

//            //maxSKUs = 20;//To-delete

//            if (maxSKUs == 0) return errorLogList;

//            //Determine no of rounds of update
//            double noOfUpdates = (double)Math.Ceiling((double)((double)inventoryDTO.Count / maxSKUs));

//            int currentIndex = 0;
//            int loopCount = 0;
//            //Create 2 loops for list
//            //1 for each CA update
//            //2nd for SKU within each update
//            for (int x = 0; x < noOfUpdates; x++)
//            {
//                int arrayCount = 0;
//                //Determine next max loop count
//                if ((currentIndex + maxSKUs) <= inventoryDTO.Count)
//                {
//                    loopCount = currentIndex + maxSKUs;
//                    arrayCount = maxSKUs;
//                }
//                else
//                {
//                    loopCount = inventoryDTO.Count;
//                    arrayCount = inventoryDTO.Count - currentIndex;
//                }//end loop calculation if

//                //noOfUpdates = 1;//to-delete

//                //Create array for items to update
//                CA.InventoryService.InventoryItemQuantityAndPrice[] inventoryList
//                    = new CA.InventoryService.InventoryItemQuantityAndPrice[arrayCount];

//                int currentCount = 0;
//                for (int x1 = currentIndex; x1 < loopCount; x1++)
//                {
//                    //System.Diagnostics.Debug.WriteLine(currentIndex.ToString());

//                    CA.InventoryService.InventoryItemQuantityAndPrice invItem
//                        = CreateInventoryItem(inventoryDTO[currentIndex]);

//                    inventoryList[currentCount] = invItem;

//                    currentCount++;
//                    currentIndex++;
//                }//end SKU for loop

//                //Loop the various profiles for vendor and Call web service
//                CA.InventoryService.APIResultOfArrayOfUpdateInventoryItemResponse result = null;
//                result = invService.UpdateInventoryItemQuantityAndPriceList(
//                        GetProfileAPIKey(profileID, dtProfiles), inventoryList);

//                GetErrorLogFromResult(result, errorLogList, inventoryDTO, showErrorsForInstockOnly);

//            }//end Update for loop

//            ZeroOutFunction(vendorID, profileID, inventoryDTO);

//            return errorLogList;
//        }//end method

//        private void ZeroOutFunction(int vendorID, int profileID, BindingList<Inventory> inventoryDTO)
//        {
//            Vendor vendorInfo = dal.GetVendor(vendorID);
//            if (vendorInfo != null)
//            {
//                if (!vendorInfo.SetOutOfStockIfNotPresented) return;

//                if (!string.IsNullOrEmpty(vendorInfo.SkuPrefix) && (vendorInfo.SkuPrefix.Length >= 3))
//                {
//                    // Get list of SKUs from CA
//                    IList<string> CASkus = GetSkusFromCAByPrefix(vendorInfo, profileID);
//                    // Get list of out of stock SKUs
//                    IList<string> outOfStockSkus = GetOutOfStockSkus(inventoryDTO, CASkus);
//                    // Update list of out of stock SKUs with appropriate quantity
//                    SetOutOfStockForItems(profileID, outOfStockSkus);
//                }
//                else if (!string.IsNullOrEmpty(vendorInfo.Label) && (vendorInfo.Label.Length >= 3))
//                {
//                    // Get list of SKUs from CA
//                    IList<string> CASkus = GetSkusFromCAByLabel(vendorInfo, profileID);
//                    // Get list of out of stock SKUs
//                    IList<string> outOfStockSkus = GetOutOfStockSkus(inventoryDTO, CASkus);
//                    // Update list of out of stock SKUs with appropriate quantity
//                    SetOutOfStockForItems(profileID, outOfStockSkus);
//                }
//            }
//        }

//        /// <summary>
//        /// Update quantity to out of stock value for list of SKUs
//        /// </summary>
//        /// <param name="profileID">Profile ID</param>
//        /// <param name="outOfStockSkus">List of out of stock SKUs</param>
//        private void SetOutOfStockForItems(int profileID, IList<string> outOfStockSkus)
//        {
//            DataTable dtProfiles = dal.GetProfiles().Tables[0];

//            //Create web service object
//            CA.InventoryService.InventoryService invService = new CA.InventoryService.InventoryService();

//            //Get web service URL
//            invService.Url = dal.GetWebServiceURL(WebServiceConstants.Inventory_Service);
//            invService.Timeout = 180000;//set timeout to 3 minutes

//            CA.InventoryService.APICredentials cred = new CA.InventoryService.APICredentials();
//            cred.DeveloperKey = "2a980ebc-63e1-4b2f-a1e1-3d68925d4db1";
//            cred.Password = "steZaph4";
//            invService.APICredentialsValue = cred;

//            //Get Max Sku to update each time
//            int maxSKUs = 0;
//            int.TryParse(dal.GetSettingValue("Max_SKU_Update"), out maxSKUs);
//            if (maxSKUs <= 0) return;

//            //Determine no of rounds of update
//            int noOfUpdates = (int)Math.Ceiling((double)((double)outOfStockSkus.Count / maxSKUs));

//            int currentIndex = 0;
//            int loopCount = 0;
//            //Create 2 loops for list
//            //1 for each CA update
//            //2nd for SKU within each update
//            for (int x = 0; x < noOfUpdates; x++)
//            {
//                int arrayCount = 0;
//                //Determine next max loop count
//                if ((currentIndex + maxSKUs) <= outOfStockSkus.Count)
//                {
//                    loopCount = currentIndex + maxSKUs;
//                    arrayCount = maxSKUs;
//                }
//                else
//                {
//                    loopCount = outOfStockSkus.Count;
//                    arrayCount = outOfStockSkus.Count - currentIndex;
//                }

//                //Create array for items to update
//                CA.InventoryService.InventoryItemQuantityAndPrice[] inventoryList
//                    = new CA.InventoryService.InventoryItemQuantityAndPrice[arrayCount];

//                int currentCount = 0;
//                for (int x1 = currentIndex; x1 < loopCount; x1++)
//                {
//                    CA.InventoryService.InventoryItemQuantityAndPrice invItem
//                        = CreateOutOfStockItem(outOfStockSkus[currentIndex]);
//                    inventoryList[currentCount] = invItem;

//                    currentCount++;
//                    currentIndex++;
//                }

//                CA.InventoryService.APIResultOfArrayOfUpdateInventoryItemResponse result = invService.
//                    UpdateInventoryItemQuantityAndPriceList(GetProfileAPIKey(profileID, dtProfiles), inventoryList);
//            }
//        }

//        /// <summary>
//        /// Get list of out of stock SKUs
//        /// </summary>
//        /// <param name="inventoryList">List of inventory items from vendor</param>
//        /// <param name="CASkus">List of SKUs from CA</param>
//        /// <returns>List of out of stock SKUs</returns>
//        private IList<string> GetOutOfStockSkus(IList<Inventory> inventoryList, IList<string> CASkus)
//        {
//            if (inventoryList == null) return CASkus;
//            if (CASkus == null) return new List<string>();

//            IList<string> outOfStockSkus = new List<string>();
//            // Loop for CA skus
//            foreach (string CASku in CASkus)
//            {
//                bool found = false;
//                // Loop for inventory items
//                foreach (Inventory inventory in inventoryList)
//                {
//                    if (inventory.SKU.Equals(CASku, StringComparison.OrdinalIgnoreCase))
//                    {
//                        found = true;
//                        break;
//                    }
//                }

//                if (!found) outOfStockSkus.Add(CASku);
//            }

//            return outOfStockSkus;
//        }

//        /// <summary>
//        /// Get list of SKUs from CA by SKU prefix
//        /// </summary>
//        /// <param name="vendorInfo">Data for vendor</param>
//        /// <param name="profileID">Profile ID</param>
//        /// <returns>List of SKUs from CA</returns>
//        private IList<string> GetSkusFromCAByPrefix(Vendor vendorInfo, int profileID)
//        {
//            // create criteria
//            CA.InventoryService.InventoryItemCriteria criteria = new ChannelAdvisor.CA.InventoryService.InventoryItemCriteria();
//            criteria.SkuStartsWith = vendorInfo.SkuPrefix;
//            criteria.QuantityCheckField = CA.InventoryService.InventoryItemQuantityField.Total;
//            criteria.QuantityCheckType = CA.InventoryService.NumericFilterType.GreaterThan;
//            criteria.QuantityCheckValue = _outOfStockQuantity;
//            criteria.PageSize = 100;
//            criteria.PageNumber = 0;

//            List<string> CASkus = new List<string>();
//            IList<string> CASkusPage;
//            // Read skus in loop (100 items per call)
//            do
//            {
//                criteria.PageNumber++;
//                CASkusPage = GetSkusPageFromCA(profileID, criteria);
//                if (CASkusPage == null)
//                    break;

//                CASkus.AddRange(CASkusPage);
//            }
//            while (true);

//            return CASkus;
//        }

//        /// <summary>
//        /// Get list of SKUs from CA by label
//        /// </summary>
//        /// <param name="vendorInfo">Data for vendor</param>
//        /// <param name="profileID">Profile ID</param>
//        /// <returns>List of SKUs from CA</returns>
//        private IList<string> GetSkusFromCAByLabel(Vendor vendorInfo, int profileID)
//        {
//            // create criteria
//            CA.InventoryService.InventoryItemCriteria criteria = new ChannelAdvisor.CA.InventoryService.InventoryItemCriteria();
//            criteria.LabelName = vendorInfo.Label;
//            criteria.QuantityCheckField = CA.InventoryService.InventoryItemQuantityField.Total;
//            criteria.QuantityCheckType = CA.InventoryService.NumericFilterType.GreaterThan;
//            criteria.QuantityCheckValue = _outOfStockQuantity;
//            criteria.PageSize = 100;
//            criteria.PageNumber = 0;

//            List<string> CASkus = new List<string>();
//            IList<string> CASkusPage;
//            // Read skus in loop (100 items per call)
//            do
//            {
//                criteria.PageNumber++;
//                CASkusPage = GetSkusPageFromCA(profileID, criteria);
//                if (CASkusPage == null)
//                    break;

//                CASkus.AddRange(CASkusPage);
//            }
//            while (true);

//            return CASkus;
//        }

//        /// <summary>
//        /// Get 1 page of SKUs from CA by criteria
//        /// </summary>
//        /// <param name="profileID">Profile ID</param>
//        /// <param name="criteria">Filter criteria for SKUs</param>
//        /// <returns>1 page of SKUs</returns>
//        private IList<string> GetSkusPageFromCA(int profileID, CA.InventoryService.InventoryItemCriteria criteria)
//        {
//            DataTable dtProfiles = dal.GetProfiles().Tables[0];

//            //Create web service object
//            CA.InventoryService.InventoryService invService = new CA.InventoryService.InventoryService();

//            //Get web service URL
//            invService.Url = dal.GetWebServiceURL(WebServiceConstants.Inventory_Service);
//            invService.Timeout = 180000;//set timeout to 3 minutes

//            CA.InventoryService.APICredentials cred = new CA.InventoryService.APICredentials();
//            cred.DeveloperKey = "2a980ebc-63e1-4b2f-a1e1-3d68925d4db1";
//            cred.Password = "steZaph4";
//            invService.APICredentialsValue = cred;


//            CA.InventoryService.APIResultOfArrayOfString response = invService.GetFilteredSkuList(GetProfileAPIKey(profileID, dtProfiles), criteria, null, null);
//            if ((response.Status == CA.InventoryService.ResultStatus.Failure) || (response.ResultData == null) ||
//                (response.ResultData.Length == 0))
//                return null;

//            return new List<string>(response.ResultData);
//        }

//        /// <summary>
//        /// Method that get the profile API Key for the passed in profile
//        /// </summary>
//        /// <param name="profileID"></param>
//        /// <param name="dtProfiles"></param>
//        /// <returns></returns>
//        private string GetProfileAPIKey(int profileID, DataTable dtProfiles)
//        {
//            string apiKey = "";

//            DataRow[] dRow = dtProfiles.Select("ID = " + profileID);
//            if (dRow.GetLength(0) > 0)
//            {
//                apiKey = dRow[0]["ProfileAPIKey"].ToString();
//            }//end if

//            return apiKey;
//        }//end method

//        /// <summary>
//        /// Method that accepts an inventory DTO and returns web service invetory item
//        /// </summary>
//        /// <param name="invDTO"></param>
//        /// <returns></returns>
//        private CA.InventoryService.InventoryItemQuantityAndPrice CreateInventoryItem(Inventory invDTO)
//        {
//            CA.InventoryService.InventoryItemQuantityAndPrice invItem = new ChannelAdvisor.CA.InventoryService.InventoryItemQuantityAndPrice();
//            invItem.Sku = invDTO.SKU;

//            //Create PriceInfo only if Price is not null
//            if (invDTO.Price != null)
//            {
//                CA.InventoryService.PriceInfo priceInfo = new ChannelAdvisor.CA.InventoryService.PriceInfo();
//                priceInfo.Cost = (decimal)invDTO.Price; //actual price                
//                priceInfo.TakeItPrice = (decimal)invDTO.MarkupPrice;//calc. price
//                priceInfo.StorePrice = (decimal)invDTO.MarkupPrice;//calc. price
//                //Specify retail price if it not null
//                if (invDTO.RetailPrice != null) priceInfo.RetailPrice = (decimal)invDTO.RetailPrice;
//                invItem.PriceInfo = priceInfo;
//            }//end price null


//            //if Qty is not null then create QuantityInfoSubmit object
//            if (invDTO.Qty != null)
//            {
//                CA.InventoryService.QuantityInfoSubmit qtyInfo = new ChannelAdvisor.CA.InventoryService.QuantityInfoSubmit();
//                qtyInfo.Total = (int)invDTO.Qty;
//                qtyInfo.UpdateType = ChannelAdvisor.CA.InventoryService.InventoryQuantityUpdateType.Absolute;
//                invItem.QuantityInfo = qtyInfo;
//            }//end qty null

//            return invItem;
//        }//end method

//        private CA.InventoryService.InventoryItemQuantityAndPrice CreateOutOfStockItem(string sku)
//        {
//            CA.InventoryService.InventoryItemQuantityAndPrice item = new ChannelAdvisor.CA.InventoryService.InventoryItemQuantityAndPrice();
//            item.Sku = sku;
//            // set NULL because in this case it will not update
//            // for more info: http://developer.channeladvisor.com/display/cadn/InventoryItemQuantityAndPrice
//            item.PriceInfo = null;

//            item.QuantityInfo = new ChannelAdvisor.CA.InventoryService.QuantityInfoSubmit();
//            item.QuantityInfo.Total = _outOfStockQuantity;
//            item.QuantityInfo.UpdateType = ChannelAdvisor.CA.InventoryService.InventoryQuantityUpdateType.Absolute;

//            return item;
//        }

//        /// <summary>
//        /// Method that accepts Inventory Update response and adds the errors to the 
//        /// passed in Error Log List
//        /// </summary>
//        /// <param name="result"></param>
//        /// <returns></returns>
//        private void GetErrorLogFromResult(CA.InventoryService.APIResultOfArrayOfUpdateInventoryItemResponse result,
//            List<ErrorLog> errorLogList,
//            IList<Inventory> items,
//            bool showErrorsForInstockOnly)
//        {
//            CA.InventoryService.UpdateInventoryItemResponse[] skuResults = result.ResultData;

//            //loop the result
//            for (int x = 0; x < result.ResultData.GetLength(0); x++)
//            {
//                if (showErrorsForInstockOnly)
//                {
//                    // Try to figure out quantity of current product
//                    bool found = false;
//                    int index = 0;
//                    while ((!found) && (index < items.Count))
//                    {
//                        if (result.ResultData[x].Sku == items[index].SKU)
//                            found = true;
//                        else
//                            index++;
//                    }
//                    // Ignore errors for SKUs which not in stock
//                    if ((found) && (items[index].Qty < 0)) continue;
//                }

//                if (result.ResultData[x].Result == false)
//                {
//                    ErrorLog errorLog = new ErrorLog(1, "Error for SKU: " + result.ResultData[x].Sku + " '" + result.ResultData[x].ErrorMessage + "'");
//                    errorLogList.Add(errorLog);
//                }//end if
//            }//end for

//        }//end method

//    }//end class

//}//end namespace
