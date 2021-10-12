using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class ContactSpecification : BaseSpecification<Contact>
    {
        public ContactSpecification()
        {
        }

        public ContactSpecification(long id)
            : base(x => x.Id == id)
        {

        }
    }
}
