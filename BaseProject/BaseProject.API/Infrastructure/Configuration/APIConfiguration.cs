// <copyright file="APIConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.API.Infrastructure.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseProject.Common.Infrastructure.Configuration;

    public class APIConfiguration : AppConfiguration
    {
        public JwtConfiguration Jwt { get; set; }
    }
}
