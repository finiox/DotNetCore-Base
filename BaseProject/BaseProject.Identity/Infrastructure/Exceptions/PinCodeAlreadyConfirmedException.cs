// <copyright file="PinCodeAlreadyConfirmedException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    public class PinCodeAlreadyConfirmedException : Exception
    {
        public PinCodeAlreadyConfirmedException()
        {
        }

        public PinCodeAlreadyConfirmedException(string message)
            : base(message)
        {
        }

        public PinCodeAlreadyConfirmedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PinCodeAlreadyConfirmedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
