using System.Linq.Expressions;
using Ecomerce.Application.Repositories;
using Ecommerce.Domain.Entities.Common;
using Ecommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories;

public class ReadRepository<T> : IReadRepsoitory<T>
    where T : BaseEntity
{
    private readonly EcommerceAPIDbContext _context;

    public ReadRepository(EcommerceAPIDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();


    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        => Table.Where(method);

    public IQueryable<T> GetAll() => Table;

    public T GetSingle(Expression<Func<T, bool>> method)
        => Table.FirstOrDefault(method);

    public T GetById(string id)
        => Table.FirstOrDefault(data => data.Id == Guid.Parse(id));

    public async Task<IQueryable<T>> GetAllAsync() => await Task.FromResult(Table);

    public async Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> method)
        => await Task.FromResult(Table.Where(method));

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        => await Table.FirstOrDefaultAsync(method);

    public async Task<T> GetByIdAsync(string id)
        => await Table.FindAsync(Guid.Parse(id));
    // => await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
}