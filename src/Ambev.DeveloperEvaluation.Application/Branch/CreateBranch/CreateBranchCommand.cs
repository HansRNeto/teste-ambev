using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branch.CreateBranch
{
    /// <summary>
    /// Command for creating a new branch.
    /// </summary>
    /// <remarks>
    /// This command represents the request to create a new branch, containing the necessary data such as the branch's name,
    /// address, and its active status. It is used to pass data to the handler that processes the creation of the branch.
    /// </remarks>
    public class CreateBranchCommand : IRequest<CreateBranchResult>
    {
        /// <summary>
        /// Gets or sets the name of the branch.
        /// </summary>
        /// <value>The name of the branch.</value>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the address of the branch.
        /// </summary>
        /// <value>The address of the branch.</value>
        public string Address { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the active status of the branch.
        /// </summary>
        /// <value>A boolean indicating if the branch is active.</value>
        public bool IsActive { get; set; } = true;
    }
}