using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branch.GetBranch;

/// <summary>
/// Validator for the <see cref="GetBranchCommand"/>.
/// </summary>
/// <remarks>
/// Ensures that the required fields for retrieving a Branch are provided and valid,
/// such as verifying that the Branch ID is not empty.
/// </remarks>
/// <seealso cref="GetBranchCommand"/>
public class GetBranchValidator : AbstractValidator<GetBranchCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBranchValidator"/> class
    /// and defines the validation rules for the <see cref="GetBranchCommand"/>.
    /// </summary>
    public GetBranchValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Branch ID is required");
    }
}