using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification()
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.Brand);
        }

        public ProductSpecification(ProductSpecParams productSpec)
            : base(x =>
            (string.IsNullOrEmpty(productSpec.Search) || x.ProductName.ToLower().Contains(productSpec.Search)) &&
            (!productSpec.CategoryId.HasValue || x.CategoryId == productSpec.CategoryId))
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.Brand);
            AddOrderBy(p => p.ProductName);
            ApplyPaging(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);
            if (!string.IsNullOrEmpty(productSpec.sort))
            {
                switch (productSpec.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.DiscountPrice);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.DiscountPrice);
                        break;
                    case "prodName":
                        AddOrderBy(p => p.ProductName);
                        break;
                    case "dateDesc":
                        AddOrderByDescending(p => p.CreatedDate);
                        break;
                    default:
                        AddOrderBy(p => p.ProductName);
                        break;

                }
            }
        }


        public ProductSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.Brand);
        }

        public ProductSpecification(long id) : base(x => x.CategoryId == id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.Brand);
        }

        public ProductSpecification(bool featured) : base(x => x.IsFeaturedProduct == featured)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.Brand);
        }

        public ProductSpecification(bool featured, bool bestseller) : base(x => x.IsBestSeller == bestseller)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.Brand);
        }

        public ProductSpecification(string name) : base(x => x.ProductName.ToLower() == name)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.Brand);
        }

        public ProductSpecification GetByCategory(long id)
        {
            return new ProductSpecification(id);
        }

    }
}
