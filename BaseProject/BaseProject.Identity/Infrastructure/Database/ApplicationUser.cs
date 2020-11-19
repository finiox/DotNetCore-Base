// <copyright file="ApplicationUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public string PinCode { get; set; }

        public int PinCodeAttempts { get; set; }

        public DateTime? PinCodeGeneration { get; set; }
    }
}
