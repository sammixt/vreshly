using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class NewsLetterSpecification : BaseSpecification<NewsLetterSubscription>
    {
        public NewsLetterSpecification()
        {
        }

        public NewsLetterSpecification(long id)
            : base(x => x.Id == id)
        {

        }

        public NewsLetterSpecification(string mail)
            : base(x => x.Email.Trim() == mail)
        {

        }
    }
}
