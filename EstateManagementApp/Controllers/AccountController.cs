using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using EstateManagementApp.Data.ViewModels;
using EstateManagementApp.Data.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EstateManagementApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.roleManager = roleManager;
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Gender = model.Gender, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var RoleChecker = await roleManager.FindByNameAsync(model.TypeofCustomer.ToString());
                    if (RoleChecker == null)
                    {
                        ViewBag.ErrorMessage = $"Role with Name ={model.TypeofCustomer} cannot be found. Admin has to first create the role";
                        return View("NotFound");
                    }
                    //Assign the user to the role
                    var response = await userManager.AddToRoleAsync(user, model.TypeofCustomer);

                    if (response.Succeeded)
                    {
                        //  await GenerateAndLogConfirmationLink(user);
                        return RedirectToAction("Login", "Account");

                    }
                    else
                    {
                        foreach (var error in response.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        //      return RedirectToAction("Register", "Account");

                    }


                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                //PrintModelErrors(result.Errors);
                //return RedirectToAction("Register", "Account");


            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(string returnUrl)
        //{
        //    LoginViewModel model = new LoginViewModel
        //    {
        //        ReturnUrl = returnUrl,
        //        ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        //    };

        //    return View(model);
        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //  model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
               var user = await userManager.FindByEmailAsync(model.Email);
                  
                // if (user != null && !user.EmailConfirmed &&
                if (user == null )
                {
                    ModelState.AddModelError("", "Email not registered yet");
                    return View(model);
                }
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false );

                //var result = await signInManager.PasswordSignInAsync(
                  //  model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                   
                                     if (await userManager.IsInRoleAsync(user,"Admin"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    //else if (signInManager.IsSignedIn(User) && User.IsInRole("Tenant"))
                    else if (await userManager.IsInRoleAsync(user, "Tenant"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                  //  else if (signInManager.IsSignedIn(User) && User.IsInRole("LandLord"))
                    else if (await userManager.IsInRoleAsync(user, "LandLord"))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }

               

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        [HttpGet]
       
        public IActionResult RegisterAdmin()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminViewModel model)
        {
            model.TypeofCustomer = "Admin";
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Gender = model.Gender, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var RoleChecker = await roleManager.FindByNameAsync(model.TypeofCustomer.ToString());
                    if (RoleChecker == null)
                    {
                        ViewBag.ErrorMessage = $"Role with Name ={model.TypeofCustomer} cannot be found. Admin has to first create the role";
                        return View("NotFound");
                    }
                    //Assign the user to the role
                    var response = await userManager.AddToRoleAsync(user, model.TypeofCustomer);

                    if (response.Succeeded)
                    {
                       
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        foreach (var error in response.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                      

                    }


                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
             

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                PrintModelErrors(result.Errors);
                return View();
            }

            await signInManager.RefreshSignInAsync(user);
            return View("ChangePasswordConfirmation");
        }

        private void PrintModelErrors(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }


    }
}
