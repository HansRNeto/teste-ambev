using Ambev.DeveloperEvaluation.Application.Product.GetProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Profile for mapping GetProduct feature requests to commands
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct feature
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Guid, GetProductCommand>()
            .ConstructUsing(id => new GetProductCommand(id));
        CreateMap<GetProductResult, GetProductResponse>();
    }
}