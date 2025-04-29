using Ambev.DeveloperEvaluation.Application.Customer.ListCustomers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.ListCustomers;

/// <summary>
/// AutoMapper profile for configuring mappings related to listing Customers.
/// </summary>
/// <remarks>
/// Defines the mappings between the ListCustomersRequest and ListCustomersCommand,
/// and between ListCustomersResult and ListCustomersResponse, enabling automatic
/// transformation between API layer models and application layer models.
/// </remarks>
public class ListCustomersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListCustomers feature
    /// </summary>
    public ListCustomersProfile()
    {
        CreateMap<ListCustomersRequest, ListCustomersCommand>();
        CreateMap<ListCustomersResult, ListCustomersResponse>();
    }
}