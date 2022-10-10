using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EstateManagementApp.Data.ViewModels
{
    public class EmailViewModel2
    {
      
        public int id { get; set; }
       
        public string LandlordEmailAddress { get; set; }
       
        public string Landlordfullname { get; set; }
        public DateTime StartAvailabilityDate { get; set; }
        public DateTime EndAvailabilityDate { get; set; }
        public string TenantEmail { get; set; }
        public string PropertyAddress { get; set; }

        [Required]
        public DateTime CustomerChosenInspectionDate { get; set; }

    }
}
