using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;
using System.Data;
using System.IO;
using System.ComponentModel;

namespace ChannelAdvisor
{
    /// <summary>
    /// 
    /// </summary>
    public class GenericPriceService : IVendorService
    {
        public Vendor VendorInfo { get; set; }

        List<string> fileList;

        public GenericPriceService(int vendorID)
        {
            DAL dal = new DAL();
            VendorInfo = dal.GetVendor(vendorID);

            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data about vendor with ID {0}", vendorID));
        }

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForGenericPrice(false);
        }//end method


        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForGenericPrice(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForGenericPrice(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Read files and create list of SKUs and their Price
            List<GenericPrice> gpList = ReadFilesInFolder(invUpdateSrcvDTO);

            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveMultipleCSVFilesAsVendorFile(fileList, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }


            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the Generic Price List and create dto
            for (int x = 0; x < gpList.Count; x++)
            {
                Inventory invDTO = new Inventory();
                invDTO.UPC = "";
                invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, gpList[x].SKU);
                invDTO.Qty = null;
                invDTO.Price = (float)(Math.Round((double)gpList[x].SellerCost, 2));//gpList[x].SellerCost;
                invDTO.RetailPrice = (float)(Math.Round((double)gpList[x].RetailPrice, 2));//gpList[x].RetailPrice;
                invDTO.MAP = gpList[x].MAP == null ? 0 : (float)gpList[x].MAP;
                invDTO.Description = "";

                lstEMGInventory.Add(invDTO);
            }//end for each

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        /// <summary>
        /// This method would read files in the folder and also add the file names to 
        /// InventoryUpdateServiceDTO so that they could be deleted after update to CA.
        /// </summary>
        /// <param name="invUpdateSrcvDTO"></param>
        /// <returns></returns>
        private List<GenericPrice> ReadFilesInFolder(InventoryUpdateServiceDTO invUpdateSrcvDTO)
        {
            //Create engine
            //FileHelperEngine engine;// = new FileHelperEngine(typeof(GenericPrice));

            //get folder for vendor
            string folder = VendorInfo.Folder;//GetFolderForVendor(vendorID);

            List<GenericPrice> gpList = new List<GenericPrice>();

            fileList = GetFiles(folder);

            //loop files
            //foreach (string file in files)
            for (int x = 0; x < fileList.Count; x++)
            {
                List<GenericPrice> gpArray = new List<GenericPrice>();

                //Check for file type
                //if (IsFileHasMAPField(fileList[x]))
                //{
                //    engine = new FileHelperEngine(typeof(GenericPrice));
                //    gpArray = (GenericPrice[])engine.ReadFile(fileList[x]);
                //}
                //else
                //{

                //    engine = new FileHelperEngine(typeof(GenericPriceNonMap));
                //    GenericPriceNonMap[] gpMapArray = (GenericPriceNonMap[])engine.ReadFile(fileList[x]);

                //    gpArray = ConvertToGenericPrice(gpMapArray);
                //}
                //GenericPrice[] gpArray = (GenericPrice[])engine.ReadFile(fileList[x]);

                gpArray = ReadFile(fileList[x]);

                foreach (GenericPrice gPrice in gpArray)
                {
                    gpList.Add(gPrice);
                }//end inner foreach

                //Add file to ToDeleteFile list if file was read
                if (gpArray.Count > 0)
                {
                    invUpdateSrcvDTO.ToDeleteFiles.Add(fileList[x]);
                }
                else
                {
                    //throw exception that the file could not be read
                    throw new Exception("The format for file: '" + fileList[x] + "' seems to be invalid.");
                }//end if

            }//end foreach
            //foreach (string file in files)
            //{
            //    GenericPrice[] gpArray;

            //    //Check for file type
            //    if (IsFileHasMAPField(file))
            //    {
            //        engine = new FileHelperEngine(typeof(GenericPrice));
            //        gpArray = (GenericPrice[])engine.ReadFile(file);
            //    }
            //    else
            //    {
            //        engine = new FileHelperEngine(typeof(GenericPriceNonMap));
            //        GenericPriceNonMap[] gpMapArray = (GenericPriceNonMap[])engine.ReadFile(file);

            //        gpArray = ConvertToGenericPrice(gpMapArray);
            //    }
            //    //GenericPrice[] gpArray = (GenericPrice[])engine.ReadFile(file);

            //    foreach (GenericPrice gPrice in gpArray)
            //    {
            //        gpList.Add(gPrice);
            //    }//end inner foreach

            //    //Add file to ToDeleteFile list if file was read
            //    if (gpArray.GetLength(0) > 0)
            //    {
            //        invUpdateSrcvDTO.ToDeleteFiles.Add(file);
            //    }
            //    else
            //    {
            //        //throw exception that the file could not be read
            //        throw new Exception("The format for file: '" + file + "' seems to be invalid.");
            //    }//end if

            //}//end foreach

            return gpList;

        }//end method

        private List<GenericPrice> ReadFile(string fileName)
        {
            StreamReader sr = File.OpenText(fileName);
            List<GenericPrice> prices = new List<GenericPrice>();
            string input = null;

            input = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                input = sr.ReadLine();
                string[] elems = input.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                prices.Add(new GenericPrice(elems));
            }

            sr.Close();

            return prices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private List<string> GetFiles(string folder)
        {
            List<string> fileList = new List<string>();

            if (!Directory.Exists(folder))
                return fileList;

            //Get list of files in directory
            string[] files = Directory.GetFiles(folder, "*.csv");
            foreach (string file in files)
            {
                fileList.Add(file);
            }

            string[] filesTxt = Directory.GetFiles(folder, "*.txt");
            foreach (string file in filesTxt)
            {
                fileList.Add(file);
            }
            return fileList;
        }

        /// <summary>
        /// Method to convert Non Map array to Generic Price
        /// </summary>
        /// <param name="gpMapArray"></param>
        /// <returns></returns>
        //private GenericPrice[] ConvertToGenericPrice(GenericPriceNonMap[] gpMapArray)
        //{
        //    GenericPrice[] gpList = new GenericPrice[gpMapArray.GetLength(0)];

        //    //Loop and convert
        //    for(int x=0; x< gpMapArray.GetLength(0); x++)
        //    {
        //        gpList[x] = gpMapArray[x].ConvertToGenericPrice();
        //    }//end for each

        //    return gpList;
        //}//end method

        /// <summary>
        /// Method to check whether the files has a MAP column
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool IsFileHasMAPField(string file)
        {
            StreamReader sr = File.OpenText(file);
            string input = null;

            input = sr.ReadLine();
            int index = input.IndexOf("MAP");

            sr.Close();

            if (index > -1) return true;
            else return false;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        private string GetFolderForVendor(int vendorID)
        {
            string folder = "";
            //Get list of dynamic vendors
            DataTable dtVendors = new DAL().GetDynamicVendors().Tables[0];

            //find folder for vendor
            DataRow[] dr = dtVendors.Select("ID =" + vendorID.ToString());

            if (dr.GetLength(0) == 0)
            {
                //throw error
                throw new Exception("Scan folder not found for vendor ID: " + vendorID.ToString());
            }
            else
            {
                folder = dr[0]["Folder"].ToString();
            }

            return folder;
        }//end method
    }
}
