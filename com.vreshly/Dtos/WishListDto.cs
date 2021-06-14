using System;
namespace com.vreshly.Dtos
{
    public class WishListDto
    {
        public WishListDto()
        {
        }
        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string user { get; set; }

        public long ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
