using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branch.UpdateBranch
{
    /// <summary>
    /// AutoMapper profile to configure the mappings for Branch update operations.
    /// </summary>
    /// <remarks>
    /// This class defines the mapping between the command to update a Branch <see cref="UpdateBranchCommand"/> 
    /// and the <see cref="Domain.Entities.Branch"/> entity, as well as the mapping 
    /// from the <see cref="Domain.Entities.Branch"/> entity to the result <see cref="UpdateBranchResult"/>.
    /// </remarks>
    public class UpdateBranchProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateBranchProfile"/> class.
        /// </summary>
        public UpdateBranchProfile()
        {
            CreateMap<UpdateBranchCommand, Domain.Entities.Branch>();
            CreateMap<Domain.Entities.Branch, UpdateBranchResult>();
        }
    }
}