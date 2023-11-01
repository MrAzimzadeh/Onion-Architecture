using Ecomerce.Application.Repositories.Products;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories;

public class ProductReadRepository : ReadRepository<Product> , IProductReadRepository
{
    public ProductReadRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}