using System;
using System.Runtime.Serialization;

namespace BLL.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed,

        [EnumMember(Value = "Shipped")]
        Shipped,

        [EnumMember(Value = "Returned")]
        Returned,

        [EnumMember(Value = "Completed")]
        Completed

    }

    public enum OrderActualStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed,

        [EnumMember(Value = "Order Cancelled")]
        Cancelled,

        [EnumMember(Value = "Processing Delivery")]
        Processing,

        [EnumMember(Value = "Shipped")]
        Shipped,

        [EnumMember(Value = "Returned")]
        Returned,

        [EnumMember(Value = "Completed")]
        Completed

    }
}
