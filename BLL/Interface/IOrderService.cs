using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Entities.OrderAggregate;

namespace BLL.Interface
{
    public interface IOrderService
    {

        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, OrderAddress shippingAddress, int paymentMethod);
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, Product product, OrderAddress shippingAddress, int paymentMethod, int quantity);
        Task<IReadOnlyList<Order>> GetOrdersForUser(string buyerEmail);
        Task<Order> GetOrdersByPaymentIntent(string paymentIntentId);

        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
        Task<Order> GetOrderByIdAsync(int id);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task UpdateOrderStatus(Order order);
        Task DeleteOrder(Order order);
        Task<IReadOnlyList<Order>> GetNewOrders();
        Task<IReadOnlyList<Order>> GetOrdersByActualSatus(OrderActualStatus orderActualStatus);
        Task<IReadOnlyList<Order>> GetOrdersByEmail(string email);
        Task UpdateProduct(int productid, int qty, bool increment);
    }
}
