using Ecomerce.Application.Repositories.Products;
using MediatR;

namespace Ecomerce.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;

    public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository,
        IProductReadRepository productReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request,
        CancellationToken cancellationToken)
    {
        Ecommerce.Domain.Entities.Product product =
            await _productReadRepository.GetByIdAsync(request.Id, tracking: true);
        product.Stock = request.Stock;
        product.Price = request.Price;
        product.Name = request.Name;
        await _productWriteRepository.SaveChangesAsync();
        return new();
    }
}