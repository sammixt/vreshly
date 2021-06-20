using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities.OrderAggregate;
using BLL.Interface;
using com.vreshly.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace com.vreshly.Controllers
{
    public class AdminOrderController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AdminOrderController(IOrderService orderService, IMapper mapper, IConfiguration config)
        {
            _orderService = orderService;
            _mapper = mapper;
            _config = config;
        }
        public async Task<IActionResult> Orders()
        {
            ViewBag.PageName = $"New Orders";
            ViewBag.returnUrl = "Orders";
            var newOrders = await _orderService.GetNewOrders();
            var orders = _mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(newOrders);
            return View(orders);
        }

        public async Task<IActionResult> ConfirmedOrders()
        {
            ViewBag.PageName = $"Confirmed Orders";
            ViewBag.returnUrl = "ConfirmedOrders";
            var newOrders = await _orderService.GetOrdersByActualSatus(OrderActualStatus.PaymentReceived);
            var orders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(newOrders);
            return View("Orders",orders);
        }

        public async Task<IActionResult> ShippedOrders()
        {
            ViewBag.PageName = $"Shipped Orders";
            ViewBag.returnUrl = "ShippedOrders";
            var newOrders = await _orderService.GetOrdersByActualSatus(OrderActualStatus.Processing);
            var orders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(newOrders);
            return View("Orders", orders);
        }

        public async Task<IActionResult> CompletedOrders()
        {
            ViewBag.PageName = $"Completed Orders";
            ViewBag.returnUrl = "CompletedOrders";
            var newOrders = await _orderService.GetOrdersByActualSatus(OrderActualStatus.Completed);
            var orders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(newOrders);
            return View("Orders", orders);
        }

        public async Task<IActionResult> CancelledOrders()
        {
            ViewBag.PageName = $"Cancelled Orders";
            ViewBag.returnUrl = "CancelledOrders";
            var newOrders = await _orderService.GetOrdersByActualSatus(OrderActualStatus.Cancelled);
            var orders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(newOrders);
            return View("Orders", orders);
        }

        public async Task<IActionResult> OrderDetails(string pi, string returnUrl,string pg)
        {
            ViewBag.PageName = $"Order Details";
            string ReturnUrl = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "Orders";
            string PageName = !string.IsNullOrEmpty(pg) ? pg : "New Orders";
            ViewBag.Breadcrumbs = $"<li class=\"breadcrumb-item\"><a href=\"\\AdminOrder\\{ReturnUrl}\">{PageName}</a></li><li class=\"breadcrumb-item active\">Details</li>";
            var orderdetail = await _orderService.GetOrdersByPaymentIntent(pi);
            var orders = _mapper.Map<Order,OrderToReturnDto>(orderdetail);
            return View(orders);
        }
        
    }
}