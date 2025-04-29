using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer
{
    /// <summary>
    /// Validator for the UpdateCustomerRequest class.
    /// This class ensures that the properties of the UpdateCustomerRequest are valid before they are processed.
    /// </summary>
    /// <remarks>
    /// The validator checks the following:
    /// - Name: Should be provided and cannot exceed 100 characters.
    /// - Email: Must be in a valid email format.
    /// - IsActive: Should be a boolean value, defaulting to true if not provided.
    /// </remarks>
    public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100).WithMessage("Customer name must not exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Customer email is required.")
                .EmailAddress().WithMessage("Please provide a valid email address.");

            RuleFor(x => x.IsActive)
                .NotEmpty().WithMessage("Customer active status is required.");
        }
    }
}