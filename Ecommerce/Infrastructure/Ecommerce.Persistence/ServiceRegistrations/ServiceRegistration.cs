using Eccomerce.Application.Abstractions;
using Ecommerce.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Persistence.ServiceRegistrations;

public static class ServiceRegistration  
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddSingleton<IProductService, ProductService>();
    }
}