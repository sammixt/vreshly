using com.vreshly.Models;

namespace com.vreshly.EmailProcessor
{
    public interface IMailer
    {
        void SendEmail(EmailMessage message);
    }
}