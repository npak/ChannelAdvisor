using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Linq;
using System.Net;
using System.Data.OleDb;
using System.ComponentModel;
using System.Data;
using Sgml;
//using Microsoft.VisualBasic.FileIO;

namespace ChannelAdvisor
{
    public class MotengService : IVendorService
    {
        public Vendor VendorInfo { get; set; }
        private HttpWebRequest request;

        private string _url = "";
        private string _dropshipfee = "";
        private string _productFileName = "";
        private string _priceFileName = "";
        private string _qtyFileName = "";
        private string _username = "";
        private string _password = "";

        private string _localFolder = "";
        private string path;

        private List<Productlist> ReadProductList = new List<Productlist>();

        #region Classes
        
        private class Productlist
        {
            public string UPС;
            public string SKU;
            public string Map;
            public string Description;
        }

        private class Pricelist
        {
            public string SKU;
            public string Price;
            public string Map;
            public string SRP;
            public string AvgShipCost;
        }

        private class Qtylist
        {
            public string SKU;
            public string Qty;
        }

        private class ListPriceQty
        {
            public string UPС;
            public string SKU;
            public string Price;
            public string Qty;
            public string Map;
            public string SRP;
            public string AvgShipCost;
            public string Description;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public MotengService()
        {
            string timeName = DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            path = System.Windows.Forms.Application.StartupPath + "\\Temp\\tempMoteng" + timeName + ".csv";
             new DAL().GetMotengInfo(out _url, out _username, out _password, out _productFileName, out _priceFileName, out _qtyFileName, out _dropshipfee);

            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";

            VendorInfo = new DAL().GetVendor((int)VendorName.Moteng);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Moteng"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            UpdateToFTP ftp = new UpdateToFTP(VendorInfo.ID);
            ftp.UploadFileFromString(ChangePipeDelimiter());

            return CreateInvUpdateServiceDTOForMoteng(false);
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            UpdateToFTP ftp = new UpdateToFTP(VendorInfo.ID);
            ftp.UploadFileFromString(ChangePipeDelimiter());

            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForMoteng(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

     
            return invUpdateSrcvDTO;

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForMoteng(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();
            List<ErrorLog> errorList = new List<ErrorLog>();

            List<ListPriceQty> list = MergePriceQty();

           
            invUpdateSrcvDTO.ErrorLogDTO = errorList;
            //Save excelfile to archive
            if (!isWinForm)
            {
                    //save csv file
                    SaveFileOnLocalPath(list);

                    //Save csv file to vendor folder and assign to DTO
                    string vendorFile = CAUtil.SaveScvFileAsVendorFile(path, VendorInfo.ID);

                    invUpdateSrcvDTO.VendorFile = vendorFile;

            }

                    //delete temp file
                    if (File.Exists(path))
                        File.Delete(path);

                
            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the datarows and create dto's
            float fee = 0;
            if (!string.IsNullOrWhiteSpace(_dropshipfee))
                fee = Convert.ToSingle(_dropshipfee);
            Inventory invDTO;
            foreach (ListPriceQty item in list)
            {
                if (!String.IsNullOrEmpty(item.SKU))   
                {    
                    invDTO = new Inventory();
                    invDTO.UPC = item.UPС;
                    invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, item.SKU);
                    if (!string.IsNullOrWhiteSpace(item.Qty))
                        invDTO.Qty = Convert.ToInt32(item.Qty);

                    if (string.IsNullOrWhiteSpace(item.Price))
                            invDTO.Price = null;   
                    else
                        {
                            invDTO.Price = fee + float.Parse(item.Price);
                            invDTO.Price = (float)(Math.Round((double)invDTO.Price, 2));
                        }

                    if (!string.IsNullOrWhiteSpace(item.Map))
                    {
                        if (item.Map != "TBD")
                            invDTO.MAP = (float)(Math.Round(float.Parse(item.Map), 2));
                        else
                            invDTO.MAP = 0;
                    }
                        

                    if (!string.IsNullOrWhiteSpace(item.SRP))
                        invDTO.RetailPrice = (float)(Math.Round(float.Parse(item.SRP), 2));
               
                    if (!string.IsNullOrWhiteSpace(item.AvgShipCost))
                    {
                        //test
                        if (item.AvgShipCost != "TBD")
                            invDTO.AvrShiftCost = (float)(Math.Round(float.Parse(item.AvgShipCost), 2));
                        else
                            invDTO.AvrShiftCost = 0;
                    }   
                    else
                        invDTO.AvrShiftCost = 0;

                    invDTO.Description = item.Description;
                    lstEMGInventory.Add(invDTO);
                    }//end string empty check

            }//end for each

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method      

        /// <summary>
        /// download and read csv to get Qty
        /// </summary>
        /// <returns></returns>
        private List<Qtylist> ReadQtyList()
        {
            List<Qtylist> list = new List<Qtylist>();
            Qtylist item;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_url + _qtyFileName);
            request.Credentials = new NetworkCredential(_username, _password);

            request.Method = WebRequestMethods.Ftp.DownloadFile;

             string line;
             bool isFirst = true;
             string[] row;

            using (Stream stream = request.GetResponse().GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    line= reader.ReadLine();
                    row = line.Split(',');
                    if (!isFirst && row.Length == 2)
                    {
                        item = new Qtylist();
                        item.SKU = row[0];
                        item.Qty = row[1];
                        list.Add(item);  
                    }
                    else
                        isFirst = false;
                   
                }
            }
            return  list;
        }

