// <copyright file="IdentityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IdentityConfiguration
    {
        public JwtConfiguration Jwt { get; set; }

        public AccountConfiguration Account { get; set; }
    }
}
