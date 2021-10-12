using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using BLL.Interface;
using com.vreshly.Models;
using com.vreshly.Service;

namespace com.vreshly.EmailProcessor
{
    public class ReadTemplate : IReadTemplate
    {
        private readonly ILogger logger;
        private readonly ISettingsService settingsServices;
        

        public ReadTemplate()
        {
            this.logger = new Logger();
        }

        string emailTemplatePath = AppDomain.CurrentDomain.BaseDirectory + @"/Template/";

        IMailer emailSender;
        public ReadTemplate(IMailer _emailSender, ISettingsService _settingsServices)
        {
            emailSender = _emailSender;
            this.settingsServices = _settingsServices;
        }
        public bool SendMailInvoice(string subject, OrderEmailContentModel content, string templateType)
        {
            bool output = false;
            try
            {
                var template = File.ReadAllText($"{emailTemplatePath}{templateType}");
                template = template.Replace("{customer_name}", content.FullName);
                template = template.Replace("{total_amount}", content.TotalAmount);
                template = template.Replace("{expected_delivery_date}", content.ExpectedDeliveryDate);
                template = template.Replace("{purchase_id}", content.PurchaseId);
                template = template.Replace("{purchase_date}", content.PurchaseDate);
                template = template.Replace("{items}", content.Items);
                template = template.Replace("{shipping}", content.Shipping);
                template = template.Replace("{shipping_amount}", content.ShippingAmount);
                template = template.Replace("{status}", content.Status);
                template = template.Replace("{paymentstatus}", content.PaymentStatus);
                
                template = template.Replace("{subject}", subject);
                template = GetSocialMediaInfor(template);

                AlternateView avHtml = AddAlternateView(template);
                List<string> email = new List<string>();
                email.Add(content.Email);
                var message = new EmailMessage(email, subject, template, true, avHtml);
                emailSender.SendEmail(message);
                output = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return output;

        }

        public bool SendMailResetPassword(string subject, ResetPasswordModel content, string templateType)
        {
            bool output = false;
            try
            {
                var template = File.ReadAllText($"{emailTemplatePath}{templateType}");
                template = template.Replace("{customer_name}", content.FullName);
                template = template.Replace("{customer_email}", content.Email);
                template = template.Replace("{resetLink}", content.ResetLink);
                

                template = template.Replace("{subject}", subject);
                template = GetSocialMediaInfor(template);

                AlternateView avHtml = AddAlternateView(template);
                List<string> email = new List<string>();
                email.Add(content.Email);
                var message = new EmailMessage(email, subject, template, true, avHtml);
                emailSender.SendEmail(message);
                output = true;
            }
            catch (Exception ex)
            {
                //log
            }
            return output;

        }

        private string GetSocialMediaInfor(string template)
        {
            var contact = settingsServices.GetContacts().Result;
            template = template.Replace("{facebook}", contact?.Facebook);
            template = template.Replace("{twitter}", contact?.Twitter);
            template = template.Replace("{instagram}", contact?.Instagram);
            template = template.Replace("{support_mail}", $"mailto:{contact?.Email}");
            return template;
        }

        private AlternateView AddAlternateView(string template)
        {
            string imageLogo = $"{emailTemplatePath}logo.png";
            string imageInstagram = $"{emailTemplatePath}instagram.png";
            string imageFacebook = $"{emailTemplatePath}facebook.png";
            string imageTwitter = $"{emailTemplatePath}twitter.png";
            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(template, null, MediaTypeNames.Text.Html);
            LinkedResource logo = new LinkedResource(imageLogo, MediaTypeNames.Image.Jpeg);
            LinkedResource instagram = new LinkedResource(imageInstagram, MediaTypeNames.Image.Jpeg);
            LinkedResource facebook = new LinkedResource(imageFacebook, MediaTypeNames.Image.Jpeg);
            LinkedResource twitter = new LinkedResource(imageTwitter, MediaTypeNames.Image.Jpeg);
            logo.ContentId = "logo";
            instagram.ContentId = "instagram";
            facebook.ContentId = "facebook";
            twitter.ContentId = "twitter";

            avHtml.LinkedResources.Add(logo);
            avHtml.LinkedResources.Add(instagram);
            avHtml.LinkedResources.Add(facebook);
            avHtml.LinkedResources.Add(twitter);
            return avHtml;
        }
    }
}
