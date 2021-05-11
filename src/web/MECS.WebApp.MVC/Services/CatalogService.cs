using MECS.Core.Domain.Entities;
using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;
        public CatalogService(HttpClient httpClient,
                              IOptions<AppSettings> options)
        {
            httpClient.BaseAddress = new Uri(options.Value.CatalogUrl);
            _httpClient = httpClient;
        }

        public async Task<ProductViewModel> Get(Guid id)
        {

            var response = await _httpClient.GetAsync($"/api/catalog/products/{id}");

            TreateErrorsResponse(response);

            return await DeserializeObjectResponse<ProductViewModel>(response);
        }

        public async Task<IEnumerable<ProductViewModel>> Get()
        {
            var response = await _httpClient.GetAsync("/api/catalog/products");

            TreateErrorsResponse(response);

            return await DeserializeObjectResponse<IEnumerable<ProductViewModel>>(response);
        }
    }
}
