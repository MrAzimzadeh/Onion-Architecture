using Ecomerce.Application.Repositories.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce.Application.Features.Commands.ProductImageFile.RemoveProductImage;

public class
    RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest,
        RemoveProductImageCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request,
        CancellationToken cancellationToken)
    {
        Ecommerce.Domain.Entities.Product? product = await _productReadRepository.Table
            .Include(z => z.ProductImageFiles)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.ProductId), cancellationToken: cancellationToken);

        Ecommerce.Domain.Entities.ProductImageFile productImageFile = product.ProductImageFiles
            .FirstOrDefault(x => x.Id == Guid.Parse(request.ImageId));

        if (productImageFile != null)
            product?.ProductImageFiles.Remove(productImageFile);

        await _productWriteRepository.SaveChangesAsync();

        return new();
    }
}