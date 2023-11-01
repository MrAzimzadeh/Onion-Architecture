using Ecommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ecommerce.Persistence;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<EcommerceAPIDbContext>
{
    public EcommerceAPIDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EcommerceAPIDbContext>();
        optionsBuilder.UseNpgsql
            ("User ID=docker;Password=docker;Host=localhost;Port=5432;Database=EcommerceAPIDb;");
        return new EcommerceAPIDbContext(optionsBuilder.Options);
    }
}