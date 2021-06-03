using System;
namespace BLL.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(long productItemId, string productName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public long ProductItemId { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }
    }
}
