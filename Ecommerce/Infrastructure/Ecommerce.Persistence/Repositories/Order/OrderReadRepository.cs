using Ecomerce.Application.Repositories.Orders;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories;

public class OrderReadRepository :  ReadRepository<Order> , IOrderReadRepository
{
    public OrderReadRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}