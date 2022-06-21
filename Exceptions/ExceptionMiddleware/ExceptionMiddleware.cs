using System.Net;
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
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var error = ex.ToValidationExceptionDetails();
                await context.Response.WriteAsJsonAsync(error);
            }
            catch(Exception ex) when (ex.GetErrorCode() == (int)HttpStatusCode.Conflict)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                var error = ex.ToConflictExceptionDetails();
                await context.Response.WriteAsJsonAsync(error);
            }
            catch(Exception ex) when (ex.GetErrorCode() == (int)HttpStatusCode.NotFound)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var error = ex.ToNotFoundExceptionDetails();
                await context.Response.WriteAsJsonAsync(error);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
