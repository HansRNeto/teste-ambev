/// <summary>
/// Represents the request model for creating a sale, including details such as sale number, date, customer, 
/// branch, total amount, and cancellation status.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// Unique identifier for the sale (business code, not database ID).
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// External reference ID of the customer associated with the sale.
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Denormalized name of the customer at the time of the sale.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;
    
    /// <summary>
    /// External reference ID of the branch where the sale occurred.
    /// </summary>
    public Guid BranchId { get; set; }
    
    /// <summary>
    /// Denormalized name of the branch where the sale occurred.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;
    
    /// <summary>
    /// Total monetary amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; } = decimal.Zero;
    
    /// <summary>
    /// Indicates whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; } = false;
}