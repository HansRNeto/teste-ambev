using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales
{
    /// <summary>
    /// Validates the request model for creating a sale, ensuring that required fields are provided 
    /// and that the data meets business rules such as valid dates, non-empty fields, and positive amounts.
    /// </summary>
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("Sale number is required.");

            RuleFor(x => x.CustomerId)
                .NotEqual(Guid.Empty).WithMessage("Customer ID is required.");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.");

            RuleFor(x => x.BranchId)
                .NotEqual(Guid.Empty).WithMessage("Branch ID is required.");

            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("Branch name is required.");

            RuleForEach(x => x.SaleItems)
                .ChildRules(items =>
                {
                    items.RuleFor(i => i.ProductId)
                        .NotEqual(Guid.Empty).WithMessage("Product ID is required.");

                    items.RuleFor(i => i.ProductName)
                        .NotEmpty().WithMessage("Product name is required.");

                    items.RuleFor(i => i.Quantity)
                        .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
                });

            RuleFor(x => x.SaleItems)
                .NotEmpty().WithMessage("At least one sale item is required.");
        }
    }
}