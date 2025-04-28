namespace Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer
{
    /// <summary>
    /// Represents the result of the customer creation operation.
    /// </summary>
    /// <remarks>
    /// This class contains the details of the customer created in the system.
    /// It includes the customer’s unique identifier and other relevant information such as name, email, 
    /// and status of the customer.
    /// The <see cref="Id"/> property represents the unique identifier assigned to the customer, while 
    /// the <see cref="Name"/>, <see cref="Email"/>, <see cref="IsActive"/>, <see cref="CreatedAt"/>, and 
    /// <see cref="UpdatedAt"/> properties hold the additional customer data.
    /// </remarks>
    public class CreateCustomerResult
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
        /// Gets or sets the name of the created customer.
        /// </summary>
        /// <remarks>
        /// This property holds the name of the customer as provided during the creation process.
        /// The name should not be empty or null.
        /// </remarks>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the created customer.
        /// </summary>
        /// <remarks>
        /// This property holds the email address of the customer.
        /// It should be in a valid email format and should not be empty or null.
        /// </remarks>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the created customer is active.
        /// </summary>
        /// <remarks>
        /// This property indicates the customer's current status. The default value is <c>true</c>.
        /// If <c>false</c>, the customer is considered inactive in the system.
        /// </remarks>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the timestamp of when the customer was created.
        /// </summary>
        /// <remarks>
        /// This property holds the date and time when the customer was created in the system.
        /// It is automatically set to the current UTC time when the customer is created.
        /// </remarks>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the timestamp of the last update made to the customer.
        /// </summary>
        /// <remarks>
        /// This property holds the date and time of the last update to the customer’s details.
        /// It is automatically set to the current UTC time when the customer is created.
        /// </remarks>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
