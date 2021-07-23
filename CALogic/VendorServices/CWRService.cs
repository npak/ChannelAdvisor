using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Data;
using System.Data.OleDb;

namespace ChannelAdvisor
{
    public class CWRService : IVendorService
    {
        public Vendor VendorInfo { get; set; }

        private string _url = "";//"https://shop.cwrelectronics.com/feeds/productdownload.php";
        private string _user = "";//"MPB_MzU4NzEzMzU4NzEzMzQ0";
        private string _password = "";//"1174276800";
     
        private string _localFolder = "";
        private string _localFileName = "CWRCatalog.scv";
        private string path;


        /// <summary>
        /// 
        /// </summary>
        public CWRService()
        {
            string timeName = DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            path = System.Windows.Forms.Application.StartupPath + "\\Temp\\tempCWRCatalog" + timeName + ".csv";

            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";

            new DAL().GetCWRSettings(out _url,
                                    out _user,
                                    out _password);

            VendorInfo = new DAL().GetVendor((int)VendorName.CWR);

            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for CWR"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForCWR(false);
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
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForCWR(true);

            //Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 

        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForCWR(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            DataTable dtProducts = GetCWRProductTable();

            if (!isWinForm)
            {
                
                    string vendorFile = CAUtil.SaveScvFileAsVendorFile(path , VendorInfo.ID);
                    invUpdateSrcvDTO.VendorFile = vendorFile;

            }

            //delete temp file
            if (File.Exists(path))
                File.Delete(path);
               
            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();
            float invetoryMRP = 0;
            //loop through the xml nodes and create dto
            //foreach (XmlNode node in nodes)
            for (int i = 0; i < dtProducts.Rows.Count; i++)
            {

                Inventory invDTO = new Inventory();

                invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[i][0].ToString() );
                invDTO.UPC = dtProducts.Rows[i][9].ToString();

                //check for blank
                if (dtProducts.Rows[i][2].ToString().Replace("\"", "") != "")
                {
                    invDTO.Qty = Convert.ToInt32(dtProducts.Rows[i][2].ToString().Replace("\"", ""));
                }
                else
                {
                    invDTO.Qty = 0;
                }

                //check for blank
                if (dtProducts.Rows[i][3].ToString().Replace("\"", "") != "")
                {
                    //invDTO.Price = float.Parse(node["price"].InnerXml);
                    invDTO.Price = (float)(Math.Round(Convert.ToDouble(dtProducts.Rows[i][3].ToString().Replace("\"", "")), 2));

                }
                else
                {
                    throw new Exception("Price for CWR UPC: " + invDTO.UPC + " is blank");
                }

                if (dtProducts.Rows[i][4].ToString().Replace("\"", "") == "")
                {
                    invDTO.RetailPrice = null;
                }
                else
                {
                    invDTO.RetailPrice = float.Parse(dtProducts.Rows[i][4].ToString().Replace("\"", ""));
                }

                invDTO.MAP = dtProducts.Rows[i][5].ToString().Replace("\"", "") == "" ? 0 : float.Parse(dtProducts.Rows[i][5].ToString().Replace("\"", ""));
                invetoryMRP = dtProducts.Rows[i][6].ToString().Replace("\"", "") == "" ? 0 : float.Parse(dtProducts.Rows[i][6].ToString().Replace("\"", ""));
                if (invetoryMRP > invDTO.MAP)
                    invDTO.MAP = invetoryMRP;

                invDTO.Description = dtProducts.Rows[i][1].ToString();

                lstEMGInventory.Add(invDTO);
            }//end for each

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        public DataTable GetCWRProductTable()
        {
            DataTable dtProducts = new DataTable();
            try
            {
                //Download file first
                //string path = _localFolder + _localFileName;
                
                using (WebClient webClient = new WebClient())
                {
                    webClient.UseDefaultCredentials = true;
                    webClient.Credentials = new NetworkCredential(_user, _password);
                    webClient.DownloadFile(_url, path);
                }

                var connString = string.Format(
                    @"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=""Text;HDR=YES;FMT=Delimited""",
                    Path.GetDirectoryName(path)
                );

                using (var conn = new OleDbConnection(connString))
                {
                    conn.Open();
                    var query = "SELECT * FROM [" + Path.GetFileName(path) + "]";
                    using (var adapter = new OleDbDataAdapter(query, conn))
                    {
                        // use the data adapter to fill the datatable
                        adapter.Fill(dtProducts);
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return dtProducts;

        }

        /// <summary>
        /// save csv file on the local path
        /// </summary>
        /// <returns></returns>
        private string SaveFileOnLocalPath(BindingList<Inventory> lstData)
        {
            var sb = new StringBuilder();
            sb.AppendLine("UPC, SKU,Qty,Price,Retail Price,MAP,Description");
            foreach (var data in lstData)
            {
                sb.AppendLine(data.UPC + "," + data.SKU + "," + data.Qty + "," + data.Price + "," + data.RetailPrice  + "," + data.MAP +  "," + data.Description);
            }

            // Create a file to write to
            var extention  = ".csv";
            //Get current time as fileName
            string fileName = DateTime.Now.ToString().Replace("/", "-").Replace(":", ".") + extention;

            //Get Vendor folder
            string vendorFolder = CAUtil.GetVendorFolder((int)VendorName.CWR);

            if (vendorFolder != null)
            {
                vendorFolder += SettingsConstant.VendorFile_Folder_Name + "\\";
                if (!Directory.Exists(vendorFolder)) Directory.CreateDirectory(vendorFolder);

                //Save file
                File.WriteAllText(vendorFolder + fileName, sb.ToString());
                //File.Copy(csvPath, vendorFolder + fileName);

                return fileName;
            }
            else
            {
                return null;
            }

           
        }


    }
}//end namespace
