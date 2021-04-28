using System;
namespace com.vreshly.Dtos
{
    public class SubCategoryDto
    {
        public SubCategoryDto()
        {
        }
        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string SubCategoryName { get; set; }

        public string Category { get; set; }
        public long CategoryId { get; set; }
    }
}
