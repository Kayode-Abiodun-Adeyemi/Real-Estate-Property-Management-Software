using EstateManagementApp.Data.Models;
using EstateManagementApp.Data.ViewModels;
using EstateManagementApp.Services.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EstateManagementApp.Controllers
{
    public class LandLordController : Controller
    {
        private readonly IBuildingRepository buildingRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IAddCategoryRepository AddCategoryRepository;

        public LandLordController(IBuildingRepository buildingRepository,
                                  IWebHostEnvironment hostingEnvironment,
                                  IAddCategoryRepository AddCategoryRepository)
        {
            this.buildingRepository = buildingRepository;
            this.AddCategoryRepository = AddCategoryRepository;
            this.hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = this.AddCategoryRepository.ListofCategories();
            ViewBag.listofCategories = model;
            return View();
        }

        [HttpPost]
        public IActionResult AddProperty(CreateBuildingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.StartAvailabilityDate >= model.EndAvailabilityDate)
                {
                    ModelState.AddModelError("", "Start Date cannot be greater or equal to End date");

                    var modelforcategory = this.AddCategoryRepository.ListofCategories();
                    ViewBag.listofCategories = modelforcategory;

                    

                    return View("Index", model);
                }

                string uniqueFileName = ProcessUploadedFile(model);

                Building newBuilding = new Building()
                {
                    BuyorSale = model.ForBuyorSale,
                    Description = model.Description,
                    HasGarage = model.HasGarage,
                    HasSwimmingPool = model.HasSwimmingPool,
                    IsPetAllowed = model.IsPetAllowed,
                    StartAvailabilityDate = model.StartAvailabilityDate,
                    EndAvailabilityDate = model.EndAvailabilityDate,
                    LandLordEmailAddress = model.LandLordEmailAddress,
                    LandLordFullName = model.LandLordFullName,
                    PropertyAddress = model.PropertyAddress,
                    RentPerMonth = model.RentPerMonth,
                    PhotoPath = uniqueFileName,
                    CategoryId = (int)model.CategoryName


                };

                //   model.Photo = uniqueFileName;
                bool result = this.buildingRepository.AddBuilding(newBuilding);
                ViewBag.Message = "Property Successfully listed";
                return RedirectToAction("Index", "LandLord");
            }

            //This populates the Category drop down
            var model1 = this.AddCategoryRepository.ListofCategories();
            ViewBag.listofCategories = model1;
            ////

            return View("Index", model);
        }

        private string ProcessUploadedFile(CreateBuildingViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo.FileName != null)
            {
                string folderPath = Path.Combine(hostingEnvironment.WebRootPath, "BuildingImages");
                uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        //[HttpGet("{Id}")]
        //public IActionResult DeleteProperty(int Id)
        //{
        //       this.buildingRepository.DeleteBuilding(Id);
        //        return RedirectToAction("ViewCategory", "Home");

        //}

        [HttpGet]
        public IActionResult SendEmail(int id)
        {
            return null;
        }

        [HttpGet]
        public IActionResult MakePayment(int id)
        {
            return null;
        }

        [HttpGet]
        public IActionResult DeleteBuilding(int id)
        {

            buildingRepository.DeleteBuilding(id);

            return RedirectToAction("Index", "Home");
        }


    }
}
