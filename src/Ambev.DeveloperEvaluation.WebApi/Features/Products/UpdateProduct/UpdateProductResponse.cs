namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Represents the response returned after updating a Product.
/// </summary>
/// <remarks>
/// Contains the details of the newly created Product, including identification, contact information, status, and timestamps.
/// </remarks>
public class UpdateProductResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the created Product.
    /// </summary>
    /// <remarks>
    /// This property holds the <see cref="Guid"/> that uniquely identifies the Product in the system.
    /// The identifier is automatically generated during the Product creation process.
    /// </remarks>
    public Guid Id { get; set; }

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
    /// Gets or sets the date and time when the product was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}