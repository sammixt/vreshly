using System;
namespace com.vreshly.Dtos
{
    public class CategoryDto
    {
        public CategoryDto()
        {
        }

        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string CategoryName { get; set; }
    }
}
