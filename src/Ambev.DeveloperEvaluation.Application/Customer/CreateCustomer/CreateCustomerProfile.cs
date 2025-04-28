using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer
{
    /// <summary>
    /// AutoMapper profile to configure the mappings for customer creation operations.
    /// </summary>
    /// <remarks>
    /// This class defines the mapping between the command to create a customer <see cref="CreateCustomerCommand"/> 
    /// and the <see cref="Domain.Entities.Customer"/> entity, as well as the mapping 
    /// from the <see cref="Domain.Entities.Customer"/> entity to the result <see cref="CreateCustomerResult"/>.
    /// </remarks>
    public class CreateCustomerProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomerProfile"/> class.
        /// </summary>
        public CreateCustomerProfile()
        {
            CreateMap<CreateCustomerCommand, Domain.Entities.Customer>();
            CreateMap<Domain.Entities.Customer, CreateCustomerResult>();
        }
    }
}