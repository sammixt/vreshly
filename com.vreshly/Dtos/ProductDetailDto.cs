using System;
using System.Collections.Generic;

namespace com.vreshly.Dtos
{
    public class ProductDetailDto
    {
        public ProductDetailDto()
        {
            ProductDetails = new ProductDto();
            RelatedProducts = new List<ProductDto>();
            Suggestions = new List<ProductDto>();

        }

        public ProductDto ProductDetails { get; set; }

        public IReadOnlyList<ProductDto> RelatedProducts { get; set; }

        public IReadOnlyList<ProductDto> Suggestions { get; set; }
    }
}
