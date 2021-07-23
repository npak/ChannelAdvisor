using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ChannelAdvisor
{
    public class Vendor
    {
        #region Properties
        public int ID { get; set; }

        public string Name { get; set; }

        public VendorType Type { get; set; }

        public string Folder { get; set; }

        public string FileArchive { get; set; }

        public bool SetOutOfStockIfNotPresented { get; set; }

        public string SkuPrefix { get; set; }

        public int? OutOfStockThreshold { get; set; }

        public int? OutOfStockQuantity { get; set; }

        public string Label { get; set; }
        #endregion

        public Vendor() { }

        public Vendor(DataRow row)
        {
            ID = int.Parse(row["ID"].ToString());
            Name = row["Vendor"].ToString();
            // TODO: Refactor IsDynamic bool to VendorType int32
            Type = (row["IsDynamic"] == DBNull.Value ? false : bool.Parse(row["IsDynamic"].ToString())) 
                ? VendorType.Dynamic : VendorType.Typical;
            Folder = row["Folder"].ToString();
            FileArchive = row["FileArchive"].ToString();
            SetOutOfStockIfNotPresented = bool.Parse(row["SetOutOfStockIfNotPresented"].ToString());
            SkuPrefix = row["SkuPrefix"].ToString();
            OutOfStockThreshold = row["OutOfStockThreshold"] == DBNull.Value ? null : (int?)int.Parse(row["OutOfStockThreshold"].ToString());
            OutOfStockQuantity = row["OutOfStockQuantity"] == DBNull.Value ? null : (int?)int.Parse(row["OutOfStockQuantity"].ToString());
            Label = row["Label"].ToString();
        }

        public bool Save()
        {
            try
            {
                DAL dal = new DAL();
                dal.SaveVendor(ID, Name, Type == VendorType.Typical ? false : true, Folder, FileArchive, SetOutOfStockIfNotPresented,
                    SkuPrefix, OutOfStockThreshold, OutOfStockQuantity, Label);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Vendor Load(int vendorID)
        {
            DAL dal = new DAL();
            return dal.GetVendor(vendorID);
        }
    }
}
