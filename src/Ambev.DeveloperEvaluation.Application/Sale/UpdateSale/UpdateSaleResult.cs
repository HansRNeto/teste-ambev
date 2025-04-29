namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale
{
    /// <summary>
    /// Represents the result of the Sale update operation.
    /// </summary>
    /// <remarks>
    /// This class contains the details of the Sale updated in the system.
    /// It includes the Sale’s unique identifier and other relevant information such as name, email, 
    /// and status of the Sale.
    /// The <see cref="Id"/> property represents the unique identifier assigned to the Sale, while 
    /// <see cref="UpdatedAt"/> properties hold the additional Sale data.
    /// </remarks>
    public class UpdateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the newly created sale.
        /// </summary>
        /// <value>A GUID that uniquely identifies the created sale in the system.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique business number assigned to the sale.
        /// </summary>
        /// <value>A string representing the business-specific sale number.</value>
        public string SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the external reference ID of the customer associated with the sale.
        /// </summary>
        /// <value>A <see cref="Guid"/> that identifies the customer.</value>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer at the time of the sale.
        /// </summary>
        /// <value>A string containing the customer's name.</value>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the external reference ID of the branch where the sale occurred.
        /// </summary>
        /// <value>A <see cref="Guid"/> that identifies the branch.</value>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the name of the branch where the sale occurred.
        /// </summary>
        /// <value>A string containing the branch's name.</value>
        public string BranchName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sale has been cancelled.
        /// </summary>
        /// <value><c>true</c> if the sale is cancelled; otherwise, <c>false</c>.</value>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the sale record was last updated.
        /// </summary>
        /// <value>A UTC <see cref="DateTime"/> indicating the last update time of the sale.</value>
        public DateTime UpdatedAt { get; set; }
    }
}