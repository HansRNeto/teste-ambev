using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct
{
    /// <summary>
    /// AutoMapper profile to configure the mappings for Product update operations.
    /// </summary>
    /// <remarks>
    /// This class defines the mapping between the command to update a Product <see cref="UpdateProductCommand"/> 
    /// and the <see cref="Domain.Entities.Product"/> entity, as well as the mapping 
    /// from the <see cref="Domain.Entities.Product"/> entity to the result <see cref="UpdateProductResult"/>.
    /// </remarks>
    public class UpdateProductProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductProfile"/> class.
        /// </summary>
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductCommand, Domain.Entities.Product>();
            CreateMap<Domain.Entities.Product, UpdateProductResult>();
        }
    }
}