// <copyright file="AccountConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AccountConfiguration
    {
        public int PinCodeLength { get; set; }

        public int PinCodeRetries { get; set; }

        public int LockoutTimeInHours { get; set; }
    }
}
