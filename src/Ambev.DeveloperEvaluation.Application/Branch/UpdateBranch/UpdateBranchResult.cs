namespace Ambev.DeveloperEvaluation.Application.Branch.UpdateBranch
{
    /// <summary>
    /// Represents the result of the Branch update operation.
    /// </summary>
    /// <remarks>
    /// This class contains the details of the Branch updated in the system.
    /// It includes the Branch’s unique identifier and other relevant information such as name, email, 
    /// and status of the Branch.
    /// The <see cref="Id"/> property represents the unique identifier assigned to the Branch, while 
    /// the <see cref="Name"/>, <see cref="Email"/>, <see cref="IsActive"/>, and 
    /// <see cref="UpdatedAt"/> properties hold the additional Branch data.
    /// </remarks>
    public class UpdateBranchResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created Branch.
        /// </summary>
        /// <remarks>
        /// This property holds the <see cref="Guid"/> that uniquely identifies the Branch in the system.
        /// The identifier is automatically generated during the Branch creation process.
        /// </remarks>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the branch.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the branch.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the active status of the branch.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the last update date of the branch.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
