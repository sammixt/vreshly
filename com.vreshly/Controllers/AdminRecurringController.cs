using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BLL.Entities.OrderAggregate;
using BLL.Entities;
using BLL.Interface;
using com.vreshly.Dtos;
using Microsoft.Extensions.Configuration;
using BLL.Entities.Identity;
using com.vreshly.Errors;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class AdminRecurringController : Controller
    {

        private readonly IRecurringOrderService _recurringOrderService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IOrderService _orderService;

        public AdminRecurringController(IRecurringOrderService recurringOrderService, IMapper mapper,
            IConfiguration config, IOrderService orderService)
        {
            _recurringOrderService = recurringOrderService;
            _mapper = mapper;
            _config = config;
            _orderService = orderService;
        }
        // GET: /<controller>/
        public async Task<IActionResult> NewOrders()
        {
            ViewBag.PageName = $"New Recurring Orders";
            ViewBag.returnUrl = "NewOrders";
            var orders = await _recurringOrderService.GetAllRecurringOrders();
            var ordersDto = _mapper.Map<IEnumerable<RecurringOrder>, IEnumerable<RecurringOrderDto>>(orders);
            var newOrder = ordersDto.Where(x => x.DateCreatedToDate < 5).ToList();
            return View(newOrder);
        }

        public async Task<IActionResult> DueSoon()
        {
            ViewBag.PageName = $"Orders Due in 5 Days";
            ViewBag.returnUrl = "DueSoon";
            var orders = await _recurringOrderService.GetAllRecurringOrders();
            var ordersDto = _mapper.Map<IEnumerable<RecurringOrder>, IEnumerable<RecurringOrderDto>>(orders);
            var newOrder = ordersDto.Where(x => x.DateDifference < 5).ToList();
            return View("NewOrders", newOrder);
        }

        public async Task<IActionResult> Details(long rd, string returnUrl, string pg)
        {
            ViewBag.PageName = $"Order Details";
            string ReturnUrl = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "NewOrders";
            string PageName = !string.IsNullOrEmpty(pg) ? pg : "New Recurring Orders";
            ViewBag.Breadcrumbs = $"<li class=\"breadcrumb-item\"><a href=\"\\AdminRecurring\\{ReturnUrl}\">{PageName}</a></li><li class=\"breadcrumb-item active\">Details</li>";
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

            return View(recurringDetailsDto);
        }

        public async Task<ActionResult> ProcessOrder(long id, int delivery)
        {
            var order = await _recurringOrderService.GetRecurringOrderById(id);
            //var getAllOrdersByEmail = (IEnumerable<RecurringOrder>)await _recurringOrderService.GetRecurringOrderByEmail(order.UserEmail);
            //var ordersDto = _mapper.Map<IEnumerable<RecurringOrder>, IEnumerable<RecurringOrderDto>>(getAllOrdersByEmail);
            var getUserInformation = await _recurringOrderService.GetCustomersInfo(order.UserEmail);
            var userInfoDto = _mapper.Map<AppUser, CustomersDto>(getUserInformation);
            OrderAddress address = new OrderAddress()
            {
                FirstName = userInfoDto.FirstName,
                LastName = userInfoDto.LastName,
                City = userInfoDto.City,
                Street = userInfoDto.Street,
                State = userInfoDto.State,
                PhoneNumber = userInfoDto.PhoneNumber,
                ZipCode = userInfoDto.ZipCode
            };
           
            var create = await _orderService.CreateOrderAsync(order.UserEmail, delivery, order.Product, address,2,order.Quantity);
            if(create != null)
            {
                order.PreviousDeliveryDate = order.NextDelievery;
                order.NextDelievery = DateTime.Now.AddDays(_recurringOrderService.GetDaysToAdd(order.Frequency));
                await _recurringOrderService.UpdateRecurringOrder(order);

                return Ok(new ApiResponse(200, "Order Created"));
            }

            return BadRequest(new ApiResponse(500, "Error occurred"));

        }


    }
}
