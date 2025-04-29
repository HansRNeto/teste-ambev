using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branch.GetBranch;

/// <summary>
/// Command to retrieve the details of a specific Branch based on its unique identifier.
/// </summary>
/// <remarks>
/// This command is used in the MediatR pipeline to request the retrieval of a Branch's information,
/// returning a <see cref="GetBranchResult"/> containing all related details.
/// </remarks>
/// <seealso cref="GetBranchResult"/>
public class GetBranchCommand : IRequest<GetBranchResult>
{
    /// <summary>
    /// Gets the unique identifier of the Branch to be retrieved.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBranchCommand"/> class with the specified Branch identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Branch.</param>
    public GetBranchCommand(Guid id)
    {
        Id = id;
    }
}