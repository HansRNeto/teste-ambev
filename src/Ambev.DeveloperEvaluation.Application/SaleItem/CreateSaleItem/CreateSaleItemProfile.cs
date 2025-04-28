using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SaleItem.CreateSaleItem
{
    /// <summary>
    /// AutoMapper profile for mapping between <see cref="CreateSaleItemCommand"/>, <see cref="Domain.Entities.SaleItem"/>, and <see cref="CreateSaleItemResult"/>.
    /// </summary>
    /// <remarks>
    /// This profile defines the mappings between:
    /// - <see cref="CreateSaleItemCommand"/> to <see cref="Domain.Entities.SaleItem"/>.
    /// - <see cref="Domain.Entities.SaleItem"/> to <see cref="CreateSaleItemResult"/>.
    /// </remarks>
    public class CreateSaleItemProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleItemProfile"/> class.
        /// </summary>
        public CreateSaleItemProfile()
        {
            CreateMap<CreateSaleItemCommand, Domain.Entities.SaleItem>();
            CreateMap<Domain.Entities.SaleItem, CreateSaleItemResult>();
        }
    }
}