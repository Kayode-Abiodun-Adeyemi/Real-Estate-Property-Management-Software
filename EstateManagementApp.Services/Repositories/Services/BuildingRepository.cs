using EstateManagementApp.Data.Models;
using EstateManagementApp.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateManagementApp.Services.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        public readonly AppDbContext context;
        public BuildingRepository(AppDbContext context)
        {
            this.context = context;
        }

        public bool AddBuilding(Building model)
        {
          
            this.context.Buildings.Add(model);
            int result = context.SaveChanges();
            if (result > 0)
                return true;
            return false;
        }

        public void DeleteBuilding(int Id)
        {
            Building model = this.context.Buildings.FirstOrDefault(b => b.Id == Id);

            if (model != null)
            {
                this.context.Buildings.Remove(model);
                int result = context.SaveChanges();         
            }
        }

        
        public IEnumerable<Building> ViewBuilding(int id)
        {
         //  Category CategoryModel= this.context.Categories.FirstOrDefault(b => b.Id == Id);
            IEnumerable<Building> Buildingmodel = this.context.Buildings.Where(m => m.CategoryId == id);

           
            return Buildingmodel;

        }

        public Building ViewSpecificBuildingg(int id, string name)
        {
            //  Category CategoryModel= this.context.Categories.FirstOrDefault(b => b.Id == Id);
            Building Buildingmodel = context.Buildings.FirstOrDefault(m => m.Id == id && m.PhotoPath == name);


            return Buildingmodel;

        }

        public  IEnumerable<Building> Search(string BuildingType)
        {
            var ListofBuildings = context.Buildings.Where(x => x.BuyorSale == BuildingType);
            return ListofBuildings;
        }
    }
}
