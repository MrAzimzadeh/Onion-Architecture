﻿using Ecomerce.Application.Repositories.Products;
using MediatR;

namespace Ecomerce.Application.Features.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request,
        CancellationToken cancellationToken)
    {
        await _productWriteRepository.AddAsync(new()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
        });
        await _productWriteRepository.SaveChangesAsync();
        return new();
    }
}