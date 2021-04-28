using System;
namespace BLL.Entities
{
    public class Brand : BaseEntity
    {
        public Brand()
        {
        }

        public string BrandName { get; set; }

        public string BrandLogo { get; set; }
    }
}
