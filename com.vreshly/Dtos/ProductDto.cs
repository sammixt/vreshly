using System;
using System.Collections.Generic;

namespace com.vreshly.Dtos
{
    public class ProductDto : UploadImageDto 
    {
        public ProductDto()
        {
        }
        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string ProductName { get; set; }
        public string ProductCodes { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool IsFeaturedProduct { get; set; }
        public bool IsBestSeller { get; set; }
        public string MainImage { get; set; }
        public string ImageOne { get; set; }
        public string ImageTwo { get; set; }
        public string ImageThree { get; set; }
        public string ImageFour { get; set; }
        public string ProductSummary { get; set; }
        public string ProductDescription { get; set; }
        public string Category { get; set; }
        public long CategoryId { get; set; }
        public string SubCategory { get; set; }
        public long SubCategoryId { get; set; }
        public string Brand { get; set; }
        public long BrandId { get; set; }
        public List<VariableDto> variables { get; set; }

        public bool SoldOut
        {
            get
            {
                return Quantity == 0;
            }
        }

        public ImageTypes ImageType { get; set; }
    }

    public enum ImageTypes
    {
        MainImage = 1,
        ImageOne = 2,
        ImageTwo = 3,
        ImageThree = 4,
        ImageFour = 5
    }
}
