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

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, OrderAddress shippingAddress, int paymentMethod)
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
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal,basket.PaymentIntent,(PaymentMethod)paymentMethod);
            //save to db
            order.CreatedDate = DateTime.Now;
            _unitOfWork.Repository<Order>().Add(order);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;

            //delete basket async
            await basketRepo.DeleteBasketAsync(basketId);
            //return order
            return order;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, Product product, OrderAddress shippingAddress, int paymentMethod, int quantity)
        {
            Guid gid = Guid.NewGuid();
            string guid = gid.ToString();

            //get items from product repo
            var items = new List<OrderItem>();

                var itemOrdered = new ProductItemOrdered(product.Id, product.ProductName, product.MainImage);
                var orderItem = new OrderItem(itemOrdered, product.DiscountPrice, quantity);
                items.Add(orderItem);
            

            //get delivery method

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //create order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal, guid, (PaymentMethod)paymentMethod);
            //save to db
            order.CreatedDate = DateTime.Now;
            _unitOfWork.Repository<Order>().Add(order);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;

            //delete basket async
            
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

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var spec = new OrdersWithItemsAndOrderingSpecifications((long)id);
            return await _unitOfWork.Repository<Order>().GetEntitiesWithSpec(spec);
        }

        public async Task<Order> GetOrdersByPaymentIntent(string paymentIntentId)
        {
            var spec = new OrdersSpecification(paymentIntentId);
            return await _unitOfWork.Repository<Order>().GetEntitiesWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUser(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecifications(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }

        public async Task<IReadOnlyList<Order>> GetNewOrders()
        {
            var spec = new OrdersSpecification(OrderStatus.PaymentReceived,PaymentMethod.OnlinePayment, 
            OrderStatus.Pending, PaymentMethod.BankPayment,PaymentMethod.PaymentOnDelivery,OrderActualStatus.Pending);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersByActualSatus(OrderActualStatus orderActualStatus)
        {
            var spec = new OrdersSpecification(orderActualStatus);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersByEmail(string email)
        {
            var spec = new OrdersSpecification(email, true); ;

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }


        public async Task UpdateOrderStatus(Order order)
        {
            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.Complete();
        }

        public async Task DeleteOrder(Order order)
        {
            _unitOfWork.Repository<Order>().Delete(order);
            await _unitOfWork.Complete();
        }

        public async Task UpdateProduct(int productid,int qty, bool increment)
        {
            var specwithId = new ProductSpecification(productid);
            var productWithId = await _unitOfWork.Repository<Product>().GetEntitiesWithSpec(specwithId);

            productWithId.Quantity = (increment) ? productWithId.Quantity + qty :
                (productWithId.Quantity != 0) ? productWithId.Quantity - qty : 0;
            _unitOfWork.Repository<Product>().Update(productWithId);
            int result = await _unitOfWork.Complete();
        }

    }
}
