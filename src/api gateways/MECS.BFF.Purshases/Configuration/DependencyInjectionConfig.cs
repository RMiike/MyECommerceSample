using MECS.BFF.Purshases.Extensions;
using MECS.BFF.Purshases.Services;
using MECS.WebAPI.Core.Extensions;
using MECS.WebAPI.Core.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;

namespace MECS.BFF.Purshases.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();


            services.AddHttpClient<ICartService, CartService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.CustomRetryAsync())
                .AddTransientHttpErrorPolicy(p =>
                    p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            ;
            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                //.AddTransientHttpErrorPolicy(
                //    p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500)))
                .AddPolicyHandler(PollyExtensions.CustomRetryAsync())
                .AddTransientHttpErrorPolicy(p =>
                    p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        }
    }
}
