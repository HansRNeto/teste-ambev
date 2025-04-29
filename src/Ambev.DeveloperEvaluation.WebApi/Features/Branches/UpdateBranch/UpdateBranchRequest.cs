namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch
{
    /// <summary>
    /// Represents the data required to update a Branch.
    /// This class contains the Branch's basic information such as name, email, and active status.
    /// </summary>
    /// <remarks>
    /// The <see cref="UpdateBranchRequest"/> class is used to capture the necessary data to update a Branch in the system. 
    /// The Branch must provide a name and email, and optionally specify their active status.
    /// </remarks>
    public class UpdateBranchRequest
    {
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
        public bool? IsActive { get; set; } = true;
    }
}