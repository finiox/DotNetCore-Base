// <copyright file="LoginPageModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.CMS.Areas.Account.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class LoginPageModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail adres")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Display(Name = "Onthoud mij?")]
        public bool RememberMe { get; set; }
    }
}
