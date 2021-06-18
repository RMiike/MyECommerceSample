using MECS.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace MECS.BFF.Purshases.Services
{
    public interface IOrderService
    {

    }
    public class OrderService : Service, IOrderService
    {

        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.OrderUrl);
        }
    }
}
