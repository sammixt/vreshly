using System;
namespace com.vreshly.Dtos
{
    public class UserDetailDto
    {
        public UserDetailDto()
        {
        }

        public long Id { get; set; }
        public string FullName { get; set; }

        public string Role { get; set; }
        public long RoleId { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string Avatar { get; set; }
        public UserInformationDto UserInformation { get; set; }
    }
}
