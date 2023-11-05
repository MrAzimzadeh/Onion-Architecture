﻿using System.Net;
using Ecomerce.Application.Repositories.Products;
using Ecomerce.Application.RequestParameters;
using Ecomerce.Application.ViewModels.Products;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
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

    public ProductController
    (
        IProductWriteRepository productWriteRepository,
        IProductReadRepository productReadRepository,
        IWebHostEnvironment hostEnvironment)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _hostEnvironment = hostEnvironment;
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
    public async Task<IActionResult> Upload()
    {
        // wwwroot/resurce/product-images
        string uploadPath = Path.Combine(_hostEnvironment.WebRootPath, "resurce/product-images");

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        // Test 
        Random random = new();
        foreach (IFormFile file in Request.Form.Files)
        {
            string fullPath = Path.Combine(uploadPath, $"{random.NextDouble()}{Path.GetExtension(file.FileName)}");
            using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,
                bufferSize: 1024 * 1024, useAsync: false);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync(); // 
        }

        return Ok();
    }
}