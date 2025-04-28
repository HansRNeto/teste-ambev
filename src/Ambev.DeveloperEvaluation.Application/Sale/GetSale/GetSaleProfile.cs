using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

/// <summary>
/// AutoMapper profile configuration for mapping <see cref="Domain.Entities.Sale"/> to <see cref="GetSaleResult"/>.
/// </summary>
/// <remarks>
/// Defines the mapping rules that transform a sale entity from the domain layer
/// into a result object used in the application layer for data transfer.
/// </remarks>
/// <seealso cref="Domain.Entities.Sale"/>
/// <seealso cref="GetSaleResult"/>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleProfile"/> class,
    /// configuring the mapping between <see cref="Domain.Entities.Sale"/> and <see cref="GetSaleResult"/>.
    /// </summary>
    public GetSaleProfile()
    {
        CreateMap<Domain.Entities.Sale, GetSaleResult>();
    }
}