using Ecomerce.Application.RequestParameters;
using MediatR;

namespace Ecomerce.Application.Features.Queries.GetAllProduct;

public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
}