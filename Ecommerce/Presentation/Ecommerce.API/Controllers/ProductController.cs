using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ecomerce.Application.Repositories.File;
using Ecomerce.Application.Repositories.InvoiceFile;
using Ecomerce.Application.Repositories.ProductImageFile;
using Ecomerce.Application.Repositories.Products;
using Ecomerce.Application.RequestParameters;
using Ecomerce.Application.Services;
using Ecomerce.Application.ViewModels.Products;
using Ecommerce.Application.Abstractions.Storeg;
using Ecommerce.Domain.Entities;
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
        IStorageService storageService, IConfiguration configuration)
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
    }


    [HttpGet]
    public IActionResult Get([FromQuery] Pagination pagination)
    {
        var totalCount = _productReadRepository.GetAll(tracking: false).Count();

        var products = _productReadRepository.GetAll(tracking: false)
            .Skip(pagination.Page * pagination.Size)
            .Take(pagination.Size)
            .Select(z => new
            {
                z.Name,
                z.CreatedDate,
                z.UpdateDate,
                z.Price,
                z.Id,
                z.Stock
            });

        return Ok(new
        {
            totalCount,
            products
        });
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        return Ok(_productReadRepository.GetById(id, false));
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VM_Create_Product createProduct)
    {
        await _productWriteRepository.AddAsync(new()
        {
            Name = createProduct.Name,
            Price = createProduct.Price,
            Stock = createProduct.Stock
        });
        await _productWriteRepository.SaveChangesAsync();
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> Put(VM_Update_Product model)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine();
        }

        Product product = await _productReadRepository.GetByIdAsync(model.Id, tracking: true);
        product.Stock = model.Stock;
        product.Price = model.Price;
        product.Name = model.Name;
        await _productWriteRepository.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete(string id)
    {
        var a = _productWriteRepository.Remove(id);
        var v = _productWriteRepository.SaveChanges();
        return Ok(new
        {
            // No Best Practice
            message = "Delete Success"
        });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Upload(string id)
    {
        var data = await _storageService.UploadAsync("photoimages", Request.Form.Files);

        Product product = await _productReadRepository.GetByIdAsync(id);

        await _fileProductImageWriteRepository.AddRangeAsync(data.Select(z =>
            new Ecommerce.Domain.Entities.ProductImageFile()
            {
                FileName = z.fileName,
                Path = z.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product> { product }
            }).ToList());
        await _fileProductImageWriteRepository.SaveChangesAsync();
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

    public async Task<IActionResult> DeleteProductImage(string productId, string imageId)
    {
        Product? product = await _productReadRepository.Table
            .Include(z => z.ProductImageFiles)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(productId));

        product.ProductImageFiles
            .Remove
                (product.ProductImageFiles.FirstOrDefault(x => x.Id == Guid.Parse(imageId)));
        await _productWriteRepository.SaveChangesAsync();
        return Ok();
    }
}