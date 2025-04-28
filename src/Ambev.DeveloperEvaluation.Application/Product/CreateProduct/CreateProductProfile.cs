using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct
{
    /// <summary>
    /// AutoMapper profile for mapping between product-related objects.
    /// </summary>
    /// <remarks>
    /// Defines mappings between <see cref="CreateProductCommand"/>, <see cref="Domain.Entities.Product"/>,
    /// and <see cref="CreateProductResult"/> to facilitate object transformations during the product creation process.
    /// </remarks>
    /// <seealso cref="CreateProductCommand"/>
    /// <seealso cref="CreateProductResult"/>
    /// <seealso cref="Domain.Entities.Product"/>
    public class CreateProductProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductProfile"/> class and configures mappings.
        /// </summary>
        public CreateProductProfile()
        {
            CreateMap<CreateProductCommand, Domain.Entities.Product>();
            CreateMap<Domain.Entities.Product, CreateProductResult>();
        }
    }
}