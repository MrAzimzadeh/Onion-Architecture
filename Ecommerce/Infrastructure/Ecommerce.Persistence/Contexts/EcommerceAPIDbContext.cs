using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Contexts;

public class EcommerceAPIDbContext : DbContext
{
    public EcommerceAPIDbContext(DbContextOptions<EcommerceAPIDbContext> options) : base(options)
    { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
}