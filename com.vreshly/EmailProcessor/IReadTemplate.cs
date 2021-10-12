using System.Collections.Generic;
using com.vreshly.Models;

namespace com.vreshly.EmailProcessor
{
    public interface IReadTemplate
    {
        bool SendMailInvoice(string subject, OrderEmailContentModel content, string templateType);
        bool SendMailResetPassword(string subject, ResetPasswordModel content, string templateType);
    }
}