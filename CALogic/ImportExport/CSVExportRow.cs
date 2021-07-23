using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelAdvisor
{
    public class CSVExportRow
    {
        #region Fields
        public string UPC;
        public string Sku;
        public string Qty;
        public string Price;
        public string MarkupPercentage;
        public string MAP;
        public string DomesticShipping;
        public string MarkupPrice;
        public string RetailPrice;
        public string Description;
        public string IsSetQtyT0Zero;
        public string AvgShipCost;
        #endregion

        private List<string> _additionalFields = new List<string>();
        public List<string> AdditionalFields
        {
            get
            {
                return _additionalFields;
            }
            set
            {
                _additionalFields = value;
            }
        }

        public static CSVExportRow GetHeaderRow()
        {
            CSVExportRow lExportRow = new CSVExportRow();
            lExportRow.UPC = "UPC";
            lExportRow.Sku = "Sku";
            lExportRow.Qty = "Qty";
            lExportRow.Price = "Price";
            lExportRow.MarkupPercentage = "Markup Percentage";
            lExportRow.MAP = "MAP";
            lExportRow.DomesticShipping = "Domestic Shipping";
            lExportRow.MarkupPrice = "Markup Price";
            lExportRow.RetailPrice = "Retail Price";
            lExportRow.Description = "Description";
            lExportRow.IsSetQtyT0Zero = "Category";
            lExportRow.AvgShipCost = "Avg Ship Cost"; 
            return lExportRow; 
        }
    }

    public class AdditionalHeaders
    {
        private List<string> _headers = new List<string>();
        public List<string> HeadersList
        {
            get
            {
                return _headers;
            }
            set
            {
                _headers = value;
            }
        }

        public AdditionalHeaders(string invenoryId)
        {
            switch (invenoryId)
            {
                case "28":
                    _headers.Add("fullVendor");
                    _headers.Add("ohioQty");
                    _headers.Add("floridaQty");
                    _headers.Add("ean");

                    _headers.Add("weight");
                    _headers.Add("dimweight");
                    _headers.Add("description");
                    _headers.Add("vendorItemcode");

                    _headers.Add("length");
                    _headers.Add("width");
                    _headers.Add("height");
                    _headers.Add("image1");
                    break;
                case "2":
                    break;
            }
        }

    }
}
