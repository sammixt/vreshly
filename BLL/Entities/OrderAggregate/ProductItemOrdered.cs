using System;
namespace BLL.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(long productItemId, string productName, string productUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            ProductUrl = productUrl;
        }

        public long ProductItemId { get; set; }

        public string ProductName { get; set; }

        public string ProductUrl { get; set; }
    }
}
