using EstateManagementApp.Data.Models;
using EstateManagementApp.Data.ViewModels;
using EstateManagementApp.Services.Repositories;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;

namespace EstateManagementApp.Controllers
{
    public class HomeController : Controller
    {
        //        private readonly ILogger<HomeController> _logger;
        //   private IEnumerable<Category> categories { get; set; }
        private readonly IAddCategoryRepository AddCategoryRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IBuildingRepository buildingRepository;
        public HomeController(
                               IAddCategoryRepository AddCategoryRepository,
                               IWebHostEnvironment hostingEnvironment,
                               IBuildingRepository buildingRepository)
        {

            this.AddCategoryRepository = AddCategoryRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.buildingRepository = buildingRepository;

        }



        [HttpGet]
        public IActionResult Index()
        {
            var model = this.AddCategoryRepository.ListofCategories();

            return View(model);
        }

        [HttpGet]
        public IActionResult EditCategory(int Id)
        {
            var model = this.AddCategoryRepository.EditCategory(Id);

            var result = new EditCategoryViewModel()
            {
                Id = model.Id,
                CategoryName = model.CategoryName
            };

            return View(result);
        }

        [HttpPost]
        public IActionResult EditCategory(EditCategoryViewModel editCategoryViewModel)
        {
            if (editCategoryViewModel.CategoryName == null)
            {
                ModelState.AddModelError("", "Category Name is Required");
                return View(editCategoryViewModel);
            }

            if (editCategoryViewModel.Photo == null)
            {
                ModelState.AddModelError("", "Photo is Required");
                return View(editCategoryViewModel);
            }

            var _categoryName = new CreateCategoryViewModel()
            {
                CategoryName = editCategoryViewModel.CategoryName
            };

            var EditCategoryModel = AddCategoryRepository.SearchCategoryByName(_categoryName);

            if (EditCategoryModel != null)
            {
                ModelState.AddModelError("", "Category Name already exists");
                return View(editCategoryViewModel);
            }

            if (ModelState.IsValid)
            {
                //Category OldRecordinTable = AddCategoryRepository.SearchCategoryById(editCategoryViewModel.Id);
                // var model = this.AddCategoryRepository.EditCategory(editCategoryViewModel.Id);

                var model1 = new CreateCategoryViewModel()
                {
                    Photo = editCategoryViewModel.Photo
                };
                string NewPath = ProcessUploadedFile(model1);

                var model = new Category()
                {
                    CategoryName = editCategoryViewModel.CategoryName,
                    //CategoryPhotoPath = OldRecordinTable.CategoryPhotoPath,
                    CategoryPhotoPath = NewPath,
                    Id = editCategoryViewModel.Id
                };

                var UpdateModel = AddCategoryRepository.UpdateCategory(model);

                return RedirectToAction("Index", "Home");
            }

            return View(editCategoryViewModel);
        }

        private string ProcessUploadedFile(CreateCategoryViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo.FileName != null)
            {
                string folderPath = Path.Combine(hostingEnvironment.WebRootPath, "CategoryImages");
                uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
            //}
        }

