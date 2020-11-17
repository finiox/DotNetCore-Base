// <copyright file="NotInRoleException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    public class NotInRoleException : Exception
    {
        public NotInRoleException()
        {
        }

        public NotInRoleException(string message)
            : base(message)
        {
        }

        public NotInRoleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NotInRoleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
