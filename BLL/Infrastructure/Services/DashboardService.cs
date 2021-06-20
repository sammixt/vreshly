using System.Threading.Tasks;
using BLL.Entities.OrderAggregate;
using BLL.Interface;
using BLL.Specifications;
using System.Linq;
using System.Collections.Generic;
using BLL.Entities;
using Microsoft.AspNetCore.Identity;
using BLL.Entities.Identity;
using System;

namespace BLL.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public DashboardService(IUnitOfWork unitOfWork,UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<int> GetNewOrders()
        {
            var spec = new OrdersSpecification(Entities.OrderAggregate.OrderActualStatus.Pending);
            var orders = (List<Order>)await _unitOfWork.Repository<Order>().ListAsync(spec);
            var newOrders = orders.Count;
            return newOrders;
        }

        public async Task<int> GetTotalSales()
        {
            var spec = new OrdersSpecification(Entities.OrderAggregate.OrderStatus.PaymentReceived);
            var orders =  (List<Order>)await _unitOfWork.Repository<Order>().ListAsync(spec);
            var totalSale = orders.Count;
            return totalSale;
        }
        public async Task<decimal> GetTotalRevenue()
        {
            var spec = new OrdersSpecification(Entities.OrderAggregate.OrderStatus.PaymentReceived);
            var orders =  (List<Order>)await _unitOfWork.Repository<Order>().ListAsync(spec);
            var totalRevenue = orders.Sum(x => x.Subtotal);
            return totalRevenue;
        }

        public async Task<decimal> GetTotalRevenuePerMonth()
        {
            var date = DateTime.Now;
            var spec = new OrdersSpecification(Entities.OrderAggregate.OrderStatus.PaymentReceived);
            var orders = (List<Order>)await _unitOfWork.Repository<Order>().ListAsync(spec);
            var totalRevenue = orders.Where(x => x.OrderDate.Month == date.Month && x.OrderDate.Year == date.Year )
                .Sum(x => x.Subtotal);
            return totalRevenue;
        }


        public async Task<int> GetTotalProduct()
        {
            var products = await _unitOfWork.Repository<Product>().ListAllAsync();
            int totalProducts = products.Count;
            return totalProducts;
        }

        public async Task<int> GetTotalCustomers()
        {
            var totalCustomers =  await Task.Run( () => _userManager.Users.Count()) ;
            return totalCustomers;
        }
    }
}