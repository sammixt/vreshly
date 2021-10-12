using System;
using System.Runtime.Serialization;

namespace BLL.Entities
{
    public enum EducativeType
    {
        [EnumMember(Value = "About Us")]
        AboutUs,

        [EnumMember(Value = "Importance")]
        Importance
    }
}
