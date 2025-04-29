using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branch.UpdateBranch
{
    /// <summary>
    /// Command used to update a Branch in the system.
    /// </summary>
    /// <remarks>
    /// This command contains the necessary properties to update a Branch, including the Branch's name, email, 
    /// and active status. The <see cref="UpdateBranchResult"/> will be returned after the Branch is updated.
    /// </remarks>
    public class UpdateBranchCommand : IRequest<UpdateBranchResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created Branch.
        /// </summary>
        /// <remarks>
        /// This property holds the <see cref="Guid"/> that uniquely identifies the Branch in the system.
        /// The identifier is automatically generated during the Branch creation process.
        /// </remarks>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the branch.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the branch.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the active status of the branch.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the last update date of the branch.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Validates the current Branch creation command using the defined validator.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="ValidationResultDetail"/> object containing validation result status and any errors encountered during validation.
        /// </returns>
        /// <remarks>
        /// This method uses the <see cref="UpdateBranchCommandValidator"/> to ensure that the Branch creation request
        /// adheres to the necessary validation rules, returning details about the validation process.
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateBranchCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}