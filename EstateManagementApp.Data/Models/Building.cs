using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EstateManagementApp.Data.Models
{
    public class Building
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Monthly Rent / Purchase Amount (NGN)")]

        [Column(TypeName = "decimal(18,2)")]



        public Decimal RentPerMonth { get; set; }
        [Required]

        [Display(Name = "Buy or Sell ?")]

        public string BuyorSale { get; set; }

        [Display(Name = "Property Address")]
        public string PropertyAddress { get; set; }
        public string IsPetAllowed { get; set; }
        public string HasSwimmingPool { get; set; }
        public string HasGarage { get; set; }

        [Required]
        [Display(Name = "Start Date for LandLord's Inspection Availability:")]
        public DateTime StartAvailabilityDate { get; set; }
        [Required]
        [Display(Name = "End Date for LandLord's Inspection Availability:")]
        public DateTime EndAvailabilityDate { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "LandLord Email Address")]
        public string LandLordEmailAddress { get; set; }

        [Required]
        [Display(Name = "LandLord Full Name")]
        public string LandLordFullName { get; set; }

        [Required]
        public string PhotoPath { get; set; }

        public int CategoryId { get; set; }

        //Foreign Tables

        [Required]
        public Category Category { get; set; }

    }
}
