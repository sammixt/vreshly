using System;
using System.Runtime.Serialization;

namespace BLL.Entities.OrderAggregate
{
    public enum PaymentMethod
    {
        [EnumMember(Value = "Payment on Delivery")]
        PaymentOnDelivery,


        [EnumMember(Value = "Online Payment")]
        OnlinePayment,

        [EnumMember(Value = "Bank Transfer")]
        BankPayment

    }
}
