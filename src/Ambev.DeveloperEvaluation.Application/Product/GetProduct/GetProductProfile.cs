using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct;

/// <summary>
/// AutoMapper profile configuration for mapping <see cref="Domain.Entities.Product"/> to <see cref="GetProductResult"/>.
/// </summary>
/// <remarks>
/// Defines the mapping rules that transform a Product entity from the domain layer
/// into a result object used in the application layer for data transfer.
/// </remarks>
/// <seealso cref="Domain.Entities.Product"/>
/// <seealso cref="GetProductResult"/>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductProfile"/> class,
    /// configuring the mapping between <see cref="Domain.Entities.Product"/> and <see cref="GetProductResult"/>.
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Domain.Entities.Product, GetProductResult>();
    }
}