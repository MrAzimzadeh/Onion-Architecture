using Ecomerce.Application.Repositories.File;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.File;

public class FileWriteRepository  : WriteRepository<Domain.Entities.File>,  IFileWriteRepository
{
    public FileWriteRepository (EcommerceAPIDbContext context) : base(context)
    {
    }
}