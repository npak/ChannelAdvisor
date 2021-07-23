using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    [DelimitedRecord(",")]
    public class InventoryExportRow
    {
        public string _upc;
        public string _sku;
        public string _qty;
        public string _price;
        public string _markupPercentage;
        public string _markupPrice; //final price
        public string _map;
        public string _desc;
        public string _retailPrice; //MSRP 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inventoryDTO"></param>
        public static InventoryExportRow CreateInventoryExportRow(Inventory inventoryDTO)
        {
            InventoryExportRow invDTOFile = new InventoryExportRow();
            //qty, price, retail price

            invDTOFile._upc = inventoryDTO.UPC;
            invDTOFile._sku = inventoryDTO.SKU;
            invDTOFile._qty = inventoryDTO.Qty == null ? "" : inventoryDTO.Qty.ToString();
            invDTOFile._price = inventoryDTO.Price == null ? "" : inventoryDTO.Price.ToString();
            invDTOFile._markupPercentage = inventoryDTO.MarkupPercentage.ToString();
            invDTOFile._markupPrice = inventoryDTO.MarkupPrice.ToString(); //final price
            invDTOFile._map = inventoryDTO.MAP.ToString();
            invDTOFile._desc = inventoryDTO.Description;
            invDTOFile._retailPrice = inventoryDTO.RetailPrice == null ? "" : inventoryDTO.RetailPrice.ToString(); //MSRP 

            return invDTOFile;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static InventoryExportRow CreateHeader()
        {
            InventoryExportRow invDTOFile = new InventoryExportRow();
            //qty, price, retail price

            invDTOFile._upc = "UPC";
            invDTOFile._sku = "SKU";
            invDTOFile._qty = "Qty";
            invDTOFile._price = "Price";
            invDTOFile._markupPercentage = "Markup Percentage";
            invDTOFile._markupPrice = "Markup Price";
            invDTOFile._map = "MAP";
            invDTOFile._desc = "Description";
            invDTOFile._retailPrice = "Retail Price";

            return invDTOFile;
        }
    }
}
