using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Sale transaction within the system, including details such as customer, branch, 
/// sale items, total amount, and status.
/// </summary>
/// <remarks>
/// This entity captures essential information about a Sale, 
/// including navigation properties for related entities like Customer, Branch, and SaleItems.
/// Provides a method to validate the Sale data according to business rules.
/// </remarks>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique sale number identifier.
    /// </summary>
    public string SaleNumber { get; set; }

    /// <summary>
    /// Gets or sets the date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the unique identifier of the customer associated with the sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer associated with the sale.
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the branch where the sale occurred.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the name of the branch where the sale occurred.
    /// </summary>
    public string BranchName { get; set; }

    /// <summary>
    /// Gets or sets the total monetary amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp of when the sale record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the UTC timestamp of when the sale record was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the list of items included in the sale.
    /// </summary>
    public virtual List<SaleItem> SaleItems { get; set; }

    /// <summary>
    /// Gets or sets the customer associated with the sale.
    /// </summary>
    public virtual Customer Customer { get; set; }

    /// <summary>
    /// Gets or sets the branch associated with the sale.
    /// </summary>
    public virtual Branch Branch { get; set; }

    /// <summary>
    /// Validates the current Sale entity based on defined business rules.
    /// </summary>
    /// <remarks>
    /// This method uses the <see cref="SaleValidator"/> to validate the entity's state and 
    /// returns a detailed validation result.
    /// </remarks>
    /// <returns>A <see cref="ValidationResultDetail"/> containing validation status and error details, if any.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}