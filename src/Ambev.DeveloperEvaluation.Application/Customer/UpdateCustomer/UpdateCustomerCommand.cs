using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customer.UpdateCustomer
{
    /// <summary>
    /// Command used to update a customer in the system.
    /// </summary>
    /// <remarks>
    /// This command contains the necessary properties to update a customer, including the customer's name, email, 
    /// and active status. The <see cref="UpdateCustomerResult"/> will be returned after the customer is updated.
    /// </remarks>
    public class UpdateCustomerCommand : IRequest<UpdateCustomerResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created customer.
        /// </summary>
        /// <remarks>
        /// This property holds the <see cref="Guid"/> that uniquely identifies the customer in the system.
        /// The identifier is automatically generated during the customer creation process.
        /// </remarks>
        public Guid Id { get; set; }
        
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
        /// Gets or sets the timestamp indicating when the customer information was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Validates the current customer creation command using the defined validator.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="ValidationResultDetail"/> object containing validation result status and any errors encountered during validation.
        /// </returns>
        /// <remarks>
        /// This method uses the <see cref="UpdateCustomerCommandValidator"/> to ensure that the customer creation request
        /// adheres to the necessary validation rules, returning details about the validation process.
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateCustomerCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}