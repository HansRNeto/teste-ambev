using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.DeleteBranch;

public class DeleteBranchProfile : Profile
{
    public DeleteBranchProfile()
    {
        CreateMap<Guid, DeleteBranchCommand>()
            .ConstructUsing(id => new DeleteBranchCommand(id));
    }
}