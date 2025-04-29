using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Product entity containing information such as name, description, price, 
/// activity status, and timestamps for creation and last update.
/// </summary>
/// <remarks>
/// The <see cref="Product"/> entity includes relationships with sales items and 
/// provides a method to validate its properties using <see cref="ProductValidator"/>.
/// </remarks>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the date and time when the product was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the collection of sale items associated with this product.
    /// </summary>
    public virtual ICollection<SaleItem> SaleItems { get; set; }

    /// <summary>
    /// Validates the current instance of the product.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing the validation result 
    /// and a list of validation errors, if any.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}