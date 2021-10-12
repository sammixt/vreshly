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

        public OrdersSpecification(OrderActualStatus orderStatus)
            : base(x => x.ActualOrderStatus == orderStatus)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }

        public OrdersSpecification(OrderStatus orderStatusPaid, PaymentMethod onlinepayment, 
            OrderStatus orderStatusPending, PaymentMethod transfer, PaymentMethod ondelivery, OrderActualStatus actualStatus)
            : base(x => ((x.Status == orderStatusPaid && x.PaymentMethod == onlinepayment)
            || (x.Status == orderStatusPending && (x.PaymentMethod == transfer || x.PaymentMethod == ondelivery))) && x.ActualOrderStatus == actualStatus )
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }

        public OrdersSpecification(string email, bool byEmail)
            : base(x => x.BuyerEmail.ToLower().Trim() == email.ToLower().Trim())
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
