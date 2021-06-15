using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Entities.OrderAggregate;

namespace BLL.Interface
{
    public interface IOrderService
    {

        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, OrderAddress shippingAddress, int paymentMethod);

        Task<IReadOnlyList<Order>> GetOrdersForUser(string buyerEmail);
        Task<Order> GetOrdersByPaymentIntent(string paymentIntentId);

        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task UpdateOrderStatus(Order order);
        Task DeleteOrder(Order order);
    }
}
