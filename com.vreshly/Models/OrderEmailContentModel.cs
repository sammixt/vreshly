using System;
namespace com.vreshly.Models
{
    public class OrderEmailContentModel
    {
        public OrderEmailContentModel()
        {
        }

        public string Items { get; set; }
        public string TotalAmount { get; set; }
        public string PurchaseDate { get; set; }
        public string ExpectedDeliveryDate { get; set; }
        public string PurchaseId { get; set; }
        public string Shipping { get; set; }
        public string ShippingAmount { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string FullName { get; set; }
        public string PaymentStatus { get; internal set; }
    }
}
