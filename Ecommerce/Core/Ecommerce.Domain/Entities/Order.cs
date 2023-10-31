using Ecommerce.Domain.Entities.Common;

namespace Ecommerce.Domain.Entities;

public class Order : BaseEntity
{
    public string Description { get; set; }
    public string Address { get; set; }
    public ICollection<Product> Products { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
}