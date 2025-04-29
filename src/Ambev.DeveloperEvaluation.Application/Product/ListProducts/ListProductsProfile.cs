using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.ListProducts;

/// <summary>
/// Defines the AutoMapper profile for mapping between Product entities and the ListProductResult model.
/// </summary>
/// <remarks>
/// This profile specifies how a collection of <see cref="Domain.Entities.Product"/> 
/// should be mapped to a collection of <see cref="ListProductsResult"/>.
/// It is used to transform data from the domain layer to the application layer
/// when listing Products.
/// </remarks>
public class ListProductsProfile : Profile
{
    public ListProductsProfile()
    {
        CreateMap<Domain.Entities.Product, ListProductsResult>();
    }
}