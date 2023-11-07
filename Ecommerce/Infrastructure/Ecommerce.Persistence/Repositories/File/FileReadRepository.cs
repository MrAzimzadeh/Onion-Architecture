using Ecomerce.Application.Repositories.File;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.File;

public class FileReadRepository : ReadRepository<Domain.Entities.File>,  IFileReadRepository
{
    public FileReadRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}