using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer
{
    /// <summary>
    /// Validator class for validating the <see cref="CreateCustomerCommand"/>.
    /// </summary>
    /// <remarks>
    /// This validator ensures that all the fields in the <see cref="CreateCustomerCommand"/>
    /// are valid before the command is processed. It includes validation for:
    /// - Name: Cannot be empty.
    /// - Email: Cannot be empty and must be a valid email format.
    /// - IsActive: Defaults to true, and no specific validation is needed for this property.
    /// </remarks>
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomerCommandValidator"/> class.
        /// </summary>
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Customer name must be provided.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Customer email must be provided.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Customer active status must be provided.");
        }
    }
}