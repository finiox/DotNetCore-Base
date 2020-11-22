// <copyright file="ReadFileModel.cs" company="PlaceholderCompany">
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

    public class ReadFileModel : IDisposable
    {
        public Stream FileStream { get; init; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        public void Dispose()
        {
            FileStream?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
