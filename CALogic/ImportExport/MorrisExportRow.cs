using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    [DelimitedRecord("\t")]
    public class MorrisExportRow
    {
        #region Fields
        public string Sku;

        public string Part;

        public string Gtin;

        public string On_Sale;

        public string Qty;

        public string Time;

        public string file_no;

        public string of;

        public string Count;

        public string file_count;

        public Detail Details { get; set; }

        #endregion

        public class Detail
        {
            public string Desc;

            public string Weight;

            public string Length;

            public string Width;

            public string Height;

            public string Cubes;

            public string Domestic_Air_Dim_Weight;

            public string Intl_Dim_Weight;

            public string Domestic_Gnd_Dim_Weight;

            public string Average_Shipping_Cost;

            public string Price;

            public List<Related>  Relateds = new List<Related>();
        }

        public class Related
        {
            public string Sku { get; set; }
            public string Part { get; set; }
            public string Desc { get; set; }

        }
        public static MorrisExportRow GetHeaderRow()
        {
            MorrisExportRow lExportRow = new MorrisExportRow();
            lExportRow.Sku = "Sku";
            lExportRow.Part = "Part";
            lExportRow.Gtin = "Gtin";
            lExportRow.On_Sale = "On_Sale";
            lExportRow.Qty = "Qty";
            lExportRow.Time = "Time";
            Detail det = new Detail();

            det.Desc = "Details Desc";
            det.Weight = "Details Weight";
            det.Length = "Details Length";
            det.Width = "Details Width";
            det.Height = "Details Height";
            det.Cubes = "Details Cubes";
            det.Domestic_Air_Dim_Weight = "Details Domestic_Air_Dim_Weight";
            det.Domestic_Gnd_Dim_Weight = "Details Domestic_Gnd_Dim_Weight";
            det.Intl_Dim_Weight = "Details Intl_Dim_Weight";
            det.Average_Shipping_Cost = "Details Average_Shipping_Cost";
            det.Price = "Price";
            lExportRow.Details = det;
            Related rel;
            for (int i = 0; i < 30; i++)
            {
                rel = new Related();
                rel.Sku = "Sku Related";
                rel.Part ="Part Related";
                rel.Desc = "Desc Related";
                lExportRow.Details.Relateds.Add(rel);
            }
            return lExportRow; 
        }
    }
    public class MorrisNightlyExportRow
    {
        #region Fields
        public string Sku;

        public string Part;

        public string Gtin;

        public string On_Sale;

        public string Qty;

        public string Time;

        public string file_no;

        public string of;

        public string Count;

        public string file_count;

        public Detail Details { get; set; }

        #endregion

        public class Detail
        {
            public string Desc;

            public string Weight;

            public string Length;

            public string Width;

            public string Height;

            public string Cubes;

            public string Domestic_Air_Dim_Weight;

            public string Intl_Dim_Weight;

            public string Domestic_Gnd_Dim_Weight;

            public string Price;

            public List<Related> Relateds = new List<Related>();
        }

        public class Related
        {
            public string Sku { get; set; }
            public string Part { get; set; }
            public string Desc { get; set; }

        }
        public static MorrisNightlyExportRow GetHeaderRow()
        {
            MorrisNightlyExportRow lExportRow = new MorrisNightlyExportRow();
            lExportRow.Sku = "Sku";
            lExportRow.Part = "Part";
            lExportRow.Gtin = "Gtin";
            lExportRow.On_Sale = "On_Sale";
            lExportRow.Qty = "Qty";
            lExportRow.Time = "Time";

            return lExportRow;
        }
    }

    public class MorrisChangesExportRow
    {
        #region Fields
        public string Sku;

        public string Part;

        public string Gtin;

        public string On_Sale;

        public string Qty;

        public string Time;

        public string file_no;

        public string of;

        public string Count;

        public string file_count;

        public Detail Details { get; set; }

        #endregion

        public class Detail
        {
            public string Desc;

            public string Weight;

            public string Length;

            public string Width;

            public string Height;

            public string Cubes;

            public string Domestic_Air_Dim_Weight;

            public string Intl_Dim_Weight;

            public string Domestic_Gnd_Dim_Weight;

            public string Price;

            public List<Related> Relateds = new List<Related>();
        }

        public class Related
        {
            public string Sku { get; set; }
            public string Part { get; set; }
            public string Desc { get; set; }

        }
        public static MorrisChangesExportRow GetHeaderRow()
        {
            MorrisChangesExportRow lExportRow = new MorrisChangesExportRow();
            lExportRow.Sku = "Sku";
            lExportRow.Part = "Part";
            lExportRow.Gtin = "Gtin";
            lExportRow.On_Sale = "On_Sale";
            lExportRow.Qty = "Qty";
            lExportRow.Time = "Time";

            return lExportRow;
        }
    }
}
