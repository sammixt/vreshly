using System;
namespace BLL.Entities
{
    public class Message : BaseEntity
    {
        public Message()
        {
        }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public MessageType MessageType { get; set; } = MessageType.Inbox;

        public bool ReadStatus { get; set; }
    }
}
