namespace Ambev.DeveloperEvaluation.Application.Branch.CreateBranch
{
    /// <summary>
    /// Represents the result of a branch creation operation.
    /// </summary>
    /// <remarks>
    /// This class contains the details of a branch that has been successfully created, including its unique identifier, 
    /// name, address, active status, and timestamps for creation and last update.
    /// </remarks>
    public class CreateBranchResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created branch.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the created branch.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the address of the created branch.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the branch is active.
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