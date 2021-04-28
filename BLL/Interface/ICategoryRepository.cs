using BLL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BLL.Interface
{

    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<List<Category>> GetCategoriesAsync();
    }
}