using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ChannelAdvisor
{
    public class PricingMarkupBL
    {
        private DataTable dtMarkup;

        /// <summary>
        /// Default constructor
        /// </summary>
        public PricingMarkupBL(int VendorID, int ProfileID)
        {
            dtMarkup = new DAL().GetPricingMarkup(VendorID, ProfileID).Tables[0];
        }//end constructor


        /// <summary>
        /// Method that accepts price and calculates markup price
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public float GetMarkupPrice(float price, out float markupPercentage)
        {
            //Get range
            DataRow[] dr = dtMarkup.Select("PriceFrom <= " + price + " AND PriceTo >=" + price);
            
            if (dr.GetLength(0) > 0)
            {
                markupPercentage = float.Parse(dr[0]["Markup"].ToString());

                return float.Parse(Math.Round((price * (markupPercentage / 100)), 2).ToString());
            }
            else
            {
                markupPercentage = 100;
                return price;
            }//end if

        }//end method

    }//end namespace

}//end namespace
