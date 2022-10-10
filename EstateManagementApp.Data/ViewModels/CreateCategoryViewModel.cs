using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EstateManagementApp.Data.ViewModels
{
    public class CreateCategoryViewModel
    {

        [Required]

        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Photo Path")]
        public IFormFile Photo { get; set; }

        public int Id { get; set; }
    }
}
