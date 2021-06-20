using System;
namespace com.vreshly.Dtos
{
    public class NewsLetterDto
    {
        public NewsLetterDto()
        {
        }

        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string Email { get; set; }

        public string CreatedDateFormatted
        {
            get
            {
                return CreatedDate.Value.Date.ToLongDateString();
            }
        }

        public string SubscribingTime
        {
            get
            {
                var daysDifference = (DateTime.Now - CreatedDate.Value.Date).Days;
                return $"{daysDifference} day(s) ago";
            }
        }
    }
}
