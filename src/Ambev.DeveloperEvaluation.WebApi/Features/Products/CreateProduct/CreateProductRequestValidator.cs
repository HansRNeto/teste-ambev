using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;

/// <summary>
/// Validator class for <see cref="CreateProductRequest"/>.
/// </summary>
/// <remarks>
/// Validates the properties of a product creation request:
/// <list type="bullet">
///   <item><description><c>Name</c> must be provided and must not exceed 100 characters.</description></item>
///   <item><description><c>Description</c> must be provided and must not exceed 500 characters.</description></item>
///   <item><description><c>Price</c> must be greater than zero.</description></item>
/// </list>
/// Ensures that only valid data is submitted for creating a new product.
/// </remarks>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name must be provided.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description must be provided.")
            .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");
    }
}