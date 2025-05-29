using DevFreela.Core.Repositories;
using DevFreela.Infraestructure.Persistence;
using DevFreela.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infraestructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRepositories()
                .AddData(configuration);
            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IProjectRepository, ProjectRepository>()
                .AddScoped<ISkillRepository, SkillRepository>()
                .AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DevFreelaCS");
            services.AddDbContext<DevFreelaDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;

        }
    }
}