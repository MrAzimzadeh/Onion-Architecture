using Ecommerce.Domain.Entities.Common;

namespace Ecomerce.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T>
    where T : BaseEntity
{
    #region sync
    bool Add(T model);
    bool AddRange(List<T> model);
    bool Remove(T model);
    bool RemoveRange(List<T> model);
    bool Remove(string id);
    bool RemoveRange(List<string> ids);
    bool Update(T model);
    int SaveChanges();
    
    
    #endregion

    #region async
    Task<bool> AddAsync(T model);
    Task<bool> AddRangeAsync(List<T> model);
    Task<bool> RemoveAsync(T model);
    Task<bool> RemoveRangeAsync(List<T> model);
    Task<bool> RemoveRangeAsync(List<string> ids);
    Task<bool> RemoveAsync(string id);
    Task<bool> UpdateAsync(T model);
    
    Task<int> SaveChangesAsync();
    #endregion
}