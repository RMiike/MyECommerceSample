using MECS.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace MECS.BFF.Purshases.Services
{
    public interface IPaymentService
    {

    }
    public class PaymentService : Service, IPaymentService
    {

        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PaymentUrl);
        }
    }
}
