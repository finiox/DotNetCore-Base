// <copyright file="UserLockedOutException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    public class UserLockedOutException : Exception
    {
        public UserLockedOutException()
        {
        }

        public UserLockedOutException(string message)
            : base(message)
        {
        }

        public UserLockedOutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserLockedOutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
