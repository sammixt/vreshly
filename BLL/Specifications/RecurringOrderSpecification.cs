using System;
using BLL.Entities;
using BLL.Entities.OrderAggregate;

namespace BLL.Specifications
{
    public class RecurringOrderSpecification : BaseSpecification<RecurringOrder>
    {
        //get by Id
        //Get By UserEmail
        //Get By Frequency
        //Get By NextDelivery Date
        public RecurringOrderSpecification()
        {
            AddInclude(x => x.Product);
        }

        public RecurringOrderSpecification(long id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Product);
        }

        public RecurringOrderSpecification(string email)
            : base(x => x.UserEmail == email)
        {
            AddInclude(x => x.Product);
        }

        public RecurringOrderSpecification(RecurringFrequency frequency)
            : base(x => x.Frequency == frequency)
        {
            AddInclude(x => x.Product);
        }
        //True use NextDelivery, false use Created Date
        public RecurringOrderSpecification(DateTime delivery, bool dateType)
            : base(x => (dateType)? x.NextDelievery.Value == delivery : x.CreatedDate.Value == delivery)
        {
            AddInclude(x => x.Product);
        }


        
    }
}
