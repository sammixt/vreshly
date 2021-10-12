using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class EducativeSpecification : BaseSpecification<Educative>
    {
        public EducativeSpecification()
        {
        }

        public EducativeSpecification(long id)
            : base(x => x.Id == id)
        {

        }

        public EducativeSpecification(bool status)
            : base(x => x.Status == status)
        {

        }

        public EducativeSpecification(EducativeType type)
           : base(x => x.EducativeType == type)
        {

        }
        public EducativeSpecification(EducativeType type, bool status)
            : base(x => x.Status == status && x.EducativeType == type)
        {

        }
    }
}
