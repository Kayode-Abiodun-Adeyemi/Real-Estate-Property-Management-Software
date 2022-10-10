using EstateManagementApp.Data.Models;
using EstateManagementApp.Data.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateManagementApp.Services.Repositories
{
    public class AddCategoryRepository : IAddCategoryRepository
    {
        public readonly AppDbContext context;
        public AddCategoryRepository(AppDbContext context)
        {
            this.context = context;
        }

        // public async Task<Category> UpdateCategory(Category NewModel)

        public Category UpdateCategory(Category NewModel)
        {
            Category OldRecord = context.Categories.FirstOrDefault(a => a.Id == NewModel.Id);
            OldRecord.CategoryName = NewModel.CategoryName;
            OldRecord.CategoryPhotoPath = NewModel.CategoryPhotoPath;
            context.Categories.Update(OldRecord);
            context.SaveChanges();

            return NewModel;

            
        }

        public async Task<bool> CreateCategory(Category categoryName)
        {
           //var _category = context.Categories.FirstOrDefault(a => a.CategoryName == categoryName.CategoryName);
          
          
                context.Categories.Add(categoryName);
            
            int result = await context.SaveChangesAsync();
            if(result > 0)
            return true;
            return false;
        }

        public IEnumerable<Category> ListofCategories()
        {
            return context.Categories.ToList();
        }

        public Category SearchCategoryByName(CreateCategoryViewModel model)
        {
            Category result = context.Categories.FirstOrDefault(a => a.CategoryName == model.CategoryName);
            return result;
        }

        public Category SearchCategoryById(int Id)
        {
            Category result = context.Categories.FirstOrDefault(a => a.Id == Id);

            return result;
        }

        public Category EditCategory(int id)
        {
            Category result = context.Categories.FirstOrDefault(a => a.Id == id);
            return result;
        }

        public bool DeleteCategory(int id)
        {
            Category category = context.Categories.FirstOrDefault(a => a.Id == id);

            if(category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                return true;
            }
            
            return false;
        }

        public IEnumerable<Building> ViewCategory(int id)
        {
           
            IEnumerable<Building> buildings = context.Buildings.ToList();

            return buildings;
        }


    }
}
