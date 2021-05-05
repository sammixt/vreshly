using System;
using System.Collections.Generic;

namespace BLL.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
        }

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
        public Category Category { get; set; }
        public long CategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public long SubCategoryId { get; set; }
        public Brand Brand { get; set; }
        public long BrandId { get; set; }
        public List<Variable> variables { get; set; }
    }
}
