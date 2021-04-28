using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Interface;
namespace BLL.Infrastructure.Data
{

    public class CategoryRepository : ICategoryRepository
    {
        public Task<List<Category>> GetCategoriesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

    }
}