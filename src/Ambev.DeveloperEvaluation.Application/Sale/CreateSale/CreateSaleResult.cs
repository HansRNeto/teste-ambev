namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

/// <summary>
/// Represents the response returned after successfully creating a new sale.
/// </summary>
/// <remarks>
/// This response contains detailed information about the created sale, including identifiers,
/// customer and branch details, financial amounts, and timestamps.
/// </remarks>
public class CreateSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created sale.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created sale in the system.</value>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the unique business number assigned to the sale.
    /// </summary>
    /// <value>A string representing the business-specific sale number.</value>
    public string SaleNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the date and time when the sale was made.
    /// </summary>
    /// <value>A <see cref="DateTime"/> indicating when the sale occurred.</value>
    public DateTime SaleDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Gets or sets the external reference ID of the customer associated with the sale.
    /// </summary>
    /// <value>A <see cref="Guid"/> that identifies the customer.</value>
    public Guid CustomerId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the name of the customer at the time of the sale.
    /// </summary>
    /// <value>A string containing the customer's name.</value>
    public string CustomerName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the external reference ID of the branch where the sale occurred.
    /// </summary>
    /// <value>A <see cref="Guid"/> that identifies the branch.</value>
    public Guid BranchId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the name of the branch where the sale occurred.
    /// </summary>
    /// <value>A string containing the branch's name.</value>
    public string BranchName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the total monetary amount of the sale.
    /// </summary>
    /// <value>A <see cref="decimal"/> representing the total sale value.</value>
    public decimal TotalAmount { get; set; } = decimal.Zero;

    /// <summary>
    /// Gets or sets a value indicating whether the sale has been cancelled.
    /// </summary>
    /// <value><c>true</c> if the sale is cancelled; otherwise, <c>false</c>.</value>
    public bool IsCancelled { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the timestamp when the sale record was created.
    /// </summary>
    /// <value>A UTC <see cref="DateTime"/> indicating when the sale was created.</value>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the timestamp when the sale record was last updated.
    /// </summary>
    /// <value>A UTC <see cref="DateTime"/> indicating the last update time of the sale.</value>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
