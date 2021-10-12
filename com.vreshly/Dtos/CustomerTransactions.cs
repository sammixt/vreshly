using System;
using System.Collections.Generic;

namespace com.vreshly.Dtos
{
    public class CustomerTransactions
    {
        public CustomerTransactions()
        {
            orders = new List<OrderToReturnDto>();
            recurringOrder = new List<RecurringOrderDto>();
        }

        public List<OrderToReturnDto> orders { get; set; }

        public List<RecurringOrderDto> recurringOrder { get; set; }
    }
}
