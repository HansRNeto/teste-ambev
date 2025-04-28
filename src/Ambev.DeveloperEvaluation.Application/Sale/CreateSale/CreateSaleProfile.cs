using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    /// <summary>
    /// AutoMapper profile for creating mappings related to sale creation.
    /// </summary>
    /// <remarks>
    /// Maps between <see cref="CreateSaleCommand"/>, <see cref="Domain.Entities.Sale"/>, and <see cref="CreateSaleResult"/>.
    /// Used to simplify object transformation in the Sale creation workflow.
    /// </remarks>
    public class CreateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleProfile"/> class
        /// and configures entity mappings.
        /// </summary>
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Domain.Entities.Sale>();
            CreateMap<Domain.Entities.Sale, CreateSaleResult>();
        }
    }
}