using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
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