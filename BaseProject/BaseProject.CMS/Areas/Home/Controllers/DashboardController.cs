﻿// <copyright file="DashboardController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.CMS.Areas.Home.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Home")]
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index() => View();
    }
}
