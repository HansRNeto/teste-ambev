using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleItem.CreateSaleItem
{
    /// <summary>
    /// Validator for the <see cref="CreateSaleItemCommand"/> to ensure it meets validation rules.
    /// </summary>
    /// <remarks>
    /// This validator ensures that the sale item command contains valid information such as:
    /// - SaleId and ProductId are not empty.
    /// - ProductName is not empty and does not exceed a certain length.
    /// - Quantity is greater than zero.
    /// - UnitPrice, DiscountPercentage, and DiscountAmount are non-negative values.
    /// - TotalAmount should be calculated based on quantity, unit price, and discounts.
    /// </remarks>
    public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
    {
        public CreateSaleItemCommandValidator()
        {
            RuleFor(x => x.SaleId)
                .NotEmpty().WithMessage("Sale ID must be provided.");
                
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID must be provided.");
                
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name must be provided.")
                .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.");
                
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to zero.");
                
            RuleFor(x => x.DiscountPercentage)
                .InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.");
                
            RuleFor(x => x.DiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount amount must be greater than or equal to zero.");
                
            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total amount must be greater than or equal to zero.");
        }
    }
}
