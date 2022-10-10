using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EstateManagementApp.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Category Photo Path")]
        public string CategoryPhotoPath { get; set; }

        //foreign tables
        public ICollection<Building> Buildings { get; set; } 
    }
}
