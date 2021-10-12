using System;
namespace BLL.Entities
{
    public class Educative : BaseEntity
    {
        public Educative()
        {
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string VideoLink { get; set; }
        public bool Status { get; set; }
        public EducativeType EducativeType { get; set; }
    }
}
