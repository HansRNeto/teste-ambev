using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a branch in the system.
    /// </summary>
    /// <remarks>
    /// A branch is associated with various sales and has properties for its name, address, and active status.
    /// It also includes timestamps for creation and last update.
    /// The validation for the branch ensures that the name, address, and active status meet specific rules.
    /// </remarks>
    public class Branch : BaseEntity
    {
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
        /// Gets or sets the creation date of the branch.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update date of the branch.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the list of sales associated with the branch.
        /// </summary>
        public virtual List<Sale> Sales { get; set; }

        /// <summary>
        /// Validates the branch entity based on predefined rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing the validation result and errors if any.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new BranchValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
