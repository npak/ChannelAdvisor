using System;
using System.Collections.Generic;
using System.Text;

namespace EMGOrderService
{
    public class EMGOrder
    {
        public string OrderNo;
        public string OrderDate;
        public string TransportID;
        public string TransportCode;
        public string DestinationName;
        public string Street;
        public string StreetSupplement;
        public string City;
        public string RegionCode;
        public string PostalCode;
        public string CountryCode;
        public string Email;
        public string Phone;

        public List<EMGItem> Items = new List<EMGItem>();
    }//end class


    public class EMGItem
    {
        public string PartID;
        public string Quantity;
        public string UOMCode;

        public EMGItem(string partID, string quantity, string uomCode)
        {
            this.PartID = partID;
            this.Quantity = quantity;
            this.UOMCode = uomCode;
        }
    }//end class
}
