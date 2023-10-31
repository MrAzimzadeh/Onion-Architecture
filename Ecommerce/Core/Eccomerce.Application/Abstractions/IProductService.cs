using System.Net.Http.Headers;
using Ecommerce.Domain.Entities;

namespace Eccomerce.Application.Abstractions;

public interface IProductService
{
    List<Product> GetProducts();
}