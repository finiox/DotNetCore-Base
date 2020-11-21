// <copyright file="ErrorViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.API.Shared.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ErrorViewModel
    {
        public static ErrorViewModel UNHANDLED_EXCEPTION =>
            new ErrorViewModel()
            {
                ErrorKey = "unhandled_exception_thrown",
                Message = "An unhandled exception was thrown, the request could not succeed."
            };

        public static ErrorViewModel MODEL_INVALID =>
            new ErrorViewModel()
            {
                ErrorKey = "error_validating",
                Message = "The model was not valid, see errors for more details."
            };

        public static ErrorViewModel NOT_FOUND =>
            new ErrorViewModel()
            {
                ErrorKey = "not_found",
                Message = "The requested item was not found."
            };

        public string Message { get; set; }

        public string ErrorKey { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }
    }
}
