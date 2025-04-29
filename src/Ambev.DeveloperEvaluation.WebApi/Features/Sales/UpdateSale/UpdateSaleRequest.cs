namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Represents the data required to update a Sale.
    /// This class contains the Sale's basic information such as name, email, and active status.
    /// </summary>
    /// <remarks>
    /// The <see cref="UpdateSaleRequest"/> class is used to capture the necessary data to update a Sale in the system. 
    /// The Sale must provide a name and email, and optionally specify their active status.
    /// </remarks>
    public class UpdateSaleRequest
    {
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
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; } = false;
    }
}