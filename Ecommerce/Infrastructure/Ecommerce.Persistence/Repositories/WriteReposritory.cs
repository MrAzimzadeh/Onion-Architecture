using Ecommerce.Domain.Entities.Common;
using Ecommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Ecomerce.Application.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecommerce.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T>
        where T : BaseEntity
    {
        private readonly EcommerceAPIDbContext _context;

        public WriteRepository(EcommerceAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public bool Add(T model)
        {
            EntityEntry<T> entityEntry = Table.Add(model);
            return entityEntry.State == EntityState.Added;
        }

        public bool AddRange(List<T> model)
        {
            Table.AddRange(model);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            entityEntry.State = EntityState.Deleted;
            return true;
        }

        public bool RemoveRange(List<T> model)
        {
            Table.RemoveRange(model);
            return true;
        }

        public bool Remove(string id)
        {
            T model = Table.FirstOrDefault(data => data.Id == Guid.Parse(id));
            if (model != null)
            {
                return Remove(model);
            }
            return false;
        }

        public bool RemoveRange(List<string> ids)
        {
            foreach (var id in ids)
            {
                T model = Table.FirstOrDefault(data => data.Id == Guid.Parse(id));
                if (model != null)
                {
                    Remove(model);
                }
            }
            return true; 
        }


        public bool Update(T model)
        {
            EntityEntry<T> entityEntry = Table.Update(model);
            entityEntry.State = EntityState.Modified;
            return true;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> model)
        {
            await Table.AddRangeAsync(model);
            return true;
        }

        public async Task<bool> RemoveAsync(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            entityEntry.State = EntityState.Deleted;
            return true;
        }

        public async Task<bool> RemoveRangeAsync(List<T> model)
        {
            Table.RemoveRange(model);
            return true;
        }

        public async Task<bool> RemoveRangeAsync(List<string> ids)
        {
            foreach (var id in ids)
            {
                T model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
                if (model != null)
                {
                    await RemoveAsync(model);
                }
            }
            return true; 
        }




        public async Task<bool> RemoveAsync(string id)
        {
            T model = Table.FirstOrDefault(data => data.Id == Guid.Parse(id));
            if (model != null)
            {
                return await RemoveAsync(model);
            }
            return false;
        }

        public async Task<bool> UpdateAsync(T model)
        {
            EntityEntry<T> entityEntry = Table.Update(model);
            entityEntry.State = EntityState.Modified;
            return true;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
