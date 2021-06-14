using System;
namespace BLL.Entities
{
    public class WishList : BaseEntity
    {
        public WishList()
        {
        }

        public string user { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

    }
}
