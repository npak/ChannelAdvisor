using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;


namespace ChannelAdvisor
{
    /// <summary>
    /// Class to have all data access methods
    /// </summary>
    public class DAL
    {
        #region fields
        DBAccess dbAccess;

        //private constants
        private const string CONST_Key_EMGURL = "EMG_URL";
        private const string CONST_Key_EMG_Customer_ID = "EMG_Customer_ID";

        private const string CONST_EMGFTP = "EMG_FTP";
        private const string CONST_EMG_FtpUserName = "EMG_FtpUsername";
        private const string CONST_EMG_FtpPassword = "EMG_FtpPassword";

        private const string CONST_SP_GetSetting = "GetSetting";
        private const string CONST_EMG_CSVFolder = "EMG_CSVFolder";
        private const string CONST_EMG_CSVFIle = "EMG_CSVFIle";
        private const string CONST_EMG_IsFTP = "EMG_CSVIsFTP";


        private const string CONST_Key_Wynit_FTP_Server = "Wynit_FTP_Server";
        private const string CONST_Key_Wynit_FTP_UserName = "Wynit_FTP_UserName";
        private const string CONST_Key_Wynit_FTP_Password = "Wynit_FTP_Password";


        private const string CONST_PCycle_URL = "PCycle_URL";
        private const string CONST_PCycle_Username = "PCycle_Username";
        private const string CONST_PCycle_Password = "PCycle_Password";
        private const string CONST_PCycle_100_Qty = "PCycle_100_Qty";
        private const string CONST_PCycle_CSVFolder = "PCycle_CSVFolder";
        private const string CONST_PCycle_CSVFIle = "PCycle_CSVFIle";
        private const string CONST_PCycle_IsFTP = "PCycle_CSVIsFTP";

        private const string CONST_Benchmark_URL = "Benchmark_URL";
        private const string CONST_Benchmark_Username = "Benchmark_Username";
        private const string CONST_Benchmark_Password = "Benchmark_Password";
        private const string CONST_Benchmark_CSVFolder = "Benchmark_CSVFolder";
        private const string CONST_Benchmark_CSVFIle = "Benchmark_CSVFIle";
        private const string CONST_Benchmark_IsFTP = "Benchmark_CSVIsFTP";

        private const string CONST_PetGear_URL = "PetGear_URL";
        private const string CONST_PetGear_CSVFolder = "PetGear_CSVFolder";
        private const string CONST_PetGear_CSVFIle = "PetGear_CSVFIle";
        private const string CONST_PetGear_IsFTP = "PetGear_CSVIsFTP";
        public const string CONST_PetGear_InStockValue = "PetGear_InStockValue";

        private const string CONST_DUpAmerica_URL = "DUpAmerica_URL";
        private const string CONST_DUpAmerica_CSVFolder = "DUpAmerica_CSVFolder";
        private const string CONST_DUpAmerica_CSVFIle = "DUpAmerica_CSVFIle";
        private const string CONST_DUpAmerica_IsFTP = "DUpAmerica_CSVIsFTP";

        private const string CONST_RockLine_URL = "Rockline_URL";
        private const string CONST_RockLine_CSVFolder = "RockLine_CSVFolder";
        private const string CONST_RockLine_CSVFIle = "RockLine_CSVFIle";
        private const string CONST_RockLine_IsFTP = "RockLine_CSVIsFTP";


        //private const string CONST_PTime_URL = "PTime_URL";
        private const string CONST_PTime_Excel_URL = "PTime_Excel_URL";
        private const string CONST_PTime_Csv_URL = "PTime_Csv_URL";
        private const string CONST_PTime_IsExcel_URL = "PTime_IsExcel_URL";

        private const string CONST_LWAPI_ApplicationID = "LWAPI_ApplicationId";
        private const string CONST_LWAPI_ApplicationSecret = "LWAPI_ApplicationSecret";
        private const string CONST_LWAPI_Token = "LWAPI_Token";
        private const string CONST_LWAPI_Folder = "LWAPI_Folder";
        private const string CONST_LWAPI_File = "LWAPI_File";
        private const string CONST_LWAPI_FromID = "LWAPI_FromID";
        private const string CONST_LWAPI_ToID = "LWAPI_ToID";

        private const string CONST_PTime_CSVFolder = "PTime_CSVFolder";
        private const string CONST_PTime_CSVFIle = "PTime_CSVFIle";
        private const string CONST_PTime_IsFTP = "PTime_CSVIsFTP";
        private const string CONST_PTime_DropShipFee = "PTime_DropShipFee";

        private const string CONST_Petra_URL = "Petra_URL";
        private const string CONST_Petra_FTPFile = "Petra_FTPFile";
        private const string CONST_Petra_Username = "Petra_Username";
        private const string CONST_Petra_Password = "Petra_Password";
        private const string CONST_Petra_CSVFolder = "Petra_CSVFolder";
        private const string CONST_Petra_CSVFIle = "Petra_CSVFIle";
        private const string CONST_Petra_CSVFIleToServer = "Petra_CSVFIleToServer";

        private const string CONST_Petra_Order_URL = "Petra_Order_URL";
        private const string CONST_Petra_Order_Username = "Petra_Order_Username";
        private const string CONST_Petra_Order_Password = "Petra_Order_Password";
        private const string CONST_Petra_Order_Folder = "Petra_Order_Folder";
        private const string CONST_Petra_Order_Archive = "Petra_Order_Archive";
        //private const string CONST_Petra_Order_File = "Petra_Order_File";
        public const string CONST_PetraReformat_InFolder = "PetraReformat_InFolder";
        public const string CONST_PetraReformat_OutFolder = "PetraReformat_OutFolder";
        public const string CONST_PetraReformat_ArchiveFolder = "PetraReformat_ArchiveFolder";

        private const string CONST_Petra_IsFTP = "Petra_CSVIsFTP";
        private const string CONST_Petra_DropShipFee = "Petra_DropShipFee";

        private const string CONST_AZ_URL = "AZ_URL";
        private const string CONST_AZ_CSVFolder = "AZ_CSVFolder";
        private const string CONST_AZ_CSVFIle = "AZ_CSVFIle";
        private const string CONST_AZ_CSVFIleImage = "AZ_CSVFIleImage";
        private const string CONST_AZ_IsFTP = "AZ_CSVIsFTP";
        private const string CONST_AZ_DropShipFee = "AZ_DropShipFee";

        private const string CONST_Moteng_URL = "Moteng_URL";
        private const string CONST_Moteng_CSVFolder = "Moteng_CSVFolder";
        private const string CONST_Moteng_CSVFIle = "Moteng_CSVFIle";
        private const string CONST_Moteng_CSVProdFIle = "Moteng_CSVProdFIle";
        private const string CONST_Moteng_CSVPriceFIle = "Moteng_CSVPriceFIle";
        private const string CONST_Moteng_CSVQtyFIle = "Moteng_CSVQtyFIle";
        private const string CONST_Moteng_CSVFIleConverted = "Moteng_CSVFIleConverted";
        private const string CONST_Moteng_IsFTP = "Moteng_CSVIsFTP";
        private const string CONST_Moteng_DropShipFee = "Moteng_DropShipFee";
        private const string CONST_Moteng_Username = "Moteng_Username";
        private const string CONST_Moteng_Password = "Moteng_Password";

        private const string CONST_NearlyNatural_URL = "NearlyNatural_URL";
        private const string CONST_NearlyNatural_CSVFolder = "NearlyNatural_CSVFolder";
        private const string CONST_NearlyNatural_CSVFIle = "NearlyNatural_CSVFIle";
        private const string CONST_NearlyNatural_CSVFIleOriginal = "NearlyNatural_CSVFIleOriginal";
        private const string CONST_NearlyNatural_IsFTP = "NearlyNatural_CSVIsFTP";
        private const string CONST_NearlyNatural_DropShipFee = "NearlyNatural_DropShipFee";

        private const string CONST_GreenSupply_URL = "GreenSupply_URL";
        private const string CONST_GreenSupply_CSVFolder = "GreenSupply_CSVFolder";
        private const string CONST_GreenSupply_CSVFIle = "GreenSupply_CSVFIle";
        private const string CONST_GreenSupply_IsFTP = "GreenSupply_CSVIsFTP";
        private const string CONST_GreenSupply_DropShipFee = "GreenSupply_DropShipFee";

        private const string CONST_Viking_URL = "Viking_URL";
        private const string CONST_Viking_CSVFolder = "Viking_CSVFolder";
        private const string CONST_Viking_CSVFIle = "Viking_CSVFIle";
        private const string CONST_Viking_IsFTP = "Viking_CSVIsFTP";
        private const string CONST_Viking_DropShipFee = "Viking_DropShipFee";

        private const string CONST_Sumdex_Folder = "Sumdex_Folder";

        private const string CONST_CWR_URL = "CWR_URL";
        private const string CONST_CWR_USER = "CWR_USER";
        private const string CONST_CWR_PSW = "CWR_PSW";

        private const string CONST_CWR_CSVFolder = "CWR_CSVFolder";
        private const string CONST_CWR_CSVFIle = "CWR_CSVFIle";
        private const string CONST_CWR_IsFTP = "CWR_CSVIsFTP";

        private const string CONST_Seawide_URL = "Seawide_URL";
        private const string CONST_Seawide_USER = "Seawide_USER";
        private const string CONST_Seawide_PSW = "Seawide_PSW";
        private const string CONST_Seawide_FTPFIle = "Seawide_FTPFIle";
        public const string CONST_Seawide_CSVFolder = "Seawide_CSVFolder";
        public const string CONST_Seawide_CSVFIle = "Seawide_CSVFIle";
        public const string CONST_Seawide_IsFTP = "Seawide_CSVIsFTP";

        private const string CONST_TWH_URL = "TWH_URL";
        private const string CONST_TWH_USER = "TWH_USER";
        private const string CONST_TWH_PSW = "TWH_PSW";
        public const string CONST_TWH_CSVFolder = "TWH_CSVFolder";
        public const string CONST_TWH_CSVFIle = "TWH_CSVFIle";
        public const string CONST_TWH_IsFTP = "TWH_CSVIsFTP";
        private const string CONST_TWH_DropShipFee = "TWH_DropShipFee";


        private const string CONST_MorrisDailySummary_URL = "MorrisDailySummary_URL";
        private const string CONST_MorrisDailySummary_CSVFolder = "MorrisDailySummary_CSVFolder";
        private const string CONST_MorrisDailySummary_CSVFIle = "MorrisDailySummary_CSVFIle";
        private const string CONST_MorrisDailySummary_CreditCSVFolder = "MorrisDailySummary_CreditCSVFolder";
        private const string CONST_MorrisDailySummary_CreditCSVFIle = "MorrisDailySummary_CreditCSVFIle";

        private const string CONST_MorrisWeeklySummary_URL = "MorrisWeeklySummary_URL";
        private const string CONST_MorrisWeeklySummary_CSVFolder = "MorrisWeeklySummary_CSVFolder";
        private const string CONST_MorrisWeeklySummary_CSVFIle = "MorrisWeeklySummary_CSVFIle";
        private const string CONST_MorrisWeeklySummary_CreditCSVFolder = "MorrisWeeklySummary_CreditCSVFolder";
        private const string CONST_MorrisWeeklySummary_CreditCSVFIle = "MorrisWeeklySummary_CreditCSVFIle";

        private const string CONST_Morris_URL = "Morris_URL";
        private const string CONST_Morris_DropShipFee = "Morris_DropShipFee";
        private const string CONST_Morris_CSVFolder = "Morris_CSVFolder";
        private const string CONST_Morris_CSVFIle = "Morris_CSVFIle";
        private const string CONST_Morris_IsFTP = "Morris_CSVIsFTP";

        private const string CONST_MorrisComplete_URL = "MorrisComplete_URL";
        private const string CONST_MorrisComplete_DropShipFee = "MorrisComplete_DropShipFee";
        private const string CONST_MorrisComplete_CSVFolder = "MorrisComplete_CSVFolder";
        private const string CONST_MorrisComplete_CSVFIle = "MorrisComplete_CSVFIle";
        private const string CONST_MorrisComplete_IsFTP = "MorrisComplete_CSVIsFTP";

        private const string CONST_MorrisNightly_URL = "MorrisNightly_URL";
        private const string CONST_MorrisNightly_DropShipFee = "MorrisNightly_DropShipFee";
        private const string CONST_MorrisNightly_CSVFolder = "MorrisNightly_CSVFolder";
        private const string CONST_MorrisNightly_CSVFIle = "MorrisNightly_CSVFIle";
        private const string CONST_MorrisNightly_IsFTP = "MorrisNightly_CSVIsFTP";

        private const string CONST_MorrisChanges_URL = "MorrisChanges_URL";
        private const string CONST_MorrisChanges_DropShipFee = "MorrisChanges_DropShipFee";
        private const string CONST_MorrisChanges_CSVFolder = "MorrisChanges_CSVFolder";
        private const string CONST_MorrisChanges_CSVFIle = "MorrisChanges_CSVFIle";
        private const string CONST_MorrisChanges_IsFTP = "MorrisChanges_CSVIsFTP";

        private const string CONST_EMG_ORDER_Ship_To_ID = "EMG_ORDER_Ship_To_ID";
        private const string CONST_EMG_ORDER_SendOrder_URL = "EMG_ORDER_SendOrder_URL";
        private const string CONST_EMG_ORDER_GetOrderStatus_URL = "EMG_ORDER_GetOrderStatus_URL";
        private const string CONST_EMG_ORDER_UOM_Code = "EMG_ORDER_UOM_Code";
        private const string CONST_EMG_ORDER_CSV_File = "EMG_ORDER_CSV_File";
        private const string CONST_EMG_ORDER_StoneEdge_DB = "EMG_ORDER_StoneEdge_DB";
        #endregion

        #region ctor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DAL()
        {
            dbAccess = new DBAccess();
        }//end constructor 
        #endregion

        #region EMG Extractor
        /// <summary>
        /// Get EMG Extractor settings from DB
        /// </summary>
        /// <returns>EMG Extractor settings</returns>
        public DataRow LoadEMGExtractorSettings()
        {
            DataSet lDS = dbAccess.ExecuteDataSet("GetEmgExtractorSettings", new SqlParameter[0] { });
            return lDS.Tables[0].Rows[0];
        }
        /// <summary>
        /// Update EMG Extractor settings
        /// </summary>
        /// <param name="aSettings">New values of settings</param>
        /// <returns>True if operation successful, otherwise false</returns>
        public bool UpdateEMGExtractorSettings(EMGExtractorSettings aSettings)
        {
            List<SqlParameter> lList = new List<SqlParameter>();
            lList.Add(new SqlParameter("@URLToLoadXML", aSettings.URLToLoadXML));
            lList.Add(new SqlParameter("@SupplierCode", aSettings.SupplierCode));
            lList.Add(new SqlParameter("@WarehouseLocation", aSettings.WarehouseLocation));
            lList.Add(new SqlParameter("@DCCode", aSettings.DCCode));
            lList.Add(new SqlParameter("@AuctionTitleMaxLength", aSettings.AuctionTitleMaxLength));
            lList.Add(new SqlParameter("@CAStoreTitleMaxLength", aSettings.CAStoreTitleMaxLength));
            lList.Add(new SqlParameter("@ClassificationMaxLength", aSettings.ClassificationMaxLength));
            lList.Add(new SqlParameter("@WarrantyLabel", aSettings.WarrantyLabel));
            lList.Add(new SqlParameter("@WarrantyDefaultValue", aSettings.WarrantyDefaultValue));
            lList.Add(new SqlParameter("@OutputFilePrefix", aSettings.OutputFilePrefix));

            return dbAccess.ExecuteCommand("UpdateEMGExtractorSettings", lList.ToArray());
        }
        #endregion

