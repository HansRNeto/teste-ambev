using Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branch.CreateBranch
{
    /// <summary>
    /// Command for creating a new branch.
    /// </summary>
    /// <remarks>
    /// This command represents the request to create a new branch, containing the necessary data such as the branch's name,
    /// address, and its active status. It is used to pass data to the handler that processes the creation of the branch.
    /// </remarks>
    public class CreateBranchCommand : IRequest<CreateBranchResult>
    {
        /// <summary>
        /// Gets or sets the name of the branch.
        /// </summary>
        /// <value>The name of the branch.</value>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the address of the branch.
        /// </summary>
        /// <value>The address of the branch.</value>
        public string Address { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the active status of the branch.
        /// </summary>
        /// <value>A boolean indicating if the branch is active.</value>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Gets or sets the creation date of the branch.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the last update date of the branch.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Validates the current branch creation command using the defined validator.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="ValidationResultDetail"/> object containing the validation status and any errors encountered during validation.
        /// </returns>
        /// <remarks>
        /// This method uses the <see cref="CreateBranchCommandValidator"/> to ensure that the branch creation request
        /// adheres to the necessary validation rules, returning details about the validation process.
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new CreateBranchCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}