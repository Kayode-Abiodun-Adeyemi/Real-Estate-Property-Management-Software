using EstateManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateManagementApp.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(string StripeEmail, string StripeToken, EmailViewModel model)
        {
            var Customers = new CustomerService();
            var Charges = new ChargeService();

            var customer = Customers.Create(new CustomerCreateOptions
            {
                Email = StripeEmail,
                Source = StripeToken
            });

            var charge = Charges.Create(new ChargeCreateOptions
            {
                Amount = (long)model.Commission,
                Description = "Commission Fee",
                 Currency= "NGN",
                 Customer = customer.Id

            });

            if(charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                EmailViewModel2 result = new EmailViewModel2()
                {
                    StartAvailabilityDate  = model.StartAvailabilityDate,
                    EndAvailabilityDate = model.EndAvailabilityDate,
                    LandlordEmailAddress  = model.LandlordEmailAddress,
                    Landlordfullname = model.Landlordfullname,
                    CustomerChosenInspectionDate = DateTime.Now,
                    PropertyAddress = model.PropertyAddress,
                    TenantEmail = model.TenantEmail

                };
                return View(result);
            }

          
            return View();


        }

    }
}