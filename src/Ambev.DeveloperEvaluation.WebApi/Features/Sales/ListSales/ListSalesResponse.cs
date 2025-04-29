namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Represents the result data for a single sale item in a paginated sales listing.
/// </summary>
/// <remarks>
/// This DTO (Data Transfer Object) contains summarized information about a sale, 
/// including identifiers, customer and branch details, sale status, total amount, and timestamps.
/// It is typically used in collections when listing multiple sales.
/// </remarks>
public class ListSalesResponse
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
    /// Date and time when the sale occurred.
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
    /// Total amount of the sale after applying any discounts.
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