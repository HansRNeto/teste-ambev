using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branch.CreateBranch
{
    /// <summary>
    /// Validator class for validating the <see cref="CreateBranchCommand"/> object.
    /// </summary>
    /// <remarks>
    /// This class is responsible for validating the input data when creating a branch. 
    /// It ensures that the provided branch name and address are not empty and the active status is properly set.
    /// </remarks>
    public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBranchCommandValidator"/> class.
        /// </summary>
        public CreateBranchCommandValidator()
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