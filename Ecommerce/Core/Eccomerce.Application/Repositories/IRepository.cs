using Ecommerce.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce.Application.Repositories;

public interface IRepository<T>
    where T : BaseEntity
{
    DbSet<T> Table { get; }
}