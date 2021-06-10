using MECS.WebApp.MVC.Extensions;
using MECS.WebApp.MVC.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected StringContent GetContent(object data)
            => new StringContent(
                        JsonSerializer.Serialize(data),
                        Encoding.UTF8,
                        "application/json");

        protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage response)
        {
            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), opt);
        }
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
        protected ResponseResult ReturnOk()
        {
            return new ResponseResult();
        }
    }
}
