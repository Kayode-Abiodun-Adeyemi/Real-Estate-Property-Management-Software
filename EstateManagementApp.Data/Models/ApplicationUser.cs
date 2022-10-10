﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateManagementApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Gender { get; set; }
    }
}
