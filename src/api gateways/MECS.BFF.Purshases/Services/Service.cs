using MECS.Core.Domain.Entities;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MECS.BFF.Purshases.Services
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
            if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                return false;

            httpResponseMessage.EnsureSuccessStatusCode();

            return true;
        }
        protected ResponseResult ReturnOk()
        {
            return new ResponseResult();
        }
    }
}
