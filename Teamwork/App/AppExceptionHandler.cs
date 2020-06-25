using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using FluentValidation;
using Newtonsoft.Json;

namespace Teamwork.App
{
    public class AppExceptionHandler
    {
        private readonly RequestDelegate _next;

        public AppExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                object response = null;
                var statusCode = StatusCodes.Status500InternalServerError;

                switch (ex)
                {
                    case UnauthorizedCommandException unauthorized:
                        statusCode = StatusCodes.Status403Forbidden;
                        response = new
                        {
                            message = "Forbidden. You are not allowed to execute this command."
                        };
                        break;
                    case EntityNotFoundException _:
                        statusCode = StatusCodes.Status404NotFound;
                        response = new
                        {
                            message = "Resource not found."
                        };
                        break;
                    case ValidationException validationException:
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        response = new
                        {
                            message = "Validation errors triggered.",
                            errors = validationException.Errors.Select(x => new
                            {
                                x.PropertyName,
                                x.ErrorMessage
                            })
                        };
                        break;
                }

                httpContext.Response.StatusCode = statusCode;

                if (response != null)
                {
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    return;
                }

                await Task.FromResult(httpContext.Response);
            }
        }
    }
}
