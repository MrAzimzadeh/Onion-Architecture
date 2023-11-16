using Ecomerce.Application.Repositories.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ecomerce.Application.Features.Queries.ProductImageFile.GetProductImages;

public class
    GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IConfiguration _configuration;

    public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
    {
        _productReadRepository = productReadRepository;
        _configuration = configuration;
    }

    public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request,
        CancellationToken cancellationToken)
    {
        Ecommerce.Domain.Entities.Product? product = await _productReadRepository.Table
            .Include(z => z.ProductImageFiles)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));
        
        return product.ProductImageFiles.Select(x =>
            new GetProductImagesQueryResponse
            {
                Path = $"{_configuration["BaseStorageUrl"]}{x.Path}",
                Storage = x.Storage,
                FileName = x.FileName,
                Id = x.Id.ToString()
            }).ToList();
    }
}