// <copyright file="UserOverviewPageModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.CMS.Areas.Account.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseProject.Identity.Infrastructure.Database;

    public class UserOverviewPageModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
