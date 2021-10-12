using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities;
using BLL.Entities.Identity;
using BLL.Entities.OrderAggregate;
using BLL.Interface;
using com.vreshly.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class CustomersController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IRecurringOrderService _recurringOrderService;
        // GET: /<controller>/

        public CustomersController(UserManager<AppUser> userManager, IMapper mapper,
            IOrderService orderService, IRecurringOrderService recurringOrderService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _orderService = orderService;
            _recurringOrderService = recurringOrderService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageName = $"Customers";
            ViewBag.returnUrl = "Index";
            var userDetails = await Task.Run(() => _userManager.Users.Include(x => x.Address).ToList());
            var userInfoDto = _mapper.Map<IEnumerable<AppUser>, IEnumerable<CustomersDto>>(userDetails);
            return View(userInfoDto);
        }

        public async Task<IActionResult> CustomerOrders(string mail)
        {
            if (string.IsNullOrEmpty(mail))
                return RedirectToAction(nameof(Index));
            ViewBag.PageName = $"Customer Orders";
            string ReturnUrl = "Index";
            string PageName = "Customers";
            ViewBag.returnUrl = "Index";
            ViewBag.pg = mail;
            ViewBag.Breadcrumbs = $"<li class=\"breadcrumb-item\"><a href=\"\\Customers\\{ReturnUrl}\">{PageName}</a></li><li class=\"breadcrumb-item active\">{mail}</li>";
            var newOrders = await _orderService.GetOrdersByEmail(mail);
            var recurringOrder = await _recurringOrderService.GetRecurringOrderByEmail(mail);
            var ordersDto = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(newOrders);
            var recuringOrderDto = _mapper.Map<IEnumerable<RecurringOrder>, IEnumerable<RecurringOrderDto>>(recurringOrder);
            CustomerTransactions customerTransactions = new CustomerTransactions()
            {
                orders = (List<OrderToReturnDto>)ordersDto,
                recurringOrder = (List<RecurringOrderDto>)recuringOrderDto
            };

            return View(customerTransactions);
        }

        public async Task<IActionResult> Details(long rd, string returnUrl, string pg)
        {
            ViewBag.PageName = $"Recurring-Order Details";
            string ReturnUrl = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "Index";
            string PageName = !string.IsNullOrEmpty(pg) ? pg : pg;
            ViewBag.Breadcrumbs = $"<li class=\"breadcrumb-item\">" +
                $"<a href=\"\\Customers\\{ReturnUrl}\">Customers</a></li>" +
                $"<li class=\"breadcrumb-item\"><a href=\"\\Customers\\CustomerOrders?mail={pg}\">{pg}</a></li>" +
                $"<li class=\"breadcrumb-item active\">Details</li>";
            var order = await _recurringOrderService.GetRecurringOrderById(rd);
            var getAllOrdersByEmail = (IEnumerable<RecurringOrder>)await _recurringOrderService.GetRecurringOrderByEmail(order.UserEmail);
            var ordersDto = _mapper.Map<IEnumerable<RecurringOrder>, IEnumerable<RecurringOrderDto>>(getAllOrdersByEmail);
            var getUserInformation = await _recurringOrderService.GetCustomersInfo(order.UserEmail);
            var userInfoDto = _mapper.Map<AppUser, CustomersDto>(getUserInformation);

            RecurringDetailsDto recurringDetailsDto = new RecurringDetailsDto()
            {
                CustomerInformation = userInfoDto,
                Orders = ordersDto
            };

            return View("../AdminRecurring/Details",recurringDetailsDto);
        }

        public async Task<IActionResult> OrderDetails(string pi, string returnUrl, string pg)
        {
            ViewBag.PageName = $"Order Details";
            string ReturnUrl = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "Index";
            string PageName = !string.IsNullOrEmpty(pg) ? pg :pg ;
            ViewBag.Breadcrumbs = $"<li class=\"breadcrumb-item\">" +
                $"<a href=\"\\Customers\\{ReturnUrl}\">Customers</a></li>" +
                $"<li class=\"breadcrumb-item\"><a href=\"\\Customers\\CustomerOrders?mail={pg}\">{pg}</a></li>" +
                $"<li class=\"breadcrumb-item active\">Details</li>";
            var orderdetail = await _orderService.GetOrdersByPaymentIntent(pi);
            var orders = _mapper.Map<Order, OrderToReturnDto>(orderdetail);
            return View("../AdminOrder/OrderDetails",orders);
        }
    }
}
