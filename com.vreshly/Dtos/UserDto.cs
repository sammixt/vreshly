using System;
namespace com.vreshly.Dtos
{
    public class UserDto
    {
        public UserDto()
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
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool PasswordMatch
        {
            get
            {
                return (Password == ConfirmPassword) ? true : false;
            }
        } 
    }

    public enum UserStatus
    {
        Active = 1,
        Inactive = 2
    }
}
