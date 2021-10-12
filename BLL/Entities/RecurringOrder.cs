using System;
using BLL.Entities.OrderAggregate;

namespace BLL.Entities
{
    public class RecurringOrder : BaseEntity
    {
        public RecurringOrder()
        {
        }

        public string UserEmail { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public RecurringFrequency Frequency { get; set; } = RecurringFrequency.Others;

        public Nullable<DateTime> NextDelievery { get; set; }

        public Nullable<DateTime> PreviousDeliveryDate { get; set; }
    }
}
