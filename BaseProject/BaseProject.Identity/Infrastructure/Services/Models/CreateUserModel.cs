// <copyright file="CreateUserModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateUserModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
