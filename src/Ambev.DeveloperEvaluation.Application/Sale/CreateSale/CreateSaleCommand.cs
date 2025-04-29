using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.SaleItem.CreateSaleItem;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

/// <summary>
/// Command to create a new sale record.
/// </summary>
/// <remarks>
/// This command encapsulates all the necessary information to create a sale,
/// including customer and branch external references, sale date, total amount, and cancellation status.
/// </remarks>
/// <see cref="CreateSaleResult"/>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Unique identifier for the sale (business code, not database ID).
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;
    
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
    
    /// <summary>
    /// Gets or sets the creation date of the sale.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last update date of the sale.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// List of sale items associated with this sale.
    /// </summary>
    public ICollection<CreateSaleItemCommand> SaleItems { get; set; } = new List<CreateSaleItemCommand>();
    
    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}