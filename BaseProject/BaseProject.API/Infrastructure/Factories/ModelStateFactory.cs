// <copyright file="ModelStateFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.API.Infrastructure.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using BaseProject.API.Shared.ViewModels;
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

            return new BadRequestObjectResult(ErrorViewModel.MODEL_INVALID.Errors = errors);
        }

        private static string FormatErrorMessage(string errorMessage) =>
            errorMessage
                .ToLower()
                .TrimEnd('.')
                .Trim();
    }
}
