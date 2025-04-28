namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct
{
    /// <summary>
    /// Represents the result of a successfully created product.
    /// </summary>
    /// <remarks>
    /// This class contains the identifier of the newly created product, typically returned
    /// after handling the <see cref="CreateProductCommand"/>.
    /// </remarks>
    /// <seealso cref="CreateProductCommand"/>
    /// <seealso cref="CreateProductHandler"/>
    public class CreateProductResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created product.
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
}