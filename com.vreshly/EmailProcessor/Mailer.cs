using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using com.vreshly.Models;
using com.vreshly.Service;

namespace com.vreshly.EmailProcessor
{
    public class Mailer : IMailer
    {
        private readonly EmailConfiguration emailConfig;
        private readonly ILogger logger;
        public Mailer(IOptions<EmailConfiguration> _emailConfig,
            ILogger logger)
        {
            emailConfig = _emailConfig.Value;
            this.logger = logger;
        }

        public void SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MailMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(emailConfig.From, "Vreshly");
            foreach (var add in message.To)
            {
                emailMessage.To.Add(add);
            }

            emailMessage.Subject = message.Subject;
            emailMessage.Body = message.Content;
            emailMessage.IsBodyHtml = message.IsBodyHtml;
            emailMessage.BodyEncoding = Encoding.UTF8;
            emailMessage.AlternateViews.Add(message.AltView);

            return emailMessage;
        }

        private void Send(MailMessage mailMessage)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            //System.Net.ServicePointManager.Expect100Continue = false;
            using (var client = new SmtpClient())
            {
                try
                {
                    client.UseDefaultCredentials = false;
                    client.Host = emailConfig.SmtpServer;
                    client.Port = emailConfig.Port;
                    client.Credentials = new NetworkCredential(emailConfig.UserName, emailConfig.Password);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Timeout = 20000;

                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    //log an error message or throw an exception or both.
                    logger.Error(ex);
                }
                finally
                {
                    client.Dispose();
                }
            }
        }

    }
}
