using Eccomerce.Infrastructure.Services;
using Ecomerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Eccomerce.Infrastructure.ServiceRegistration;

public static class ServiceRegistration
{
    public static void AddInfrastructureService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFileService, FileService>();

    }        
}