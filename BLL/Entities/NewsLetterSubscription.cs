using System;
namespace BLL.Entities
{
    public class NewsLetterSubscription : BaseEntity
    {
        public NewsLetterSubscription()
        {
        }

        public string Email { get; set; }
    }
}
