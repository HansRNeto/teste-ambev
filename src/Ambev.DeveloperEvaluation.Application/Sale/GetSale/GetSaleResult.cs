namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

/// <summary>
/// Represents the result data returned when retrieving a sale.
/// </summary>
/// <remarks>
/// This DTO (Data Transfer Object) contains detailed information about a specific sale,
/// including identifiers, customer and branch information, total value, status, and timestamps.
/// It is typically used in response models when querying sale data.
/// </remarks>
public class GetSaleResult
{
    /// <summary>
    /// Unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Unique sale number generated for the transaction.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Unique identifier of the customer associated with the sale.
    /// </summary>
    public Guid CustomerId { get; set; } = Guid.Empty;

    /// <summary>
    /// Name of the customer associated with the sale.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Unique identifier of the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; } = Guid.Empty;

    /// <summary>
    /// Name of the branch where the sale was made.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Total amount of the sale after discounts.
    /// </summary>
    public decimal TotalAmount { get; set; } = decimal.Zero;

    /// <summary>
    /// Indicates whether the sale has been canceled.
    /// </summary>
    public bool IsCancelled { get; set; } = false;

    /// <summary>
    /// Timestamp when the sale record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Timestamp when the sale record was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
