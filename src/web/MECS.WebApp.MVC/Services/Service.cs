using MECS.WebApp.MVC.Extensions;
using System.Net.Http;

namespace MECS.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected bool TreateErrorsResponse(HttpResponseMessage httpResponseMessage)
        {
            switch ((int) httpResponseMessage.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpResponseException(httpResponseMessage.StatusCode);
                case 400:
                    return false;
            }

            httpResponseMessage.EnsureSuccessStatusCode();

            return true;
        }
    }
}
