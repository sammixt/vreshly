using System;
using System.ComponentModel.DataAnnotations;

namespace com.vreshly.Dtos
{
    public class AddressDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public int Id { get;  set; }
    }
}
