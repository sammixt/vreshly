using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class UserInformationSpecification : BaseSpecification<UserInformation>
    {
        public UserInformationSpecification() 
        {
        }

        public UserInformationSpecification(int id) : base(x => x.Id == id)
        {
        }

        public UserInformationSpecification(long userId) : base(x => x.UserId == userId)
        {

        }
    }
}
