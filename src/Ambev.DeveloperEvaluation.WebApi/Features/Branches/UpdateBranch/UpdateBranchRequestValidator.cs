using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch
{
    /// <summary>
    /// Validator for the UpdateBranchRequest class.
    /// This class ensures that the properties of the UpdateBranchRequest are valid before they are processed.
    /// </summary>
    /// <remarks>
    /// The validator checks the following:
    /// - Name: Should be provided and cannot exceed 100 characters.
    /// - Email: Must be in a valid email format.
    /// - IsActive: Should be a boolean value, defaulting to true if not provided.
    /// </remarks>
    public class UpdateBranchRequestValidator : AbstractValidator<UpdateBranchRequest>
    {
        public UpdateBranchRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Branch name must be provided.")
                .MaximumLength(100).WithMessage("Branch name must not exceed 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Branch address must be provided.")
                .MaximumLength(255).WithMessage("Branch address must not exceed 255 characters.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Branch active status must be specified.");
        }
    }
}