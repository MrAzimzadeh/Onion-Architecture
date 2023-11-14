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
    //ctor injection hell 
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IFileService _fileService;
    private readonly IFileReadRepository _fileReadRepository;
    private readonly IFileWriteRepository _fileWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IFileProductImageWriteRepository _fileProductImageWriteRepository;
    private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
    private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
    private readonly IStorageService _storageService;
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public ProductController
    (
        IProductWriteRepository productWriteRepository,
        IProductReadRepository productReadRepository,
        IWebHostEnvironment hostEnvironment,
        IFileService fileService, IFileReadRepository fileReadRepository,
        IFileWriteRepository fileWriteRepository,
        IProductImageFileReadRepository productImageFileReadRepository,
        IFileProductImageWriteRepository fileProductImageWriteRepository,
        IInvoiceFileReadRepository invoiceFileReadRepository,
        IInvoiceFileWriteRepository invoiceFileWriteRepository,
        IStorageService storageService, IConfiguration configuration, IMediator mediator)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _hostEnvironment = hostEnvironment;
        _fileService = fileService;
        _fileReadRepository = fileReadRepository;
        _fileWriteRepository = fileWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _fileProductImageWriteRepository = fileProductImageWriteRepository;
        _invoiceFileReadRepository = invoiceFileReadRepository;
        _invoiceFileWriteRepository = invoiceFileWriteRepository;
        _storageService = storageService;
        _configuration = configuration;
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

    [HttpGet("Test")]
    public IActionResult Test()
    {
        var a = _fileReadRepository.GetAll(false);
        var b = _productImageFileReadRepository.GetAll(false);
        var c = _invoiceFileReadRepository.GetAll(false);
        return Ok(
            new
            {
                a,
                b,
                c
            }
        );
    }

    [HttpGet("GetImage/{id}")]
    public async Task<IActionResult> GetImage(string id)
    {
        Product? product = await _productReadRepository.Table
            .Include(z => z.ProductImageFiles)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

        return Ok(product.ProductImageFiles.Select(x =>
            new
            {
                Path = $"{_configuration["BaseStorageUrl"]}{x.Path}",
                x.FileName,
                x.Storage
            }));
    }

    [HttpDelete("GetImage/{ProductId}")]
    public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest request)
    {
        // request.ImageId = imageId;
        RemoveProductImageCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}