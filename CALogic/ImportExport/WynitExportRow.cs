using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;


namespace ChannelAdvisor
{
    /// <summary>
    /// This class represents a row for exporting Wynit inventory data to tab delimited text file
    /// </summary>
    [DelimitedRecord("\t")]
    public class WynitExportRow
    {
        #region Fields
        public string InventoryNumber;

        public string MPN;

        public string ShortDesription;

        public string UPC;

        public string RetailPrice;

        public string Weight;

        public string SupplierCode;

        public string WarehouseLocation;

        public string DCCode;
        #endregion

        public static WynitExportRow CreateHeader()
        {
            WynitExportRow HeaderRow = new WynitExportRow();

            HeaderRow.InventoryNumber = "Inventory number";
            HeaderRow.MPN = "MPN";
            HeaderRow.ShortDesription = "Short description";
            HeaderRow.UPC = "UPC";
            HeaderRow.RetailPrice = "Retail price";
            HeaderRow.Weight = "Weight";
            HeaderRow.SupplierCode = "Supplier Code";
            HeaderRow.WarehouseLocation = "Warehouse Location";
            HeaderRow.DCCode = "DC Code";

            return HeaderRow;
        }
    }

    
}
