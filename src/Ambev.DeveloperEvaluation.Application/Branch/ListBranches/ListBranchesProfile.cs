using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branch.ListBranches;

/// <summary>
/// Defines the AutoMapper profile for mapping between Branch entities and the ListBranchesResult model.
/// </summary>
/// <remarks>
/// This profile specifies how a collection of <see cref="Domain.Entities.Branch"/> 
/// should be mapped to a collection of <see cref="ListBranchesResult"/>.
/// It is used to transform data from the domain layer to the application layer
/// when listing Branches.
/// </remarks>
public class ListBranchesProfile : Profile
{
    public ListBranchesProfile()
    {
        CreateMap<Domain.Entities.Branch, ListBranchesResult>();
    }
}