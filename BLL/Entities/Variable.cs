using System;
using System.Collections.Generic;
namespace BLL.Entities
{
    public class Variable : BaseEntity
    {
        public Variable()
        {
        }

        public string VariableName { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public List<VariableDetail> VariableDetails { get; set; }
    }
}
