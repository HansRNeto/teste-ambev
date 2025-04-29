using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validates the input data for a <see cref="Customer"/> entity.
/// </summary>
/// <remarks>
/// This validator ensures that the <see cref="Customer.Name"/> entity has valid data:
/// <list type="bullet">
///     <item><description>The <see cref="Customer"/> must be non-empty and between 2 and 100 characters long.</description></item>
///     <item><description>The <see cref="Customer"/> must be a valid email address and cannot be empty.</description></item>
///     <item><description>The <see cref="Customer"/> status must not be null.</description></item>
/// </list>
/// If the validation fails, error messages are provided to indicate which fields are invalid.
/// </remarks>
/// <seealso cref="AbstractValidator{T}"/>
/// <seealso cref="FluentValidation"/>
/// <seealso cref="FluentValidation.Results"/>
public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name cannot be empty.")
            .Length(2, 100).WithMessage("Customer name must be between 2 and 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("Active status is required.");
    }
}