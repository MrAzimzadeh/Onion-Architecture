using Ecomerce.Application.Repositories.InvoiceFile;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.InvoiceFile;

public class InvoiceFileReadRepository : ReadRepository<Domain.Entities.InvoiceFile> , IInvoiceFileReadRepository
{
    public InvoiceFileReadRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}