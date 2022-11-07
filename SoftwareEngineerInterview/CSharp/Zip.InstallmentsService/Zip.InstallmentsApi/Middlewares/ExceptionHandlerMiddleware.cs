using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Zip.InstallmentsApi.Model;

namespace Zip.InstallmentsApi.Middlewares
{
    public class ExceptionHandlerZipMiddleware
    {
        public readonly RequestDelegate requestDelegate;
        private readonly ILogger<ExceptionHandlerZipMiddleware> logger;
        private readonly IWebHostEnvironment environment;

        public ExceptionHandlerZipMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlerZipMiddleware> logger,
                IWebHostEnvironment environment)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch(Exception ex)
            {
                await HandleError(context, ex);
            }
        }

        private Task HandleError(HttpContext context, Exception exception)
        {
            logger.LogError(exception, exception.Message);
            var statusCode = HttpStatusCode.InternalServerError;
            var  errorResponse = new Error((int)statusCode,exception.Message);
            if (environment.IsDevelopment())
            {
                errorResponse.ErrorDetails = exception.StackTrace.ToString();
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
