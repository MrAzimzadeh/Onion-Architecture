using File = Ecommerce.Domain.Entities;
namespace Ecomerce.Application.Repositories.File;

public interface IFileWriteRepository : IWriteRepository<File::File>
{
    
}