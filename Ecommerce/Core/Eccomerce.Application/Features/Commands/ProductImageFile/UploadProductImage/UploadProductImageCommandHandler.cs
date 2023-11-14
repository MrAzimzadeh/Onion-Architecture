using Ecomerce.Application.Repositories.ProductImageFile;
using Ecomerce.Application.Repositories.Products;
using Ecommerce.Application.Abstractions.Storeg;
using MediatR;

namespace Ecomerce.Application.Features.Commands.ProductImageFile.UploadProductImage;

public class
    UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest,
        UploadProductImageCommandResponse>
{
    private readonly IStorageService _storageService;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IFileProductImageWriteRepository _fileProductImageWriteRepository;

    public UploadProductImageCommandHandler(IStorageService storageService,
        IProductReadRepository productReadRepository, IFileProductImageWriteRepository fileProductImageWriteRepository)
    {
        _storageService = storageService;
        _productReadRepository = productReadRepository;
        _fileProductImageWriteRepository = fileProductImageWriteRepository;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _storageService.UploadAsync(
            "photoimages",
            request.File);

        Ecommerce.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

        await _fileProductImageWriteRepository.AddRangeAsync(data.Select(z =>
            new Ecommerce.Domain.Entities.ProductImageFile()
            {
                FileName = z.fileName,
                Path = z.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Ecommerce.Domain.Entities.Product> { product }
            }).ToList());
        await _fileProductImageWriteRepository.SaveChangesAsync();
        return new();
    }
}