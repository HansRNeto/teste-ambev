namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;

/// <summary>
/// Represents the response returned after updating a Branch.
/// </summary>
/// <remarks>
/// Contains the details of the newly created Branch, including identification, contact information, status, and timestamps.
/// </remarks>
public class UpdateBranchResponse
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