        /// <summary>
        /// download and read csv to get Prices
        /// </summary>
        /// <returns></returns>
        private List<Pricelist> ReadPriceList()
        {
            List<Pricelist> list = new List<Pricelist>();
            Pricelist item;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_url + _priceFileName);
            request.Credentials = new NetworkCredential(_username, _password);

            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://www.rapidretail.net/Inventory/ssi-inv.csv");
            //request.Credentials = new NetworkCredential("ssi_bdl631", "Rapid_631!");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            string line;
            bool isFirst = true;
            string[] row;

            using (Stream stream = request.GetResponse().GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    row = line.Split(',');
                    if (!isFirst && row.Length == 5)
                    {
                        item = new Pricelist();
                        item.SKU = row[0];
                        item.Price = row[1];
                        item.Map = row[2];
                        item.SRP = row[3];
                        item.AvgShipCost  = row[4];
                        list.Add(item);
                    }
                    else
                        isFirst = false;

                }
            }
            return list;
        }


        /// <summary>
        /// combain qty and prices
        /// </summary>
        /// <returns></returns>
        private List<ListPriceQty> MergePriceQty()
        {
            List<Pricelist> plist = ReadPriceList();
          
            List<Qtylist> qlist = ReadQtyList();
            List<ListPriceQty> result = new List<ListPriceQty>();

            // get left outer join result
                result= (from l1 in plist
                          join l2 in qlist on l1.SKU equals l2.SKU into gj
                          from l2_left in gj.DefaultIfEmpty()

                         //2nd outer join taking advantage of a short-circuiting check for null...
                         from prod in ReadProductList.Where(pr => l2_left != null && (pr.SKU == l2_left.SKU )).DefaultIfEmpty()

                          select new ListPriceQty
                          {
                            UPС  = prod == null ? String.Empty : prod.UPС,
                            SKU = l1.SKU,
                            Price = l1.Price,
                            Qty = l2_left == null ? String.Empty : l2_left.Qty,
                            //Map = prod == null ? String.Empty : prod.Map,
                            Map = l1.Map,
                            SRP = l1.SRP,
                            AvgShipCost = l1.AvgShipCost, 
                            Description = prod == null ? String.Empty : prod.Description
                           }).ToList<ListPriceQty>();

            // get remander
            var remaider = (from l2 in qlist
                          // join prod in ReadProductList on l2.SKU equals prod.SKU
                           join l1 in plist on l2.SKU equals l1.SKU into t
                           from l1_left in t.DefaultIfEmpty()
                           join prod in ReadProductList on l2.SKU equals prod.SKU into pr
                           from prod_left in pr.DefaultIfEmpty()
  
                           where l1_left == null 

                           select new ListPriceQty
                           {
                               UPС = prod_left == null ? String.Empty : prod_left.UPС,
                               SKU = l2.SKU,
                               Qty = l2.Qty,
                               //Map = prod_left == null ? String.Empty : prod_left.Map,
                               Price = l1_left ==null ? string.Empty :l1_left.Price,
                               Map = l1_left ==null ? string.Empty :l1_left.Map,
                               SRP = l1_left == null ? string.Empty : l1_left.SRP,
                               AvgShipCost = l1_left == null ? string.Empty : l1_left.AvgShipCost,
                               Description = prod_left == null ? String.Empty : prod_left.Description
                           }).ToList<ListPriceQty>();
            if (remaider.Count >0)
                result.AddRange(remaider);

            // get remainProducts
            var remainProd = (from pr in ReadProductList
                           join l1 in result on pr.SKU equals l1.SKU into t
                           from l1_left in t.DefaultIfEmpty()
                           where l1_left == null

                           select new ListPriceQty
                           {
                               UPС = pr.UPС,
                               SKU = pr.SKU,
                               Price = l1_left == null ? string.Empty : l1_left.Price,
                               Map = l1_left == null ? string.Empty : l1_left.Map,
                               SRP = l1_left == null ? string.Empty : l1_left.SRP,
                               AvgShipCost = l1_left == null ? string.Empty : l1_left.AvgShipCost,
                               Description = pr.Description
                           }).ToList<ListPriceQty>();
            if (remainProd.Count >0)
                result.AddRange(remainProd);


            return result;
        }


        public string ChangePipeDelimiter()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_url + _productFileName);
            request.Credentials = new NetworkCredential(_username, _password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            string line;
            var sb = new StringBuilder();

            string[] row;
            bool isFirst = true;
            Productlist item;
            using (Stream stream = request.GetResponse().GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine().Replace("\"", "''");
                   
                    // create product list
                    row = line.Split('|');
                    if (!isFirst)
                    {
                        item = new Productlist();
                        item.UPС = row[9].Replace("\"", "");
                        item.SKU = row[0].Replace("\"", "");
                        item.Map = row[14].Replace("\"", "");
                        item.Description = row[1].Replace("\"", "");
                        ReadProductList.Add(item);
                    }
                    else
                        isFirst = false;

                    // create a string to upload to ftp
                    line = line.Replace("|", "\",\"");
                    sb.AppendLine("\"" + line + "\"");

                }
            }
            return sb.ToString();

        }

        /// <summary>
        /// save merged file on the local path
        /// </summary>
        /// <returns></returns>
        private void SaveFileOnLocalPath(List<ListPriceQty> lstData)
        {
            var sb = new StringBuilder();
            sb.AppendLine("UPС,SKU,Price,Qty,MAP,SRP,AVG SHIP COST,Description");
            foreach (var data in lstData)
            {
                sb.AppendLine(data.UPС + "," + data.SKU + "," + data.Price + "," + data.Qty + "," + data.Map + "," + data.SRP + "," + data.AvgShipCost  + "," + data.Description);
            }
            // Create a file to write to
            File.WriteAllText(path, sb.ToString());
        }

    }//end class
}//end namespace
