namespace Ambev.DeveloperEvaluation.Application.Product.ListProducts;

/// <summary>
/// Represents the result data for a single product item in a paginated product listing.
/// </summary>
/// <remarks>
/// This DTO (Data Transfer Object) contains summarized information about a product, 
/// including identifiers, product name, description, price, status, and timestamps.
/// It is typically used in collections when listing multiple products.
/// </remarks>
public class ListProductsResult
{
    /// <summary>
    /// Unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Name of the product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Price of the product.
    /// </summary>
    public decimal Price { get; set; } = decimal.Zero;

    /// <summary>
    /// Indicates whether the product is active or not.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Timestamp of when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Timestamp of when the product was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}