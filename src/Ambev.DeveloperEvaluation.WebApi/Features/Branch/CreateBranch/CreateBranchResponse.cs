namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.CreateBranch
{
    /// <summary>
    /// Represents the response model for the creation of a new branch.
    /// This model contains the details of the branch that was created, 
    /// including its unique identifier, name, address, status, and timestamps for creation and updates.
    /// </summary>
    public class CreateBranchResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier for the branch.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the branch.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the address of the branch.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the active status of the branch.
        /// A value of true indicates the branch is active, false indicates it is inactive.
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Gets or sets the date and time when the branch was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Gets or sets the date and time when the branch was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}