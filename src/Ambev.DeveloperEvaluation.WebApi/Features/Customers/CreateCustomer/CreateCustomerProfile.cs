using Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer
{
    /// <summary>
    /// Defines the mapping profile for creating a customer.
    /// This class configures the mappings between the <see cref="CreateCustomerRequest"/>, <see cref="CreateCustomerCommand"/>, <see cref="CreateCustomerResult"/>, and <see cref="CreateCustomerResponse"/> classes.
    /// </summary>
    /// <remarks>
    /// The <see cref="CreateCustomerProfile"/> class uses AutoMapper to define the object-to-object mappings needed for creating a customer.
    /// It maps the request DTO (<see cref="CreateCustomerRequest"/>) to the command DTO (<see cref="CreateCustomerCommand"/>) and the result from the application layer (<see cref="CreateCustomerResult"/>) to the response DTO (<see cref="CreateCustomerResponse"/>).
    /// </remarks>
    public class CreateCustomerProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomerProfile"/> class.
        /// </summary>
        public CreateCustomerProfile()
        {
            CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
            CreateMap<CreateCustomerResult, CreateCustomerResponse>();
        }
    }
}