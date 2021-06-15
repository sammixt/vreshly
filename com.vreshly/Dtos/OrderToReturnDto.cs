using System;
using System.Collections.Generic;
using BLL.Entities.OrderAggregate;

namespace com.vreshly.Dtos
{
    public class OrderToReturnDto
    {
        public OrderToReturnDto()
        {
        }

        public long Id { get; set; }
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderAddress ShipToAddress { get; set; }

        public string DeliveryMethod { get; set; }

        public decimal ShippingPrice { get; set; }

        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }

        public decimal Subtotal { get; set; }

        public string Status { get; set; }

        public string PaymentMethod { get; set; }

        public decimal Total { get; set; }

    }
}
