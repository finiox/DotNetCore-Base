// <copyright file="UploadFileModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Common.Infrastructure.Files.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UploadFileModel
    {
        public Stream FileStream { get; init; }

        public string FileName { get; set; }
    }
}
