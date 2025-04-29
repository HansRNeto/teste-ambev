using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.DeleteCustomer;

public class DeleteCustomerProfile : Profile
{
    public DeleteCustomerProfile()
    {
        CreateMap<Guid, DeleteCustomerCommand>()
            .ConstructUsing(id => new DeleteCustomerCommand(id));
    }
}