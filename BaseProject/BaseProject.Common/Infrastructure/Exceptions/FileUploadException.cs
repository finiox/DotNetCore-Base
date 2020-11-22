// <copyright file="FileUploadException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Common.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Common.Infrastructure.Files.Models;

    public class FileUploadException : Exception
    {
        public FileUploadException()
        {
        }

        public FileUploadException(UploadFileModel model)
        {
            UploadModel = model;
        }

        public FileUploadException(UploadFileModel model, Exception innerException)
            : base(null, innerException)
        {
            UploadModel = model;
        }

        public FileUploadException(string message)
            : base(message)
        {
        }

        public FileUploadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected FileUploadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public UploadFileModel UploadModel { get; init; }

        public bool HasModel => UploadModel != null;
    }
}
