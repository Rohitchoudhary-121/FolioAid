using Application.Handlers.Account;
using Application.Interface;
using FluentValidation;
using FolioAid.Services;
using Repository.Generic;
using System.Reflection;

namespace FolioAid.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEmailTemplate, EmailService>();
            services.AddScoped<IHelperService, HelperService>();
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(RegisterUserHandler).Assembly);
            });
            return services;
        }

    }
}
