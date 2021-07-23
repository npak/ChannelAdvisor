using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ChannelAdvisor
{
    public class DAL
    {
        DBAccess dbAccess;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DAL()
        {
            dbAccess = new DBAccess();
        }//end constructor

        /// <summary>
        /// Method to get list of vendors
        /// </summary>
        /// <returns></returns>
        public DataSet GetDistinctVendors()
        {
            return dbAccess.ExecuteDataSet("GetVendorsForCAUpdate");
        }

        /// <summary>
        /// Method to retreive Frequency Weekdays
        /// </summary>
        /// <returns></returns>
        public DataSet GetFrequencyWeekDays(int vendorID)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@VendorID", vendorID);

            return dbAccess.ExecuteDataSet("GetFrequencyWeekDays", param);
        }//end method


        /// <summary>
        /// Method to retreive Frequency Times
        /// </summary>
        /// <returns></returns>
        public DataSet GetFrequencyTimes(int vendorID)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@VendorID", vendorID);

            return dbAccess.ExecuteDataSet("GetFrequencyTimes", param);
        }//end method

    }//end class

}//end namespace
