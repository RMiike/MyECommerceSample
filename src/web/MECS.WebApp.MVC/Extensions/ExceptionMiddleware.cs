using Microsoft.AspNetCore.Http;
using Refit;
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
                HandleRequestExceptionAsync(context, ex.StatusCode);
            }
            catch (ValidationApiException ex)
            {
                HandleRequestExceptionAsync(context, ex.StatusCode);
            }
            catch (ApiException ex)
            {
                HandleRequestExceptionAsync(context, ex.StatusCode);
            }
        }
        private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/sign-in?ReturnUrl={context.Request.Path}");
                return;
            }
            context.Response.StatusCode = (int) statusCode;
        }
    }
}
