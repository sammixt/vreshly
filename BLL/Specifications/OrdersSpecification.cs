using System;
using BLL.Entities.OrderAggregate;

namespace BLL.Specifications
{
    public class OrdersSpecification : BaseSpecification<Order>
    {
        public OrdersSpecification(string paymentIntent)
            : base(x => x.PaymentIntentId.ToLower().Trim() == paymentIntent.ToLower().Trim())
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }

        public OrdersSpecification(OrderStatus orderStatus)
            : base(x => x.Status == orderStatus)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
