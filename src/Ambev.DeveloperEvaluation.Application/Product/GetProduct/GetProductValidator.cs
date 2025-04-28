using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct;

/// <summary>
/// Validator for the <see cref="GetProductCommand"/>.
/// </summary>
/// <remarks>
/// Ensures that the required fields for retrieving a Product are provided and valid,
/// such as verifying that the Product ID is not empty.
/// </remarks>
/// <seealso cref="GetProductCommand"/>
public class GetProductValidator : AbstractValidator<GetProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductValidator"/> class
    /// and defines the validation rules for the <see cref="GetProductCommand"/>.
    /// </summary>
    public GetProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}