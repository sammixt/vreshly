using System;
namespace com.vreshly.Dtos
{
    public class UserInformationDto
    {
        public UserInformationDto()
        {
        }
        public long Id { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public UserDto User { get; set; }
        public long UserId { get; set; }

        public bool GenderType { get; set; }

        void SetGender()
        {

            if (Gender.ToLower().Equals("male"))
            {
                GenderType = true;
            }
            else
            {
                GenderType = false;
            }
        }
    }
}
