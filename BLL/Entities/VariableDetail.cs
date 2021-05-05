using System;
namespace BLL.Entities
{
    public class VariableDetail : BaseEntity
    {
        public VariableDetail()
        {
        }

        public string Detail { get; set; }
        public Variable Variable { get; set; }
        public long VariableId { get; set; }
    }
}
