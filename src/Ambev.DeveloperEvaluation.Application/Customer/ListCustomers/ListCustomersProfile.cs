using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customer.ListCustomers;

/// <summary>
/// Defines the AutoMapper profile for mapping between Customer entities and the ListCustomerResult model.
/// </summary>
/// <remarks>
/// This profile specifies how a collection of <see cref="Domain.Entities.Customer"/> 
/// should be mapped to a collection of <see cref="ListCustomersResult"/>.
/// It is used to transform data from the domain layer to the application layer
/// when listing Customers.
/// </remarks>
public class ListCustomersProfile : Profile
{
    public ListCustomersProfile()
    {
        CreateMap<Domain.Entities.Customer, ListCustomersResult>();
    }
}