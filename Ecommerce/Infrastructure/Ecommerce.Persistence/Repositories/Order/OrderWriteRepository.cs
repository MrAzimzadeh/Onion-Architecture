using Ecomerce.Application.Repositories.Orders;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories;

public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}