namespace Ambev.DeveloperEvaluation.Application.SaleItem.CreateSaleItem
{
    /// <summary>
    /// Represents the result of a created sale item.
    /// </summary>
    /// <remarks>
    /// This class contains the unique identifier of the newly created sale item.
    /// It is used to return the result after a sale item has been successfully created.
    /// </remarks>
    public class CreateSaleItemResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale item.
        /// </summary>
        public Guid Id { get; set; }
    }
}