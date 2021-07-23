using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChannelAdvisor
{
    public class GenericPrice
    {
        public string SKU;
        public float SellerCost;
        public float? RetailPrice;
        public float? MAP;

        public GenericPrice(string[] elems)
        {
            if (elems.Length < 2) throw new Exception("SKU or Seller cost cann't be null");

            if (elems[0].StartsWith("\""))
                this.SKU = elems[0].Substring(1, elems[0].Length - 2);
            else
                this.SKU = elems[0];

            SellerCost = float.Parse(elems[1], new CultureInfo("en-US"));

            if (elems.Length >= 3)
            {
                if (elems[2].StartsWith("\""))
                    this.RetailPrice = float.Parse(elems[2].Substring(1, elems[2].Length - 2));
                else
                    this.RetailPrice = float.Parse(elems[2], new CultureInfo("en-US"));
            }

            if (elems.Length >= 4)
                this.MAP = float.Parse(elems[3], new CultureInfo("en-US"));
        }
    }
}
