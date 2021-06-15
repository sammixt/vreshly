using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class WishListSpecification : BaseSpecification<WishList>
    {
        public WishListSpecification()
        {
            AddInclude(x => x.Product);
        }

        public WishListSpecification(string email)
            : base(x => x.user == email)
        {
            AddInclude(x => x.Product);
        }

        public WishListSpecification(long id)
            :base(x => x.Id == id)
        {
            AddInclude(x => x.Product);
        }
    }
}
