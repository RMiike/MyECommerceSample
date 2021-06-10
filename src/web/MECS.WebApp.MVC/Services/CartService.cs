using MECS.Core.Domain.Entities;
using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;
        public CartService(HttpClient httpClient,
                  IOptions<AppSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.CartUrl);
            _httpClient = httpClient;
        }
        public async Task<CartViewModel> Get()
        {
            var response = await _httpClient.GetAsync("/api/cart/");
            TreateErrorsResponse(response);
            return await DeserializeObjectResponse<CartViewModel>(response);
        }
        public async Task<ResponseResult> AddItem(ItemProductViewModel model)
        {
            var item = GetContent(model);
            var response = await _httpClient.PostAsync($"/api/cart/", item);
            if (!TreateErrorsResponse(response))
                return await DeserializeObjectResponse<ResponseResult>(response);
            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateItem(Guid idProduct, ItemProductViewModel model)
        {
            var item = GetContent(model);
            var response = await _httpClient.PutAsync($"/api/cart/{model.IdProduct}", item);
            if (!TreateErrorsResponse(response))
                return await DeserializeObjectResponse<ResponseResult>(response);
            return ReturnOk();
        }
        public async Task<ResponseResult> RemoveItem(Guid idProduct)
        {
            var response = await _httpClient.DeleteAsync($"/api/cart/{idProduct}");
            if (!TreateErrorsResponse(response))
                return await DeserializeObjectResponse<ResponseResult>(response);
            return ReturnOk();
        }
    }
}
