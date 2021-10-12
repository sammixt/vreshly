using System;
namespace com.vreshly.Dtos
{
    public class ContactDto
    {
        public ContactDto()
        {
        }

        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
    }

    public class ContactAddressDto
    {
        public ContactAddressDto()
        {

        }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ContactSocialMedia
    {
        public ContactSocialMedia()
        {

        }

        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
    }
}
