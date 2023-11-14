using MediatR;

namespace Ecomerce.Application.Features.Queries.ProductImageFile.GetProductImages;

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest , GetProductImagesQueryResponse>
{
    public Task<GetProductImagesQueryResponse> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}