using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelAdvisor
{
    public class RateParameters
    {
        // "{  \"carrierCode\": \"ups\",  \"serviceCode\": null,  \"packageCode\": null,  \"fromPostalCode\": \"78703\",  \"toState\": \"DC\",  
        //\"toCountry\": \"US\",  \"toPostalCode\": \"20500\",  \"toCity\": \"Washington\",  
        //\"weight\": {    \"value\": 3,    \"units\": \"ounces\"  },  
        //\"dimensions\": {    \"units\": \"inches\",    \"length\": 7,    \"width\": 5,    \"height\": 6  },  
        //\"confirmation\": \"delivery\",  \"residential\": false}";
  
        public string carrierCode { get; set; }
        public string serviceCode { get; set; }

        public string packageCode { get; set; }
        public string fromPostalCode { get; set; }
        public string toState { get; set; }
        public string toCountry { get; set; }
        public string toPostalCode { get; set; }
        public string toCity { get; set; }
        public string wvalue { get; set; }
        public string wunits { get; set; }
        public string dlength { get; set; }
        public string dwidth { get; set; }
        public string dheight { get; set; }
        public string dunits { get; set; }

        public string confirmation { get; set; }
        public string residential { get; set; }
    }

  
    public class Rate
    {
        private string serviceName;
        private string serviceCode;
        private double shipmentCost;
        private double otherCost;

        public string ServiceName
        {
            get
            {
                return serviceName;
            }
            set
            {
                serviceName = value;
            }
        }
        public string ServiceCode
        {
            get
            {
                return serviceCode;
            }
            set
            {
                serviceCode = value;
            }
        }
        public double ShipmentCost
        {
            get
            {
                return shipmentCost;
            }
            set
            {
                shipmentCost = value;
            }
        }
        public double OtherCost
        {
            get
            {
                return otherCost;
            }
            set
            {
                otherCost = value;
            }
        }
    }

    public class RateToDisplay
    {
        private string sku;
        private string serviceName;
        private string serviceCode;
        private double shipmentCost;
        private double purchasePrice;
        private double insuranceCost;
        private double otherCost;

        public string ServiceName
        {
            get
            {
                return serviceName;
            }
            set
            {
                serviceName = value;
            }
        }
        public string SKU
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
            }
        }
        public string ServiceCode
        {
            get
            {
                return serviceCode;
            }
            set
            {
                serviceCode = value;
            }
        }
        public double ShipmentCost
        {
            get
            {
                return shipmentCost;
            }
            set
            {
                shipmentCost = value;
            }
        }
        public double InsuranceCost
        {
            get
            {
                return insuranceCost;
            }
            set
            {
                insuranceCost = value;
            }
        }
        public double PurchasePrice
        {
            get
            {
                return purchasePrice;
            }
            set
            {
                purchasePrice = value;
            }
        }

        public double OtherCost
        {
            get
            {
                return otherCost;
            }
            set
            {
                otherCost = value;
            }
        }
    }

    public class RateToOutputOLD
    {
        public string sku { get; set; }
        public double purchasePrice { get; set; }
        public double existingDomesticShiping { get; set; }
        public double shipmentCost { get; set; }
        public double insuranceCost { get; set; }
        public double otherCost { get; set; }
        public double USPSPriorityMail { get; set; }
        public double USPSParcelSelect { get; set; }
        public double USPSFirstClassMail { get; set; }
        public double UPSGround { get; set; }
        public double UPSNextDayAirSaver { get; set; }
        public string BestService { get; set; }
        public double BestRate { get; set; }
    }

    public class RateToOutput
    {
        public string sku { get; set; }
        public double purchasePrice { get; set; }
        public double existingDomesticShiping { get; set; }
        public double shipmentCost { get; set; }
        public double otherCost { get; set; }
        public double USPSPriorityMail { get; set; }
        public double PriorityMailinsurance { get; set; }
       
        public double USPSParcelSelect { get; set; }
        public double ParcelSelectinsurance { get; set; }
       
        public double USPSFirstClassMail { get; set; }
        public double FirstClassMailinsurance { get; set; }
       
        public double UPSGround { get; set; }
        public double Groundinsurance { get; set; }
       
        public double UPSNextDayAirSaver { get; set; }
        public double NextDayAirSaverinsurance { get; set; }
       
        public string BestService { get; set; }
        public double BestRate { get; set; }
    }
    public class Carrier
    {
        public string name { get; set; }
        public string code { get; set; }
        public string accountNumber { get; set; }
        public Boolean requiresFundedAccount { get; set; }
        public double balance { get; set; }
        public string nickname { get; set; }
        public string shippingProviderId { get; set; }
        public Boolean primary { get; set; }

    }

    public class Service
    {
        public string carrierCode { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public Boolean domestic { get; set; }
        public Boolean international { get; set; }

    }
    public class Package
    {
        public string carrierCode;
        public string code;
        public string name;
        public Boolean domestic;
        public Boolean international;
    }

    public class Serviice
    {
        public string carrierCode;
        public string code;
        public string name;
        public Boolean domestic;
        public Boolean international;
    }

    public class CsvFile
    {
        public string sku;
        public string weightValue;
        public string sizeLength;
        public string sizeWidth;
        public string sizeHeight;        
    }

}
