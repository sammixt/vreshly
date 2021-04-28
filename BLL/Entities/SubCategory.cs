namespace BLL.Entities
{
    public class SubCategory : BaseEntity
    {
        public SubCategory()
        {

        }

        public string SubCategoryName { get; set; }

        public Category Category { get; set; }
        public long CategoryId { get; set; }
    }
}