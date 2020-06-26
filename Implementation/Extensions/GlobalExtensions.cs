using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Implementation.Errors;
using Application.DTO.Pagination;
using Domain.Entities;
using System.Runtime.CompilerServices;

namespace Implementation.Extensions
{
    public static class GlobalExtensions
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
