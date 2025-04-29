namespace Ambev.DeveloperEvaluation.Application.Customer.ListCustomers;

/// <summary>
/// Represents the result data for a single Customer item in a paginated Customer listing.
/// </summary>
/// <remarks>
/// This DTO (Data Transfer Object) contains summarized information about a Customer, 
/// including identifiers, Customer name, email, status, and timestamps.
/// It is typically used in collections when listing multiple Customers.
/// </remarks>
public class ListCustomersResult
{
    /// <summary>
    /// Unique identifier of the Customer.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Name of the Customer.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the Customer.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the Customer is active or not.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Timestamp of when the Customer was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Timestamp of when the Customer was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}