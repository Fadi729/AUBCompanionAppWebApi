﻿using System.Net;
using FluentValidation;

namespace CompanionApp.Exceptions.ExceptionMiddlewareNS
{
    public class ExceptionMiddleware
    {
        readonly RequestDelegate _request;

        public ExceptionMiddleware(RequestDelegate request)
        {
            _request = request;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            #region Bad Request Exception
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var error = ex.ToValidationExceptionDetails();
                await context.Response.WriteAsJsonAsync(error);
            }
            #endregion
            #region Conflict Exception
            catch (Exception ex) when (ex.GetErrorCode() == (int)HttpStatusCode.Conflict)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                var error = ex.ToConflictExceptionDetails();
                await context.Response.WriteAsJsonAsync(error);
            }
            #endregion
            #region Not Found Exception
            catch (Exception ex) when (ex.GetErrorCode() == (int)HttpStatusCode.NotFound)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var error = ex.ToNotFoundExceptionDetails();
                await context.Response.WriteAsJsonAsync(error);
            } 
            #endregion
            catch(Exception)
            {
                throw;
            }
        }
    }
}