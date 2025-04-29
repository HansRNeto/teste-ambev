using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

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
    /// External reference ID of the customer associated with the sale.
    /// </summary>
    public Guid CustomerId { get; set; } = Guid.Empty;
    
    /// <summary>
    /// Denormalized name of the customer at the time of the sale.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// External reference ID of the branch where the sale occurred.
    /// </summary>
    public Guid BranchId { get; set; } = Guid.Empty;
    
    /// <summary>
    /// Denormalized name of the branch where the sale occurred.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// List of sale items associated with this sale.
    /// </summary>
    public ICollection<CreateSaleItemRequest> SaleItems { get; set; }
}