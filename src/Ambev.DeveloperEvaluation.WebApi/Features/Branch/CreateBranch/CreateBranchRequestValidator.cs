using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.CreateBranch
{
    /// <summary>
    /// Validates the request model for creating a new branch, ensuring that required fields are provided
    /// and meet the business rules, such as non-empty values for the name and address.
    /// </summary>
    public class CreateBranchRequestValidator : AbstractValidator<CreateBranchRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBranchRequestValidator"/> class.
        /// </summary>
        public CreateBranchRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Branch name is required.")
                .Length(3, 100).WithMessage("Branch name must be between 3 and 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Branch address is required.")
                .Length(5, 200).WithMessage("Branch address must be between 5 and 200 characters.");
        }
    }
}