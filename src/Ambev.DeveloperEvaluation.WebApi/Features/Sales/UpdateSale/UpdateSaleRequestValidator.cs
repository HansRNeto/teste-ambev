using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Validator for the UpdateSaleRequest class.
    /// This class ensures that the properties of the UpdateSaleRequest are valid before they are processed.
    /// </summary>
    /// <remarks>
    /// The validator checks the following:
    /// - Name: Should be provided and cannot exceed 100 characters.
    /// - Email: Must be in a valid email format.
    /// - IsActive: Should be a boolean value, defaulting to true if not provided.
    /// </remarks>
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
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
        }
    }
}