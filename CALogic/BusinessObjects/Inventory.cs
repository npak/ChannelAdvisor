using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelAdvisor
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Inventory
    {
        private string _upc;
        private string _sku;
        private int? _qty;
        private float? _price;
        private float? _avrShiftCost;

        private float _markupPercentage;
        private float _markupPrice; //final price
        private float _map;
        private string _desc;
        private float? _retailPrice; //MSRP 
        public decimal? DomesticShipping { get; set; }
        public string Category { get; set; }

        public Inventory() 
        {
            _additionalFields = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string UPC
        {
            get
            {
                return _upc;
            }
            set
            {
                _upc = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SKU
        {
            get
            {
                return _sku;
            }
            set
            {
                _sku = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? Qty
        {
            get
            {
                return _qty;
            }
            set
            {
                _qty = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float? Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }

        public float? AvrShiftCost
        {
            get
            {
                return _avrShiftCost;
            }
            set
            {
                _avrShiftCost = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float MarkupPrice
        {
            get
            {
                return _markupPrice;
            }
            set
            {
                _markupPrice = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float MarkupPercentage
        {
            get
            {
                return _markupPercentage;
            }
            set
            {
                _markupPercentage = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float MAP
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get
            {
                return _desc;
            }
            set
            {
                _desc = value;
            }
        }

        public float? RetailPrice
        {
            get
            {
                return _retailPrice;
            }
            set
            {
                _retailPrice = value;
            }
        }

        private List<string> _additionalFields;
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

    }
}
