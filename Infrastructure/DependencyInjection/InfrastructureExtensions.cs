using Application.Data;
using Application.Data.Repository;
using Application.Services.CreateWorkingDay;
using Infrastructure.Data.Repository;
using Infrastructure.Database;
using Infrastructure.Database.Context;
using Infrastructure.Services.CreateWorkingDay;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddDataBase(configuration);
            services.AddUnitOfWork();
            services.AddRepositories();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateWorkingDayService, CreateWorkingDayService>();
            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWorkshopRepository, WorkshopRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            return services;
        }

        private static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkshopContext>(
                (builder) =>
                {
                    builder.UseSqlServer(configuration.GetConnectionString("WorkshopContext"));
                });

            return services;
        }
    }
}
