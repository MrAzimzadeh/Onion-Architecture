using Ecommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Persistence;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<EcommerceAPIDbContext>
{
    public EcommerceAPIDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<EcommerceAPIDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql
            (Configuration.ConnectionString);
        return new EcommerceAPIDbContext(dbContextOptionsBuilder.Options); 
    }
}