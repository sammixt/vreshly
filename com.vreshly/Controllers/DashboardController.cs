using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities.OrderAggregate;
using BLL.Interface;
using com.vreshly.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace com.vreshly.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashbaordService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardService dashboardService, IOrderService orderService, IMapper mapper)
        {
            _dashbaordService = dashboardService;
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<ActionResult> GetDashboardData()
        {
            int totalSales = await _dashbaordService.GetTotalSales();
            decimal totalRevenue = await _dashbaordService.GetTotalRevenue();
            int totalCustomers = await _dashbaordService.GetTotalCustomers();
            int totalProducts = await _dashbaordService.GetTotalProduct();
            decimal monthsRevenue = await _dashbaordService.GetTotalRevenuePerMonth();
            int newOrders = await _dashbaordService.GetNewOrders();

            return Ok(new 
            {
                sales = totalSales,
                revenues = totalRevenue,
                products = totalProducts,
                customers = totalCustomers,
                revenuePerMonth = monthsRevenue,
                neworders = newOrders
            });
        }

       

        public async Task<ActionResult> GetNewOrders()
        {
            var newOrders = (List<Order>)await _orderService.GetNewOrders();
            var topSixOrders = newOrders.Take(6).ToList();
            var orders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(topSixOrders);
            return Ok(orders);
        }

    }
}