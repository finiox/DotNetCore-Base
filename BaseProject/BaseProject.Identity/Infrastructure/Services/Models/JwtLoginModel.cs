// <copyright file="JwtLoginModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class JwtLoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
