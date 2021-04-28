using System;
namespace com.vreshly.Dtos
{
    public class BrandDto : UploadImageDto
    {
        public BrandDto()
        {
        }
        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string BrandName { get; set; }
        public string BrandLogo { get; set; }
    }
}
