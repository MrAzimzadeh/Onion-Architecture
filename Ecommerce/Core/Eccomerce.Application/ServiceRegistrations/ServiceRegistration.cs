using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace Ecomerce.Application.ServiceRegistrations;

public static class ServiceRegistration
{
    public static void AddApplicationService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
    }