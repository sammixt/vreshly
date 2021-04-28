using System;
using System.Linq.Expressions;
using BLL.Entities;

namespace BLL.Specifications
{
    public class CategorySpecification : BaseSpecification<Category>
    {
        public CategorySpecification()
        {
        }

        public CategorySpecification(int id) : base(x => x.Id == id)
        {
            
        }

        public CategorySpecification(string name): base(x => x.CategoryName.ToLower() == name)
        {

        }
    }
}
