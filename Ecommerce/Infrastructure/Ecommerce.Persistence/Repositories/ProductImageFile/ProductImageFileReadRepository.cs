using Ecomerce.Application.Repositories;
using Ecomerce.Application.Repositories.ProductImageFile;
using Ecomerce.Application.Repositories.Products;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.ProductImageFile;

public class ProductImageFileReadRepository : ReadRepository<Domain.Entities.ProductImageFile> , IProductImageFileReadRepository
{
    public ProductImageFileReadRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}