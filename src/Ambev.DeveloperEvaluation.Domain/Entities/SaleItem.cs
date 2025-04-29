using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item within a sale transaction, containing product information, pricing, and discount details.
/// </summary>
/// <remarks>
/// The <see cref="SaleItem"/> entity tracks individual product sales, quantities, unit prices, 
/// and any applicable discounts within a Sale. It supports validation to ensure data consistency 
/// using the <see cref="SaleItemValidator"/>.
/// </remarks>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier of the associated Sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated Product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name of the Product.
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the Product sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the Product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage applied to the Product.
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// Gets or sets the discount amount applied to the Product.
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets or sets the total amount for this SaleItem after discounts.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the SaleItem has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp of the SaleItem.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the last updated timestamp of the SaleItem.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Gets or sets the Sale associated with this SaleItem.
    /// </summary>
    public virtual Sale Sale { get; set; }
    
    /// <summary>
    /// Gets or sets the Product associated with this SaleItem.
    /// </summary>
    public virtual Product Product { get; set; }
    
    /// <summary>
    /// Validates the current SaleItem instance using <see cref="SaleItemValidator"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> indicating whether the entity is valid and listing any validation errors.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
