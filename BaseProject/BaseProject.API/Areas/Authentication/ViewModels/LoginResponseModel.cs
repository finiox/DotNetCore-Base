// <copyright file="LoginResponseModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.API.Areas.Authentication.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LoginResponseModel
    {
        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
