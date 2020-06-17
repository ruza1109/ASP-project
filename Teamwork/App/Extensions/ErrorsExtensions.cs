using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamwork.App.Errors;

namespace Teamwork.App.Extensions
{
    public static class ErrorsExtensions
    {
        public static UnprocessableEntityObjectResult AsUnprocessableEntity(this ValidationResult validation)
        {
            var errors = validation.Errors.Select(e => new ClientError
            {
                Message = e.ErrorMessage,
                Property = e.PropertyName
            });

            return new UnprocessableEntityObjectResult(new
            {
                Errors = errors
            });
        }

    }
}
