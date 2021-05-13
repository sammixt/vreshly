using System;
namespace com.vreshly.Dtos
{
    public class VariableDetailDto
    {
        public VariableDetailDto()
        {
        }

        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

        public string Detail { get; set; }
        public string Variable { get; set; }
        public long VariableId { get; set; }
    }
}
