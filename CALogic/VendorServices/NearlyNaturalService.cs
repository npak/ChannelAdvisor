using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Data;
using System.Data.OleDb;
using System.ComponentModel;

namespace ChannelAdvisor
{
    public class NearlyNaturalService : IVendorService
    {
        public Vendor VendorInfo { get; set; }

        private string _url = "";
        private string _dropshipfee = "";

        private string _localFolder = "";
        private string _localFileName = "NearlyNatural.xls";
        private string path;

        public DataTable dtProducts { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NearlyNaturalService()
        {
 
            string timeName = DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            path = System.Windows.Forms.Application.StartupPath + "\\Temp\\tempNearlyNatural" + timeName + ".xsl";
            new DAL().GetNearlyNaturalInfo(out _url, out _dropshipfee);

            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";

            VendorInfo = new DAL().GetVendor((int)VendorName.NearlyNatural);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Nearly Natural"));

        }//end constructor


        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForNearlyNatural(false);
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForNearlyNatural(true);

            //Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForNearlyNatural(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Download excel and return datatble
            DataTable dtProducts = GetNearlyNaturalProductTable();

            // change and update on ftp
            string csvContent = ChangeExcelFile(dtProducts);
            UpdateToFTP ftp = new UpdateToFTP(VendorInfo.ID);
            ftp.UploadFileFromString(csvContent);

            //Save excelfile to archive
            if (!isWinForm)
            {
                try
                {
                    //save csv file
                    // Create a file to write to
                    File.WriteAllText(path, csvContent);
                    //Save xml file to vendor folder and assign to DTO
                    string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_localFolder + _localFileName, VendorInfo.ID);
                    invUpdateSrcvDTO.VendorFile = vendorFile;
                }
                finally
                {
                    //delete temp file
                    if (File.Exists(path))
                        File.Delete(path);
                }
                ////Save xml file to vendor folder and assign to DTO
                //string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_localFolder + _localFileName, VendorInfo.ID);
                //invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            BindingList<Inventory> lstInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the Generic Price List and create dto
            float fee = 0;
            if (!string.IsNullOrWhiteSpace(_dropshipfee))
                fee = Convert.ToSingle(_dropshipfee);

            for (int i = 1; i < dtProducts.Rows.Count; i++)
            {
                if (dtProducts.Rows[i][0] != null)
                {
                    if (!String.IsNullOrEmpty(dtProducts.Rows[i][0].ToString()))
                    {
                        Inventory invDTO = new Inventory();

                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[i][0].ToString());
                        
                        // check in-stosk
                        if (dtProducts.Rows[i][5].ToString().ToLower()=="no" || dtProducts.Rows[i][5].ToString().ToLower() =="discontinued")
                            invDTO.Qty = 0;
                        else if (dtProducts.Rows[i][5].ToString().ToLower() =="yes")
                            invDTO.Qty = 100;
                        
                        if (!string.IsNullOrWhiteSpace(dtProducts.Rows[i][4].ToString().Replace("$", "").Replace("#N/A","")))
                        {
                            string ss = dtProducts.Rows[i][4].ToString();
                            invDTO.Price = fee + float.Parse(dtProducts.Rows[i][4].ToString().Replace("$", "").Replace("#N/A", ""));
                            invDTO.Price = (float)(Math.Round((double)invDTO.Price, 2));
                        }
                        
                        invDTO.RetailPrice = null;
                        invDTO.MAP = 0;
                        invDTO.Description = "\"" + dtProducts.Rows[i][2].ToString() + "\"";

                        lstInventory.Add(invDTO);
                    }//end string empty check
                }//end null check

            }//end for each

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        private DataTable GetNearlyNaturalProductTable()
        {
            //Download file first
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string path = _localFolder + _localFileName;
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(_url, path);
            }
           
            // create a new blank datatable
            DataTable dtSheets = new DataTable();
            dtSheets = ExcelUtils.ReadExcelSheet(_localFolder + _localFileName, "Sheet1$", "ExcelConStringWithHeader");

            return dtSheets;

        }//end method

        /// <summary>
        /// save changed file on the local path
        /// </summary>
        /// <returns></returns>
        private string ChangeExcelFile(DataTable dt)
        {
            var csvContent = string.Empty;
            string[] dimention;
            string strdimention = string.Empty;
            string strReplaced = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                var arr = new List<string>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == 15)
                    {
                        if (i == 0)
                        {
                            arr.Add("\"Shipping Box Length\"");
                            arr.Add("\"Shipping Box Width\"");
                            arr.Add("\"Shipping Box Height\"");
                        }
                        else
                        {
                            //10.125" x 10.125" x 17.5"
                            strdimention = dt.Rows[i][j].ToString();
                            strdimention = strdimention.Replace("\"", "").Replace("''", "");

                            dimention = strdimention.Split('x');
                            if (dimention.Length != 3)
                                for (int k = 0; k < 3; k++)
                                    arr.Add("");
                            else
                                for (int l = 0; l < 3; l++)
                                    arr.Add(dimention[l].Trim());
                        }
                    }
                    else if (j == 14)
                    {
                        strReplaced = dt.Rows[i][j].ToString().ToLower().Replace("lbs", "").Replace("lvs", "").Trim();
                        arr.Add(strReplaced);
                    }
                    else
                    {
                        strReplaced = dt.Rows[i][j].ToString().Trim();
                        strReplaced = strReplaced.Replace("”", "''").Replace("“", "''");
                        strReplaced = strReplaced.Replace("’", "'").Replace("\"", "''").Replace("–", "-").Replace("…","...");
                        strReplaced = strReplaced.Replace("é", "e");
                        strReplaced = "\"" + strReplaced + "\"";
                        arr.Add(strReplaced);
                    }
                }
                csvContent += string.Join(",", arr) + "\n";
            }

            return csvContent;
        }
    }
}//end namespace
