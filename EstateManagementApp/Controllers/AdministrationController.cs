using EstateManagementApp.Data.Models;
using EstateManagementApp.Services.Repositories;
using EstateManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace EstateManagementApp.Controllers
{

    [Authorize(Roles="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<AdministrationController> logger;
        private readonly IAddCategoryRepository AddCategoryRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AdministrationController> logger,
            IAddCategoryRepository AddCategoryRepository,
             IWebHostEnvironment hostingEnvironment)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
            this.AddCategoryRepository = AddCategoryRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole(model.RoleName);
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded) return RedirectToAction("ListRoles", "administration");

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rolewith Id = {Id} cannot be found";
                return View("NotFound");
            }

            EditRoleViewModel model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in await userManager.GetUsersInRoleAsync(role.Name))
            {
                model.Users.Add(user.UserName);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            IdentityRole role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded) return RedirectToAction("ListRoles");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users.ToList())
            {
                model.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = await userManager.IsInRoleAsync(user, role.Name)
                });

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else continue;

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1)) continue;
                    else return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }


        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel model)
        {

            if (!ModelState.IsValid) return View();

            Category _category = AddCategoryRepository.SearchCategoryByName(model);

            if (_category != null)
            {
                ModelState.AddModelError("", "Category Name already exists");
                return View();
            }

            string uniqueFileName = ProcessUploadedFile(model);
           Category newCategory = new Category()
            {
                CategoryName = model.CategoryName,
                 CategoryPhotoPath = uniqueFileName
              
            };
            await AddCategoryRepository.CreateCategory(newCategory);
            //   return RedirectToAction("Index", "Home", new { id = newCategory.Id });
            return RedirectToAction("Index", "Home");
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
        }

        [HttpPost]
        
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.Error = $"User with id {id} not found.";
                return View("NotFound");
            }

            try
            {
                //foreach(var user in userManager.Users)
                //{
                //    bool Result = await userManager.IsInRoleAsync(user, role.Name);
                //    if (Result)
                //    {
                //        ModelState.AddModelError(" ", "This role cannot be deleted as there are Users associated with it. You need to delete users associated with it first ");
                //        return View("ListRoles");
                //    }
                //}
               
                IdentityResult result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                return View("ListRoles");
            }
            catch (DbUpdateException ex)
            {
                logger.LogError($"Error deleting role {ex}");

                ViewBag.ErrorTitle = $"{role.Name} role is in use";
                ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users" +
                    $"in this role. If you want to delete this role, please remove the users from" +
                    $"the role and try to delete again.";
                return View("Error");
            }
        }




    }
}
