using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Implementation.Errors;

namespace Implementation.Extensions
{
    public static class ErrorsExtensions
    {
        //public static UnprocessableEntityObjectResult AsUnprocessableEntity(this ValidationResult validation)
        //{
        //    var errors = validation.Errors.Select(e => new ClientError
        //    {
        //        Message = e.ErrorMessage,
        //        Property = e.PropertyName
        //    });

        //    return new UnprocessableEntityObjectResult(new
        //    {
        //        Errors = errors
        //    });
        //}

    }
}
