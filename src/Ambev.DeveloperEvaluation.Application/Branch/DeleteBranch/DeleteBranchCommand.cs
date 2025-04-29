using Ambev.DeveloperEvaluation.Application.Branch.DeleteBranch;
using MediatR;

/// <summary>
/// Represents the command to delete an existing Branch by its unique identifier.
/// </summary>
/// <remarks>
/// This command encapsulates the ID of the Branch to be deleted from the system.
/// </remarks>
public class DeleteBranchCommand : IRequest<DeleteBranchResponse>
{
    /// <summary>
    /// Gets the unique identifier of the Branch to be deleted.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBranchCommand"/> class with the specified Branch ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Branch to delete.</param>
    public DeleteBranchCommand(Guid id)
    {
        Id = id;
    }
}