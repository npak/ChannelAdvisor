using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ChannelAdvisor
{
    public static class CAUtils
    {
        /// <summary>
        /// Get profile API key for Channel Advisor services
        /// </summary>
        /// <param name="profileID">Profile identifier</param>
        /// <returns>String which represents API key</returns>
        public static string GetProfileAPIKey(int profileID)
        {
            DataRow[] profiles = new DAL().GetProfiles().Tables[0].Select("ID = " + profileID);
            string apiKey = "";

            if (profiles.GetLength(0) > 0)
                apiKey = profiles[0]["ProfileAPIKey"].ToString();

            return apiKey;
        }

        /// <summary>
        /// Get credentials for Channel Advisor Inventory Service
        /// </summary>
        /// <returns>Credentials for Channel Advisor Inventory Service</returns>
        public static CA.InventoryService.APICredentials GetInventoryServiceCredentials()
        {
            CA.InventoryService.APICredentials credentials = new CA.InventoryService.APICredentials();
            credentials.DeveloperKey = "2a980ebc-63e1-4b2f-a1e1-3d68925d4db1";
            credentials.Password = "steZaph4";

            return credentials;
        }

        /// <summary>
        /// Get credentials for Channel Advisor Store Service
        /// </summary>
        /// <returns>Credentials for Channel Advisor Store Service</returns>
        public static CAStoreService.APICredentials GetStoreServiceCredentials()
        {
            CAStoreService.APICredentials credentials = new CAStoreService.APICredentials();
            credentials.DeveloperKey = "2a980ebc-63e1-4b2f-a1e1-3d68925d4db1";
            credentials.Password = "steZaph4";

            return credentials;
        }

        /// <summary>
        /// Get object which represents Channel Advisor Order Service
        /// </summary>
        /// <returns>Object which represents Channel Advisor Order Service</returns>
        public static CAStoreService.StoreService GetStoreService()
        {
            CAStoreService.StoreService storeService = new CAStoreService.StoreService();
            //Get web service URL
            storeService.Url = new DAL().GetWebServiceURL((int)CAServiceType.StoreService);
            storeService.Timeout = 180000;//set timeout to 3 minutes

            storeService.APICredentialsValue = CAUtils.GetStoreServiceCredentials();

            return storeService;
        }
    }
}
