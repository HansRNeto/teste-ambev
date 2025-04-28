namespace Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer
{
    /// <summary>
    /// Represents the result of the customer creation operation.
    /// </summary>
    /// <remarks>
    /// This class contains the identifier of the newly created customer.
    /// The <see cref="Id"/> property represents the unique identifier assigned to the customer.
    /// </remarks>
    public class CreateCustomerResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created customer.
        /// </summary>
        /// <remarks>
        /// This property holds the <see cref="Guid"/> that uniquely identifies the customer in the system.
        /// </remarks>
        public Guid Id { get; set; }
    }
}