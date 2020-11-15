// <copyright file="GetAllViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.API.Areas.Example.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseProject.Common.Areas.Example.Models;

    public class GetAllViewModel
    {
        public IList<ExampleDto> ExampleEntities { get; set; }
    }
}
