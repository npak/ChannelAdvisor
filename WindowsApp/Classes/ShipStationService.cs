using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace ChannelAdvisor
{
    
    /// <summary>
    /// class to manage with ShipStation APIs data
    /// </summary>
    public  class ShipStationService
    {
        
        /// <summary>
        /// Get RateParameters Object
        /// </summary>
        /// <param name="row"> array of weight and dimentions</param>
        public RateParameters GetRateParameters(string[] row)
        {
            ShipStationAPIs ssa = new ShipStationAPIs();
            return ssa.GetRateParametersObject(row);
        }//end method

        ///// <summary>
        ///// method to to Read list of objects RateToDisplay
        ///// upload on ftp 
        ///// </summary>
        ///// <param></param>
        /////<returns>list of objects</returns>
        //public List<RateToDisplay> GetRates()
        //{
        //    ShipStationAPIs ssa = new ShipStationAPIs();
        //    List<RateToDisplay> list =ssa.GetRateList();
        //    return list;
        //}

        /// <summary>
        /// method to to Read list of objects RateToDisplay
        /// upload on ftp 
        /// </summary>
        /// <param></param>
        ///<returns>list of objects</returns>
        public List<RateToOutput> GetRatesOutput()
        {
            ShipStationAPIs ssa = new ShipStationAPIs();
            List<RateToOutput> list = ssa.GetRateListOutput();
            
            if (list!=null)
                ssa.UploadRateCsvFile(list, ssa.OutputFileName);
            return list;
        }

        /// <summary>
        /// method to search Rates by rateparam object
        /// </summary>
        /// <param></param>
        public List<Rate> SearchRatesByParams(string carriercode, string str)
        {
            string[] dim = str.Split(',');
            ShipStationAPIs ssa = new ShipStationAPIs();
            ssa.DefaultCarrier = carriercode;
            return  ssa.SearchRateListByParams(ssa.GetRateParametersObject(dim));
        }

        public List<Carrier> GetCarriersList()
        {
            ShipStationAPIs ssa = new ShipStationAPIs();
            List<Carrier> listCarrier = JsonConvert.DeserializeObject<List<Carrier>>(ssa.ReadAPI_ListCarriers());
            return listCarrier;
        }

        public List<Service> GetServicesList()
        {
            List<Service> listServices = new List<Service>();
            ShipStationAPIs ssa = new ShipStationAPIs();
            List<Carrier> listCarrier = JsonConvert.DeserializeObject<List<Carrier>>(ssa.ReadAPI_ListCarriers());
            foreach (Carrier carr in listCarrier)
            {
                listServices.AddRange(JsonConvert.DeserializeObject<List<Service>>(ssa.ReadAPI_ListServices(carr.code)) );
            }
            return listServices;
        }
    }//end class

}//end namespace
