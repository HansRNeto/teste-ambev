using Ambev.DeveloperEvaluation.Application.Product.GetProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customer.GetCustomer;

/// <summary>
/// AutoMapper profile configuration for mapping <see cref="Domain.Entities.Customer"/> to <see cref="GetCustomerResult"/>.
/// </summary>
/// <remarks>
/// Defines the mapping rules that transform a Customer entity from the domain layer
/// into a result object used in the application layer for data transfer.
/// </remarks>
/// <seealso cref="Domain.Entities.Customer"/>
/// <seealso cref="GetCustomerResult"/>
public class GetCustomerProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCustomerProfile"/> class,
    /// configuring the mapping between <see cref="Domain.Entities.Customer"/> and <see cref="GetCustomerResult"/>.
    /// </summary>
    public GetCustomerProfile()
    {
        CreateMap<Domain.Entities.Customer, GetCustomerResult>();
    }
}