using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class RoleSpecification : BaseSpecification<Role>
    {
        public RoleSpecification()
        {
        }

        public RoleSpecification(int id) : base(x => x.Id == id)
        {

        }
    }
}
