using System;
using System.Runtime.Serialization;
using System.Text;
using BLL.Entities.OrderAggregate;
using com.vreshly.Extensions;
using com.vreshly.Models;

namespace com.vreshly.Helper
{
    public class GenerateOrderMail
    {
        public Order order;
        public GenerateOrderMail(Order order)
        {
            this.order = order;
        }

        public string OrderItemsContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in order.OrderItems)
            {
                sb.AppendLine($"<tr><td style = 'font-family: 'Montserrat',Arial,sans-serif; font-size: 14px; padding-top: 10px; padding-bottom: 10px; width: 80%;' width = '80%'>" +
                    $"{item.ItemOrdered.ProductName} Qty:{item.Quantity}</td><td align = 'right' style = 'font-family: 'Montserrat',Arial,sans-serif; font-size: 14px; text-align: right; width: 20%;' width = '20%' >{item.Price}</td></tr>");
            }
            return sb.ToString();
        }



        public OrderEmailContentModel GetContent()
        {
            OrderEmailContentModel content = new OrderEmailContentModel();
            content.Items = OrderItemsContent();
            content.TotalAmount = order.GetTotal().ToString();
            content.PurchaseDate = order.OrderDate.UtcDateTime.ToLongDateString();
            content.ExpectedDeliveryDate = order.OrderDate.AddDays(7).UtcDateTime.ToLongDateString();
            content.PurchaseId = order.PaymentIntentId;
            content.Shipping = order.DeliveryMethod.ShortName;
            content.ShippingAmount = order.DeliveryMethod.Price.ToString();
            content.Email = order.BuyerEmail;
            content.Status = order.ActualOrderStatus.GetAttributeOfType<EnumMemberAttribute>().Value;
            content.PaymentStatus = order.Status.GetAttributeOfType<EnumMemberAttribute>().Value;
            content.FullName = order.ShipToAddress.FirstName + " " + order.ShipToAddress.LastName;
            return content;
        }

      
    }

   
}
