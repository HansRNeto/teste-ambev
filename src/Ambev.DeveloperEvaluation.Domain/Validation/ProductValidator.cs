using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the <see cref="Product"/> entity.
/// </summary>
/// <remarks>
/// Ensures that required fields are populated and contain valid values.
/// </remarks>
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name must be provided.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("Product description must not exceed 255 characters.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Product price must be greater than or equal to zero.");

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("Creation date must be provided.");

        RuleFor(x => x.UpdatedAt)
            .NotEmpty().WithMessage("Update date must be provided.");
    }
}