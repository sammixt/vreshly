using System;
namespace BLL.Entities
{
    public class AppRedis : BaseEntity
    {
        public AppRedis()
        {
        }

        
        public new string Id { get; set; }

        public string Value { get; set; }

        public DateTimeOffset Expiry { get; set; }
    }
}
