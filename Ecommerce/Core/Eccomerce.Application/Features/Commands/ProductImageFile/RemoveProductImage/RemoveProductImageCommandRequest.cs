using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerce.Application.Features.Commands.ProductImageFile.RemoveProductImage;

public class RemoveProductImageCommandRequest : IRequest<RemoveProductImageCommandResponse>
{
    [FromRoute] public string ProductId { get; set; }
    [FromQuery] public string? ImageId { get; set; }
}