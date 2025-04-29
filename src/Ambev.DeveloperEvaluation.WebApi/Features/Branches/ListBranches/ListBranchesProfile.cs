using Ambev.DeveloperEvaluation.Application.Branch.ListBranches;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.ListBranches;

/// <summary>
/// AutoMapper profile for configuring mappings related to listing Branches.
/// </summary>
/// <remarks>
/// Defines the mappings between the ListBranchesRequest and ListBranchesCommand,
/// and between ListBranchesResult and ListBranchesResponse, enabling automatic
/// transformation between API layer models and application layer models.
/// </remarks>
public class ListBranchesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListBranches feature
    /// </summary>
    public ListBranchesProfile()
    {
        CreateMap<ListBranchesRequest, ListBranchesCommand>();
        CreateMap<ListBranchesResult, ListBranchesResponse>();
    }
}