using Ecomerce.Application.Repositories;
using Ecomerce.Application.Repositories.Customers;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories;

public class CustomerWriteRepository : WriteRepository<Customer> , ICustomerWriteReposiyory
{
    public CustomerWriteRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}