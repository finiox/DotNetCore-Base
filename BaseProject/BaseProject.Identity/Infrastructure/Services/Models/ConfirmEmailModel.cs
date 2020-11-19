// <copyright file="ConfirmEmailModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ConfirmEmailModel
    {
        public string Email { get; set; }

        public string Pin { get; set; }
    }
}
