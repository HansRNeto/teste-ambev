namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer;

/// <summary>
/// Represents the response returned after updating a customer.
/// </summary>
/// <remarks>
/// Contains the details of the newly created customer, including identification, contact information, status, and timestamps.
/// </remarks>
public class UpdateCustomerResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the customer.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the email address of the customer.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a value indicating whether the customer is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the date and time when the customer was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}