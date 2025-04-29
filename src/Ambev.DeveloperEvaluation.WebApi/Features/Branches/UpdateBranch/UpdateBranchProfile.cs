using Ambev.DeveloperEvaluation.Application.Branch.UpdateBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch
{
    /// <summary>
    /// Defines the mapping profile for updating a Branch.
    /// This class configures the mappings between the <see cref="UpdateBranchRequest"/>, <see cref="UpdateBranchCommand"/>, <see cref="UpdateBranchResult"/>, and <see cref="UpdateBranchResponse"/> classes.
    /// </summary>
    /// <remarks>
    /// The <see cref="UpdateBranch.UpdateBranchProfile"/> class uses AutoMapper to define the object-to-object mappings needed for updating a Branch.
    /// It maps the request DTO (<see cref="UpdateBranchRequest"/>) to the command DTO (<see cref="UpdateBranchCommand"/>) and the result from the application layer (<see cref="UpdateBranchResult"/>) to the response DTO (<see cref="UpdateBranchResponse"/>).
    /// </remarks>
    public class UpdateBranchProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateBranch.UpdateBranchProfile"/> class.
        /// </summary>
        public UpdateBranchProfile()
        {
            CreateMap<UpdateBranchRequest, UpdateBranchCommand>();
            CreateMap<UpdateBranchResult, UpdateBranchResponse>();
        }
    }
}