        #region EMG Order Status


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public bool DeleteEMGOrdersSentOrder(string orderNo)
        {
            //create paramater
            SqlParameter paramOrderNo = new SqlParameter("@OrderNo", orderNo);

            return dbAccess.ExecuteCommand("DeleteEMGOrdersSentOrder", paramOrderNo);
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetEMGOrdersSent()
        {
            return dbAccess.ExecuteDataSet("GetEMGOrdersSent");
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public bool UpdateTrackingNos(OrderStatus orderStatus)
        {
            //get access connection string
            string conString = ConfigurationManager.ConnectionStrings["AccessConnection"].ConnectionString;

            //get database path
            SqlParameter paramKey = new SqlParameter("@Key", CONST_EMG_ORDER_StoneEdge_DB);
            string dbPath = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            //append db path to connection string
            conString = String.Format(conString, dbPath);

            //string conString = @"Provider=Microsoft.ACE.OLEDB.12.0; User Id=; Password=; Data Source=C:\Program Files\StoneEdge\OrderManagerDataBU20090226.mdb";
            string sql = "INSERT INTO Tracking(OrderNum, PickupDate, Carrier, ShippersMethod, TrackingID) " +
                            "VALUES(@OrderNum, @PickupDate, @Carrier, @ShippersMethod, @TrackingID)";

            //check for multiple tracking no
            string trackingNos = orderStatus.ShipReference;
            string[] trackingNoArray = null;

            if (trackingNos.IndexOf(",") > -1)
            {
                trackingNoArray = trackingNos.Split(Convert.ToChar(","));
            }
            else
            {
                trackingNoArray = new string[1];
                trackingNoArray[0] = trackingNos;
            }//end if


            //get Carrier and shippers method
            string carrier = "";
            string shippersMethod = "";

            DataTable dtRouteDetails = GetRouteCodeDetails(orderStatus.ShippingMethod).Tables[0];
            if (dtRouteDetails.Rows.Count > 0)
            {
                carrier = dtRouteDetails.Rows[0]["Carrier"] == null ? "" : dtRouteDetails.Rows[0]["Carrier"].ToString();
                shippersMethod = dtRouteDetails.Rows[0]["ShippingMethod"] == null ? "" : dtRouteDetails.Rows[0]["ShippingMethod"].ToString();
            }//end if

            //Create sql statement for updating orderdetails
            string orderDetailsSQL = "UPDATE [Order Details] SET DateShipped = @DateShipped WHERE OrderNumber = @OrderNum AND ProductType='Tangible'";
            //Create parameters for updating order details
            OleDbParameter paramOrderNumORD = new OleDbParameter("@OrderNum", Convert.ToInt32(orderStatus.OrderNo));
            OleDbParameter paramOrderShipDate = new OleDbParameter("@DateShipped", Convert.ToDateTime(orderStatus.ShipDate));

            //Create sql statemement to update Net Supplier Cost
            string ordersUpdSQL = "UPDATE Orders SET LocalSortCurrency1 = @NetAmt WHERE OrderNumber = @OrderNum";
            //Create parameters for updating orders table
            OleDbParameter paramOrderNumORD2 = new OleDbParameter("@OrderNum", Convert.ToInt32(orderStatus.OrderNo));
            OleDbParameter paramOrderNetAmt = new OleDbParameter("@NetAmt", orderStatus.NetAmount);


            int rowCount = 0;

            using (OleDbConnection con = new OleDbConnection(conString))
            {

                //update statement
                OleDbCommand updStatement = new OleDbCommand(orderDetailsSQL, con);
                updStatement.Parameters.Add(paramOrderShipDate);
                updStatement.Parameters.Add(paramOrderNumORD);

                //update orders statement
                OleDbCommand ordersUpdStatement = new OleDbCommand(ordersUpdSQL, con);
                ordersUpdStatement.Parameters.Add(paramOrderNetAmt);
                ordersUpdStatement.Parameters.Add(paramOrderNumORD2);

                try
                {
                    con.Open();

                    //loop tracking nos and update
                    for (int x = 0; x < trackingNoArray.GetLength(0); x++)
                    {
                        //Create parameters
                        OleDbParameter paramOrderNum = new OleDbParameter("@OrderNum", orderStatus.OrderNo);
                        OleDbParameter paramPickupDate = null;
                        if (orderStatus.ShipDate != null)
                        {
                            DateTime tempDate = (DateTime)orderStatus.ShipDate;
                            paramPickupDate = new OleDbParameter("@PickupDate", tempDate.ToString("dd-MMM-yyyy"));
                        }
                        else
                        {
                            paramPickupDate = new OleDbParameter("@PickupDate", "");
                        }

                        OleDbParameter paramCarrier = new OleDbParameter("@Carrier", carrier);
                        OleDbParameter paramShipMethod = new OleDbParameter("@ShippersMethod", shippersMethod);
                        OleDbParameter paramTracking = new OleDbParameter("@TrackingID", trackingNoArray[x].Trim());

                        //insert statement
                        OleDbCommand statement = new OleDbCommand(sql, con);

                        statement.Parameters.Add(paramOrderNum);
                        statement.Parameters.Add(paramPickupDate);
                        statement.Parameters.Add(paramCarrier);
                        statement.Parameters.Add(paramShipMethod);
                        statement.Parameters.Add(paramTracking);

                        rowCount = statement.ExecuteNonQuery();
                    }//end for



                    int updCount = updStatement.ExecuteNonQuery();

                    updCount = ordersUpdStatement.ExecuteNonQuery();

                    con.Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("GetOrderStatus Error - " + ex.Message);
                    throw ex;
                }

            }//end using

            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }//end if

        }//end method


        /// <summary>
        /// Delete schedules for all order statuses which are over 1 month old
        /// </summary>
        public void DeleteEMGOrderStatusSchedulesOver1MonthOld()
        {
            dbAccess.ExecuteCommand("DeleteEMGOrderStatusSchedulesOver1MonthOld");
        }//end method


        /// <summary>
        /// Method to delete schedule
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        public void DeleteOrderStatusSchedule(string fromDate, string toDate)
        {
            //Create sql parameter
            SqlParameter paramFromDate = new SqlParameter("@FromDate", fromDate);
            SqlParameter paramToDate = new SqlParameter("@ToDate", toDate);

            dbAccess.ExecuteCommand("DeleteEMGOrderStatusSchedule", paramFromDate, paramToDate);

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderStatusList"></param>
        /// <returns></returns>
        public void SaveEMGOrderStatusList(List<OrderStatus> orderStatusList)
        {
            Guid scheduleID = SaveEMGOrderStatusSchedule();

            //loop and save Order Status
            for (int x = 0; x < orderStatusList.Count; x++)
            {
                SaveEMGOrderStatus(orderStatusList[x], scheduleID);
            }//end for
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderStatus"></param>
        public void SaveEMGOrderStatus(OrderStatus orderStatus, Guid scheduleID)
        {
            //Create parameters
            SqlParameter paramScheduleID = new SqlParameter("@ScheduleID", scheduleID);
            SqlParameter paramOrderNo = new SqlParameter("@OrderNo", orderStatus.OrderNo);
            SqlParameter paramStatus = new SqlParameter("@Status", orderStatus.Status);
            SqlParameter paramErrorMessage = new SqlParameter("@ErrorMessage", orderStatus.ErrorMessage);
            SqlParameter paramOrderStatus = new SqlParameter("@OrderStatus", orderStatus.OrderStatusText);
            SqlParameter paramShipReference = new SqlParameter("@ShipReference", orderStatus.ShipReference);
            SqlParameter paramShippingMethod = new SqlParameter("@ShippingMethod", orderStatus.ShippingMethod);
            SqlParameter paramShippingCost = new SqlParameter("@ShippingCost", orderStatus.ShippingCost);
            SqlParameter paramShipDate = new SqlParameter("@ShipDate", orderStatus.ShipDate);
            SqlParameter paramNetAmount = new SqlParameter("@NetAmount", orderStatus.NetAmount);
            SqlParameter paramPayment = new SqlParameter("@Payment", orderStatus.Payment);
            SqlParameter paramPaymentDate = new SqlParameter("@PaymentDate", orderStatus.PaymentDate);
            SqlParameter paramIsStoneEdgeUpdated = new SqlParameter("@IsStoneEdgeUpdated", orderStatus.IsStoneEdgeUpdated);

            dbAccess.ExecuteCommand("SaveEMGOrderStatus", paramScheduleID,
                                        paramOrderNo,
                                        paramStatus,
                                        paramErrorMessage,
                                        paramOrderStatus,
                                        paramShipReference,
                                        paramShippingMethod,
                                        paramShippingCost,
                                        paramShipDate,
                                        paramNetAmount,
                                        paramPayment,
                                        paramPaymentDate,
                                        paramIsStoneEdgeUpdated);
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Guid SaveEMGOrderStatusSchedule()
        {
            Guid id = Guid.NewGuid();

            //create parameter
            SqlParameter paramID = new SqlParameter("@ID", id);

            dbAccess.ExecuteCommand("SaveEMGOrderStatusSchedule", paramID);

            return id;
        }//end methof

        /// <summary>
        /// Method gets the last schedule run time
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public DateTime GetLastEMGOrderStatusRunTime()
        {
            //return datetime
            return Convert.ToDateTime(dbAccess.ExecuteScalar("GetLastEMGOrderStatusRunTime"));
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetPendingEMGOrders()
        {
            return dbAccess.ExecuteDataSet("GetPendingEMGOrders");
        }//end method


        /// <summary>
        /// Method to get schedules which were run between the specified period
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataSet GetEMGOrderStatusSchedule(string fromDate, string toDate)
        {
            //Create sql parameter
            SqlParameter paramFromDate = new SqlParameter("@FromDate", fromDate);
            SqlParameter paramToDate = new SqlParameter("@ToDate", toDate);

            return dbAccess.ExecuteDataSet("GetEMGOrderStatusSchedule", paramFromDate, paramToDate);

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public DataSet GetEMGOrderStatus(Guid scheduleID)
        {
            //Create sql parameter
            SqlParameter paramScheduleID = new SqlParameter("@ScheduleID", scheduleID);

            return dbAccess.ExecuteDataSet("GetEMGOrderStatus", paramScheduleID);
        }//end method



        #endregion

        #region EMG Order Update


        /// <summary>
        /// Delete schedules for all order update schedules which are over 1 month old
        /// </summary>
        public void DeleteEMGOrderUpdateSchedulesOver1MonthOld()
        {
            dbAccess.ExecuteCommand("DeleteEMGOrderUpdateSchedulesOver1MonthOld");
        }//end method

        /// <summary>
        /// Method to delete schedule
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        public void DeleteOrderUpdateSchedule(string fromDate, string toDate)
        {
            //Create sql parameter
            SqlParameter paramFromDate = new SqlParameter("@FromDate", fromDate);
            SqlParameter paramToDate = new SqlParameter("@ToDate", toDate);

            dbAccess.ExecuteCommand("DeleteEMGOrderUpdateSchedule", paramFromDate, paramToDate);

        }//end method


        /// <summary>
        /// Method to get schedules which were run between the specified period
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataSet GetEMGOrderUpdateSchedules(string fromDate, string toDate)
        {
            //Create sql parameter
            SqlParameter paramFromDate = new SqlParameter("@FromDate", fromDate);
            SqlParameter paramToDate = new SqlParameter("@ToDate", toDate);

            return dbAccess.ExecuteDataSet("GetEMGOrderUpdateSchedules", paramFromDate, paramToDate);

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public DataSet GetEMGSuccessfulUpdates(Guid scheduleID)
        {
            //Create sql parameter
            SqlParameter paramScheduleID = new SqlParameter("@ScheduleID", scheduleID);

            return dbAccess.ExecuteDataSet("GetEMGSuccessfulUpdates", paramScheduleID);
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public DataSet GetEMGUnSuccessfulUpdates(Guid scheduleID)
        {
            //Create sql parameter
            SqlParameter paramScheduleID = new SqlParameter("@ScheduleID", scheduleID);

            return dbAccess.ExecuteDataSet("GetEMGUnSuccessfulUpdates", paramScheduleID);
        }//end method




        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeCode"></param>
        /// <returns></returns>
        public DataSet GetRouteCodeDetails(string routeCode)
        {
            SqlParameter paramRouteCode = new SqlParameter("@RouteCode", routeCode);

            return dbAccess.ExecuteDataSet("GetRouteCodeDetails", paramRouteCode);
        }//end method

        /// <summary>
        /// Method to retreive Transport Codes
        /// </summary>
        /// <returns></returns>
        public DataSet GetTransportCodes()
        {
            return dbAccess.ExecuteDataSet("GetTransportCodes");
        }//end method



        /// <summary>
        /// Method to save Transport Codes. First delete all Transport Codes
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SaveTransportCodes(DataTable dt)
        {
            using (SqlConnection sqlCn = dbAccess.GetConnection())
            {
                sqlCn.Open();
                SqlTransaction trans = sqlCn.BeginTransaction();

                //first delete transport codes
                dbAccess.ExecuteCommandTrans("DeleteTransportCodes", sqlCn, trans);

                //loop through datatable and insert each row
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        SqlParameter paramCode = new SqlParameter("@Code", dr["Code"]);
                        SqlParameter paramCarrierID = new SqlParameter("@CarrierID", dr["CarrierID"]);
                        SqlParameter paramTransportID = new SqlParameter("@TransportID", dr["TransportID"]);
                        SqlParameter paramCarrier = new SqlParameter("@Carrier", dr["Carrier"]);
                        SqlParameter paramShipMethod = new SqlParameter("@ShippingMethod", dr["ShippingMethod"]);

                        dbAccess.ExecuteCommandTrans("SaveTransportCode",
                                                        sqlCn,
                                                        trans,
                                                        paramCode,
                                                        paramCarrierID,
                                                        paramTransportID,
                                                        paramCarrier,
                                                        paramShipMethod);
                    }//end if

                }//end foreach

                trans.Commit();
                return true;

            }//end using

        }//end method


        /// <summary>
        /// Method gets the last schedule run time
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public DateTime GetLastEMGOrderUpdaterRunTime()
        {
            //return datetime
            return Convert.ToDateTime(dbAccess.ExecuteScalar("GetLastEMGOrderUpdaterRunTime"));
        }//end method



        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipToID"></param>
        /// <param name="sendOrderURL"></param>
        /// <param name="getOrderStatusURL"></param>
        /// <param name="uomCode"></param>
        /// <param name="csvFile"></param>
        /// <returns></returns>
        public bool SaveEMGOrderUpdateSettings(string shipToID,
                                            string sendOrderURL,
                                            string getOrderStatusURL,
                                            string uomCode,
                                            string csvFile,
                                            string stoneEdgeDB)
        {
            //Create parameters
            SqlParameter paramShipToID = new SqlParameter("@ShipToID", shipToID);
            SqlParameter paramSendOrderURL = new SqlParameter("@SendOrderURL", sendOrderURL);
            SqlParameter paramGetOrderStatusURL = new SqlParameter("@GetOrderStatusURL", getOrderStatusURL);
            SqlParameter paramUOMCode = new SqlParameter("@UOMCode", uomCode);
            SqlParameter paramCSVFile = new SqlParameter("@CSVFile", csvFile);
            SqlParameter paramStoneEdgeDB = new SqlParameter("@StoneEdgeDB", stoneEdgeDB);


            return dbAccess.ExecuteCommand("SaveEMGOrderUpdateSettings",
                                            paramShipToID,
                                            paramSendOrderURL,
                                            paramGetOrderStatusURL,
                                            paramUOMCode,
                                            paramCSVFile,
                                            paramStoneEdgeDB);

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="shipToID"></param>
        /// <param name="sendOrderURL"></param>
        /// <param name="getOrderStatusURL"></param>
        /// <param name="uomCode"></param>
        /// <param name="csvFile"></param>
        public void GetEMGOrderUpdateSettings(out string customerID,
                                            out string shipToID,
                                            out string sendOrderURL,
                                            out string getOrderStatusURL,
                                            out string uomCode,
                                            out string csvFile,
                                            out string stoneedgeDBPath)
        {
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Key_EMG_Customer_ID);
            customerID = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_EMG_ORDER_Ship_To_ID);
            shipToID = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_EMG_ORDER_SendOrder_URL);
            sendOrderURL = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_EMG_ORDER_GetOrderStatus_URL);
            getOrderStatusURL = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_EMG_ORDER_UOM_Code);
            uomCode = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_EMG_ORDER_CSV_File);
            csvFile = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_EMG_ORDER_StoneEdge_DB);
            stoneedgeDBPath = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="totalOrders"></param>
        /// <returns></returns>
        public bool SaveEMGOrderUpdateSchedule(Guid scheduleID, int totalOrders)
        {
            //Create parameters
            SqlParameter paramScheduleID = new SqlParameter("@ID", scheduleID);
            SqlParameter paramTotalOrders = new SqlParameter("@TotalOrders", totalOrders);

            return dbAccess.ExecuteCommand("SaveEMGOrderUpdateSchedule", paramScheduleID, paramTotalOrders);

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="orderNo"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SaveEMGOrderUpdateStatus(Guid scheduleID,
                                            string orderNo,
                                            string status,
                                            string message)
        {
            if (status == "0")
            {
                //Save to orders sent
                SaveEMGOrdersSent(orderNo, false);

                return SaveEMGSuccessfulUpdates(scheduleID, orderNo, status, message);
            }
            else
            {
                return SaveEMGUnSuccessfulUpdates(scheduleID, orderNo, status, message);
            }//end if
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="orderNo"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SaveEMGSuccessfulUpdates(Guid scheduleID,
                                            string orderNo,
                                            string status,
                                            string message)
        {
            //Create parameters
            SqlParameter paramScheduleID = new SqlParameter("@ScheduleID", scheduleID);
            SqlParameter paramOrderNo = new SqlParameter("@OrderNo", orderNo);
            SqlParameter paramStatus = new SqlParameter("@Status", status);
            SqlParameter paramMessage = new SqlParameter("@Message", message);

            return dbAccess.ExecuteCommand("SaveEMGSuccessfulUpdates", paramScheduleID, paramOrderNo, paramStatus, paramMessage);

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="orderNo"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SaveEMGUnSuccessfulUpdates(Guid scheduleID,
                                            string orderNo,
                                            string status,
                                            string message)
        {
            //Create parameters
            SqlParameter paramScheduleID = new SqlParameter("@ScheduleID", scheduleID);
            SqlParameter paramOrderNo = new SqlParameter("@OrderNo", orderNo);
            SqlParameter paramStatus = new SqlParameter("@Status", status);
            SqlParameter paramMessage = new SqlParameter("@Message", message);

            return dbAccess.ExecuteCommand("SaveEMGUnSuccessfulUpdates", paramScheduleID, paramOrderNo, paramStatus, paramMessage);

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="isComplete"></param>
        /// <returns></returns>
        public bool SaveEMGOrdersSent(string orderNo, bool isComplete)
        {
            //Create parameters
            SqlParameter paramOrderNo = new SqlParameter("@OrderNo", orderNo);
            SqlParameter paramIsComplete = new SqlParameter("@IsComplete", isComplete);

            return dbAccess.ExecuteCommand("SaveEMGOrdersSent", paramOrderNo, paramIsComplete);

        }//end method


        /// <summary>
        /// Method to retreive Frequency Weekdays
        /// </summary>
        /// <returns></returns>
        public DataSet GetEMGFrequencyWeekDays(int serviceType)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@ServiceType", serviceType);

            return dbAccess.ExecuteDataSet("GetEMGFrequencyWeekDays", param);
        }//end method


        /// <summary>
        /// Method to retreive EMG Frequency Times
        /// </summary>
        /// <returns></returns>
        public DataSet GetEMGFrequencyTimes(int serviceType)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@ServiceType", serviceType);

            return dbAccess.ExecuteDataSet("GetEMGFrequencyTimes", param);
        }//end method


        /// <summary>
        /// method to save EMG frequency weekdays and time
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="lstWeekDays"></param>
        /// <param name="dtFrequencyTimes"></param>
        /// <returns></returns>
        public bool SaveEMGFrequency(int serviceType,
                                    List<WeekDayDTO> lstFreqWeekDays,
                                    DataTable dtFrequencyTimes)
        {
            using (SqlConnection sqlCn = dbAccess.GetConnection())
            {
                sqlCn.Open();
                SqlTransaction trans = sqlCn.BeginTransaction();


                //Save Single profile frequency
                SaveEMGFreqWeekDays(serviceType, lstFreqWeekDays, sqlCn, trans);
                SaveEMGFreqTimes(serviceType, dtFrequencyTimes, sqlCn, trans);


                trans.Commit();
                return true;
            }//end using

        }//end method

        /// <summary>
        /// Method to save Frequency WeekDays. First delete all Weekdays for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SaveEMGFreqWeekDays(int serviceType, List<WeekDayDTO> lstWeekDays, SqlConnection sqlCn, SqlTransaction trans)
        {

            //first delete markups for the vendor
            SqlParameter paramServiceType = new SqlParameter("@EMGServiceType", serviceType);
            dbAccess.ExecuteCommandTrans("DeleteEMGFrequencyWeekDays", sqlCn, trans, paramServiceType);

            //loop through List
            foreach (WeekDayDTO weekDay in lstWeekDays)
            {
                paramServiceType = new SqlParameter("@EMGServiceType", serviceType);
                SqlParameter paramWeekDay = new SqlParameter("@WeekDay", weekDay.WeekDay);
                SqlParameter paramIsEnabled = new SqlParameter("@IsEnabled", weekDay.IsEnabled);

                dbAccess.ExecuteCommandTrans("SaveEMGFrequencyWeekDay", sqlCn, trans, paramServiceType, paramWeekDay, paramIsEnabled);
            }//end for each

        }//end method


        /// <summary>
        /// Method to save Frequency Times. First delete all Frequency times for the selected Service Type
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SaveEMGFreqTimes(int serviceType, DataTable dt, SqlConnection sqlCn, SqlTransaction trans)
        {
            //first delete markups for the vendor
            SqlParameter paramServiceType = new SqlParameter("@EMGServiceType", serviceType);
            dbAccess.ExecuteCommandTrans("DeleteEMGFrequencyTimes", sqlCn, trans, paramServiceType);

            //loop through datatable and insert each row
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    paramServiceType = new SqlParameter("@EMGServiceType", serviceType);
                    SqlParameter paramTime = new SqlParameter("@Time", dr["Time"]);

                    dbAccess.ExecuteCommandTrans("SaveEMGFrequencyTime", sqlCn, trans, paramServiceType, paramTime);
                }//end if

            }//end foreach

        }//end method



        #endregion

        #region CWR Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool SaveCWRSettings(string url,
                                    string user,
                                    string password, string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            //SqlParameter paramFields = new SqlParameter("@Fields", "NA");
            //S//qlParameter paramDelim = new SqlParameter("@Delim", "NA");
            SqlParameter paramUser = new SqlParameter("@USER", user);
            SqlParameter paramPSW = new SqlParameter("@PSW", password);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);

