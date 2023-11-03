﻿using Ecomerce.Application.Repositories.Products;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    //ctor injection hell 
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;

    public ProductController
    (
        IProductWriteRepository productWriteRepository,
        IProductReadRepository productReadRepository
    )
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
    }


    [HttpGet("GetProducts")]
    public async Task<IActionResult> GetProducts()
    {
        await _productWriteRepository.AddRangeAsync(new()
        {
            new() { Id = Guid.NewGuid(), Name = "Product 1 ", Price = 100, CreatedDate = DateTime.UtcNow, Stock = 10 },
            new() { Id = Guid.NewGuid(), Name = "Product 2 ", Price = 200, CreatedDate = DateTime.UtcNow, Stock = 10 },
            new() { Id = Guid.NewGuid(), Name = "Product 3 ", Price = 300, CreatedDate = DateTime.UtcNow, Stock = 10 },
            new() { Id = Guid.NewGuid(), Name = "Product 4 ", Price = 400, CreatedDate = DateTime.UtcNow, Stock = 10 },
            new() { Id = Guid.NewGuid(), Name = "Product 5 ", Price = 500, CreatedDate = DateTime.UtcNow, Stock = 10 },
            new() { Id = Guid.NewGuid(), Name = "Product 6 ", Price = 600, CreatedDate = DateTime.UtcNow, Stock = 10 },
            new() { Id = Guid.NewGuid(), Name = "Product 7 ", Price = 700, CreatedDate = DateTime.UtcNow, Stock = 10 },
        });
        await _productWriteRepository.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("GetProductById")]
    public async Task<IActionResult> GetProductById(string id)
    {
        Product product =
            await _productReadRepository.GetByIdAsync("b2d0197a-c66d-4835-a447-333af9537256", tracking: false);
        product.Name = "salam";
        await _productWriteRepository.SaveChangesAsync();

        return Ok(product);
    }
}