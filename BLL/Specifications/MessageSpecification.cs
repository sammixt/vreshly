using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class MessageSpecification : BaseSpecification<Message>
    {
        public MessageSpecification()
        {
        }

        public MessageSpecification(long id) : base(x => x.Id == id)
        {
            AddOrderByDescending(x => x.CreatedDate);
        }

        public MessageSpecification(MessageType messageType) : base(x => x.MessageType == messageType)
        {
            AddOrderByDescending(x => x.CreatedDate);
        }
    }
}
