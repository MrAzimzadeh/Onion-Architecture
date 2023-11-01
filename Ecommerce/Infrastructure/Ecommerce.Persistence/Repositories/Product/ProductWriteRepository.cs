using Ecomerce.Application.Repositories.Products;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories;

public class ProductWriteRepository : WriteRepository<Product>,  IProductWriteRepository
{
    public ProductWriteRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}