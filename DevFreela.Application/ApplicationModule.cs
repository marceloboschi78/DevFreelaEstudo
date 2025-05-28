using DevFreela.Application.CQRS.Commands;
using DevFreela.Application.Models;
using DevFreela.Application.Services;
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

            return services;
        }

        //adicionar serviços aqui
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISkillService, SkillService>();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblyContaining<ProjectInsertCommand>());
            
            services.AddTransient<IPipelineBehavior<ProjectInsertCommand, ResultViewModel<int>>, ProjectInsertCommandValidateBehavior>();

            return services;
        }
    }
}
