using Ecommerce.Domain.Entities;

namespace Ecomerce.Application.Features.Queries.Product.GetByIdProduct;

public class GetByIdProductQueryResponse
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }
}