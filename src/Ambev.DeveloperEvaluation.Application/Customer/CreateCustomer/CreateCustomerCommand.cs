using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer
{
    /// <summary>
    /// Command used to create a new customer in the system.
    /// </summary>
    /// <remarks>
    /// This command contains the necessary properties to create a new customer, including the customer's name, email, 
    /// and active status. The <see cref="CreateCustomerResult"/> will be returned after the customer is created.
    /// </remarks>
    public class CreateCustomerCommand : IRequest<CreateCustomerResult>
    {
        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer. It should not be empty.
        /// </value>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        /// <value>
        /// The email address of the customer. It should not be empty.
        /// </value>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets a value indicating whether the customer is active.
        /// </summary>
        /// <value>
        /// A boolean value indicating whether the customer is active. The default value is <c>true</c>.
        /// </value>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Gets or sets the creation date of the customer.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the last update date of the customer.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Validates the current customer creation command using the defined validator.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="ValidationResultDetail"/> object containing validation result status and any errors encountered during validation.
        /// </returns>
        /// <remarks>
        /// This method uses the <see cref="CreateCustomerCommandValidator"/> to ensure that the customer creation request
        /// adheres to the necessary validation rules, returning details about the validation process.
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}