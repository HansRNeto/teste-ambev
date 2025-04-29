using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branch.GetBranch;

/// <summary>
/// AutoMapper profile configuration for mapping <see cref="Domain.Entities.Branch"/> to <see cref="GetBranchResult"/>.
/// </summary>
/// <remarks>
/// Defines the mapping rules that transform a Branch entity from the domain layer
/// into a result object used in the application layer for data transfer.
/// </remarks>
/// <seealso cref="Domain.Entities.Branch"/>
/// <seealso cref="GetBranchResult"/>
public class GetBranchProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBranchProfile"/> class,
    /// configuring the mapping between <see cref="Domain.Entities.Branch"/> and <see cref="GetBranchResult"/>.
    /// </summary>
    public GetBranchProfile()
    {
        CreateMap<Domain.Entities.Branch, GetBranchResult>();
    }
}