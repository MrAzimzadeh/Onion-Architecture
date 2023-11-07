using Ecomerce.Application.Repositories;
using Ecomerce.Application.Repositories.Customers;
using Ecomerce.Application.Repositories.File;
using Ecomerce.Application.Repositories.InvoiceFile;
using Ecomerce.Application.Repositories.Orders;
using Ecomerce.Application.Repositories.ProductImageFile;
using Ecomerce.Application.Repositories.Products;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;
using Ecommerce.Persistence.Repositories.File;
using Ecommerce.Persistence.Repositories.InvoiceFile;
using Ecommerce.Persistence.Repositories.ProductImageFile;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.ServiceRegistrations;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        // context 
        services.AddDbContext<EcommerceAPIDbContext>(options =>
            options.UseNpgsql(Configuration.ConnectionString));
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteReposiyory, CustomerWriteRepository>();

        // order
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

        // product
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        // file
        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();

        // product image file
        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IFileProductImageWriteRepository, ProductImageFileWriteRepository>();

        // invoice
        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
    }
}