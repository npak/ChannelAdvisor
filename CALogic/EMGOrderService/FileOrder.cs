using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;

namespace EMGOrderService
{
    [DelimitedRecord(",")]
    [IgnoreFirst(1)]
    public class FileOrder
    {
        [FieldQuoted()]
        public string OrderNo;
        [FieldQuoted()]
        public string OrderDate;
        [FieldQuoted()]
        public string Name;
        [FieldQuoted()]
        public string Street;
        [FieldQuoted()]
        public string StreetSupplement;
        [FieldQuoted()]
        public string PostalCode;
        [FieldQuoted()]
        public string City;
        [FieldQuoted()]
        public string RegionCode;
        [FieldQuoted()]
        public string CountryCode;
        [FieldQuoted()]
        public string ShippingMethod;
        [FieldQuoted()]
        public string LineItem;
        [FieldQuoted()]
        public string PartID;
        [FieldQuoted()]
        public string Quantity;
        [FieldQuoted()]
        public string Phone;
    }//end class
}
