using System;
namespace BLL.Entities
{
    public class Contact : BaseEntity
    {
        public Contact()
        {
        }

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
}
