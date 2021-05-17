using FluentValidation.Results;
using MECS.Client.API.Application.Commands;
using MECS.Client.API.Data.Repository;
using MECS.Client.API.Interfaces;
using MECS.Core.Data.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MECS.Client.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();
            services.AddScoped<IClientRepository, ClientRepository>();

        }
    }
}
