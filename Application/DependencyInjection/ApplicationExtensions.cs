using Application.Middlewares;
using Application.UseCases.CreateWorkshop;
using Application.UseCases.CreateWorkshop.Input;
using Application.UseCases.CreateWorkshop.Validator;
using Application.UseCases.Customer.CreateCustomer;
using Application.UseCases.Customer.CreateCustomer.Input;
using Application.UseCases.Customer.CreateCustomer.Validator;
using Application.UseCases.GetWorkshopWorkload;
using Application.UseCases.Service.CreateService;
using Application.UseCases.Service.DeleteService;
using Application.UseCases.Service.GetReport;
using Application.UseCases.Service.GetReport.Input;
using Application.UseCases.Service.GetReport.Validator;
using Application.UseCases.Service.GetServices;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddUseCases();
            services.AddValidators();
            services.AddMiddlewares();
            return services;
        }

        private static void AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationMiddleware>();
        }

        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateWorkshopUseCase, CreateWorkshopUseCase>();
            services.AddScoped<IGetWorkshopWorkloadUseCase, GetWorkshopWorkloadUseCase>();
            services.AddScoped<ICreateServiceUseCase, CreateServiceUseCase>();
            services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
            services.AddScoped<IGetServicesUseCase, GetServicesUseCase>();
            services.AddScoped<IDeleteServiceUseCase, DeleteServiceUseCase>();
            services.AddScoped<IGetReportUseCase, GetReportUseCase>();
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateWorkshopInput>, CreateWorkshopInputValidator>();
            services.AddTransient<IValidator<CreateCustomerInput>, CreateCustomerInputValidator>();
            services.AddTransient<IValidator<GetReportInput>, GetReportInputValidator>();
        }
    }
}
