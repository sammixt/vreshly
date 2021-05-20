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
           : base(x => (!productSpec.CategoryId.HasValue || x.CategoryId == productSpec.CategoryId))
        {

        }
    }
}
