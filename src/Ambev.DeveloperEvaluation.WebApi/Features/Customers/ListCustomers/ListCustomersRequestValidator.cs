using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.ListCustomers;

/// <summary>
/// Validator for the ListCustomersRequest.
/// </summary>
/// <remarks>
/// Ensures that the request parameters for listing Customers are valid,
/// including checks for pagination (page number and page size) and sorting options.
/// </remarks>
public class ListCustomersRequestValidator : AbstractValidator<ListCustomersRequest>
{
    public ListCustomersRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithMessage("PageSize must be between 1 and 100.");
    }
}