using MediatR;

namespace Ecomerce.Application.Features.Queries.Product.GetByIdProduct;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest , GetByIdProductQueryResponse>
{
    public Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}