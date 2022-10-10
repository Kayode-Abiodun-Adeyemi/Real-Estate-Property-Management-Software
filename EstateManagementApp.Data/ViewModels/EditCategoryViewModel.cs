using EstateManagementApp.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EstateManagementApp.Data.ViewModels
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        [Required]

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Photo Path")]
        public IFormFile Photo { get; set; }
    }
}
