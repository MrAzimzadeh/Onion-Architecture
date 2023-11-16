using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ecomerce.Application.Features.Commands;
using Ecomerce.Application.Features.Commands.Product.CreateProduct;
using Ecomerce.Application.Features.Commands.Product.RemoveProduct;
using Ecomerce.Application.Features.Commands.Product.UpdateProduct;
using Ecomerce.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using Ecomerce.Application.Features.Commands.ProductImageFile.UploadProductImage;
using Ecomerce.Application.Features.Queries.Product.GetAllProduct;
using Ecomerce.Application.Features.Queries.Product.GetByIdProduct;
using Ecomerce.Application.Features.Queries.ProductImageFile.GetProductImages;
using Ecomerce.Application.Repositories.File;
using Ecomerce.Application.Repositories.InvoiceFile;
using Ecomerce.Application.Repositories.ProductImageFile;
using Ecomerce.Application.Repositories.Products;
using Ecomerce.Application.RequestParameters;
using Ecomerce.Application.Services;
using Ecomerce.Application.ViewModels.Products;
using Ecommerce.Application.Abstractions.Storeg;
using Ecommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController
    (
        IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("GetAlls")]
    public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
    {
        GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
        return Ok(response);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
    {
        GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductCommandRequest createProductCommandRequest)
    {
        CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);

        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest request)
    {
        UpdateProductCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }


    [HttpDelete("deleteProduct/{Id}")]
    public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
    {
        RemoveProductCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Upload(
        [FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
    {
        UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
        return Ok();
    }


    [HttpGet("GetImage/{Id}")]
    public async Task<IActionResult> GetImage([FromRoute] GetProductImagesQueryRequest request)
    {
        List<GetProductImagesQueryResponse> response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("GetImage/{ProductId}")]
    public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest request)
    {
        // request.ImageId = imageId;
        RemoveProductImageCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}