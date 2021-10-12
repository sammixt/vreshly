using System;
namespace com.vreshly.Dtos
{
    public class MessageDto
    {
        public MessageDto()
        {
        }

        public long Id { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }

        public Nullable<DateTime> UpdateDate { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string MessageType { get; set; } 

        public bool ReadStatus { get; set; }

        public string DateCreated
        {
            get
            {
               return  CreatedDate.Value.ToString("dd MMM yyyy, HH:mm");
            }
        }

        public string TimeCreated
        {
            get
            {
                return CreatedDate.Value.ToString("hh:mm tt");
            }
        }
    }
}
