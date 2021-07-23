using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace ChannelAdvisor
{
    /// <summary>
    /// This class represents utilities for working with excel files
    /// </summary>
    public static class ExcelUtils
    {
        /// <summary>
        /// Get name of sheet in excel file by index
        /// </summary>
        /// <param name="fileName">Name of excel file</param>
        /// <param name="connString">Connection string which is using to read excel file</param>
        /// <param name="index">Index of page</param>
        /// <returns>Name of sheet</returns>
        public static string GetSheetNameByIndex(string fileName, string connString, int index)
        {
            string connectionString = string.Format(ConfigurationManager.AppSettings[connString].ToString(), fileName);
            string sheetName = string.Empty;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                DataTable tables = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if ((tables != null) && (tables.Rows.Count > 0))
                {
                    sheetName = tables.Rows[index]["TABLE_NAME"].ToString();
                    if (sheetName.StartsWith("'"))
                        sheetName = sheetName.Substring(1, sheetName.Length - 2);
                }
                connection.Close();
            }

            return sheetName;
        }

        /// <summary>
        /// Get name of sheet in excel file which contains specified substring
        /// </summary>
        /// <param name="fileName">Name of excel file</param>
        /// <param name="connString">Connection string which is using to read excel file</param>
        /// <param name="substring">String which sheet name contain</param>
        /// <returns>Name of sheet</returns>
        public static string GetSheetNameWithSubstring(string fileName, string connString, string substring)
        {
            string connectionString = string.Format(ConfigurationManager.AppSettings[connString].ToString(), fileName);
            string sheetName = string.Empty;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                DataTable tables = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if ((tables != null) && (tables.Rows.Count > 0))
                {
                    foreach(DataRow row in tables.Rows)
                        if (row["TABLE_NAME"].ToString().Contains(substring))
                        {
                            sheetName = row["TABLE_NAME"].ToString();
                            sheetName = sheetName.Substring(1, sheetName.Length - 2);
                            break;
                        }
                }
                connection.Close();
            }

            return sheetName;
        }

        /// <summary>
        /// Reads sheet in excel file
        /// </summary>
        /// <param name="fileName">Name of excel file</param>
        /// <param name="sheetName">Name of sheet in excel file</param>
        /// <param name="connString">Connection string which is using to read excel file</param>
        /// <returns>DataTable which contains data from sheet in excel file</returns>
        public static DataTable ReadExcelSheet(string fileName, string sheetName, string connString)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            if (String.IsNullOrEmpty(sheetName))
                throw new ArgumentNullException("sheetName");

            if (String.IsNullOrEmpty(connString))
                throw new ArgumentNullException("connString");
            
            var setting = ConfigurationManager.AppSettings[connString];

            if (String.IsNullOrEmpty(setting))
                throw new InvalidOperationException(String.Format("Cannot read '{0}' from settings", connString));

            string connectionString = String.Format(ConfigurationManager.AppSettings[connString], fileName);
            DataTable table = new DataTable();

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    OleDbDataAdapter lAdapter = new OleDbDataAdapter(string.Format("select * from [{0}]", sheetName), connection);
                    lAdapter.Fill(table);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(string.Format("Cannot read {0} file", fileName));
                throw new Exception(string.Format(connectionString + ":: " + fileName + ":: " + ex.Message));
            }

            return table;
        }

        /// <summary>
        /// Gets number of sheets in specified excel file
        /// </summary>
        /// <param name="fileName">Name of excel file</param>
        /// <param name="connString">Connection string which is using to read excel file</param>
        /// <returns>Number of sheets in excel file</returns>
        public static int GetNumberOfTabs(string fileName, string connString)
        {
            string connectionString = string.Format(ConfigurationManager.AppSettings[connString].ToString(), fileName);

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                DataTable tables = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (tables != null)
                    return tables.Rows.Count;

                connection.Close();
            }

            return 0;
        }
    }
}
