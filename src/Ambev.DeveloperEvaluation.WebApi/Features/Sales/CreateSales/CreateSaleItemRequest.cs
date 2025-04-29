namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

/// <summary>
/// Represents a request to create a sale item with product details.
/// </summary>
/// <remarks>
/// This class contains information about a product being sold in a sale transaction,
/// including its unique identifier, name, and the quantity being purchased.
/// </remarks>
public class CreateSaleItemRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; } = Guid.Empty;
        
    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the product in the sale item.
    /// </summary>
    public int Quantity { get; set; } = 0;
}