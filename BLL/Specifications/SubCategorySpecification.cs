using System;
using System.Linq.Expressions;
using BLL.Entities;

namespace BLL.Specifications
{
    public class SubCategorySpecification : BaseSpecification<SubCategory>
    {
        public SubCategorySpecification()
        {
            AddInclude(x => x.Category);
        }

        public SubCategorySpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Category);
        }

        public SubCategorySpecification(int categoryid, string name) : base(x => x.CategoryId == categoryid && x.SubCategoryName.ToLower() == name)
        {
            AddInclude(x => x.Category);
        }

        public SubCategorySpecification(long id): base(x => x.CategoryId == id)
        {
            AddInclude(x => x.Category);
        }

        public SubCategorySpecification GetByCategory(long id)
        {
            return new SubCategorySpecification(id);
        }
    }
}