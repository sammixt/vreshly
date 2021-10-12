using System;
namespace com.vreshly.Dtos
{
    public class RecurringOrderDto
    {
        public RecurringOrderDto()
        {
        }
        public long Id { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }

        public Nullable<DateTime> UpdateDate { get; set; }

        public string UserEmail { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public long ProductId { get; set; }

        public ProductDto Product { get; set; }

        public string Frequency { get; set; }

        public int InputFrequency { get; set; }

        public Nullable<DateTime> NextDelievery { get; set; }

        public Nullable<DateTime> PreviousDeliveryDate { get; set; }

        public int DateDifference
        {
            get
            {

                return (NextDelievery.HasValue) ? (NextDelievery.Value - DateTime.Now).Days : 0;
            }
        }

        public string NextDeliveryDateString
        {
            get
            {
                return (NextDelievery.HasValue) ? NextDelievery.Value.ToShortDateString() : "";
            }
        }

        public string PreviousDeliveryDateString
        {
            get
            {
                return (PreviousDeliveryDate.HasValue) ? PreviousDeliveryDate.Value.ToShortDateString() : "";
            }
        }

        public int DateCreatedToDate
        {
            get
            {
                return (CreatedDate.HasValue) ? (DateTime.Now - CreatedDate.Value).Days : 0;
            }
        }
    }
}
