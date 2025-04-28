namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer
{
    /// <summary>
    /// Represents the data required to create a new customer.
    /// This class contains the customer's basic information such as name, email, and active status.
    /// </summary>
    /// <remarks>
    /// The <see cref="CreateCustomerRequest"/> class is used to capture the necessary data to create a new customer in the system. 
    /// The customer must provide a name and email, and optionally specify their active status.
    /// </remarks>
    public class CreateCustomerRequest
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
    }
}