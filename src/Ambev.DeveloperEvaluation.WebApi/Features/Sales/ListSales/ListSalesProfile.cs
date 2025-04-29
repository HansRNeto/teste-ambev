using Ambev.DeveloperEvaluation.Application.Sale.ListSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// AutoMapper profile for configuring mappings related to listing sales.
/// </summary>
/// <remarks>
/// Defines the mappings between the ListSalesRequest and ListSalesCommand,
/// and between ListSalesResult and ListSalesResponse, enabling automatic
/// transformation between API layer models and application layer models.
/// </remarks>
public class ListSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSales feature
    /// </summary>
    public ListSalesProfile()
    {
        CreateMap<ListSalesRequest, ListSalesCommand>();
        CreateMap<ListSalesResult, ListSalesResponse>();
    }
}