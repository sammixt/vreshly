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
