using System;
namespace BLL.Entities.OrderAggregate
{
    public class OrderAddress
    {
        public OrderAddress()
        {
        }

        public OrderAddress(string firstName, string lastName, string street, string city, string state, string zipCode, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            PhoneNumber = phoneNumber;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
