using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Entities.OrderAggregate;
using BLL.Interface;
using BLL.Specifications;

namespace BLL.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository basketRepo;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            this.basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, OrderAddress shippingAddress)
        {
            //get basket from basket repo
            var basket = await basketRepo.GetBasketAsync(basketId);

            //get items from product repo
            var items = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.ProductName, productItem.MainImage);
                var orderItem = new OrderItem(itemOrdered, productItem.DiscountPrice, item.Quantity);
                items.Add(orderItem);
            }

            //get delivery method

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //create order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal);
            //save to db
            _unitOfWork.Repository<Order>().Add(order);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;

            //delete basket async
            await basketRepo.DeleteBasketAsync(basketId);
            //return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecifications(id,buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEntitiesWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUser(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecifications(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
