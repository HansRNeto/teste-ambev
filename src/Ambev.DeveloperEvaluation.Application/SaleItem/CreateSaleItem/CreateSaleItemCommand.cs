using Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItem.CreateSaleItem
{
    /// <summary>
    /// Command for creating a new sale item.
    /// </summary>
    /// <remarks>
    /// This command is used to create a sale item by providing the sale details including 
    /// product information, quantity, price, discount, and the total amount.
    /// </remarks>
    public class CreateSaleItemCommand : IRequest<CreateSaleItemResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to which this item belongs.
        /// </summary>
        public Guid SaleId { get; set; }
        
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public Guid ProductId { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product in the sale item.
        /// </summary>
        public int Quantity { get; set; } = 0;
        
        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; } = decimal.Zero;
        
        /// <summary>
        /// Gets or sets the discount percentage applied to the product.
        /// </summary>
        public decimal DiscountPercentage { get; set; } = decimal.Zero;
        
        /// <summary>
        /// Gets or sets the discount amount applied to the product.
        /// </summary>
        public decimal DiscountAmount { get; set; } = decimal.Zero;
        
        /// <summary>
        /// Gets or sets the total amount of the sale item after applying discounts.
        /// </summary>
        public decimal TotalAmount { get; set; } = decimal.Zero;

        /// <summary>
        /// Gets or sets a value indicating whether the sale item is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; } = false;
        
        /// <summary>
        /// Validates the current sale item creation command using the defined validator.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="ValidationResultDetail"/> object containing the validation status and any errors encountered during validation.
        /// </returns>
        /// <remarks>
        /// This method uses the <see cref="CreateSaleItemCommandValidator"/> to ensure that the sale item creation request
        /// adheres to the necessary validation rules, returning details about the validation process.
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleItemCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
