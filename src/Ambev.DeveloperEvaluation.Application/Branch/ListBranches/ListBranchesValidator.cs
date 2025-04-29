using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branch.ListBranches;

/// <summary>
/// Validates the input data for listing Branches with pagination.
/// </summary>
/// <remarks>
/// Ensures that pagination parameters like PageNumber and PageSize are provided
/// and have acceptable values to prevent invalid queries.
/// </remarks>
public class ListBranchesValidator : AbstractValidator<ListBranchesCommand>
{
    public ListBranchesValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than zero.")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size must be less than or equal to 100.");
    }
}