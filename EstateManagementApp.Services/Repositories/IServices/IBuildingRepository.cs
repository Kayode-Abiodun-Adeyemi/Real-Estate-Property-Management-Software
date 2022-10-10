using EstateManagementApp.Data.Models;
using EstateManagementApp.Data.ViewModels;
using System.Collections.Generic;

namespace EstateManagementApp.Services.Repositories
{
    public interface IBuildingRepository
    {
        bool AddBuilding(Building model);
        void DeleteBuilding(int Id);
        IEnumerable<Building> ViewBuilding(int id);

        Building ViewSpecificBuildingg(int id, string name);

        IEnumerable<Building> Search(string BuildingType);


    }
}