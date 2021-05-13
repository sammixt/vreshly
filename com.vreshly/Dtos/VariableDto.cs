using System;
using System.Collections.Generic;
namespace com.vreshly.Dtos
{
    public class VariableDto
    {
        public VariableDto()
        {
        }
        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

        public string VariableName { get; set; }
        public string Product { get; set; }
        public long ProductId { get; set; }
        public List<VariableDetailDto> VariableDetails { get; set; }
    }
}
