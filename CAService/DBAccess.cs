using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ChannelAdvisor
{
    /// <summary>
    /// Class that would perform all the dbaccess
    /// </summary>
    public class DBAccess
    {
        //local constants
        private const string CONST_CAConnection = "CAConnection";

        //local variables
        private string connectionString = "";


        /// <summary>
        /// Default constructor
        /// </summary>
        public DBAccess()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[CONST_CAConnection].ToString();
        }


        /// <summary>
        /// Method to execute store proc with parameters
        /// </summary>
        /// <param name="storeProc"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool ExecuteCommand(string storedProc, params SqlParameter[] parameters)
        {
            using (SqlConnection sqlCn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCmd = new SqlCommand(storedProc, sqlCn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                //add parameters to sql command
                foreach(SqlParameter parameter in parameters)
                {
                    sqlCmd.Parameters.Add(parameter);
                }//end foreach

                //execute
                sqlCn.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCn.Close();

                return true;
            }
        }//end method

        /// <summary>
        /// Method that returns a new sql connection
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }//end method


        /// <summary>
        /// Method to execute store proc with parameters
        /// </summary>
        /// <param name="storeProc"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool ExecuteCommandTrans(string storedProc, 
                                        SqlConnection sqlCn, 
                                        SqlTransaction trans, 
                                        params SqlParameter[] parameters)
        {
            
                SqlCommand sqlCmd = new SqlCommand(storedProc, sqlCn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = trans;

                //add parameters to sql command
                foreach (SqlParameter parameter in parameters)
                {
                    sqlCmd.Parameters.Add(parameter);
                }//end foreach

                //execute
                sqlCmd.ExecuteNonQuery();

                return true;
            
        }//end method

        /// <summary>
        /// Method that will return a dataset for the passed in stored proc
        /// </summary>
        /// <param name="storedProc"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string storedProc, params SqlParameter[] parameters)
        {
            DataSet returnDS = new DataSet();

            using (SqlConnection sqlCn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCmd = new SqlCommand(storedProc, sqlCn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                //add parameters to sql command
                foreach (SqlParameter parameter in parameters)
                {
                    sqlCmd.Parameters.Add(parameter);
                }//end foreach

                SqlDataAdapter adap = new SqlDataAdapter(sqlCmd);

                adap.Fill(returnDS);
            }

            return returnDS; 
        }//end method

        /// <summary>
        /// Method to get a single value
        /// </summary>
        /// <param name="storedProc"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Object ExecuteScalar(string storedProc, params SqlParameter[] parameters)
        {
            Object data;
            using (SqlConnection sqlCn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCmd = new SqlCommand(storedProc, sqlCn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                //add parameters to sql command
                foreach (SqlParameter parameter in parameters)
                {
                    sqlCmd.Parameters.Add(parameter);
                }//end foreach
                
                sqlCn.Open();
                data = sqlCmd.ExecuteScalar();
                sqlCn.Close();
            }

            return data;
        }//end method

    }//end class

}//end namespace
