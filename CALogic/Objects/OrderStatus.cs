using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelAdvisor
{
    public class OrderStatus
    {
        public string OrderNo;
        public string Status;
        public string ErrorMessage;
        public string OrderStatusText;
        public string ShipReference;
        public string ShippingMethod;
        public float ShippingCost;
        public DateTime? ShipDate;
        public float NetAmount;
        public float Payment;
        public DateTime? PaymentDate;

        public bool IsStoneEdgeUpdated = false;
    }
}
