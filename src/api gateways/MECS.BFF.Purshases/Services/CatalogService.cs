using MECS.BFF.Purshases.Models;
using MECS.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MECS.BFF.Purshases.Services
{
    public interface ICatalogService
    {
        Task<ItemProductDTO> Get(Guid id);
    }
    public class CatalogService : Service, ICatalogService
    {

        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
        }

        public async Task<ItemProductDTO> Get(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/catalog/products/{id}");

            TreateErrorsResponse(response);

            return await DeserializeObjectResponse<ItemProductDTO>(response);
        }
    }
}
