namespace Ambev.DeveloperEvaluation.Application.Branch.GetBranch;

/// <summary>
/// Represents the result data returned when retrieving a Branch.
/// </summary>
/// <remarks>
/// This DTO (Data Transfer Object) contains detailed information about a specific Branch,
/// including identifiers, name, address, status, and timestamps.
/// It is typically used in response models when querying Branch data.
/// </remarks>
public class GetBranchResult
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
    /// Physical address of the Branch.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the Branch is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Timestamp indicating when the Branch record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Timestamp indicating the last time the Branch record was updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}