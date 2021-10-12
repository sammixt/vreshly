using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities.OrderAggregate;
using BLL.Infrastructure.Paystack;
using BLL.Interface;
using com.vreshly.Dtos;
using com.vreshly.EmailProcessor;
using com.vreshly.Errors;
using com.vreshly.Extensions;
using com.vreshly.Helper;
using com.vreshly.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;



namespace com.vreshly.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IReadTemplate readTemplate;
        public OrdersController(IOrderService orderService, IMapper mapper, IConfiguration config, IReadTemplate readTemplate)
        {
            _orderService = orderService;
            _mapper = mapper;
            _config = config;
            this.readTemplate = readTemplate;
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
            GenerateOrderMail orderMail = new GenerateOrderMail(order);
            var content = orderMail.GetContent();
            await  Task.Run(() =>
              {
                readTemplate.SendMailInvoice("Invoice", content, TemplateFiles.InvoiceTemplate);
            });
            
            //send mail

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
                //send mail
                GenerateOrderMail orderMail = new GenerateOrderMail(order);
                var content = orderMail.GetContent();
                await Task.Run(() =>
                {
                    readTemplate.SendMailInvoice("Invoice", content, TemplateFiles.InvoiceTemplate);
                });
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

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrdersForUser(email);

            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrderByIdAsync(id,email);
            if (orders == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Order,OrderToReturnDto>(orders));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrderStatus([FromQuery]int id, int status)
        {
            var orders = await _orderService.GetOrderByIdAsync(id);
            if (orders == null) return NotFound(new ApiResponse(404));
            orders.ActualOrderStatus = (OrderActualStatus)status;
            if((OrderActualStatus)status == OrderActualStatus.PaymentReceived)
            {
                if(orders.PaymentMethod == PaymentMethod.BankPayment ||
                    orders.PaymentMethod == PaymentMethod.PaymentOnDelivery)
                {
                    orders.Status = OrderStatus.PaymentReceived;
                    foreach(var item in orders.OrderItems)
                    {
                        _ = _orderService.UpdateProduct(
                                (int)item.ItemOrdered.ProductItemId,
                                item.Quantity,
                                false
                                );
                    }
                }
            }

            if((OrderActualStatus)status == OrderActualStatus.Cancelled && orders.Status == OrderStatus.PaymentReceived)
            {
                foreach (var item in orders.OrderItems)
                {
                    _ = _orderService.UpdateProduct(
                            (int)item.ItemOrdered.ProductItemId,
                            item.Quantity,
                            true
                            );
                }
            }
            await _orderService.UpdateOrderStatus(orders);
            //send mail
            //send mail
            GenerateOrderMail orderMail = new GenerateOrderMail(orders);
            var content = orderMail.GetContent();
            await Task.Run(() =>
            {
                readTemplate.SendMailInvoice("Invoice", content, TemplateFiles.InvoiceTemplate);
            });
            
            return Ok(new ApiResponse(200, orders.ActualOrderStatus.GetAttributeOfType<EnumMemberAttribute>().Value));
        }

        //[HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}
