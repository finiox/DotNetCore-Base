// <copyright file="FileReadException.cs" company="PlaceholderCompany">
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

    public class FileReadException : Exception
    {
        public FileReadException()
        {
        }

        public FileReadException(string? message, string? fileName)
            : base(message)
        {
            FileName = fileName;
        }

        public FileReadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected FileReadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string? FileName { get; init; }
    }
}
