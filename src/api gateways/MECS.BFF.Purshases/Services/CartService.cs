using MECS.BFF.Purshases.Models;
using MECS.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MECS.BFF.Purshases.Services
{
    public interface ICartService
    {
        Task<CartDTO> Get();
        //Task<int> GetQuantity();
        Task<ResponseResult> AddItem(ItemCartDTO product);
        Task<ResponseResult> UpdateItem(Guid idProduct, ItemCartDTO model);
        Task<ResponseResult> RemoveItem(Guid idProduct);
    }
    public class CartService : Service, ICartService
    {

        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }

        public async Task<ResponseResult> AddItem(ItemCartDTO product)
        {
            var item = GetContent(product);
            var response = await _httpClient.PostAsync($"/api/cart/", item);
            if (!TreateErrorsResponse(response))
                return await DeserializeObjectResponse<ResponseResult>(response);
            return ReturnOk();
        }

        public async Task<CartDTO> Get()
        {
            var response = await _httpClient.GetAsync("/api/cart/");
            TreateErrorsResponse(response);
            return await DeserializeObjectResponse<CartDTO>(response);
        }

        //public async Task<int> GetQuantity()
        //{
        //    var response = await _httpClient.GetAsync("/api/cart-quantity/");
        //    TreateErrorsResponse(response);
        //    return await DeserializeObjectResponse<int>(response);

        //}
        public async Task<ResponseResult> UpdateItem(Guid idProduct, ItemCartDTO model)
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
