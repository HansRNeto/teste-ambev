using Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer
{
    /// <summary>
    /// Represents the data required to update a customer.
    /// This class contains the customer's basic information such as name, email, and active status.
    /// </summary>
    /// <remarks>
    /// The <see cref="UpdateCustomerRequest"/> class is used to capture the necessary data to update a customer in the system. 
    /// The customer must provide a name and email, and optionally specify their active status.
    /// </remarks>
    public class UpdateCustomerRequest
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
        public bool IsActive { get; set; } = true;
    }
}