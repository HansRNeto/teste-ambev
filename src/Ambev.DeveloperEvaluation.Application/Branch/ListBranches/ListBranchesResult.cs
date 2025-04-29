namespace Ambev.DeveloperEvaluation.Application.Branch.ListBranches;

/// <summary>
/// Represents the result data for a single Branch item in a paginated Branch listing.
/// </summary>
/// <remarks>
/// This DTO (Data Transfer Object) contains summarized information about a Branch, 
/// including identifiers, Branch name, address, status, and timestamps.
/// It is typically used in collections when listing multiple Branches.
/// </remarks>
public class ListBranchesResult
{
    /// <summary>
    /// Unique identifier of the Branch.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Name of the Branch.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Address of the Branch.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the Branch is active or not.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Timestamp of when the Branch was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Timestamp of when the Branch was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}