using Ambev.DeveloperEvaluation.Application.Branch.CreateBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch
{
    /// <summary>
    /// AutoMapper profile class for mapping between CreateBranchRequest, CreateBranchCommand, 
    /// and CreateBranchResult to CreateBranchResponse.
    /// </summary>
    public class CreateBranchProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBranchProfile"/> class.
        /// This profile configures the mapping between the request and response models and 
        /// their corresponding command and result models.
        /// </summary>
        public CreateBranchProfile()
        {
            CreateMap<CreateBranchRequest, CreateBranchCommand>();
            CreateMap<CreateBranchResult, CreateBranchResponse>();
        }
    }
}