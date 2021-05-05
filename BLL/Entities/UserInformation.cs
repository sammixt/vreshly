using System;
namespace BLL.Entities
{
    public class UserInformation : BaseEntity
    {
        public UserInformation()
        {
        }

        public Nullable<DateTime> DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public User User { get; set; }
        public long UserId { get; set; }
    }
}
