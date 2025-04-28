using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

/// <summary>
/// Validator for the <see cref="GetSaleCommand"/>.
/// </summary>
/// <remarks>
/// Ensures that the required fields for retrieving a sale are provided and valid,
/// such as verifying that the sale ID is not empty.
/// </remarks>
/// <seealso cref="GetSaleCommand"/>
public class GetSaleValidator : AbstractValidator<GetSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleValidator"/> class
    /// and defines the validation rules for the <see cref="GetSaleCommand"/>.
    /// </summary>
    public GetSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}