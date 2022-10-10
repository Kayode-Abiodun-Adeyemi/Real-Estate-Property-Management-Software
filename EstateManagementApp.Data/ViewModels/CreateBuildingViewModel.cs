using EstateManagementApp.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EstateManagementApp.Data.ViewModels
{
    public class CreateBuildingViewModel
    {
        [Required]
        [Display(Name = "Property Address")]
        public string PropertyAddress { get; set; }

        [Required]
        [Display(Name = "Monthly Rent / Purchase Amount (NGN)")]
        [Column(TypeName = "decimal(18,2)")]

        public Decimal RentPerMonth { get; set; }

        [Required]
        [Display(Name = "Buy / Sale ?")]
        public string ForBuyorSale { get; set; }

        [Display(Name = "Furnished?")]
        public string IsPetAllowed { get; set; }

        [Display(Name = "Does it has Wifi ?")]
        public string HasSwimmingPool { get; set; }

        [Display(Name = "Does it has a Parking Space?")]
        public string HasGarage { get; set; }
        [Display(Name = "Start Date for LandLord's Inspection Availability:")]
        public DateTime StartAvailabilityDate { get; set; }

        [Display(Name = "End Date for LandLord's Inspection Availability:")]
        public DateTime EndAvailabilityDate { get; set; }

        [Required]
        [Display(Name = "LandLord Email Address")]
        public string LandLordEmailAddress { get; set; }

        [Required]
        [Display(Name = "LandLord Full Name")]
        public string LandLordFullName { get; set; }

        public string Description { get; set; }

        [Required]
        public int CategoryName { get; set; }

        // public PhotoPath Photo { get; set; }
        [Display(Name = "Category Photo Path")]
        public IFormFile Photo { get; set; }
    }
}