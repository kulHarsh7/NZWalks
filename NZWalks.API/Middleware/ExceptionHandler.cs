using Microsoft.Extensions.Logging;
using System.Net;

namespace NZWalks.API.Middleware
{
    public class ExceptionHandler
    {
        
        private readonly ILogger<ExceptionHandler> logger;
        private readonly RequestDelegate next;

        public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                //Logging
                logger.LogError(ex, ex.Message);

                //returning msg
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var errorId=Guid.NewGuid();
                var result = new {
                    Id=errorId,
                    ErrorMessage="Something went wrong",
                };

               await httpContext.Response.WriteAsJsonAsync(result);
            }
        }
    }
    

}
