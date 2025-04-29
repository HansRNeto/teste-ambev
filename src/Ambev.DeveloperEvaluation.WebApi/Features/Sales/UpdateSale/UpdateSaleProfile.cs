using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Defines the mapping profile for updating a Sale.
    /// This class configures the mappings between the <see cref="UpdateSaleRequest"/>, <see cref="UpdateSaleCommand"/>, <see cref="UpdateSaleResult"/>, and <see cref="UpdateSaleResponse"/> classes.
    /// </summary>
    /// <remarks>
    /// The <see cref="UpdateSale.UpdateSaleProfile"/> class uses AutoMapper to define the object-to-object mappings needed for updating a Sale.
    /// It maps the request DTO (<see cref="UpdateSaleRequest"/>) to the command DTO (<see cref="UpdateSaleCommand"/>) and the result from the application layer (<see cref="UpdateSaleResult"/>) to the response DTO (<see cref="UpdateSaleResponse"/>).
    /// </remarks>
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSale.UpdateSaleProfile"/> class.
        /// </summary>
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        }
    }
}