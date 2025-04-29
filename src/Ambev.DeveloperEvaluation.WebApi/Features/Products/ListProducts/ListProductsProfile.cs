using Ambev.DeveloperEvaluation.Application.Product.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// AutoMapper profile for configuring mappings related to listing Products.
/// </summary>
/// <remarks>
/// Defines the mappings between the ListProductsRequest and ListProductsCommand,
/// and between ListProductsResult and ListProductsResponse, enabling automatic
/// transformation between API layer models and application layer models.
/// </remarks>
public class ListProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListProducts feature
    /// </summary>
    public ListProductsProfile()
    {
        CreateMap<ListProductsRequest, ListProductsCommand>();
        CreateMap<ListProductsResult, ListProductsResponse>();
    }
}