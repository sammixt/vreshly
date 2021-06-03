using System;
namespace com.vreshly.Dtos
{
    public class OrderItemDto
    {
        public OrderItemDto()
        {
        }

        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
