using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EstateManagementApp.Data.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
       

        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name ="Gender")]
        public string Gender { get; set; }
        [Required]
        [Display(Name ="Type of Customer")]
        public string TypeofCustomer { get; set; }
    }
}
