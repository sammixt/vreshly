using System;
using System.Runtime.Serialization;

namespace BLL.Entities
{
    public enum MessageType
    {
        [EnumMember(Value = "Inbox")]
        Inbox,

        [EnumMember(Value = "Sent Item")]
        Sent,

        [EnumMember(Value = "Draft")]
        Draft,

        [EnumMember(Value = "Sent Item")]
        Trash

    }
}
