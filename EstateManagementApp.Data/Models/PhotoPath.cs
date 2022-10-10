using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EstateManagementApp.Data.Models
{
    public class PhotoPath
    {
        public int Id { get; set; }

        [Required]
        public string FotoPath { get; set; }
     

        //foreign table

        [Required]
        public Building Building { get; set; }
    }
}
