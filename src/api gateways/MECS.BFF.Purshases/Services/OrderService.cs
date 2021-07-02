using MECS.BFF.Purshases.Models;
using MECS.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MECS.BFF.Purshases.Services
{
    public interface IOrderService
    {
        Task<VoucherDTO> GetVoucherByCode(string code);
    }
    public class OrderService : Service, IOrderService
    {

        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.OrderUrl);
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {

            var response = await _httpClient.GetAsync($"/api/voucher/{code}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            TreateErrorsResponse(response);

            return await DeserializeObjectResponse<VoucherDTO>(response);

        }
    }
}
