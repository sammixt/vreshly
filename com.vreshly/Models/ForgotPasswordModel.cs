using System;
using System.ComponentModel.DataAnnotations;

namespace com.vreshly.Models
{
    public class ForgotPasswordModel
    {
        public ForgotPasswordModel()
        {
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
