
namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    /// <summary>
    /// Represents the data required to update a Product.
    /// This class contains the Product's basic information such as name, email, and active status.
    /// </summary>
    /// <remarks>
    /// The <see cref="UpdateProductRequest"/> class is used to capture the necessary data to update a Product in the system. 
    /// The Product must provide a name and email, and optionally specify their active status.
    /// </remarks>
    public class UpdateProductRequest
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    
        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    
        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; } = decimal.Zero;

        /// <summary>
        /// Gets or sets a value indicating whether the product is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}