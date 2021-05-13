using System;
namespace com.vreshly.Dtos
{
    public class RoleDto
    {
        public RoleDto()
        {
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        
    }
}
