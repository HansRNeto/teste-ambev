namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct
{
    /// <summary>
    /// Represents the result of the Product update operation.
    /// </summary>
    /// <remarks>
    /// This class contains the details of the Product updated in the system.
    /// It includes the Product’s unique identifier and other relevant information such as name, email, 
    /// and status of the Product.
    /// The <see cref="Id"/> property represents the unique identifier assigned to the Product, while 
    /// the <see cref="Name"/>, <see cref="Email"/>, <see cref="IsActive"/>, and 
    /// <see cref="UpdatedAt"/> properties hold the additional Product data.
    /// </remarks>
    public class UpdateProductResult
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
}
