namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch
{
    /// <summary>
    /// Represents the request model for creating a new branch.
    /// This model contains the necessary data that the client must provide 
    /// when creating a new branch, including the name and address of the branch.
    /// </summary>
    public class CreateBranchRequest
    {
        /// <summary>
        /// Gets or sets the name of the branch.
        /// </summary>
        /// <value>The name of the branch. This value must be provided by the client when creating a new branch.</value>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the address of the branch.
        /// </summary>
        /// <value>The address of the branch. This value must be provided by the client when creating a new branch.</value>
        public string Address { get; set; } = string.Empty;
    }
}