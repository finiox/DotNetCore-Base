// <copyright file="JwtLoginRequestModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Authentication.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class JwtLoginRequestModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
