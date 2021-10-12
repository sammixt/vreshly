using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace com.vreshly.Models
{
    public class EmailMessage
    {
        public EmailMessage()
        {
        }

        public EmailMessage(IEnumerable<string> to, string subject, string content, bool isBodyHtml, AlternateView altView)
        {
            To = new List<string>();

            To.AddRange(to);
            Subject = subject;
            Content = content;
            IsBodyHtml = isBodyHtml;
            AltView = altView;
        }

        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsBodyHtml { get; set; }
        public AlternateView AltView { get; set; }

        
    }
}
