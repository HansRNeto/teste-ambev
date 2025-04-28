using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branch.CreateBranch
{
    /// <summary>
    /// AutoMapper profile for mapping between <see cref="CreateBranchCommand"/>, <see cref="Domain.Entities.Branch"/>, and <see cref="CreateBranchResult"/>.
    /// </summary>
    /// <remarks>
    /// This class is used to define the mappings for converting the <see cref="CreateBranchCommand"/> to a <see cref="Domain.Entities.Branch"/>
    /// and for mapping a <see cref="Domain.Entities.Branch"/> to the <see cref="CreateBranchResult"/>.
    /// </remarks>
    public class CreateBranchProfile : Profile  
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBranchProfile"/> class with the mappings defined.
        /// </summary>
        public CreateBranchProfile()
        {
            CreateMap<CreateBranchCommand, Domain.Entities.Branch>();
            CreateMap<Domain.Entities.Branch, CreateBranchResult>();
        }
    }
}