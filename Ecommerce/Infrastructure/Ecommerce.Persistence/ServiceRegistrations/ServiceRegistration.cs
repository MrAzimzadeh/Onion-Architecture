
using Ecommerce.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.ServiceRegistrations
{

    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EcommerceAPIDbContext>(options =>
                options.UseNpgsql
                    ("User ID=docker;Password=docker;Host=localhost;Port=5432;Database=EcommerceAPIDb;"));
        }
    }
}