﻿using System;
using System.Collections.Generic;

namespace BLL.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(string buyerEmail, OrderAddress shipToAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, decimal subtotal,string paymentIntent)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntent;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderAddress ShipToAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        public decimal Subtotal { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}
