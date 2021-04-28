using System;
namespace BLL.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
        }

        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

    }
}
