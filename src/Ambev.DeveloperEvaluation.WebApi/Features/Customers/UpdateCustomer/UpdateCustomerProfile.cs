using Ambev.DeveloperEvaluation.Application.Customer.UpdateCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer
{
    /// <summary>
    /// Defines the mapping profile for updating a customer.
    /// This class configures the mappings between the <see cref="UpdateCustomerRequest"/>, <see cref="UpdateCustomerCommand"/>, <see cref="UpdateCustomerResult"/>, and <see cref="UpdateCustomerResponse"/> classes.
    /// </summary>
    /// <remarks>
    /// The <see cref="UpdateCustomer.UpdateCustomerProfile"/> class uses AutoMapper to define the object-to-object mappings needed for updating a customer.
    /// It maps the request DTO (<see cref="UpdateCustomerRequest"/>) to the command DTO (<see cref="UpdateCustomerCommand"/>) and the result from the application layer (<see cref="UpdateCustomerResult"/>) to the response DTO (<see cref="UpdateCustomerResponse"/>).
    /// </remarks>
    public class UpdateCustomerProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCustomer.UpdateCustomerProfile"/> class.
        /// </summary>
        public UpdateCustomerProfile()
        {
            CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>();
            CreateMap<UpdateCustomerResult, UpdateCustomerResponse>();
        }
    }
}