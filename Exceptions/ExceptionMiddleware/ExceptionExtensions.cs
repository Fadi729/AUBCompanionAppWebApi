using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CompanionApp.Exceptions.ExceptionMiddlewareNS
{
    public static class ExceptionExtensions
    {
        public static int                      GetErrorCode                         (this Exception exception)
        {
            var exceptionType = exception.GetType().GetProperty("ErrorCode");

            if (exceptionType is not null)
                return (int)exceptionType.GetValue(exception)!;
            
            return
                -1;
        }
        public static ValidationProblemDetails ToValidationExceptionDetails         (this ValidationException exception)
        {
            ValidationProblemDetails error = new()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Status = (int)HttpStatusCode.BadRequest
            };
            foreach (ValidationFailure validationFailure in exception.Errors)
            {
                if (error.Errors.ContainsKey(validationFailure.PropertyName))
                {
                    error.Errors[validationFailure.PropertyName] = error.Errors[validationFailure.PropertyName]
                        .Concat(new[] { validationFailure.ErrorMessage })
                        .ToArray();
                    continue;
                }
                error.Errors.Add(
                    new KeyValuePair<string, string[]>(validationFailure.PropertyName, new[] { validationFailure.ErrorMessage }));
            }
            return error;
        }
        public static ValidationProblemDetails ToConflictExceptionDetails           (this Exception exception)
        {
            ValidationProblemDetails error =  new()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                Status = (int)HttpStatusCode.Conflict,
                Title = HttpStatusCode.Conflict.ToString()
            };
            error.Errors.Add(new KeyValuePair<string, string[]>(HttpStatusCode.Conflict.ToString(),new[] { exception.Message }));
            return error;
        }
        public static ValidationProblemDetails ToNotFoundExceptionDetails           (this Exception exception)
        {
            ValidationProblemDetails error =  new()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                Status = (int)HttpStatusCode.NotFound,
                Title = HttpStatusCode.NotFound.ToString(),
                
            };
            error.Errors.Add(new KeyValuePair<string, string[]>(HttpStatusCode.NotFound.ToString(),new[] { exception.Message }));
            return error;
        }
        public static ValidationProblemDetails ToUnauthorizedRequestExceptionDetails(this Exception exception)
        {
            ValidationProblemDetails error = new()
            {
                Type   = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
                Status = (int)HttpStatusCode.Unauthorized,
                Title = HttpStatusCode.Unauthorized.ToString()
            };
            error.Errors.Add(new KeyValuePair<string, string[]>(HttpStatusCode.Unauthorized.ToString(), new[] { exception.Message }));
            return error;
        }

        public static ValidationProblemDetails ToAuthExceptionDetails(this AuthException exception)
        {
            ValidationProblemDetails error = new();
            switch (exception.ErrorCode)
            {
                case (int)HttpStatusCode.Conflict:
                    error.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
                    break;
                case (int)HttpStatusCode.Unauthorized:
                    error.Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
                    break;
                case (int)HttpStatusCode.BadRequest:
                    error.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                    break;
            }

            error.Title = ((HttpStatusCode) exception.ErrorCode).ToString();
            error.Status = exception.ErrorCode;
            error.Errors.Add(new KeyValuePair<string, string[]>(((HttpStatusCode) exception.ErrorCode).ToString(), exception.ErrorMessages.ToArray()));
            
            return error;
        }
    }
}