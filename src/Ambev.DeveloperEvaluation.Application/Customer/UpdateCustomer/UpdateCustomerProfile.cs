using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customer.UpdateCustomer
{
    /// <summary>
    /// AutoMapper profile to configure the mappings for customer update operations.
    /// </summary>
    /// <remarks>
    /// This class defines the mapping between the command to update a customer <see cref="UpdateCustomerCommand"/> 
    /// and the <see cref="Domain.Entities.Customer"/> entity, as well as the mapping 
    /// from the <see cref="Domain.Entities.Customer"/> entity to the result <see cref="UpdateCustomerResult"/>.
    /// </remarks>
    public class UpdateCustomerProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCustomerProfile"/> class.
        /// </summary>
        public UpdateCustomerProfile()
        {
            CreateMap<UpdateCustomerCommand, Domain.Entities.Customer>();
            CreateMap<Domain.Entities.Customer, UpdateCustomerResult>();
        }
    }
}