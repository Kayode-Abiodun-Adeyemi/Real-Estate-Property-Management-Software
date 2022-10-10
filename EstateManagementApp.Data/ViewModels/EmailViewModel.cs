using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EstateManagementApp.Data.ViewModels
{
    

    public class EmailViewModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        [Display(Name ="Landlord Email")]
        public string LandlordEmailAddress { get; set; }
        [Required]
        [Display(Name = "Landlord FullName")]
        public string Landlordfullname { get; set; }
        [Required]
        [Display(Name = "Inspection Start Date")]
        public DateTime StartAvailabilityDate { get; set; }
        [Required]
        [Display(Name = "Inspection End Date")]
        public DateTime EndAvailabilityDate { get; set; }

        [Required]
        [Display(Name = "Customer Availability Date")]
        public DateTime CustomerChosenInspectionDate { get; set; } = new DateTime();

        [Required]
        [Display(Name = "Monthly Rent")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal RentPerMonth { get; set; }

        [Required]
        [Display(Name = "Enter Your Payment")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentFee { get; set; }

       

        public string TenantEmail { get; set; }
        public string PropertyAddress { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Commission { get; set; }


    }
}
