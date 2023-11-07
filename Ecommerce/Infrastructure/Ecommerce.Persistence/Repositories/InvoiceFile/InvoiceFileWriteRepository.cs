using Ecomerce.Application.Repositories.InvoiceFile;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.InvoiceFile;

public class InvoiceFileWriteRepository : WriteRepository<Domain.Entities.InvoiceFile> , IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}