using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities.OrderAggregate;
using BLL.Infrastructure.Paystack;
using BLL.Interface;
using com.vreshly.Dtos;
using com.vreshly.Errors;
using com.vreshly.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public OrdersController(IOrderService orderService, IMapper mapper, IConfiguration config)
        {
            _orderService = orderService;
            _mapper = mapper;
            _config = config;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody]OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, OrderAddress>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.deliveryMethod, orderDto.BasketId, address, orderDto.paymentMethod);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem Creating  Order"));
            string key = _config["Paystack:Publickey"];
            var totalAmount = order.GetTotal();

            return Ok(new
            {
               info = order,
               pubKey = key,
               total = totalAmount
            });
        }

        [HttpGet]
        public async Task<ActionResult<TransactionResponseModel>> VerifyTransaction(string reference)
        {
            var client = HttpConnection.CreateClient(_config["Paystack:Secretkey"]);
            var response = await client.GetAsync($"transaction/verify/{reference}");

            var json = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<TransactionResponseModel>(json);

            var order = await _orderService.GetOrdersByPaymentIntent(reference);
            if(output != null)
            {
                order.Status = output.data.status.Equals("success") ? OrderStatus.PaymentReceived : OrderStatus.PaymentFailed;
                await _orderService.UpdateOrderStatus(order);
                return Ok(output.data);
            }

            return BadRequest(new ApiResponse(400, "Unable to verify payment"));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(string reference)
        {
            var order = await _orderService.GetOrdersByPaymentIntent(reference);
            if (order == null) return NotFound(new ApiResponse(404));
            await _orderService.DeleteOrder(order);
            return Ok(new ApiResponse(200, $"Order Deleted"));
        }

        //[HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrdersForUser(email);

            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }

        //[HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrderByIdAsync(id,email);
            if (orders == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Order,OrderToReturnDto>(orders));
        }

        //[HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}
