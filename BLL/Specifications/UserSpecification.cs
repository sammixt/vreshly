using System;
using BLL.Entities;
namespace BLL.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
       

        public UserSpecification()
        {
            AddInclude(x => x.Role);
            AddInclude(x => x.UserInformation);
           
        }

        public UserSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Role);
            AddInclude(x => x.UserInformation);
        }

        public UserSpecification(string username) : base(x => x.Username.ToLower() == username.ToLower())
        {
            AddInclude(x => x.Role);
            AddInclude(x => x.UserInformation);
        }
    }
}
