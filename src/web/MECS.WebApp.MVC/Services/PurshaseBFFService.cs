using MECS.Core.Domain.Entities;
using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Services
{
    public class PurshaseBFFService : Service, IPurshaseBFFService
    {
        private readonly HttpClient _httpClient;
        public PurshaseBFFService(HttpClient httpClient,
                  IOptions<AppSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.PurshaseBFFUrl);
            _httpClient = httpClient;
        }
        public async Task<CartViewModel> Get()
        {
            var response = await _httpClient.GetAsync("/api/purshase/cart/");
            TreateErrorsResponse(response);
            return await DeserializeObjectResponse<CartViewModel>(response);
        }
        public async Task<ResponseResult> AddItem(ItemProductViewModel model)
        {
            var item = GetContent(model);
            var response = await _httpClient.PostAsync($"/api/purshase/cart/itens", item);
            if (!TreateErrorsResponse(response))
                return await DeserializeObjectResponse<ResponseResult>(response);
            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateItem(Guid idProduct, ItemProductViewModel model)
        {
            var item = GetContent(model);
            var response = await _httpClient.PutAsync($"/api/purshase/cart/itens/{model.IdProduct}", item);
            if (!TreateErrorsResponse(response))
                return await DeserializeObjectResponse<ResponseResult>(response);
            return ReturnOk();
        }
        public async Task<ResponseResult> RemoveItem(Guid idProduct)
        {
            var response = await _httpClient.DeleteAsync($"/api/purshase/cart/itens/{idProduct}");
            if (!TreateErrorsResponse(response))
                return await DeserializeObjectResponse<ResponseResult>(response);
            return ReturnOk();
        }

        public async Task<int> GetQuantity()
        {
            var response = await _httpClient.GetAsync("/api/purshase/cart-quantity/");
            TreateErrorsResponse(response);
            return await DeserializeObjectResponse<int>(response);
        }
    }
}