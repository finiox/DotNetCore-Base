// <copyright file="ExampleSummary.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Common.Areas.Example.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Common.Areas.Shared.Models;

    public class ExampleSummary : BaseSummary
    {
        public string Label { get; set; }

        public string LabelUppercase => this.Label.ToUpper();
    }
}
