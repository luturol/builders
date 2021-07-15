using System.Collections.Generic;
using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Builders.Extensions
{
    public static class ValidationResultExtension
    {
        public static ProblemDetails ToProblemDetails(this ValidationResult validation, HttpStatusCode status)
        {
            var problemDetails = new ProblemDetails()
            {
                Title = "Invalid parameters",
                Status = (int) status,
                Detail = "Invalid parameters was informed"
            };

            foreach(var error in validation.Errors)
            {
                problemDetails.Extensions.Add(error.PropertyName, error.ErrorMessage);
            }

            return problemDetails;
        }
    }
}