            return dbAccess.ExecuteCommand("SaveCWRSettings", paramURL, paramUser, paramPSW, paramFoldername, paramFilename, paramIsftp);

        }//end method



        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// 
        public void GetCWRSettings(out string url,
                             out string user,
                             out string password)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_CWR_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_CWR_USER);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            user = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_CWR_PSW);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            password = obj == null ? "" : obj.ToString();

        }//end method


        public void GetCWRSettings(out string url,
                                    out string user,
                                    out string password, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_CWR_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_CWR_USER);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            user = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_CWR_PSW);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            password = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_CWR_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_CWR_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_CWR_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();
        }//end method

        #endregion

        #region Seawide Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="foldername"></param>
        /// <param name="filename"></param>
        /// <param name="isftp"></param>
        /// <returns></returns>
        public bool SaveSeawideSettings(string url,
                                    string user,
                                    string password, string ftpfilename, string foldername, string filename, bool isftp)
        {
            if (UpdateSetting(CONST_Seawide_URL, url) &&
                UpdateSetting(CONST_Seawide_USER, user) &&
                UpdateSetting(CONST_Seawide_PSW, password) &&
                 UpdateSetting(CONST_Seawide_FTPFIle, ftpfilename) &&
                UpdateSetting(CONST_Seawide_CSVFolder, foldername) &&
                UpdateSetting(CONST_Seawide_CSVFIle, filename) &&
               UpdateSetting(CONST_Seawide_IsFTP, isftp ? "1" : "0"))
                return true;
            else
                return false;

            ////Create parameters
            //SqlParameter paramURL = new SqlParameter("@URL", url);         
            //SqlParameter paramUser = new SqlParameter("@USER", user);
            //SqlParameter paramPSW = new SqlParameter("@PSW", password);
            //SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            //SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            //SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);

            //return dbAccess.ExecuteCommand("SaveCWRSettings", paramURL, paramUser, paramPSW, paramFoldername, paramFilename, paramIsftp);

        }//end method



        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// 
        public void GetSeawideSettings(out string url,
                             out string user,
                             out string password)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Seawide_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Seawide_USER);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            user = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Seawide_PSW);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            password = obj == null ? "" : obj.ToString();

        }//end method


        public void GetSeawideSettings(out string url,
                                    out string user,
                                    out string password, out string ftpfilename, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Seawide_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Seawide_USER);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            user = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Seawide_PSW);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            password = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Seawide_FTPFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            ftpfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Seawide_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Seawide_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Seawide_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();
        }//end method

        #endregion


        #region TWH Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="filename"></param>
        /// <param name="isftp"></param>
        /// <param name="isftp"></param>
        /// <returns></returns>
        public bool SaveTWHSettings(string url,
                                    string user,
                                    string password, string foldername, string filename, bool isftp, string dropshipfee)
        {
            if (UpdateSetting(CONST_TWH_URL, url) &&
                UpdateSetting(CONST_TWH_USER, user) &&
                UpdateSetting(CONST_TWH_PSW, password) &&
                UpdateSetting(CONST_TWH_CSVFolder, foldername) &&
                UpdateSetting(CONST_TWH_CSVFIle, filename) &&
                UpdateSetting(CONST_TWH_IsFTP, isftp ? "1" : "0") &&
                UpdateSetting(CONST_TWH_DropShipFee, dropshipfee))
                return true;
            else
                return false;

            ////Create parameters
            //SqlParameter paramURL = new SqlParameter("@URL", url);         
            //SqlParameter paramUser = new SqlParameter("@USER", user);
            //SqlParameter paramPSW = new SqlParameter("@PSW", password);
            //SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            //SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            //SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);

            //return dbAccess.ExecuteCommand("SaveCWRSettings", paramURL, paramUser, paramPSW, paramFoldername, paramFilename, paramIsftp);

        }//end method



        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// 
        public void GetTWHSettings(out string url,
                             out string user,
                             out string password)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_TWH_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_TWH_USER);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            user = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_TWH_PSW);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            password = obj == null ? "" : obj.ToString();

        }//end method


        public void GetTWHSettings(out string url,
                                    out string user,
                                    out string password,out string csvfolder, out string csvfilename, out string csvIsftp, out string dropshipfee)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_TWH_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_TWH_USER);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            user = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_TWH_PSW);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            password = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_TWH_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_TWH_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_TWH_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_TWH_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();
            
        }//end method

        #endregion

        #region Morris Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool SaveMorrisSettings(string url, string dropshipfee, string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            // SqlParameter paramCount = new SqlParameter("@UrlCount", url_count);
            SqlParameter paramFee = new SqlParameter("@DropShipFee", dropshipfee);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@IsFTP", isftp);

            return dbAccess.ExecuteCommand("SaveMorrisSettings", paramURL, paramFee, paramFoldername, paramFilename, paramIsftp);

        }//end method



        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void GetMorrisSettings(out string url, out string dropshipfee, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Morris_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Morris_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Morris_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Morris_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Morris_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

        }//end method

        #endregion

        #region Morris Daily Summary Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool SaveMorrisDailySummarySettings(string url, string foldername, string filename, string creditfoldername, string creditfilename)
        {
            //Create parameters
            if (UpdateSetting(CONST_MorrisDailySummary_CreditCSVFolder, creditfoldername) &&
                 UpdateSetting(CONST_MorrisDailySummary_CreditCSVFIle, creditfilename))
            {
                SqlParameter paramURL = new SqlParameter("@URL", url);
                SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
                SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);

                return dbAccess.ExecuteCommand("SaveMorrisDailySummarySettings", paramURL, paramFoldername, paramFilename);
            }
            else
                return false;

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void GetMorrisDailySummarySettings(out string url, out string csvfolder, out string csvfilename, out string csvCreditfolder, out string csvCreditfilename)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_MorrisDailySummary_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisDailySummary_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisDailySummary_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();
            if (csvfilename == "")
                csvfilename = "daily_summary_01292018.csv";

            paramKey = new SqlParameter("@Key", CONST_MorrisDailySummary_CreditCSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvCreditfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisDailySummary_CreditCSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvCreditfilename = obj == null ? "" : obj.ToString();
            if (csvCreditfilename == "")
                csvCreditfilename = "creditdaily_summary_01292018.csv";
        }//end method

        #endregion


        #region Morris Weekly Summary Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="foldername"></param>
        /// <param name="fields"></param>
        /// <param name="creditfoldername"></param>
        /// <param name="crediname"></param>
        /// <returns></returns>
        public bool SaveMorrisWeeklySummarySettings(string url, string foldername, string filename, string creditfoldername, string creditfilename)
        {
            if (UpdateSetting(CONST_MorrisWeeklySummary_URL, url) &&
                UpdateSetting(CONST_MorrisWeeklySummary_CSVFolder, foldername) &&
                UpdateSetting(CONST_MorrisWeeklySummary_CSVFIle, filename) &&
                UpdateSetting(CONST_MorrisWeeklySummary_CreditCSVFolder, creditfoldername) &&
                UpdateSetting(CONST_MorrisWeeklySummary_CreditCSVFIle, creditfilename) 
                )
                return true;
            else
                return false;

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="csvfolder"></param>
        /// <param name="csvfilename"></param>
        /// <param name="csvCreditfolder"></param>
        /// <param name="csvCreditfilename"></param>
        public void GetMorrisWeeklySummarySettings(out string url, out string csvfolder, out string csvfilename, out string csvCreditfolder, out string csvCreditfilename)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_MorrisWeeklySummary_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisWeeklySummary_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisWeeklySummary_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();
  
            paramKey = new SqlParameter("@Key", CONST_MorrisWeeklySummary_CreditCSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvCreditfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisWeeklySummary_CreditCSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvCreditfilename = obj == null ? "" : obj.ToString();

        }//end method

        #endregion

        #region Morris Complete Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool SaveMorrisCompleteSettings(string url, string dropshipfee, string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            // SqlParameter paramCount = new SqlParameter("@UrlCount", url_count);
            SqlParameter paramFee = new SqlParameter("@DropShipFee", dropshipfee);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@IsFTP", isftp);

            return dbAccess.ExecuteCommand("SaveMorrisCompleteSettings", paramURL, paramFee, paramFoldername, paramFilename, paramIsftp);

        }//end method



        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void GetMorrisCompleteSettings(out string url, out string dropshipfee, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_MorrisComplete_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisComplete_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisComplete_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisComplete_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisComplete_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

        }//end method

        #endregion

        #region Morris Nightly Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool SaveMorrisNightlySettings(string url, string dropshipfee, string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            // SqlParameter paramCount = new SqlParameter("@UrlCount", url_count);
            SqlParameter paramFee = new SqlParameter("@DropShipFee", dropshipfee);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@IsFTP", isftp);

            return dbAccess.ExecuteCommand("SaveMorrisNightlySettings", paramURL, paramFee, paramFoldername, paramFilename, paramIsftp);

        }//end method



        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void GetMorrisNightlySettings(out string url, out string dropshipfee, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_MorrisNightly_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisNightly_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisNightly_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisNightly_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisNightly_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

        }//end method

        #endregion

        #region Morris Changes Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool SaveMorrisChangesSettings(string url, string dropshipfee, string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            // SqlParameter paramCount = new SqlParameter("@UrlCount", url_count);
            SqlParameter paramFee = new SqlParameter("@DropShipFee", dropshipfee);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@IsFTP", isftp);

            return dbAccess.ExecuteCommand("SaveMorrisChangesSettings", paramURL, paramFee, paramFoldername, paramFilename, paramIsftp);

        }//end method



        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fields"></param>
        /// <param name="delim"></param>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void GetMorrisChangesSettings(out string url, out string dropshipfee, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_MorrisChanges_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisChanges_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisChanges_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisChanges_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_MorrisChanges_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

        }//end method

        #endregion

        #region Sumdex Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public bool SaveSumdexSettings(string folder)
        {
            return UpdateSetting(CONST_Sumdex_Folder, folder);
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSumdexFolder()
        {
            SqlParameter paramFolder = new SqlParameter("@Key", CONST_Sumdex_Folder);
            return dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramFolder).ToString();

        }//end method

        #endregion

        #region Haier settings
        public bool SaveHaierSettings(string folder)
        {
            return UpdateSetting("Haier_Folder", folder);
        }

        public string GetHaierFolder()
        {
            SqlParameter paramFolder = new SqlParameter("@Key", "Haier_Folder");
            return dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramFolder).ToString();

        }
        #endregion

        #region Picnic Time Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="excelUrl"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="qtyForAvailable"></param>
        /// <param name="qtyForDate"></param>
        /// <returns></returns>
        public bool SavePicnicTimeSettings(string csvurl,
                                            string excelUrl,
                                            string isexcelfile, string foldername, string filename, bool isftp, string dropshipfee)
        {
            //Create parameters
            SqlParameter paramExcelURL = new SqlParameter("@ExcelURL", excelUrl);
            SqlParameter paramCsvURL = new SqlParameter("@CsvURL", csvurl);
            SqlParameter paramIsExcel = new SqlParameter("@IsExcelURL", isexcelfile);


            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);
            SqlParameter paramDropshifee = new SqlParameter("@Dropshipfee", dropshipfee);

            return dbAccess.ExecuteCommand("SavePicnicTimeInfo", paramExcelURL, paramCsvURL, paramIsExcel, paramFoldername, paramFilename, paramIsftp, paramDropshifee);


        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="excelUrl"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="qtyForAvailable"></param>
        /// <param name="qtyForDate"></param>
        //public void GetPicnicTimeInfo(out string csvurl,
        //                            out string excelUrl,
        //                            out string username,
        //                            out string password,
        //                            out string qtyForAvailable,
        //                            out string qtyForDate)
        //{

        //    SqlParameter paramKey = new SqlParameter("@Key", CONST_PTime_URL);
        //    url = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

        //    paramKey = new SqlParameter("@Key", CONST_PTime_Excel_URL);
        //    excelUrl = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

        //    paramKey = new SqlParameter("@Key", CONST_PTime_Username);
        //    username = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

        //    paramKey = new SqlParameter("@Key", CONST_PTime_Password);
        //    password = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

        //    paramKey = new SqlParameter("@Key", CONST_PTime_Qty_For_Available);
        //    qtyForAvailable = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

        //    paramKey = new SqlParameter("@Key", CONST_PTime_Qty_For_Date);
        //    qtyForDate = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

        //}//end method

        public void GetPicnicTimeInfo(out string csvurl,
                            out string excelUrl,
                            out string isexcelfile, out string csvfolder, out string csvfilename, out string csvIsftp, out string dropshipfee)
        {
            //     private const string CONST_PTime_Excel_URL = "PTime_Excel_URL";
            //private const string CONST_PTime_Csv_URL = "PTime_Csv_URL";
            //private const string CONST_PTime_IsExcel_URL = "PTime_IsExcel_URL";

            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_PTime_Excel_URL);
            excelUrl = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_PTime_Csv_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvurl = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PTime_IsExcel_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            isexcelfile = obj == null ? "" : obj.ToString();
            paramKey = new SqlParameter("@Key", CONST_PTime_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PTime_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PTime_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PTime_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();
        }//end method
        #endregion

        #region Petra Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="excelUrl"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="qtyForAvailable"></param>
        /// <param name="qtyForDate"></param>
        /// <returns></returns>
        public bool SavePetraSettings(string url,
                                            string ftpFile,
                                            string username,
                                            string password,
                                            string foldername, string filename, bool isftp, string dropshipfee, string filenameToServer)
       {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramExcelURL = new SqlParameter("@FtpFile", ftpFile);
            SqlParameter paramUsername = new SqlParameter("@Username", username);
            SqlParameter paramPassword = new SqlParameter("@Password", password);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);
            SqlParameter paramdropshipfee = new SqlParameter("@Dropshipfee", dropshipfee);
            SqlParameter paramFilenameToServer = new SqlParameter("@CSVFileToServer", filenameToServer);
            return dbAccess.ExecuteCommand("SavePetraInfo", paramURL, paramExcelURL, paramUsername, paramPassword, paramFoldername, paramFilename, paramIsftp, 
                paramdropshipfee, paramFilenameToServer);


        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="excelUrl"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="csvfolder"></param>
        /// <param name="csvfilename"></param>
        /// <param name="csvIsftp"></param>

        public void GetPetraInfo(out string ftpUrl,
                            out string ftpFileName,
                            out string username,
                            out string password,
                            out string csvfolder, out string csvfilename, out string csvIsftp, out string dropshipfee, out string csvfilenameToServer)

        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Petra_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            ftpUrl = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_FTPFile);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            ftpFileName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_Username);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            username = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_Password);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            password = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_CSVFIleToServer);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilenameToServer = obj == null ? "" : obj.ToString();

        }//end method

        public void GetPetraInfo(out string ftpUrl,
                        out string ftpFileName,
                        out string username,
                        out string password,
                        out string dropshipfee)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Petra_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            ftpUrl = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_FTPFile);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            ftpFileName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_Username);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            username = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_Password);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            password = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();

        }//end method

        #endregion

        #region Petra Order Module Settings

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SavePetraOrderSettings(string orderFtpurl, string orderUsername, string orderPassword,
                                            string foldername, string archivename
                                            )
        {
            //Create parameters
            SqlParameter paramOrderURL = new SqlParameter("@OrderURL", orderFtpurl);
            SqlParameter paramOrderUsername = new SqlParameter("@OrderUsername", orderUsername);
            SqlParameter paramOrderPassword = new SqlParameter("@OrderPassword", orderPassword);

            SqlParameter paramFoldername = new SqlParameter("@FolderName", foldername);
            SqlParameter paramArchivename = new SqlParameter("@Archivename", archivename);
            return dbAccess.ExecuteCommand("SavePetraOrderInfo", paramOrderURL, paramOrderUsername, paramOrderPassword, paramFoldername, paramArchivename);


        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="excelUrl"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="csvfolder"></param>
        /// <param name="csvfilename"></param>
        /// <param name="csvIsftp"></param>

        public void GetPetraOrderInfo(out string orderFtpurl, out string orderUsername, out string orderPassword,
                                        out string folderName, out string archiveName)

        {
            object obj;

            SqlParameter paramKey = new SqlParameter("@Key", CONST_Petra_Order_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            orderFtpurl = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_Order_Username);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            orderUsername = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_Order_Password);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            orderPassword = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_Order_Folder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            folderName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_Order_Archive);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            archiveName = obj == null ? "" : obj.ToString();

        }//end method


        #endregion

        #region Petra Order Reformating Settings

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SavePetraOrderReformatSettings(string orderFtpurl, string orderUsername, string orderPassword, string inFolder,string outFolder, string archiveFolder)
        {

            if (UpdateSetting(CONST_Petra_Order_URL, orderFtpurl) &&
                UpdateSetting(CONST_Petra_Order_Username, orderUsername) &&
                UpdateSetting(CONST_Petra_Order_Password, orderPassword) && 
                UpdateSetting(CONST_PetraReformat_InFolder, inFolder) &&
                UpdateSetting(CONST_PetraReformat_ArchiveFolder, archiveFolder) &&
               UpdateSetting(CONST_PetraReformat_OutFolder, outFolder))
                return true;
            else
                return false;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvInFoldername"></param>
        /// <param name="OutFoldername"></param>

        public void GetPetraOrderReformatInfo(out string orderFtpurl, out string orderUsername, out string orderPassword, out string inFolderName, out string outFolderName, out string archiveFolderName)
        {
            object obj;

            SqlParameter paramKey = new SqlParameter("@Key", CONST_Petra_Order_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            orderFtpurl = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_Order_Username);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            orderUsername = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Petra_Order_Password);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            orderPassword = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PetraReformat_InFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            inFolderName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PetraReformat_OutFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            outFolderName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PetraReformat_ArchiveFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            archiveFolderName = obj == null ? "" : obj.ToString();

        }//end method


        #endregion

        #region AZ Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="foldername"></param>
        /// <param name="filename"></param>
        /// <param name="isftp"></param>
        /// <param name="dropshipfee"></param>
        /// <returns></returns>
        public bool SaveAZSettings(string csvnameWithImage, string url,
                                            string foldername, string filename, bool isftp, string dropshipfee)
        {
            //Create parameters
            UpdateSetting(CONST_AZ_CSVFIleImage, csvnameWithImage);

            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);
            SqlParameter paramdropshipfee = new SqlParameter("@Dropshipfee", dropshipfee);
            return dbAccess.ExecuteCommand("SaveAZInfo", paramURL, paramFoldername, paramFilename, paramIsftp, paramdropshipfee);


        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="csvfolder"></param>
        /// <param name="csvfilename"></param>
        /// <param name="csvIsftp"></param>
        /// <param name="dropshipfee"></param>

        public void GetAZInfo(out string url,
                            out string csvfolder, out string csvfilename, out string csvIsftp, out string dropshipfee, out string csvwthImage)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_AZ_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();


            paramKey = new SqlParameter("@Key", CONST_AZ_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_AZ_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_AZ_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_AZ_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_AZ_CSVFIleImage);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvwthImage = obj == null ? "" : obj.ToString();

        }//end method
        /// <summary>
        /// 
        /// <param name="url"></param>
        /// <param name="dropshipfee"></param>
        /// </summary>

        public void GetAZInfo(out string url, out string dropshipfee)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_AZ_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_AZ_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();
        }//end method

        #endregion

        #region Moteng Settings

        /// <summary>
        /// Save Moteng Settings
        /// </summary>
        /// <param name="url"></param>
        /// <param name="foldername"></param>
        /// <param name="prodfilename"></param>
        /// <param name="isftp"></param>
        /// <param name="dropshipfee"></param>
        /// <returns></returns>
        public bool SaveMotengSettings(string foldername, string csvFilename, string csvnameConverted, string url, string user, string password,
                                            string prodfilename, string pricefilename, string qtyfilename, bool isftp, string dropshipfee)
        {
            //Create parameters
           
            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramUser = new SqlParameter("@User", user);
            SqlParameter paramPSW = new SqlParameter("@PSW", password);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", csvFilename);
            SqlParameter paramProdFilename = new SqlParameter("@CSVProdFile", prodfilename);
            SqlParameter paramPriceFilename = new SqlParameter("@CSVPriceFile", pricefilename);
            SqlParameter paramQtyFilename = new SqlParameter("@CSVQtyFile", qtyfilename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);
            SqlParameter paramdropshipfee = new SqlParameter("@Dropshipfee", dropshipfee);
            SqlParameter paramFilenameConverted = new SqlParameter("@CSVFileConverted", csvnameConverted);
            return dbAccess.ExecuteCommand("SaveMotengSettings", paramURL, paramUser, paramPSW,paramFoldername, paramFilename,paramProdFilename, paramPriceFilename, paramQtyFilename, paramIsftp, paramdropshipfee, paramFilenameConverted);


        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="csvfolder"></param>
        /// <param name="csvfilename"></param>
        /// <param name="csvIsftp"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="">csvConverted</param>
        /// <param name="dropshipfee"></param>

        public void GetMotengInfo(out string url, out string csvfolder,
                            out string csvfilename, out string csvProdfilename, out string csvPricefilename, out string csvQtyfilename, out string csvIsftp, out string dropshipfee, out string username, out string password, out string csvConverted)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key",CONST_Moteng_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_CSVProdFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvProdfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_CSVPriceFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvPricefilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_CSVQtyFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvQtyfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_Username);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            username = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_Password);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            password = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_CSVFIleConverted);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvConverted = obj == null ? "" : obj.ToString();

        }//end method

        /// <summary>
        /// 
        /// <param name="url"></param>
        /// <param name="dropshipfee"></param>
        /// </summary>

        public void GetMotengInfo(out string url, out string dropshipfee)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Moteng_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();
        }//end method

        /// <summary>
        /// 
        /// <param name="url"></param>
        /// <param name="dropshipfee"></param>
        /// </summary>

        public void GetMotengInfo(out string url, out string _username, out string _password, out string productFileName, out string priceFileName, out string qtyFileName,out string _dropshipfee)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Moteng_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_Username);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            _username = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_Password);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            _password = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_CSVProdFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            productFileName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_CSVPriceFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            priceFileName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_CSVQtyFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            qtyFileName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Moteng_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            _dropshipfee = obj == null ? "" : obj.ToString();

        }//end method
        #endregion

        #region Nearly Natural Settings

        /// <summary>
        /// Save Nearly Natural settings
        /// </summary>
        /// <param name="url"></param>
        /// <param name="foldername"></param>
        /// <param name="filename"></param>
        /// <param name="isftp"></param>
        /// <param name="dropshipfee"></param>
        /// <returns></returns>
        public bool SaveNearlyNaturalSettings(string csvnameOriginal, string url,
                                            string foldername, string filename, bool isftp, string dropshipfee)
        {
            //Create parameters
            UpdateSetting(CONST_NearlyNatural_CSVFIleOriginal, csvnameOriginal);

            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);
            SqlParameter paramdropshipfee = new SqlParameter("@Dropshipfee", dropshipfee);
            return dbAccess.ExecuteCommand("SaveNearlyNaturalInfo", paramURL, paramFoldername, paramFilename, paramIsftp, paramdropshipfee);


        }//end method


        /// <summary>
        /// Read NearlyNatural settings
        /// </summary>
        /// <param name="url"></param>
        /// <param name="csvfolder"></param>
        /// <param name="csvfilename"></param>
        /// <param name="csvIsftp"></param>
        /// <param name="dropshipfee"></param>

        public void GetNearlyNaturalInfo(out string url,
                            out string csvfolder, out string csvfilename, out string csvIsftp, out string dropshipfee, out string csvOriginal)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_NearlyNatural_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();


            paramKey = new SqlParameter("@Key", CONST_NearlyNatural_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_NearlyNatural_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_NearlyNatural_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_NearlyNatural_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_NearlyNatural_CSVFIleOriginal);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvOriginal = obj == null ? "" : obj.ToString();

        }//end method

        /// <summary>
        /// Get NearlyNatural URL and Dropshipfee
        /// </summary>        
        /// <param name="url"></param>
        /// <param name="dropshipfee"></param>


        public void GetNearlyNaturalInfo(out string url, out string dropshipfee)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_NearlyNatural_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_NearlyNatural_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();
        }//end method

        #endregion


        #region Green Supply Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="foldername"></param>
        /// <param name="filename"></param>
        /// <param name="isftp"></param>
        /// <returns></returns>
        public bool SaveGreenSupplySettings(string url,
                                            string foldername, string filename, bool isftp, string dropshipfee)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);
            SqlParameter paramDropship = new SqlParameter("@Dropshipfee", dropshipfee);

            return dbAccess.ExecuteCommand("SaveGreenSupplyInfo", paramURL, paramFoldername, paramFilename, paramIsftp, paramDropship);
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="csvfolder"></param>
        /// <param name="csvfilename"></param>
        /// <param name="csvIsftp"></param>

        public void GetGreenSupplyInfo(out string url,
                            out string csvfolder, out string csvfilename, out string csvIsftp, out string dropshipfee)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_GreenSupply_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();


            paramKey = new SqlParameter("@Key", CONST_GreenSupply_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_GreenSupply_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_GreenSupply_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();
            
            paramKey = new SqlParameter("@Key", CONST_GreenSupply_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();
        }//end method

        /// <summary>
        /// 
        /// <param name="url"></param>
        /// </summary>

        public void GetGreenSupplyUrl(out string url, out string dropshipfee)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_GreenSupply_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_GreenSupply_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();
        }//end method

        #endregion

        #region Viking Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="foldername"></param>
        /// <param name="filename"></param>
        /// <param name="isftp"></param>
        /// <returns></returns>
        public bool SaveVikingSettings(string url,
                                            string foldername, string filename, bool isftp, string dropshipfee)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);
            SqlParameter paramDropship = new SqlParameter("@Dropshipfee", dropshipfee);

            return dbAccess.ExecuteCommand("SaveVikingInfo", paramURL, paramFoldername, paramFilename, paramIsftp, paramDropship);
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="csvfolder"></param>
        /// <param name="csvfilename"></param>
        /// <param name="csvIsftp"></param>

        public void GetVikingInfo(out string url,
                            out string csvfolder, out string csvfilename, out string csvIsftp, out string dropshipfee)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Viking_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();


            paramKey = new SqlParameter("@Key", CONST_Viking_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Viking_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Viking_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Viking_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();
        }//end method

        /// <summary>
        /// 
        /// <param name="url"></param>
        /// </summary>

        public void GetVikingUrl(out string url, out string dropshipfee)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Viking_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Viking_DropShipFee);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            dropshipfee = obj == null ? "" : obj.ToString();
        }//end method

        #endregion
        
        #region DressUpAmerica Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool SaveDressUpAmericaSettings(string url, string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);

            return dbAccess.ExecuteCommand("SaveDressUpAmericaInfo", paramURL, paramFoldername, paramFilename, paramIsftp);
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void GetDressUpAmericaInfo(out string url)
        {
            SqlParameter paramKey = new SqlParameter("@Key", CONST_DUpAmerica_URL);
            url = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

        }//end method

        public void GetDressUpAmericaInfo(out string url, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            SqlParameter paramKey = new SqlParameter("@Key", CONST_DUpAmerica_URL);
            url = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            object obj;

            paramKey = new SqlParameter("@Key", CONST_DUpAmerica_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_DUpAmerica_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_DUpAmerica_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();
        }//end m
        #endregion


        #region RockLine Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool SaveRockLineSettings(string url, string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);

            return dbAccess.ExecuteCommand("SaveRocklineInfo", paramURL, paramFoldername, paramFilename, paramIsftp);
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// 
        public void GetRockLineInfo(out string url)
        {
            SqlParameter paramKey = new SqlParameter("@Key", CONST_RockLine_URL);
            url = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();
        }//end method

        public void GetRockLineInfo(out string url, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            object obj;

            SqlParameter paramKey = new SqlParameter("@Key", CONST_RockLine_URL);
            url = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_RockLine_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_RockLine_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_RockLine_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

        }//end method

        #endregion


        #region Pacific Cycle Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="qtyFor100"></param>
        /// <returns></returns>
        public bool SavePacficCycleSettings(string url,
                                            string username,
                                            string password,
                                            string qtyFor100, string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramUsername = new SqlParameter("@Username", username);
            SqlParameter paramPassword = new SqlParameter("@Password", password);
            SqlParameter param100Qty = new SqlParameter("@100Qty", qtyFor100);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);

            return dbAccess.ExecuteCommand("SavePacificCycleInfo",
                                            paramURL,
                                            paramUsername,
                                            paramPassword,
                                            param100Qty, paramFoldername, paramFilename, paramIsftp);

        }//end method

        public bool SavePacficCycleInlineToysSettings(string url, string username, string password, string qtyFor100)
        {
            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramUsername = new SqlParameter("@Username", username);
            SqlParameter paramPassword = new SqlParameter("@Password", password);
            SqlParameter param100Qty = new SqlParameter("@100Qty", qtyFor100);

            return dbAccess.ExecuteCommand("SavePacificCycleInlineToysInfo",
                                            paramURL,
                                            paramUsername,
                                            paramPassword,
                                            param100Qty);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="qtyFor100"></param>
        public void GetPacificCycleInfo(out string url,
                                            out string username,
                                            out string password,
                                            out string qtyFor100)
        {
            //Get EMG URL first
            SqlParameter paramKey = new SqlParameter("@Key", CONST_PCycle_URL);
            url = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_PCycle_Username);
            username = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_PCycle_Password);
            password = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_PCycle_100_Qty);
            qtyFor100 = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();


        }//end method

        public void GetPacificCycleInfo(out string url,
                                       out string username,
                                       out string password,
                                       out string qtyFor100, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            //Get EMG URL first
            SqlParameter paramKey = new SqlParameter("@Key", CONST_PCycle_URL);
            url = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_PCycle_Username);
            username = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_PCycle_Password);
            password = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_PCycle_100_Qty);
            qtyFor100 = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            object obj;

            paramKey = new SqlParameter("@Key", CONST_PCycle_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PCycle_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PCycle_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

        }//end method

        public void GetPacificCycleInlineToysInfo(out string url, out string username, out string password, out string qtyFor100)
        {
            //Get EMG URL first
            SqlParameter paramKey = new SqlParameter("@Key", "PacificCycleInlineToys_URL");
            url = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", "PacificCycleInlineToys_Login");
            username = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", "PacificCycleInlineToys_Password");
            password = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", "PacificCycleInlineToys_Quantity100");
            qtyFor100 = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();


        }
 
        #endregion

        #region Shipstaion Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromzip"></param>
        /// <param name="tozip"></param>
        /// <returns></returns>
        public bool SaveShipstationSettings(string fromzip, string tozip, string stampsInsurance, string upsInsurance, string inputFile,string outputFile,
                                            string prioritymailLevel,string firstclassLevel,string parcelselectLevel,
                                            string uspsMarkup1, string uspsMarkupParcel, string uspsMarkupPriority, string requireSignature)
        {
            if (UpdateSetting(SettingsConstant.ssGetRate_FromZip, fromzip) &&
                UpdateSetting(SettingsConstant.ssGetRate_ToZip, tozip) &&
                UpdateSetting(SettingsConstant.ssStampsInsurance, stampsInsurance) &&
                UpdateSetting(SettingsConstant.ssUPSInsurance, upsInsurance) &&
                UpdateSetting(SettingsConstant.ssInputFile, inputFile) &&
                UpdateSetting(SettingsConstant.ssOutputFile, outputFile) &&
                UpdateSetting(SettingsConstant.ssUSPSPriorityMail, prioritymailLevel) &&
                UpdateSetting(SettingsConstant.ssUSPS1ClassMail, firstclassLevel) &&
                UpdateSetting(SettingsConstant.ssUSPSParcelSelect, parcelselectLevel) &&
                UpdateSetting(SettingsConstant.ssUSPSMarkup1, uspsMarkup1) &&
                UpdateSetting(SettingsConstant.ssUSPSMarkupParcel, uspsMarkupParcel) &&
                UpdateSetting(SettingsConstant.ssUSPSMarkupPriority, uspsMarkupPriority) &&
                UpdateSetting(SettingsConstant.ssRequireSignature, requireSignature)
                )
                return true;
            else
                return false;
        }//end method

        /// <summary>
        ///  save selected services separated with (,)
        /// </summary>
        /// <param name="strServices"> sring contains selected services separated with ,</param>
        /// <returns></returns>
        public bool SaveShipstationServicesString(string strServices)
        {
            return UpdateSetting(SettingsConstant.ssAPIServices, strServices);
        }//end method

        public void GetServicesString(out string str)
        {
            object obj;

            SqlParameter paramKey = new SqlParameter("@Key", SettingsConstant.ssAPIServices);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            str = obj == null ? "" : obj.ToString();

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromzip"></param>
        /// <param name="tozip"></param>


        public void GetShipstationInfo(out string fromzip, out string toZip, out string stamp, out string ups, out string input, out string output, out string priorityMail, out string firsClassmail, out string parcelSelect, out string uspsMarkup1, out string uspsMarkupParcel, out string uspsMarkupPriority, out string requireSignature)
        {
            object obj;

            SqlParameter paramKey = new SqlParameter("@Key", SettingsConstant.ssGetRate_FromZip);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            fromzip = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.ssGetRate_ToZip);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            toZip = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.ssStampsInsurance);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            stamp = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.ssUPSInsurance);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            ups = obj == null ? "" : obj.ToString();
            
            paramKey = new SqlParameter("@Key", SettingsConstant.ssInputFile);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            input = obj == null ? "" : obj.ToString();
            
            paramKey = new SqlParameter("@Key", SettingsConstant.ssOutputFile);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            output = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.ssUSPSPriorityMail);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            priorityMail = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.ssUSPS1ClassMail);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            firsClassmail  = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.ssUSPSParcelSelect);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            parcelSelect = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.ssUSPSMarkup1);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            uspsMarkup1  = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.ssUSPSMarkupParcel);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            uspsMarkupParcel = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.ssUSPSMarkupPriority);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            uspsMarkupPriority = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.ssRequireSignature);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            requireSignature = obj == null ? "" : obj.ToString();

        }//end method

        #endregion

        #region Amazon MWS Settings

        /// <summary>
        /// get input file name and result file name
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <param name="toSaveFileName"></param>


        public void GetAmazonMWSInfo(out string inputFileName, out string outputFileName)
        {
            object obj;

            SqlParameter paramKey = new SqlParameter("@Key", SettingsConstant.mwsInputFileName);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            inputFileName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.mwsOutputFileName);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            outputFileName = obj == null ? "" : obj.ToString();

        }//end method

        /// <summary>
        /// Save Amazon MWS settings
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <param name="toSaveFileName"></param>
        /// <returns></returns>
        public bool SaveAmazonMWSSettings(string inputFileName, string outputFileName)
        {
            if (UpdateSetting(SettingsConstant.mwsInputFileName , inputFileName) &&
                UpdateSetting(SettingsConstant.mwsOutputFileName, outputFileName)
                )
                return true;
            else
                return false;
        }//end method


        #endregion

        #region LW API Settings

        /// <summary>
        /// get folder name and result file name
        /// </summary>
        /// <param name="FolderName"></param>
        /// <param name="toSaveFileName"></param>
        public void GetLWAPIInfo(out string folderName, out string fileName, out string LWAPI_ID, out string LWAPI_Secret, out string LWAPI_Token, out string fromID, out string toID)
        {
            object obj;

            SqlParameter paramKey = new SqlParameter("@Key", CONST_LWAPI_ApplicationID);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            LWAPI_ID = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_LWAPI_ApplicationSecret);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            LWAPI_Secret = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_LWAPI_Token);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            LWAPI_Token = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_LWAPI_Folder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            folderName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_LWAPI_File);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            fileName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_LWAPI_FromID);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            fromID  = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_LWAPI_ToID);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            toID = obj == null ? "" : obj.ToString();

        }//end method

        public void GetLWAPIInfo(out string LWAPI_ID, out string LWAPI_Secret, out string LWAPI_Token)
        {
            object obj;

            SqlParameter paramKey = new SqlParameter("@Key", CONST_LWAPI_ApplicationID);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            LWAPI_ID = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_LWAPI_ApplicationSecret);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            LWAPI_Secret = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_LWAPI_Token);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            LWAPI_Token = obj == null ? "" : obj.ToString();
        }//end method

        public void GetLWAPIInfo(out string fromID, out string toID)
        {
            object obj;

            SqlParameter paramKey = new SqlParameter("@Key", CONST_LWAPI_FromID);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            fromID = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_LWAPI_ToID);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            toID = obj == null ? "" : obj.ToString();

        }//end method

        public void GetLWAPIcsvfolder(out string folderName, out string fileName)
        {
            object obj;

            SqlParameter  paramKey = new SqlParameter("@Key", CONST_LWAPI_Folder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            folderName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_LWAPI_File);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            fileName = obj == null ? "" : obj.ToString();

        }//end method


        /// <summary>
        /// Save Amazon MWS settings
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <param name="toSaveFileName"></param>
        /// <returns></returns>
        public bool SaveLWAPISettings(string folderName, string fileName, string LWAPI_ID, string LWAPI_Secret, string LWAPI_Token, string fromID, string toID)
        {
            if (UpdateSetting(CONST_LWAPI_ApplicationID, LWAPI_ID) &&
                UpdateSetting(CONST_LWAPI_ApplicationSecret, LWAPI_Secret) &&
                UpdateSetting(CONST_LWAPI_Token, LWAPI_Token) &&
                UpdateSetting(CONST_LWAPI_Folder, folderName) &&
                UpdateSetting(CONST_LWAPI_File, fileName) &&
                UpdateSetting(CONST_LWAPI_FromID, fromID) &&
                UpdateSetting(CONST_LWAPI_ToID, toID)
                )
                return true;
            else
                return false;
        }//end method


        #endregion


        #region MorrisXML Creator Settings

        /// <summary>
        /// get input, processed and readyrToUpload file names
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <param name="processedFileName"></param>
        /// <param name="readyrToUploadFileName"></param>

        public void GetMorrisXMLCreatorInfo(out string inputFileName, out string processFileName, out string readyToUploadFileName, out string errorFolderName)
        {
            object obj;

            SqlParameter paramKey = new SqlParameter("@Key", SettingsConstant.morrisXMLInputFileName);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            inputFileName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.morrisXMLProcessedFileName);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            processFileName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.morrisXMLReadyFileName);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            readyToUploadFileName = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", SettingsConstant.morrisXMLErrorFolderName);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            errorFolderName = obj == null ? "" : obj.ToString();
        }//end method

        /// <summary>
        /// Save Morris XML settings
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <param name="processedFileName"></param>
        /// <param name="readyrToUploadFileName"></param>
        /// <returns></returns>
        public bool SaveMorrisXMLCreatorSettings(string inputFileName, string processFileName,string readyToUploadFileName, string errorFolderName)
        {
            if (UpdateSetting(SettingsConstant.morrisXMLInputFileName, inputFileName) &&
                UpdateSetting(SettingsConstant.morrisXMLProcessedFileName, processFileName) &&
                UpdateSetting(SettingsConstant.morrisXMLReadyFileName, readyToUploadFileName) &&
                UpdateSetting(SettingsConstant.morrisXMLErrorFolderName, errorFolderName)
                )
                return true;
            else
                return false;
        }//end method


        #endregion


        #region Benchmark Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SaveBenchmarkSettings(string url,
                                            string username,
                                            string password,
                                            string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramUsername = new SqlParameter("@Username", username);
            SqlParameter paramPassword = new SqlParameter("@Password", password);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);

            return dbAccess.ExecuteCommand("SaveBenchmarkInfo",
                                            paramURL,
                                            paramUsername,
                                            paramPassword,
                                            paramFoldername, paramFilename, paramIsftp);

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void GetBenchmarkInfo(out string url,
                                            out string username,
                                            out string password)
        {
            //Get EMG URL first
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Benchmark_URL);
            url = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_Benchmark_Username);
            username = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            paramKey = new SqlParameter("@Key", CONST_Benchmark_Password);
            password = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

        }//end method

        public void GetBenchmarkInfo(out string url,
                                       out string username,
                                       out string password,
                                       out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_Benchmark_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Benchmark_Username);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            username = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Benchmark_Password);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            password = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Benchmark_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Benchmark_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_Benchmark_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

        }//end method

        #endregion


        #region Pet Gear Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="foldername"></param>
        /// <param name="filename"></param>
        /// <param name="isftp"></param>
        /// <returns></returns>
        public bool SavePetGearSettings(string url,
                                            string foldername, string filename, bool isftp, string inStockValue)
        {
            //Create parameters
            SqlParameter paramURL = new SqlParameter("@URL", url);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);
            SqlParameter paramInStockValue = new SqlParameter("@InStockValue", inStockValue);
            return dbAccess.ExecuteCommand("SavePetGearInfo",
                                            paramURL,
                                            paramFoldername, paramFilename, paramIsftp, paramInStockValue);

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="csvfolder"></param>
        /// <param name="csvfilename"></param>
        /// <param name="csvIsftp"></param>

        public void GetPetGearInfo(out string url,
                                        out string csvfolder, out string csvfilename, out string csvIsftp, out string inStockValue)
        {
            //Get EMG URL first
            SqlParameter paramKey = new SqlParameter("@Key", CONST_PetGear_URL);
            //url = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey).ToString();

            object obj;
            paramKey = new SqlParameter("@Key", CONST_PetGear_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PetGear_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfolder = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PetGear_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvfilename = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PetGear_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            csvIsftp = obj == null ? "" : obj.ToString();

            paramKey = new SqlParameter("@Key", CONST_PetGear_InStockValue);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            inStockValue = obj == null ? "" : obj.ToString();

        }//end method
        public void GetPetGearInfo(out string url)
        {
            object obj;
            SqlParameter paramKey = new SqlParameter("@Key", CONST_PetGear_URL);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramKey);
            url = obj == null ? "" : obj.ToString();

        }//end method

        #endregion

        #region Wynit Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpServer"></param>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpPassword"></param>
        /// <returns></returns>
        public bool SaveWynitInfo(string ftpServer, string ftpUserName, string ftpPassword)
        {
            //Create parameters
            SqlParameter paramFTPServer = new SqlParameter("@FTPServer", ftpServer);
            SqlParameter paramFTPUserName = new SqlParameter("@FTPUserName", ftpUserName);
            SqlParameter paramFTPPasword = new SqlParameter("@FTPPassword", ftpPassword);

            return dbAccess.ExecuteCommand("SaveWynitInfo", paramFTPServer, paramFTPUserName, paramFTPPasword);

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpServer"></param>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpPassword"></param>
        public void GetWynitInfo(out string ftpServer, out string ftpUserName, out string ftpPassword)
        {
            //Get FTP Server
            SqlParameter paramFTPServer = new SqlParameter("@Key", CONST_Key_Wynit_FTP_Server);
            ftpServer = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramFTPServer).ToString();

            //Get FTP User Name
            SqlParameter paramFTPUserName = new SqlParameter("@Key", CONST_Key_Wynit_FTP_UserName);
            ftpUserName = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramFTPUserName).ToString();

            //Get FTP Password
            SqlParameter paramFTPPassword = new SqlParameter("@Key", CONST_Key_Wynit_FTP_Password);
            ftpPassword = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramFTPPassword).ToString();

        }//end method

        #endregion

        #region EMG Settings

        /// <summary>
        /// Method to save EMG Information
        /// </summary>
        /// <param name="emgURL"></param>
        /// <param name="emgCustomerID"></param>
        /// <returns></returns>
        public bool SaveEMGInfo(string ftp, string ftpUser, string ftpassword, string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramFtp = new SqlParameter("@FTP", ftp);
            SqlParameter paramUser = new SqlParameter("@FtpUsername", ftpUser);
            SqlParameter parampassword = new SqlParameter("@FtpPassword", ftpassword);

            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);

            return dbAccess.ExecuteCommand("SaveEMGInfo", paramFtp, paramUser, parampassword, paramFoldername, paramFilename, paramIsftp);


        }//end method

        /// <summary>
        /// Method to get EMG Info in dataset
        /// </summary>
        /// <returns></returns>
        public void GetEMGInfo(out string emgFTP, out string emgFtpUser, out string emgFtpPassword, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            object obj;
            //Get EMG URL first
            SqlParameter paramFTP = new SqlParameter("@Key", CONST_EMGFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramFTP);
            emgFTP = obj == null ? "" : obj.ToString();

            SqlParameter paramFtpUser = new SqlParameter("@Key", CONST_EMG_FtpUserName);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramFtpUser);
            emgFtpUser = obj == null ? "" : obj.ToString();

            SqlParameter paramFtpPassword = new SqlParameter("@Key", CONST_EMG_FtpPassword);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramFtpPassword);
            emgFtpPassword = obj == null ? "" : obj.ToString();


            SqlParameter paramCsvFolder = new SqlParameter("@Key", CONST_EMG_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramCsvFolder);
            csvfolder = obj == null ? "" : obj.ToString();

            SqlParameter paramFileName = new SqlParameter("@Key", CONST_EMG_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramFileName);
            csvfilename = obj == null ? "" : obj.ToString();

            SqlParameter paramIsFTP = new SqlParameter("@Key", CONST_EMG_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramIsFTP);
            csvIsftp = obj == null ? "" : obj.ToString();

        }//end method

        // delete
        public bool SaveEMGInfo(string emgURL, string emgCustomerID, string foldername, string filename, bool isftp)
        {
            //Create parameters
            SqlParameter paramEMGURL = new SqlParameter("@EMGURL", emgURL);
            SqlParameter paramCustomerID = new SqlParameter("@EMGCustomerID", emgCustomerID);
            SqlParameter paramFoldername = new SqlParameter("@CSVFolder", foldername);
            SqlParameter paramFilename = new SqlParameter("@CSVFile", filename);
            SqlParameter paramIsftp = new SqlParameter("@CSVIsFTP", isftp);

            return dbAccess.ExecuteCommand("SaveEMGInfo", paramEMGURL, paramCustomerID, paramFoldername, paramFilename, paramIsftp);


        }//end method

        public void GetEMGInfo(out string emgURL, out string emgCustomerID, out string csvfolder, out string csvfilename, out string csvIsftp)
        {
            object obj;
            //Get EMG URL first
            SqlParameter paramURLKey = new SqlParameter("@Key", CONST_Key_EMGURL);
            emgURL = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramURLKey).ToString();

            //Get EMG Customer ID
            SqlParameter paramCustomerIDKey = new SqlParameter("@Key", CONST_Key_EMG_Customer_ID);
            emgCustomerID = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramCustomerIDKey).ToString();

            SqlParameter paramCsvFolder = new SqlParameter("@Key", CONST_EMG_CSVFolder);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramCsvFolder);
            csvfolder = obj == null ? "" : obj.ToString();

            SqlParameter paramFileName = new SqlParameter("@Key", CONST_EMG_CSVFIle);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramFileName);
            csvfilename = obj == null ? "" : obj.ToString();

            SqlParameter paramIsFTP = new SqlParameter("@Key", CONST_EMG_IsFTP);
            obj = dbAccess.ExecuteScalar(CONST_SP_GetSetting, paramIsFTP);
            csvIsftp = obj == null ? "" : obj.ToString();

        }//end method



        #endregion EmgSettings

        #region Pricing Markup

        /// <summary>
        /// Method to retreive pricing markup dataset
        /// </summary>
        /// <returns></returns>
        public DataSet GetPricingMarkup(int vendorID, int profileID)
        {
            //Create parameter
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            SqlParameter paramProfileID = new SqlParameter("@ProfileID", profileID);

            return dbAccess.ExecuteDataSet("GetPricingMarkup", paramVendorID, paramProfileID);
        }//end method


        /// <summary>
        /// Method to save Pricing Markup. First delete all markups for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SavePricingMarkup(int vendorID, int profileID, DataTable dt)
        {
            using (SqlConnection sqlCn = dbAccess.GetConnection())
            {
                sqlCn.Open();
                SqlTransaction trans = sqlCn.BeginTransaction();

                //first delete markups for the vendor
                SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
                SqlParameter paramProfileID = new SqlParameter("@ProfileID", profileID);
                dbAccess.ExecuteCommandTrans("DeletePricingMarkup", sqlCn, trans, paramVendorID, paramProfileID);

                //loop through datatable and insert each row
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        paramVendorID = new SqlParameter("@VendorID", vendorID);
                        paramProfileID = new SqlParameter("@ProfileID", profileID);
                        SqlParameter paramPriceFrom = new SqlParameter("@PriceFrom", dr["PriceFrom"]);
                        SqlParameter paramPriceTo = new SqlParameter("@PriceTo", dr["PriceTo"]);
                        SqlParameter paramMarkup = new SqlParameter("@Markup", dr["Markup"]);

                        dbAccess.ExecuteCommandTrans("SavePricingMarkup", sqlCn, trans, paramVendorID, paramProfileID, paramPriceFrom, paramPriceTo, paramMarkup);
                    }//end if

                }//end foreach

                trans.Commit();
                return true;

            }//end using

        }//end method

        #endregion

        #region Profiles To Update For Vendor

        /// <summary>
        /// Method to save which profiles to be updated for a vendor
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SaveProfilesToUpdateForVendor(int vendorID, List<int> profiles)
        {
            using (SqlConnection sqlCn = dbAccess.GetConnection())
            {
                sqlCn.Open();
                SqlTransaction trans = sqlCn.BeginTransaction();

                //first delete markups for the vendor
                SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
                dbAccess.ExecuteCommandTrans("DeleteProfilesToUpdateForVendor", sqlCn, trans, paramVendorID);

                //loop the list
                for (int x = 0; x < profiles.Count; x++)
                {
                    paramVendorID = new SqlParameter("@VendorID", vendorID);
                    SqlParameter paramProfile = new SqlParameter("@ProfileID", profiles[x]);

                    dbAccess.ExecuteCommandTrans("SaveProfilesToUpdate", sqlCn, trans, paramVendorID, paramProfile);

                }//end for

                trans.Commit();
                return true;

            }//end using

        }//end method


        /// <summary>
        /// Profiles selected for a vendor
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public DataSet GetProfilesForVendor(int vendorID)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@VendorID", vendorID);

            return dbAccess.ExecuteDataSet("GetProfilesToUpdateForVendor", param);
        }//end method

        #endregion

        #region Profiles

        /// <summary>
        /// Method to retreive pricing markup dataset
        /// </summary>
        /// <returns></returns>
        public DataSet GetProfiles()
        {
            return dbAccess.ExecuteDataSet("GetProfiles");
        }//end method

        /// <summary>
        /// Method to save Profiles. First delete all profiles for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SaveProfiles(DataTable dt)
        {
            using (SqlConnection sqlCn = dbAccess.GetConnection())
            {
                sqlCn.Open();
                SqlTransaction trans = sqlCn.BeginTransaction();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                        SqlParameter paramID = new SqlParameter("@ID", dr["ID", DataRowVersion.Original]);

                        try
                        {
                            dbAccess.ExecuteCommandTrans("DeleteProfile", sqlCn, trans, paramID);
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 547)//reference exists
                            {
                                throw new Exception("The Profile: " + dr["Profile", DataRowVersion.Original].ToString()
                                    + " is being referenced by a Vendor. Please remove all references to this profile from 'Vendor Profiles' and 'Pricing Markup' screens.");
                            }
                            else
                            {
                                throw;
                            }
                        }//end catch


                    }//end deleted row if
                    else if (dr.RowState != DataRowState.Unchanged) //added or modified row
                    {

                        SqlParameter paramID = new SqlParameter("@ID", dr["ID"].ToString() == "" ? DBNull.Value : dr["ID"]);
                        SqlParameter paramProfile = new SqlParameter("@Profile", dr["Profile"].ToString().Trim());
                        SqlParameter paramProfileAPIKey = new SqlParameter("@ProfileAPIKey", dr["ProfileAPIKey"].ToString().Trim());

                        dbAccess.ExecuteCommandTrans("SaveProfile", sqlCn, trans, paramID, paramProfile, paramProfileAPIKey);
                    }//
                }//end for each

                trans.Commit();
                return true;
            }//end using


            //using (SqlConnection sqlCn = dbAccess.GetConnection())
            //{
            //    sqlCn.Open();
            //    SqlTransaction trans = sqlCn.BeginTransaction();

            //    //first delete markups for the vendor
            //    SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            //    dbAccess.ExecuteCommandTrans("DeleteProfiles", sqlCn, trans, paramVendorID);

            //    //loop through datatable and insert each row
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        if (dr.RowState != DataRowState.Deleted)
            //        {
            //            paramVendorID = new SqlParameter("@VendorID", vendorID);
            //            SqlParameter paramProfileID = new SqlParameter("@ProfileID", dr["ProfileID"]);
            //            SqlParameter paramAPIKey = new SqlParameter("@APIKey", dr["APIKey"]);
            //            SqlParameter paramIsDisabled = null;
            //            if (dr["IsDisabled"].ToString() == "")
            //            {
            //                paramIsDisabled = new SqlParameter("@IsDisabled", false);
            //            }
            //            else
            //            {
            //                paramIsDisabled = new SqlParameter("@IsDisabled", dr["IsDisabled"]);
            //            }


            //            dbAccess.ExecuteCommandTrans("SaveProfile", sqlCn, trans, paramVendorID, paramProfileID, paramAPIKey, paramIsDisabled);
            //        }//end if

            //    }//end foreach

            //    trans.Commit();
            //    return true;

            //}//end using

        }//end method

        #endregion

        #region Frequency

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


        #region SingleProfileFreq


        /// <summary>
        /// Method to retreive Frequency Weekdays
        /// </summary>
        /// <returns></returns>
        public DataSet GetSingleProfileFreqWeekDays(int vendorID)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@VendorID", vendorID);

            return dbAccess.ExecuteDataSet("GetSingleProfileFreqWeekDays", param);
        }//end method


        /// <summary>
        /// Method to retreive Frequency Times
        /// </summary>
        /// <returns></returns>
        public DataSet GetSingleProfileFreqTimes(int vendorID)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@VendorID", vendorID);

            return dbAccess.ExecuteDataSet("GetSingleProfileFreqTimes", param);
        }//end method

        /// <summary>
        /// Profiles selected for a vendor
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public DataSet GetSingleProfileFreqProfiles(int vendorID)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@VendorID", vendorID);

            return dbAccess.ExecuteDataSet("GetSingleProfileFreqProfiles", param);
        }//end method


        /// <summary>
        /// Method to save Frequency WeekDays. First delete all Weekdays for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SaveSingleProfileFreqWeekDays(int vendorID, List<WeekDayDTO> lstWeekDays, SqlConnection sqlCn, SqlTransaction trans)
        {

            //first delete markups for the vendor
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            dbAccess.ExecuteCommandTrans("DeleteSingleProfileFreqWeekDays", sqlCn, trans, paramVendorID);

            //loop through List
            foreach (WeekDayDTO weekDay in lstWeekDays)
            {
                paramVendorID = new SqlParameter("@VendorID", vendorID);
                SqlParameter paramWeekDay = new SqlParameter("@WeekDay", weekDay.WeekDay);
                SqlParameter paramIsEnabled = new SqlParameter("@IsEnabled", weekDay.IsEnabled);

                dbAccess.ExecuteCommandTrans("SaveSingleProfileFreqWeekDay", sqlCn, trans, paramVendorID, paramWeekDay, paramIsEnabled);
            }//end for each

        }//end method

        /// <summary>
        /// Method to save Frequency Times. First delete all Frequency times for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SaveSingleProfileFreqTimes(int vendorID, DataTable dt, SqlConnection sqlCn, SqlTransaction trans)
        {
            //first delete markups for the vendor
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            dbAccess.ExecuteCommandTrans("DeleteSingleProfileFreqTimes", sqlCn, trans, paramVendorID);

            //loop through datatable and insert each row
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    paramVendorID = new SqlParameter("@VendorID", vendorID);
                    SqlParameter paramTime = new SqlParameter("@Time", dr["Time"]);

                    dbAccess.ExecuteCommandTrans("SaveSingleProfileFreqTime", sqlCn, trans, paramVendorID, paramTime);
                }//end if

            }//end foreach

        }//end method

        /// <summary>
        /// Method to save which profiles to be updated for a vendor
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SaveSingleProfileFreqProfile(int vendorID, int profileID, SqlConnection sqlCn, SqlTransaction trans)
        {

            //first delete markups for the vendor
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            dbAccess.ExecuteCommandTrans("DeleteSingleProfileFreqProfiles", sqlCn, trans, paramVendorID);

            paramVendorID = new SqlParameter("@VendorID", vendorID);
            SqlParameter paramProfile = new SqlParameter("@ProfileID", profileID);

            dbAccess.ExecuteCommandTrans("SaveSingleProfileFreqProfile", sqlCn, trans, paramVendorID, paramProfile);

            return true;

        }//end method

        #endregion

        #region MultiProfileFreq

        /// <summary>
        /// Method to retreive Frequency Weekdays
        /// </summary>
        /// <returns></returns>
        public DataSet GetMultiProfileFreqWeekDays(int vendorID)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@VendorID", vendorID);

            return dbAccess.ExecuteDataSet("GetMultiProfileFreqWeekDays", param);
        }//end method


        /// <summary>
        /// Method to retreive Frequency Times
        /// </summary>
        /// <returns></returns>
        public DataSet GetMultiProfileFreqTimes(int vendorID)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@VendorID", vendorID);

            return dbAccess.ExecuteDataSet("GetMultiProfileFreqTimes", param);
        }//end method

        /// <summary>
        /// Profiles selected for a vendor
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public DataSet GetMultiProfileFreqProfiles(int vendorID)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@VendorID", vendorID);

            return dbAccess.ExecuteDataSet("GetMultiProfileFreqProfiles", param);
        }//end method


        /// <summary>
        /// Method to save Frequency WeekDays. First delete all Weekdays for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SaveMultiProfileFreqWeekDays(int vendorID, List<WeekDayDTO> lstWeekDays, SqlConnection sqlCn, SqlTransaction trans)
        {

            //first delete markups for the vendor
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            dbAccess.ExecuteCommandTrans("DeleteMultiProfileFreqWeekDays", sqlCn, trans, paramVendorID);

            //loop through List
            foreach (WeekDayDTO weekDay in lstWeekDays)
            {
                paramVendorID = new SqlParameter("@VendorID", vendorID);
                SqlParameter paramWeekDay = new SqlParameter("@WeekDay", weekDay.WeekDay);
                SqlParameter paramIsEnabled = new SqlParameter("@IsEnabled", weekDay.IsEnabled);

                dbAccess.ExecuteCommandTrans("SaveMultiProfileFreqWeekDay", sqlCn, trans, paramVendorID, paramWeekDay, paramIsEnabled);
            }//end for each

        }//end method

        /// <summary>
        /// Method to save Frequency Times. First delete all Frequency times for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SaveMultiProfileFreqTimes(int vendorID, DataTable dt, SqlConnection sqlCn, SqlTransaction trans)
        {
            //first delete markups for the vendor
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            dbAccess.ExecuteCommandTrans("DeleteMultiProfileFreqTimes", sqlCn, trans, paramVendorID);

            //loop through datatable and insert each row
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    paramVendorID = new SqlParameter("@VendorID", vendorID);
                    SqlParameter paramTime = new SqlParameter("@Time", dr["Time"]);

                    dbAccess.ExecuteCommandTrans("SaveMultiProfileFreqTime", sqlCn, trans, paramVendorID, paramTime);
                }//end if

            }//end foreach

        }//end method

        /// <summary>
        /// Method to save which profiles to be updated for a vendor
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SaveMultiProfileFreqProfiles(int vendorID, List<int> profiles, SqlConnection sqlCn, SqlTransaction trans)
        {

            //first delete markups for the vendor
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            dbAccess.ExecuteCommandTrans("DeleteMultiProfileFreqProfiles", sqlCn, trans, paramVendorID);

            //loop the list
            for (int x = 0; x < profiles.Count; x++)
            {
                paramVendorID = new SqlParameter("@VendorID", vendorID);
                SqlParameter paramProfile = new SqlParameter("@ProfileID", profiles[x]);

                dbAccess.ExecuteCommandTrans("SaveMultiProfileFreqProfile", sqlCn, trans, paramVendorID, paramProfile);

            }//end for
            return true;


        }//end method

        #endregion


        /// <summary>
        /// method to save frequency weekdays and time
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="lstWeekDays"></param>
        /// <param name="dtFrequencyTimes"></param>
        /// <returns></returns>
        public bool SaveFrequency(int vendorID,
                                    List<WeekDayDTO> lstSingleProfileFreqWeekDays,
                                    DataTable dtSingleProfileFrequencyTimes,
                                    int singleProfileFreqProfileID,
                                    List<WeekDayDTO> lstMultiProfileFreqWeekDays,
                                    DataTable dtMultiProfileFrequencyTimes,
                                    List<int> multiProfileFreqProfiles)
        {
            using (SqlConnection sqlCn = dbAccess.GetConnection())
            {
                sqlCn.Open();
                SqlTransaction trans = sqlCn.BeginTransaction();

                //SaveFrequencyWeekDays(vendorID, lstWeekDays, sqlCn, trans);
                //SaveFrequencyTimes(vendorID, dtFrequencyTimes, sqlCn, trans);

                //Save Single profile frequency
                SaveSingleProfileFreqWeekDays(vendorID, lstSingleProfileFreqWeekDays, sqlCn, trans);
                SaveSingleProfileFreqTimes(vendorID, dtSingleProfileFrequencyTimes, sqlCn, trans);
                SaveSingleProfileFreqProfile(vendorID, singleProfileFreqProfileID, sqlCn, trans);

                //Save multi profile frequency
                SaveMultiProfileFreqWeekDays(vendorID, lstMultiProfileFreqWeekDays, sqlCn, trans);
                SaveMultiProfileFreqTimes(vendorID, dtMultiProfileFrequencyTimes, sqlCn, trans);
                SaveMultiProfileFreqProfiles(vendorID, multiProfileFreqProfiles, sqlCn, trans);


                trans.Commit();
                return true;
            }//end using

        }//end method

        /// <summary>
        /// Method to save Frequency WeekDays. First delete all Weekdays for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SaveFrequencyWeekDays(int vendorID, List<WeekDayDTO> lstWeekDays, SqlConnection sqlCn, SqlTransaction trans)
        {

            //first delete markups for the vendor
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            dbAccess.ExecuteCommandTrans("DeleteFrequencyWeekDays", sqlCn, trans, paramVendorID);

            //loop through List
            foreach (WeekDayDTO weekDay in lstWeekDays)
            {
                paramVendorID = new SqlParameter("@VendorID", vendorID);
                SqlParameter paramWeekDay = new SqlParameter("@WeekDay", weekDay.WeekDay);
                SqlParameter paramIsEnabled = new SqlParameter("@IsEnabled", weekDay.IsEnabled);

                dbAccess.ExecuteCommandTrans("SaveFrequencyWeekDay", sqlCn, trans, paramVendorID, paramWeekDay, paramIsEnabled);
            }//end for each

        }//end method

        /// <summary>
        /// Method to save Frequency Times. First delete all Frequency times for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void SaveFrequencyTimes(int vendorID, DataTable dt, SqlConnection sqlCn, SqlTransaction trans)
        {
            //first delete markups for the vendor
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            dbAccess.ExecuteCommandTrans("DeleteFrequencyTimes", sqlCn, trans, paramVendorID);

            //loop through datatable and insert each row
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    paramVendorID = new SqlParameter("@VendorID", vendorID);
                    SqlParameter paramTime = new SqlParameter("@Time", dr["Time"]);

                    dbAccess.ExecuteCommandTrans("SaveFrequencyTime", sqlCn, trans, paramVendorID, paramTime);
                }//end if

            }//end foreach

        }//end method


        #endregion Frequency

        #region Web Services

        /// <summary>
        /// Method to get list of web services
        /// </summary>
        /// <returns></returns>
        public DataSet GetWebServices()
        {
            return dbAccess.ExecuteDataSet("GetWebServices");

        }//end method

        /// <summary>
        /// Method to update Service URL
        /// </summary>
        /// <param name="serviceID"></param>
        /// <param name="serviceURL"></param>
        public bool UpdateServiceURLs(DataTable dtServices)
        {
            int serviceID = 0;
            string serviceURL = "";

            //loop the datatable
            foreach (DataRow dr in dtServices.Rows)
            {
                serviceID = Convert.ToInt32(dr["ID"]);
                serviceURL = dr["ServiceURL"].ToString();

                //Create parameter
                SqlParameter paramID = new SqlParameter("@ID", serviceID);
                SqlParameter paramURL = new SqlParameter("@ServiceURL", serviceURL);

                if (dbAccess.ExecuteCommand("UpdateServiceURL", paramID, paramURL) == false) return false;
            }//end foreach

            return true;
        }//end method


        /// <summary>
        /// Method to get web service URL
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public string GetWebServiceURL(int serviceID)
        {
            //Create parameter
            SqlParameter paramID = new SqlParameter("@ID", serviceID);

            //Get URL
            return dbAccess.ExecuteScalar("GetWebServiceURL", paramID).ToString();
        }//end method

        #endregion

        #region Blocked SKUs

        /// <summary>
        /// Method to retreive Blocked SKUs two tables Wildcard and simple list 
        /// </summary>
        /// <returns></returns>
        public DataSet GetBlockedSKUs()
        {
    
            return dbAccess.ExecuteDataSet("GetBlockedSKUs", null);
        }//end method


        /// <summary>
        /// Method to save Blocked SKUs. First delete all Duplicate SKUs for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SaveBlockedSKUs(DataTable dt)
        {
            using (SqlConnection sqlCn = dbAccess.GetConnection())
            {
                sqlCn.Open();
                SqlTransaction trans = sqlCn.BeginTransaction();

                 dbAccess.ExecuteCommandTrans("DeleteBlockedSKUs", sqlCn, trans, null);

                //loop through datatable and insert each row
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        //sqlCommand.Parameters.Add("@HasPaid", SqlDbType.Bit).Value = hasPaid;
                        SqlParameter paramSKU = new SqlParameter("@SKU", dr["SKU"]);
                        SqlParameter paramIsWildcard = new SqlParameter("@IsWildcard", dr["SKU"].ToString().Contains("*")? 1 : 0);

                        dbAccess.ExecuteCommandTrans("SaveBlockedSKU", sqlCn, trans, paramSKU, paramIsWildcard);
                    }//end if

                }//end foreach

                trans.Commit();
                return true;

            }//end using

        }//end method


        #endregion


        #region Duplicate SKUs

        /// <summary>
        /// Method to retreive Duplicate SKUs
        /// </summary>
        /// <returns></returns>
        public DataSet GetDuplicateSKUs(int vendorID)
        {
            //Create parameter
            SqlParameter param = new SqlParameter("@VendorID", vendorID);

            return dbAccess.ExecuteDataSet("GetDuplicateSKUs", param);
        }//end method


        /// <summary>
        /// Method to save Duplicate SKUs. First delete all Duplicate SKUs for the selected vendor
        /// and then insert new data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SaveDuplicateSKUs(int vendorID, DataTable dt)
        {
            using (SqlConnection sqlCn = dbAccess.GetConnection())
            {
                sqlCn.Open();
                SqlTransaction trans = sqlCn.BeginTransaction();

                //first delete duplicate SKUs for the vendor
                SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
                dbAccess.ExecuteCommandTrans("DeleteDuplicateSKUs", sqlCn, trans, paramVendorID);

                //loop through datatable and insert each row
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        paramVendorID = new SqlParameter("@VendorID", vendorID);
                        SqlParameter paramUPC = new SqlParameter("@UPC", dr["UPC"] == DBNull.Value ? "" : dr["UPC"]);
                        SqlParameter paramSKU = new SqlParameter("@SKU", dr["SKU"] == DBNull.Value ? "" : dr["SKU"]);
                        SqlParameter paramNewSKU = new SqlParameter("@NewSKU", dr["NewSKU"]);

                        dbAccess.ExecuteCommandTrans("SaveDuplicateSKU", sqlCn, trans, paramVendorID, paramUPC, paramSKU, paramNewSKU);
                    }//end if

                }//end foreach

                trans.Commit();
                return true;

            }//end using

        }//end method


        #endregion

        #region Settings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateSetting(string key, string value)
        {
            //Create parameter
            SqlParameter paramKey = new SqlParameter("@Key", key);
            SqlParameter paramValue = new SqlParameter("@Value", value);

            return dbAccess.ExecuteCommand("UpdateSetting", paramKey, paramValue);
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetSettings()
        {
            return dbAccess.ExecuteDataSet("GetSettings");
        }//end method


        /// <summary>
        /// Get Setting value for key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSettingValue(string key)
        {
            SqlParameter paramKey = new SqlParameter("@key", key);
            var res = dbAccess.ExecuteScalar("GetSetting", paramKey);
            return res != null ? res.ToString() : String.Empty;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsAutoUpdateDisabled()
        {
            string settingValue = GetSettingValue(SettingsConstant.Auto_Update_Disable);

            if (settingValue.Trim() == "0")
            {
                return false;
            }
            else if (settingValue.Trim() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }

        }//end method

        public IDictionary<string, string> GetSetOfSettings(IList<string> keys)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (string key in keys)
            {
                result.Add(key, GetSettingValue(key));
            }
            return result;
        }

        #endregion

        #region Vendors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="fileArchive"></param>
        [Obsolete]
        public void SaveVendorFileArchive(int vendorID, string fileArchive)
        {
            SqlParameter paramID = new SqlParameter("@VendorID", vendorID);
            SqlParameter paramFileArchive = new SqlParameter("@FileArchive", fileArchive);

            dbAccess.ExecuteCommand("SaveVendorFileArchive", paramID, paramFileArchive);
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public Vendor GetVendor(int vendorID)
        {
            SqlParameter paramID = new SqlParameter("@ID", vendorID);

            DataTable vendor = dbAccess.ExecuteDataSet("GetVendor", paramID).Tables[0];
            if (vendor.Rows.Count > 0)
                return new Vendor(vendor.Rows[0]);
            else
                return null;
        }

        /// <summary>
        /// Method to get list of vendors
        /// </summary>
        /// <returns></returns>
        public DataSet GetVendors()
        {
            return dbAccess.ExecuteDataSet("GetVendors");
        }

        /// <summary>
        /// Method to get list of vendors
        /// </summary>
        /// <returns></returns>
        public DataSet GetDistinctVendors()
        {
            return dbAccess.ExecuteDataSet("GetVendorsForCAUpdate");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="vendor"></param>
        /// <param name="isDynamic"></param>
        /// <param name="folder"></param>
        public void SaveVendor(int? ID, string vendor, bool isDynamic, string folder, string fileArchive,
                                bool setOutOfStockIfNotPresented, string SKUPrefix, int? outOfStockThreshold, int? outOfStockQuantity, string label)
        {
            //If ID is blank then get ID
            if ((ID == null) || (ID == 0))
            {
                ID = Convert.ToInt32(dbAccess.ExecuteFunction("GetMAXVendorID"));

                //Start dyamic vendors from 31
                if (ID < 31)
                {
                    ID = 31;
                }
                else
                {
                    ID++;
                }//end if
            }//end if

            //Create sql parameters
            SqlParameter paramID = new SqlParameter("@ID", ID);
            SqlParameter paramVendor = new SqlParameter("@Vendor", vendor);
            SqlParameter paramIsDynamic = new SqlParameter("@IsDynamic", isDynamic);
            SqlParameter paramFolder = new SqlParameter("@Folder", folder);
            SqlParameter paramFileArchive = new SqlParameter("@FileArcive", fileArchive);
            SqlParameter setOutOfStockIfNotPresentedParam = new SqlParameter("@SetOutOfStockIfNotPresented", setOutOfStockIfNotPresented);
            SqlParameter SkuPrefixParam;
            if (string.IsNullOrEmpty(SKUPrefix))
            {
                SkuPrefixParam = new SqlParameter("@SKUPrefix", DBNull.Value);
            }
            else
            {
                SkuPrefixParam = new SqlParameter("@SKUPrefix", SKUPrefix);
            }
            SqlParameter outOfStockThresholdParam;
            if (outOfStockThreshold.HasValue)
                outOfStockThresholdParam = new SqlParameter("@OutOfStockThreshold", outOfStockThreshold.Value);
            else
                outOfStockThresholdParam = new SqlParameter("@OutOfStockThreshold", DBNull.Value);
            SqlParameter outOfStockQuantityParam;
            if (outOfStockQuantity.HasValue)
                outOfStockQuantityParam = new SqlParameter("@OutOfStockQuantity", outOfStockQuantity.Value);
            else
                outOfStockQuantityParam = new SqlParameter("@OutOfStockQuantity", DBNull.Value);
            SqlParameter LabelParam;
            if (string.IsNullOrEmpty(label))
            {
                LabelParam = new SqlParameter("@Label", DBNull.Value);
            }
            else
            {
                LabelParam = new SqlParameter("@Label", label);
            }

            dbAccess.ExecuteCommand("SaveVendor",
                                        paramID, paramVendor, paramIsDynamic, paramFolder, paramFileArchive,
                                        setOutOfStockIfNotPresentedParam, SkuPrefixParam, outOfStockThresholdParam,
                                        outOfStockQuantityParam, LabelParam);


        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetDynamicVendors()
        {
            return dbAccess.ExecuteDataSet("GetDynamicVendors");
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteDynamicVendor(int ID)
        {
            try
            {
                //Create parameter
                SqlParameter paramID = new SqlParameter("ID", ID);

                dbAccess.ExecuteCommand("DeleteDynamicVendor", paramID);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new Exception("Some other records related to the Vendor like Pricing " +
                                        "Markups, Frequency etc need to be deleted first " +
                                        "before this vendor can be deleted.");
                }
                else
                {
                    throw (ex);
                }
            }

        }//end method

        #endregion

        #region Schedule


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataSet GetVendorFilesForDate(int vendorID, string fromDate, string toDate)
        {
            //Create parameter
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            SqlParameter paramFromDate = new SqlParameter("@FromDate", fromDate);
            SqlParameter paramToDate = new SqlParameter("@ToDate", toDate);

            return dbAccess.ExecuteDataSet("GetVendorFilesForDate", paramVendorID, paramFromDate, paramToDate);
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataSet GetCAFilesForDate(int vendorID, string fromDate, string toDate)
        {
            //Create parameter
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            SqlParameter paramFromDate = new SqlParameter("@FromDate", fromDate);
            SqlParameter paramToDate = new SqlParameter("@ToDate", toDate);

            return dbAccess.ExecuteDataSet("GetCAFilesForDate", paramVendorID, paramFromDate, paramToDate);
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetVendorFilesOver1WeekOld()
        {
            return dbAccess.ExecuteDataSet("GetVendorFilesOver1WeekOld");
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetVendorFilesOver1MonthOld()
        {
            return dbAccess.ExecuteDataSet("GetVendorFilesOver1MonthOld");
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetCAFilesOver1WeekOld()
        {
            return dbAccess.ExecuteDataSet("GetCAFilesOver1WeekOld");
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetCAFilesOver1MonthOld()
        {
            return dbAccess.ExecuteDataSet("GetCAFilesOver1MonthOld");
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="localErrors"></param>
        /// <param name="profileErrors"></param>
        /// <param name="vendorFile"></param>
        /// <param name="caFile"></param>
        /// <param name="profileCAFiles"></param>
        public void SaveScheduleLogs(int vendorID,
                                        List<ErrorLog> localErrors,
                                        Dictionary<int, List<ErrorLog>> profileErrors,
                                        string vendorFile,
                                        string caFile,
                                        Dictionary<int, string> profileCAFiles)
        {
            using (SqlConnection sqlCn = dbAccess.GetConnection())
            {
                sqlCn.Open();

                //Save schedule and get schedule ID
                Guid scheduleID = SaveSchedule(vendorID, vendorFile, sqlCn);

                //Save Profiles
                SaveScheduleProfiles(scheduleID, profileErrors, profileCAFiles, sqlCn);

                //Save Local errors
                SaveLocalErrorLog(scheduleID, localErrors, sqlCn);

                //Save CA errors
                SaveCAErrorLog(scheduleID, profileErrors, sqlCn);

            }//end using

        }//end method


        /// <summary>
        /// Method to save CA errors
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="profileErrors"></param>
        /// <param name="sqlCn"></param>
        private void SaveCAErrorLog(Guid scheduleID,
                                    Dictionary<int, List<ErrorLog>> profileErrors,
                                    SqlConnection sqlCn)
        {
            //Create parameters
            SqlParameter paramScheduleID;
            SqlParameter paramProfileID;
            SqlParameter paramErrorDesc;

            int profileID;
            List<ErrorLog> caErrors;

            foreach (KeyValuePair<int, List<ErrorLog>> profileError in profileErrors)
            {
                profileID = profileError.Key;
                caErrors = profileError.Value;

                //Loop the Error Log list and save
                for (int x = 0; x < caErrors.Count; x++)
                {
                    paramScheduleID = new SqlParameter("@ScheduleID", scheduleID);
                    paramProfileID = new SqlParameter("@ProfileID", profileID);
                    paramErrorDesc = new SqlParameter("@ErrorDesc", caErrors[x].ErrorDesc);

                    dbAccess.ExecuteCommandWithConnection("SaveCAErrorLog",
                                                            sqlCn,
                                                            paramScheduleID,
                                                            paramProfileID,
                                                            paramErrorDesc);

                }//end for
            }//end foreach

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="localErrors"></param>
        /// <param name="sqlCn"></param>
        private void SaveLocalErrorLog(Guid scheduleID,
                                        List<ErrorLog> localErrors,
                                        SqlConnection sqlCn)
        {
            //Create parameter
            SqlParameter paramScheduleID;
            SqlParameter paramErrorDesc;

            //loop error log
            for (int x = 0; x < localErrors.Count; x++)
            {
                paramScheduleID = new SqlParameter("@ScheduleID", scheduleID);
                paramErrorDesc = new SqlParameter("@ErrorDesc", localErrors[x].ErrorDesc);

                dbAccess.ExecuteCommandWithConnection("SaveLocalErrorLog",
                                                        sqlCn,
                                                        paramScheduleID,
                                                        paramErrorDesc);

            }//end for

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="profileErrors"></param>
        private void SaveScheduleProfiles(Guid scheduleID,
                                            Dictionary<int, List<ErrorLog>> profileErrors,
                                            Dictionary<int, string> profileCAFiles,
                                            SqlConnection sqlCn)
        {
            //Create parameters
            SqlParameter paramScheduleID;
            SqlParameter paramProfileID;
            SqlParameter paramIsSuccess;
            SqlParameter paramCAFile;

            foreach (int profileID in profileErrors.Keys)
            {
                //get caFile
                string caFile;
                profileCAFiles.TryGetValue(profileID, out caFile);

                //Assign parameter values
                paramScheduleID = new SqlParameter("@ScheduleID", scheduleID);
                paramProfileID = new SqlParameter("@ProfileID", profileID);
                paramIsSuccess = new SqlParameter("@IsSuccess", true);
                paramCAFile = new SqlParameter("@CAFile", caFile);

                dbAccess.ExecuteCommandWithConnection("SaveScheduleProfile",
                                                        sqlCn,
                                                        paramScheduleID,
                                                        paramProfileID,
                                                        paramIsSuccess,
                                                        paramCAFile);


            }//end foreach
        }//end method

        /// <summary>
        /// Method saves Schedule and return the ScheduleID
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        private Guid SaveSchedule(int vendorID, string vendorFile, SqlConnection sqlCn)
        {

            Guid scheduleID = Guid.NewGuid();

            //Create parameters
            SqlParameter paramID = new SqlParameter("@ID", scheduleID);
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            SqlParameter paramIsSuccess = new SqlParameter("@IsSuccess", true);
            SqlParameter paramVendorFile = new SqlParameter("@VendorFile", vendorFile);

            //Execute command

            dbAccess.ExecuteCommandWithConnection("SaveSchedule",
                                                    sqlCn,
                                                    paramID,
                                                    paramVendorID,
                                                    paramIsSuccess,
                                                    paramVendorFile);

            return scheduleID;

        }//end method


        /// <summary>
        /// Method to get schedules which were run between the specified period
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataSet GetSchedules(int vendorID, string fromDate, string toDate)
        {
            //Create sql parameter
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            SqlParameter paramFromDate = new SqlParameter("@FromDate", fromDate);
            SqlParameter paramToDate = new SqlParameter("@ToDate", toDate);

            return dbAccess.ExecuteDataSet("GetSchedules", paramVendorID, paramFromDate, paramToDate);

        }//end method

        /// <summary>
        /// Method to get Local errors
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public DataSet GetLocalErrorLog(Guid scheduleID)
        {
            //Create sql parameter
            SqlParameter param = new SqlParameter("@ScheduleID", scheduleID);

            //return dataser
            return dbAccess.ExecuteDataSet("GetLocalErrorLog", param);

        }//end method


        /// <summary>
        /// Method to get Schedule Errors
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public DataSet GetScheduleProfiles(Guid scheduleID)
        {
            //Create sql parameter
            SqlParameter param = new SqlParameter("@ScheduleID", scheduleID);

            //return dataset
            return dbAccess.ExecuteDataSet("GetScheduleProfiles", param);

        }//end method

        /// <summary>
        /// Method to get CA errors
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="profileID"></param>
        /// <returns></returns>
        public DataSet GetCAErrorLog(Guid scheduleID, int profileID)
        {
            //Create parameter
            SqlParameter paramScheduleID = new SqlParameter("@ScheduleID", scheduleID);
            SqlParameter paramProfileID = new SqlParameter("@ProfileID", profileID);

            //return dataset
            return dbAccess.ExecuteDataSet("GetCAErrorLog", paramScheduleID, paramProfileID);
        }//end method


        /// <summary>
        /// Method gets the last schedule run time
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public DateTime GetLastScheduleRunTime(int vendorID)
        {
            //Create parameter
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);

            //return datetime
            return Convert.ToDateTime(dbAccess.ExecuteScalar("GetLastScheduleRunTime", paramVendorID));
        }//end method


        /// <summary>
        /// Method to delete schedule
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        public void DeleteSchedule(int vendorID, string fromDate, string toDate)
        {
            //Create sql parameter
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            SqlParameter paramFromDate = new SqlParameter("@FromDate", fromDate);
            SqlParameter paramToDate = new SqlParameter("@ToDate", toDate);

            dbAccess.ExecuteCommand("DeleteSchedule", paramVendorID, paramFromDate, paramToDate);

        }//end method


        /// <summary>
        /// Delete schedules for all vendors which are over 1 week old
        /// </summary>
        public void DeleteScheduleOver1WeekOld()
        {
            dbAccess.ExecuteCommand("DeleteScheduleOver1WeekOld");
        }//end method


        /// <summary>
        /// Delete schedules for all vendors which are over 1 month old
        /// </summary>
        public void DeleteScheduleOver1MonthOld()
        {
            dbAccess.ExecuteCommand("DeleteScheduleOver1MonthOld");
        }//end method

        #endregion

        #region General

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="profileID"></param>
        /// <returns></returns>
        public DataSet GetCASaveDetails(int vendorID, int profileID)
        {
            //create parameter
            SqlParameter paramVendorID = new SqlParameter("@VendorID", vendorID);
            SqlParameter paramProfileID = new SqlParameter("@ProfileID", profileID);

            //execute
            return dbAccess.ExecuteDataSet("GetCAFileSaveDetails", paramVendorID, paramProfileID);
        }//end method


        #endregion
    }//end class

}//end namespace
