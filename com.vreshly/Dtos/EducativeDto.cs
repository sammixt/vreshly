using System;
namespace com.vreshly.Dtos
{
    public class EducativeDto : UploadImageDto
    {
        public EducativeDto()
        {
        }

        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string VideoLink { get; set; }
        public bool Status { get; set; }
        public string EducativeType { get; set; }
    }
}
