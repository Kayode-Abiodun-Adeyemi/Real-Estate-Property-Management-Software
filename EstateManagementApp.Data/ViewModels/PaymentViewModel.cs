using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EstateManagementApp.Data.ViewModels
{
    public class PaymentViewModel
    {
        [Required]
        [Display(Name = "Monthly Rent")]
        [Column(TypeName = "decimal(18,4)")]
        public Decimal RentPerMonth { get; set; }

        [Required]
        [Display(Name = "Enter Your Payment")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal PaymentFee { get; set; }
    }
}
