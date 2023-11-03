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

    #region Tracking

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();
        return query.Where(method);
    }

    public T GetSingle(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query.FirstOrDefault(method);
    }

    public T GetById(string id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query.FirstOrDefault(x => x.Id == Guid.Parse(id));
    }

    public async Task<IQueryable<T>> GetAllAsync(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await Task.FromResult(query);
    }

    public Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return Task.FromResult(query.Where(method));
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(method);
    }

    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
    }

    #endregion
}