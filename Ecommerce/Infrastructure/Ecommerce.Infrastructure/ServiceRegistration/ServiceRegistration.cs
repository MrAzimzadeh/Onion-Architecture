using Ecomerce.Application.Abstractions.Storeg;
using Ecomerce.Application.Services;
using Ecomerce.Infrastructure.Enums;
using Ecomerce.Infrastructure.Services;
using Ecomerce.Infrastructure.Services.Storage;
using Ecomerce.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace Ecomerce.Infrastructure.ServiceRegistration;

public static class ServiceRegistration
{
    public static void AddInfrastructureService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStorage, StorageService>();
    }

    public static void AddStorage<T>(this IServiceCollection serviceCollection)
        where T : class, IStorage
    {
        serviceCollection.AddScoped<IStorage, T>();
    }

    public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Local:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
            case StorageType.Azure:
                serviceCollection.AddScoped<IStorage>();
                break;
            case StorageType.Aws:
                serviceCollection.AddScoped<IStorage>();
                break;
            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}