using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale
{
    /// <summary>
    /// Command used to update a Sale in the system.
    /// </summary>
    /// <remarks>
    /// This command contains the necessary properties to update a Sale, including the Sale's name, email, 
    /// and active status. The <see cref="UpdateSaleResult"/> will be returned after the Sale is updated.
    /// </remarks>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created Sale.
        /// </summary>
        /// <remarks>
        /// This property holds the <see cref="Guid"/> that uniquely identifies the Sale in the system.
        /// The identifier is automatically generated during the Sale creation process.
        /// </remarks>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Unique identifier for the sale (business code, not database ID).
        /// </summary>
        public string SaleNumber { get; set; }
    
        /// <summary>
        /// External reference ID of the customer associated with the sale.
        /// </summary>
        public Guid CustomerId { get; set; }
    
        /// <summary>
        /// Denormalized name of the customer at the time of the sale.
        /// </summary>
        public string CustomerName { get; set; }
    
        /// <summary>
        /// External reference ID of the branch where the sale occurred.
        /// </summary>
        public Guid BranchId { get; set; }
    
        /// <summary>
        /// Denormalized name of the branch where the sale occurred.
        /// </summary>
        public string BranchName { get; set; }
    
        /// <summary>
        /// Total monetary amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }
    
        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the Sale was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Validates the current Sale creation command using the defined validator.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="ValidationResultDetail"/> object containing validation result status and any errors encountered during validation.
        /// </returns>
        /// <remarks>
        /// This method uses the <see cref="UpdateSaleCommandValidator"/> to ensure that the Sale creation request
        /// adheres to the necessary validation rules, returning details about the validation process.
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}