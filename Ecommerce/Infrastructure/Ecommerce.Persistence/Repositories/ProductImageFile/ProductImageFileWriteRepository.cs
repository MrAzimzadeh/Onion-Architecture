using Ecomerce.Application.Repositories.ProductImageFile;
using Ecomerce.Application.Repositories.Products;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.ProductImageFile;

public class ProductImageFileWriteRepository : WriteRepository<Domain.Entities.ProductImageFile> , IFileProductImageWriteRepository
{
    public ProductImageFileWriteRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}