using System;
using System.Runtime.Serialization;

namespace BLL.Entities.OrderAggregate
{
    public enum RecurringFrequency
    {
        [EnumMember(Value = "Daily")]
        Daily = 1,


        [EnumMember(Value = "Weekly")]
        Weekly,

        [EnumMember(Value = "Bi-Weekly")]
        BiWeekly,

        [EnumMember(Value = "Monthly")]
        Monthly,

        [EnumMember(Value = "Bi-Monthly")]
        BiMonthly,

        [EnumMember(Value = "Quarterly")]
        Quaterly,

        [EnumMember(Value = "Others")]
        Others
    }
}
