using System;
namespace com.vreshly.Models
{
    public class ResetPasswordModel
    {
        public ResetPasswordModel()
        {
           
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string ResetLink { get; set; }
    }
}
