using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customer.GetCustomer;

/// <summary>
/// Validator for the <see cref="GetCustomerCommand"/>.
/// </summary>
/// <remarks>
/// Ensures that the required fields for retrieving a Customer are provided and valid,
/// such as verifying that the Customer ID is not empty.
/// </remarks>
/// <seealso cref="GetCustomerCommand"/>
public class GetCustomerValidator : AbstractValidator<GetCustomerCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCustomerValidator"/> class
    /// and defines the validation rules for the <see cref="GetCustomerCommand"/>.
    /// </summary>
    public GetCustomerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Customer ID is required");
    }
}