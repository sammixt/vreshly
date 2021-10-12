using System;
using BLL.Entities;

namespace BLL.Specifications
{
    public class AppRedisSpecification : BaseSpecification<AppRedis>
    {
        public AppRedisSpecification()
        {
        }

        public AppRedisSpecification(string key)
            : base(x => x.Id.Trim() == key)
        {

        }
    }
}
