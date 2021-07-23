using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelAdvisor.Models
{
    public class StockItemLocation
    {
        public string StockLocationId { get; set; }
        public string Location { get; set; }
        public string StockLevel { get; set; }
        public string StockValue { get; set; }

    }
    public class ExtendedProperty
    {
        public string RowId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

    }
    public class OrderDetails
    {
        public string OrderId { get; set; }
        public string CreatedDate { get; set; } //ReceivedDate
        public string Status { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; } //BarcodeNumber
        public string Source { get; set; } //Source from General Info
        public string MarketplaceID { get; set; }  //ReferenceNum
        public string ShippingCustomerName { get; set; } //ChannelBuyerName
        public string State { get; set; } // Region
        public string ZipCode { get; set; } //PostCode
        public string CategoryName { get; set; }
        public string PricePerUnit { get; set; } //PricePerUnit
        public string UnitCost  { get; set; } //Unit cost 
        public string Quantity { get; set; } //Quantity
        public string LineDiscount { get; set; } //Discount
        public string Cost { get; set; }
        public string CostIncTax { get; set; } // Region
        public string Subtotal { get; set; } //PostCode
        public string ShippingCost { get; set; } //PostageCost
        public string TaxRate { get; set; } //TaxRate
        public string OrderTax { get; set; } //Tax 
        public string OrderTotal { get; set; } //otalCharge
        public string TotalWeight { get; set; }
        public string TrackingNumber { get; set; }

        private List<ExtendedProperty> _domesticShipping; 
        public List<ExtendedProperty> DomesticShipping
        {
            get
            {
                if (_domesticShipping == null)
                    return new List<ExtendedProperty>();
                else
                    return _domesticShipping;
            }
            set
            {
                _domesticShipping = value;
            }
        }


    }

}
