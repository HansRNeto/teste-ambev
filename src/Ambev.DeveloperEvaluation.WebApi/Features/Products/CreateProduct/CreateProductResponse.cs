namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;

/// <summary>
/// Represents the response returned after creating a product.
/// </summary>
public class CreateProductResponse
{
    /// <summary>
    /// Unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }
    
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
    /// Indicates whether the product is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Date and time when the product was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}