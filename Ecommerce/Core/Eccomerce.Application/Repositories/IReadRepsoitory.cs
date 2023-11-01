using System.Linq.Expressions;
using Ecommerce.Domain.Entities.Common;

namespace Ecomerce.Application.Repositories;

public interface IReadRepsoitory<T> : IRepository<T>
    where T : BaseEntity
{
    // select IQueryable optimizetion

    #region sync
    IQueryable<T> GetAll();
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method);
    T GetSingle(Expression<Func<T, bool>> method);
    T GetById(string id);
    #endregion

    #region Async
    Task<IQueryable<T>> GetAllAsync();
    Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> method);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method);
    Task<T> GetByIdAsync(string id);
    #endregion
}