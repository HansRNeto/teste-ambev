using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branch.UpdateBranch
{
    /// <summary>
    /// Validator class for validating the <see cref="UpdateBranchCommand"/>.
    /// </summary>
    /// <remarks>
    /// This validator ensures that all the fields in the <see cref="UpdateBranchCommand"/>
    /// are valid before the command is processed. It includes validation for:
    /// - Name: Cannot be empty.
    /// - Email: Cannot be empty and must be a valid email format.
    /// - IsActive: Defaults to true, and no specific validation is needed for this property.
    /// </remarks>
    public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateBranchCommandValidator"/> class.
        /// </summary>
        public UpdateBranchCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Branch Id must be provided.");
            
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