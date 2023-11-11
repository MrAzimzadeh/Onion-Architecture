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


    #region Tracking

    #region syncTracking

    IQueryable<T> GetAll(bool tracking = true);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
    T GetSingle(Expression<Func<T, bool>> method, bool tracking = true);
    T GetById(string id, bool tracking = true);

    #endregion

    #region AsyncTracking   

    Task<IQueryable<T>> GetAllAsync(bool tracking = true);
    Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> GetByIdAsync(string id, bool tracking = true);

    #endregion

    #endregion
}