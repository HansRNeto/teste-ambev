using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validates the input data for a Branch entity.
    /// </summary>
    /// <remarks>
    /// Ensures that the Branch entity's Name, Address, and IsActive properties meet the required validation rules:
    /// - Name: Must not be empty and must be between 2 and 100 characters.
    /// - Address: Must not be empty and must be between 5 and 200 characters.
    /// - IsActive: Must not be null.
    /// </remarks>
    public class BranchValidator : AbstractValidator<Branch>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BranchValidator"/> class.
        /// </summary>
        public BranchValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Branch name cannot be empty.")
                .Length(2, 100).WithMessage("Branch name must be between 2 and 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address cannot be empty.")
                .Length(5, 200).WithMessage("Address must be between 5 and 200 characters.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Active status is required.");
        }
    }
}