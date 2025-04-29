namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Represents the response data returned when retrieving a Product.
/// </summary>
/// <remarks>
/// This DTO (Data Transfer Object) contains detailed information about a specific Product,
/// including identifiers, customer and branch details, total amount, status, and timestamps.
/// It is typically used in response models when querying Product data.
/// </remarks>
public class GetProductResponse
{
    /// <summary>
    /// Unique identifier of the Product.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Name of the Product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description providing additional details about the Product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Price of the Product.
    /// </summary>
    public decimal Price { get; set; } = decimal.Zero;

    /// <summary>
    /// Indicates whether the Product is currently active and available.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Timestamp indicating when the Product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Timestamp indicating the last time the Product was updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
