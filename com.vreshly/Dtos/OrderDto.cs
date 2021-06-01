using System;
namespace com.vreshly.Dtos
{
    public class OrderDto
    {
        public OrderDto()
        {
        }

        public  string  BasketId { get; set; }

        public int deliveryMethod { get; set; }

        public AddressDto ShipToAddress { get; set; }
    }
}
