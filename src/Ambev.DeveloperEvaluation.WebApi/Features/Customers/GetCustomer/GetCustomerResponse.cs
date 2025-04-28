namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetCustomer;

/// <summary>
/// Represents the response data returned when retrieving a Customer.
/// </summary>
/// <remarks>
/// This DTO (Data Transfer Object) contains detailed information about a specific Customer,
/// including identifiers, customer and branch details, total amount, status, and timestamps.
/// It is typically used in response models when querying Customer data.
/// </remarks>
public class GetCustomerResponse
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
    /// Indicates whether the Customer is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Timestamp indicating when the Customer record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Timestamp indicating the last time the Customer record was updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
