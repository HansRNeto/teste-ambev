namespace Ambev.DeveloperEvaluation.Application.Branch.CreateBranch
{
    /// <summary>
    /// Represents the result of a branch creation operation.
    /// </summary>
    /// <remarks>
    /// This class contains the ID of the newly created branch, which is typically
    /// returned after a successful branch creation in the system.
    /// </remarks>
    public class CreateBranchResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created branch.
        /// </summary>
        public Guid Id { get; set; }
    }
}