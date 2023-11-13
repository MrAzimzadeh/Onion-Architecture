using MediatR;

namespace Ecomerce.Application.Features.Commands;

public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }
}