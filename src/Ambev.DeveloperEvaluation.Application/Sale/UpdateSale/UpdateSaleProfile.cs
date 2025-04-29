using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale
{
    /// <summary>
    /// AutoMapper profile to configure the mappings for Sale update operations.
    /// </summary>
    /// <remarks>
    /// This class defines the mapping between the command to update a Sale <see cref="UpdateSaleCommand"/> 
    /// and the <see cref="Domain.Entities.Sale"/> entity, as well as the mapping 
    /// from the <see cref="Domain.Entities.Sale"/> entity to the result <see cref="UpdateSaleResult"/>.
    /// </remarks>
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleProfile"/> class.
        /// </summary>
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleCommand, Domain.Entities.Sale>();
            CreateMap<Domain.Entities.Sale, UpdateSaleResult>();
        }
    }
}