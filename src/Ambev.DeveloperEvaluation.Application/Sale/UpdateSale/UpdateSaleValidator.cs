using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale
{
    /// <summary>
    /// Validator class for validating the <see cref="UpdateSaleCommand"/>.
    /// </summary>
    /// <remarks>
    /// This validator ensures that all the fields in the <see cref="UpdateSaleCommand"/>
    /// are valid before the command is processed. It includes validation for:
    /// - Name: Cannot be empty.
    /// - Email: Cannot be empty and must be a valid email format.
    /// - IsActive: Defaults to true, and no specific validation is needed for this property.
    /// </remarks>
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleCommandValidator"/> class.
        /// </summary>
        public UpdateSaleCommandValidator()
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