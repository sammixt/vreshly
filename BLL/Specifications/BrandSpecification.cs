using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class BrandSpecification : BaseSpecification<Brand>
    {
        public BrandSpecification()
        {
        }

        public BrandSpecification(int id) : base(x => x.Id == id)
        {

        }

        public BrandSpecification(string name) : base(x => x.BrandName.ToLower() == name)
        {

        }

    }
}
