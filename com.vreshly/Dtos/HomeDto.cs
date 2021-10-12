using System;
using System.Collections.Generic;

namespace com.vreshly.Dtos
{
    public class HomeDto
    {
        public HomeDto()
        {
            FeaturedProducts = new List<ProductDto>();
            BestSellers = new List<ProductDto>();
            Banner = new BannerDto();
            Educative = new EducativeDto();
        }

        public List<ProductDto> FeaturedProducts { get; set; }

        public List<ProductDto> BestSellers { get; set; }

        public BannerDto Banner { get; set; }

        public EducativeDto Educative { get; set; }
    }
}
