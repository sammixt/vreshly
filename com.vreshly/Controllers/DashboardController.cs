using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities;
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
        private readonly IRecurringOrderService _recurringOrderService;

        public DashboardController(IDashboardService dashboardService, IOrderService orderService, IMapper mapper,
            IRecurringOrderService recurringOrderService)
        {
            _dashbaordService = dashboardService;
            _orderService = orderService;
            _mapper = mapper;
            _recurringOrderService = recurringOrderService;
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

        public async Task<ActionResult> GetReccurringOrdersDueInFiveDays()
        {
            var allRecurringOrders = await _recurringOrderService.GetAllRecurringOrders();
            var allRecurringOrdersDto = _mapper.Map<IEnumerable<RecurringOrder>, IEnumerable<RecurringOrderDto>>(allRecurringOrders);
            var dueInFiveDays = allRecurringOrdersDto.Where(x => x.DateDifference < 5).ToList();
            return Ok(dueInFiveDays);
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