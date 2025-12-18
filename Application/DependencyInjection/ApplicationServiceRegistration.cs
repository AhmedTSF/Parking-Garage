
using Application.ServiceImplementations;
using Application.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICarService, CarService>(); 
        services.AddScoped<ISpotService, SpotService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }

}
