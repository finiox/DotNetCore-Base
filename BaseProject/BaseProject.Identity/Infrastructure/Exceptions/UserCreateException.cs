// <copyright file="UserCreateException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    public class UserCreateException : Exception
    {
        public UserCreateException()
        {
        }

        public UserCreateException(string message)
            : base(message)
        {
        }

        public UserCreateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserCreateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
