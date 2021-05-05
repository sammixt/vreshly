using System;
namespace BLL.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
        }

        public string FullName { get; set; }

        public Role Role { get; set; }
        public long RoleId { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string Avatar { get; set; }

        public UserInformation UserInformation { get; set; }
    }
}
