using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct
{
    /// <summary>
    /// Command used to update a Product in the system.
    /// </summary>
    /// <remarks>
    /// This command contains the necessary properties to update a Product, including the Product's name, email, 
    /// and active status. The <see cref="UpdateProductResult"/> will be returned after the Product is updated.
    /// </remarks>
    public class UpdateProductCommand : IRequest<UpdateProductResult>
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
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Validates the current Product creation command using the defined validator.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="ValidationResultDetail"/> object containing validation result status and any errors encountered during validation.
        /// </returns>
        /// <remarks>
        /// This method uses the <see cref="UpdateProductCommandValidator"/> to ensure that the Product creation request
        /// adheres to the necessary validation rules, returning details about the validation process.
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateProductCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}