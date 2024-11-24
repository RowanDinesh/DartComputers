using Application.Web.Exceptions;
using DartComputers.Web.WebModels;
using System.Net;

namespace DartComputers.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _requestDelegate(context);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails customProblemDetails = new();

            switch (ex)
            {
                case BadRequestExeption BadRequestExeption:
                    statusCode = HttpStatusCode.BadRequest;
                    customProblemDetails = new CustomProblemDetails()
                    {
                        Title = BadRequestExeption.Message,
                        Status = (int)statusCode,
                        Type = nameof(BadRequestExeption),
                        Detail = BadRequestExeption.InnerException?.Message,
                        Errors = BadRequestExeption.ValidationErrors

                    };
                    break;
                
                   
            }

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(customProblemDetails);
        }
    }
}
