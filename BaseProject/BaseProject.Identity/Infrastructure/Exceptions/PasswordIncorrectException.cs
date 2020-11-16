// <copyright file="PasswordIncorrectException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    public class PasswordIncorrectException : Exception
    {
        public PasswordIncorrectException()
        {
        }

        public PasswordIncorrectException(string message)
            : base(message)
        {
        }

        public PasswordIncorrectException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PasswordIncorrectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
