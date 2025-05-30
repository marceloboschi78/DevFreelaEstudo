using DevFreela.API.Validators;
using DevFreela.Application.CQRS.Commands;
using DevFreela.Application.Models;
using DevFreela.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        //adicionar apenas esse metodo no program.cs
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();
            services.AddHandlers();
            services.AddValidation();

            return services;
        }

        //adicionar serviços aqui
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            // removidos apos CQRS

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblyContaining<ProjectInsertCommand>());

            services.AddTransient<IPipelineBehavior<ProjectInsertCommand, ResultViewModel<int>>, ProjectInsertCommandValidateBehavior>(); //ex de decorator

            return services;
        }

        private static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ProjectInsertCommandValidator>();
            
            return services;
        }
    }
}