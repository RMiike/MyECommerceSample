using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomHttpResponseException ex)
            {
                HandleRequestExceptionAsync(context, ex);
            }
        }
        private static void HandleRequestExceptionAsync(HttpContext context, CustomHttpResponseException customHttpResponseException)
        {
            if (customHttpResponseException.StatusCode == HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/sign-in?ReturnUrl={context.Request.Path}");
                return;
            }
            context.Response.StatusCode = (int) customHttpResponseException.StatusCode;
        }
    }
}
