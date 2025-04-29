using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    /// <summary>
    /// Validator for the <see cref="CreateSaleCommand"/>.
    /// </summary>
    /// <remarks>
    /// This class validates the properties of the <see cref="CreateSaleCommand"/> to ensure they meet the required business rules and constraints.
    /// </remarks>
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleCommandValidator"/> class.
        /// </summary>
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("Sale number must be provided.")
                .MaximumLength(50).WithMessage("Sale number must not exceed 50 characters.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID must be provided.");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name must be provided.")
                .MaximumLength(150).WithMessage("Customer name must not exceed 150 characters.");

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("Branch ID must be provided.");

            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("Branch name must be provided.")
                .MaximumLength(150).WithMessage("Branch name must not exceed 150 characters.");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total amount must be greater than or equal to zero.");
        }
    }
}