        [HttpGet]
        public IActionResult DeleteCategory(int Id)
        {
            bool model = this.AddCategoryRepository.DeleteCategory(Id);

            //if(model)
            //{
            //    ModelState.AddModelError("", "Record not Found");
            //    return View(model);
            //}

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult ViewCategory(int Id)
        {
            IEnumerable<Building> model = this.AddCategoryRepository.ViewCategory(Id);

            //if(model)
            //{
            //    ModelState.AddModelError("", "Record not Found");
            return View(model);


            // return RedirectToAction("ViewCategory", "Home", "model");

        }


        [HttpGet]
        public IActionResult ViewProperty(int id)
        {
            IEnumerable<Building> building = this.buildingRepository.ViewBuilding(id);
            return View(building);
        }

        [HttpGet]
        public IActionResult ViewPropertyById(int Id, string name)
        {
            Building model3 = this.buildingRepository.ViewSpecificBuildingg(Id, name);
            return View(model3);
        }



        [HttpGet]
        public IActionResult ViewEmail(SendEmailVM model)
        {
            var EmailViewModel = new EmailViewModel()
            {
                Landlordfullname = model.LandLordFullName,
                LandlordEmailAddress = model.LandLordEmailAddress,
                StartAvailabilityDate = model.StartAvailabilityDate,
                EndAvailabilityDate = model.EndAvailabilityDate,
                id = model.Id,
                TenantEmail = model.TenantEmail,
                PropertyAddress = model.PropertyAddress,
                RentPerMonth = model.Fee,
                Commission = (model.Fee * 10) / 100

            };

            return View(EmailViewModel);
        }

        [HttpPost]
        public IActionResult ViewEmail(EmailViewModel model)

        {
            if (model.CustomerChosenInspectionDate == null)
            {
                ModelState.AddModelError("", "Customer Data Availability is required");
                return View("SendEmail", model);
            }

            if (model.CustomerChosenInspectionDate >= model.StartAvailabilityDate && model.CustomerChosenInspectionDate <= model.EndAvailabilityDate)
            {
                string htmlMessage = "<!DOCTYPE html>" +
                                  "<html>" +
                                  "<body style = \"background-color:blue; text-align:center \">" +
                                  "<p> Dear  " + model.Landlordfullname + "," +
                                   "<br/>" + " " + "</p>" +

                                  "<p>A prospective Tenant / Buyer with the email - " + model.TenantEmail + ", has shown interest in your property situated at:   " +
                                   model.PropertyAddress + "." + "</p>" +
                                  "<p>The proposed time of inspection of the said property is:  " + model.CustomerChosenInspectionDate +
                                  "<p> Please kindly make yourself available.</p>" +
                                  "<p> Thank you.</p>" +
                                  "<p> Management</p>" +
                                  "<p> Dominion Akindipe Estate Agency</p>" +
                                  "</html>";
                var Message = new MimeMessage();
                Message.From.Add(new MailboxAddress("Estate Management", "sddpcoursehallam2022@gmail.com"));
                Message.To.Add(new MailboxAddress(model.Landlordfullname, model.LandlordEmailAddress));
                Message.Subject = "Notification of Customer Viewing / Inspection Visit Request";
                //Message.Body = new TextPart("plain")
                //{
                //    Text = "Hello World Testing"
                //};
                Message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = htmlMessage
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("sddpcoursehallam2022@gmail.com", "mhxnftvnaqvrgqwy");
                    client.Send(Message);
                    client.Disconnect(true);
                }

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", $"Date must be within the LandLord's availability Date of {model.StartAvailabilityDate } to {model.EndAvailabilityDate }");
            
            var models = new EmailViewModel2()
            {
                EndAvailabilityDate = model.EndAvailabilityDate,
                LandlordEmailAddress = model.LandlordEmailAddress,
                Landlordfullname = model.Landlordfullname,
                PropertyAddress = model.PropertyAddress,
                StartAvailabilityDate = model.StartAvailabilityDate,
                TenantEmail = model.TenantEmail,
                CustomerChosenInspectionDate = model.CustomerChosenInspectionDate
            };
            return View("~/Views/Checkout/Create.cshtml", models);
        }

        [HttpGet]
        public IActionResult MakePayment(EmailViewModel model)
        {
            if (model.RentPerMonth.Equals(null))
            {

                ModelState.AddModelError("", "Payment is required");
                return View("~/Views/Checkout/Create.cshtml", model);
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("MakePayment")]
        public IActionResult MakePaymentPost(EmailViewModel2 model)
        {
            if (model.CustomerChosenInspectionDate.Equals(null))
            {
                ModelState.AddModelError("", "Payment is required");
                return View(model);
            }

            if (model.CustomerChosenInspectionDate >= model.StartAvailabilityDate && model.CustomerChosenInspectionDate <= model.EndAvailabilityDate)
            {
                //save payment in DB

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Date is Outside the LandLord's Availability");
            return View(model);
        }

        [HttpGet]

        public IActionResult Search(string idname)
        {

            IEnumerable<Building> ListofBuildings = this.buildingRepository.Search(idname);
            return View(ListofBuildings);
        }

        [HttpGet]

        public IActionResult MakeInspection()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CancelInspection()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }


    }

}