using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// Validator for the ListProductsRequest.
/// </summary>
/// <remarks>
/// Ensures that the request parameters for listing Products are valid,
/// including checks for pagination (page number and page size) and sorting options.
/// </remarks>
public class ListProductsRequestValidator : AbstractValidator<ListProductsRequest>
{
    public ListProductsRequestValidator()
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