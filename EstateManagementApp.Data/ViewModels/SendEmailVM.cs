using System;
using System.Collections.Generic;
using System.Text;

namespace EstateManagementApp.Data.ViewModels
{
    public class SendEmailVM
    {
        public int Id { get; set; }
        public string LandLordEmailAddress { get; set; }
        public string LandLordFullName { get; set; }
        public DateTime StartAvailabilityDate { get; set; }
        public DateTime EndAvailabilityDate { get; set; }

        public string TenantEmail { get; set; }
        public string PropertyAddress { get; set; }
        public decimal Fee { get; set; }

    }
}
