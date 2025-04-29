using Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    /// <summary>
    /// Defines the mapping profile for updating a Product.
    /// This class configures the mappings between the <see cref="UpdateProductRequest"/>, <see cref="UpdateProductCommand"/>, <see cref="UpdateProductResult"/>, and <see cref="UpdateProductResponse"/> classes.
    /// </summary>
    /// <remarks>
    /// The <see cref="UpdateProduct.UpdateProductProfile"/> class uses AutoMapper to define the object-to-object mappings needed for updating a Product.
    /// It maps the request DTO (<see cref="UpdateProductRequest"/>) to the command DTO (<see cref="UpdateProductCommand"/>) and the result from the application layer (<see cref="UpdateProductResult"/>) to the response DTO (<see cref="UpdateProductResponse"/>).
    /// </remarks>
    public class UpdateProductProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProduct.UpdateProductProfile"/> class.
        /// </summary>
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<UpdateProductResult, UpdateProductResponse>();
        }
    }
}