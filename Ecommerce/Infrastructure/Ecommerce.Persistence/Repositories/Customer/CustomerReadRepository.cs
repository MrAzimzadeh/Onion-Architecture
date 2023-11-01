using Ecomerce.Application.Repositories.Customers;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories;

public class CustomerReadRepository : ReadRepository<Customer>,  ICustomerReadRepository
{
    
    public CustomerReadRepository(EcommerceAPIDbContext context) : base(context)
    {
        
    }
    
}