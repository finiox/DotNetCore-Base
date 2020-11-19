// <copyright file="ModelStateDictionaryExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.API.Infrastructure.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public static class ModelStateFactory
    {
        public static BadRequestObjectResult InvalidResponse(ActionContext actionContext)
        {
            var errors = actionContext.ModelState
                .Where(i => i.Value.Errors.Any())
                .ToDictionary(
                    key => key.Key,
                    value => value.Value.Errors
                        .Select(i => FormatErrorMessage(i.ErrorMessage))
                        .ToArray());

            return new BadRequestObjectResult(
                new ErrorModel()
                {
                    Errors = errors
                });
        }

        private static string FormatErrorMessage(string errorMessage)
        {
            var regex = new Regex("[_]{2,}", RegexOptions.None);
            return regex.Replace(
                errorMessage
                    .ToLower()
                    .TrimEnd('.')
                    .Trim()
                    .Replace(' ', '_'),
                "_");
        }

        public struct ErrorModel
        {
            public Dictionary<string, string[]> Errors { get; set; }
        }
    }
}
