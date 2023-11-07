using File = Ecommerce.Domain.Entities;

namespace Ecomerce.Application.Repositories.InvoiceFile;

public interface IInvoiceFileWriteRepository : IWriteRepository<File::InvoiceFile>
{
}