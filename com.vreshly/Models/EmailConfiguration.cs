using System;
namespace com.vreshly.Models
{
    public class EmailConfiguration
    {
        public EmailConfiguration()
        {
        }

        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
