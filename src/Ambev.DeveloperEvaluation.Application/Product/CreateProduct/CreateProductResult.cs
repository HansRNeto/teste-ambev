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
    }
}