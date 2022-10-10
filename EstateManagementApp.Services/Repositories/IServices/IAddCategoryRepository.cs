using EstateManagementApp.Data.Models;
using EstateManagementApp.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstateManagementApp.Services.Repositories
{
    public interface IAddCategoryRepository
    {
        Task<bool> CreateCategory(Category categoryName);
        IEnumerable<Category> ListofCategories();
        Category SearchCategoryByName(CreateCategoryViewModel CategoryName);
         Category EditCategory(int id);
        bool DeleteCategory(int id);
        Category UpdateCategory(Category categoryName);

        Category SearchCategoryById(int Id);

        IEnumerable<Building> ViewCategory(int id);
    }
}