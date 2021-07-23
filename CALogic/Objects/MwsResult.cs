using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelAdvisor.Objects
{
    public class MwsResult
    {
        public string UPC { get; set; }
        public string ASIN { get; set; }

        public string Brand { get; set; }
        public string Color { get; set; }
        public string ItemPartNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string NumberOfItems { get; set; }
        public string PackageQuantity { get; set; }
        public string PartNumber { get; set; }
        public string Size { get; set; }
        public string Title { get; set; }
        public string Binding { get; set; }
        public string ProductGroup { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeSubcategory { get; set; }
        public string SmallImageURL { get; set; }
        
        public string Price { get; set; } //  ListPrice/Amount    
        public string VariationChildsString { get; set; }
        //public string SalesRankingsString { get; set; } 
    }

}
