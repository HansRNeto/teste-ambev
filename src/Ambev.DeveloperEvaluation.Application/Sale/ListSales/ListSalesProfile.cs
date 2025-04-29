using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSales;

/// <summary>
/// Defines the AutoMapper profile for mapping between Sale entities and the ListSaleResult model.
/// </summary>
/// <remarks>
/// This profile specifies how a collection of <see cref="Domain.Entities.Sale"/> 
/// should be mapped to a collection of <see cref="ListSalesResult"/>.
/// It is used to transform data from the domain layer to the application layer
/// when listing sales.
/// </remarks>
public class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        CreateMap<Domain.Entities.Sale, ListSalesResult>();
    }
}