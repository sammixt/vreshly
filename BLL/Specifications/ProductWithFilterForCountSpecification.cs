using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification()
        {
        }

        public ProductWithFilterForCountSpecification(ProductSpecParams productSpec)
           : base(x =>
           (string.IsNullOrEmpty(productSpec.Search) || x.ProductName.ToLower().Contains(productSpec.Search)) &&
           (!productSpec.CategoryId.HasValue || x.CategoryId == productSpec.CategoryId))
        {

        }
    }
}
