using Ambev.DeveloperEvaluation.Application.Branch.GetBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;

/// <summary>
/// Profile for mapping GetBranch feature requests to commands
/// </summary>
public class GetBranchProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetBranch feature
    /// </summary>
    public GetBranchProfile()
    {
        CreateMap<Guid, GetBranchCommand>()
            .ConstructUsing(id => new GetBranchCommand(id));
        CreateMap<GetBranchResult, GetBranchResponse>();
    }
}