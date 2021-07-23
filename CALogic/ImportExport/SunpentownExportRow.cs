using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    [DelimitedRecord("\t")]
    public class SunpentownExportRow
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
        public string Attribute5Name;
        public string Attribute5Value;
        public string Attribute6Name;
        public string Attribute6Value;
        public string ShipZoneName;
        public string ShipCarrierCode;
        public string ShipClassCode;
        public string ShipRateFirstItem;
        public string ShipRateAdditionalItem;
        #endregion

        public static SunpentownExportRow CreateHeader()
        {
            SunpentownExportRow lRow = new SunpentownExportRow();
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
            lRow.Attribute5Name = "Attribute5Name";
            lRow.Attribute5Value = "Attribute5Value";
            lRow.Attribute6Name = "Attribute6Name";
            lRow.Attribute6Value = "Attribute6Value";
            lRow.ShipZoneName = "Ship Zone Name";
            lRow.ShipCarrierCode = "Ship Carrier Code";
            lRow.ShipClassCode = "Ship Class Code";
            lRow.ShipRateFirstItem = "Ship Rate First Item";
            lRow.ShipRateAdditionalItem = "Ship Rate Additional Item";

            return lRow;
        }
    }
}
