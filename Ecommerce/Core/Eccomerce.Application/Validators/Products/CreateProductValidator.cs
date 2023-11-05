using Ecomerce.Application.ViewModels.Products;
using FluentValidation;

namespace Ecomerce.Application.Validators.Products;

public class CreateProductValidator : AbstractValidator<VM_Create_Product>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required")
            .MaximumLength(150)
            .MinimumLength(5)
            .WithMessage("Name must be between 5 and 150 characters");
        RuleFor(x => x.Stock)
            .NotEmpty()
            .NotNull()
            .WithMessage("Price is required")
            .Must(x => x >= 0)
            .WithMessage("Price must be greater than or equal to 0");

        RuleFor(x => x.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Price is required")
            .Must(x => x >= 0)
            .WithMessage("Price must be greater than or equal to 0");
    }
}