using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    /// <summary>
    /// Represents row for exporting to tab-delimited text file
    /// </summary>
    [DelimitedRecord("\t")]
    public class PicnicTimeExportRow
    {
        #region Fields
        public string AuctionTitle;
        public string InventoryNumber;
        public string Weight;
        public string UPC;
        public string MPN;
        public string Description;
        public string SellerCost;
        public string RetailPrice;
        public string PictureURLs;
        public string SupplierCode;
        public string WarehouseLocation;
        public string RelationshipName;
        public string VariationParentSKU;
        public string DCCode;
        public string CAStoreTitle;
        public string CAStoreDescription;
        public string Classification;
        public string Attribute1Name;
        public string Attribute1Value;
        public string Attribute2Name;
        public string Attribute2Value;
        public string Attribute3Name;
        public string Attribute3Value;
        public string Attribute4Name;
        public string Attribute4Value;
        #endregion

        /// <summary>
        /// Create header for exporting file
        /// </summary>
        /// <returns>Header row</returns>
        public static PicnicTimeExportRow CreateHeader()
        {
            PicnicTimeExportRow lRow = new PicnicTimeExportRow();
            lRow.AuctionTitle = "Auction Title";
            lRow.InventoryNumber = "Inventory Number";
            lRow.Weight = "Weight";
            lRow.UPC = "UPC";
            lRow.MPN = "MPN";
            lRow.Description = "Description";
            lRow.SellerCost = "Seller Cost";
            lRow.RetailPrice = "Retail Price";
            lRow.PictureURLs = "Picture URLs";
            lRow.SupplierCode = "Supplier Code";
            lRow.WarehouseLocation = "Warehouse Location";
            lRow.RelationshipName = "Relationship Name";
            lRow.VariationParentSKU = "Variation Parent SKU";
            lRow.DCCode = "DC Code";
            lRow.CAStoreTitle = "ChannelAdvisor Store Title";
            lRow.CAStoreDescription = "ChannelAdvisor Store Description";
            lRow.Classification = "Classification";
            lRow.Attribute1Name = "Attribute1Name";
            lRow.Attribute1Value = "Attribute1Value";
            lRow.Attribute2Name = "Attribute2Name";
            lRow.Attribute2Value = "Attribute2Value";
            lRow.Attribute3Name = "Attribute3Name";
            lRow.Attribute3Value = "Attribute3Value";
            lRow.Attribute4Name = "Attribute4Name";
            lRow.Attribute4Value = "Attribute4Value";

            return lRow;
        }
    }
}
