using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Persistence.Contexts
{
    public class EcommerceAPIDbContext : DbContext
    {
        public EcommerceAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdateDate = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        // Handle any other states, or do nothing
                        break;
                }
            }

            return base.SaveChanges();
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // ChandeTracker => Entryiler  uzerinde  olan    Deyisiklikleri  ya da yeni  elave  olunmus  obyektleri  tapmaq  ucun  istifade  olunur
            ChangeTracker.DetectChanges();
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                };
            }


            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}