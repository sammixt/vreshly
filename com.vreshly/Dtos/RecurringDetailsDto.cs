using System;
using System.Collections.Generic;

namespace com.vreshly.Dtos
{
    public class RecurringDetailsDto
    {
        public RecurringDetailsDto()
        {
            Orders = new List<RecurringOrderDto>();
        }

        public CustomersDto CustomerInformation { get; set; }

        public IEnumerable<RecurringOrderDto> Orders { get; set; }
    }
}
