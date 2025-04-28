using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct
{
    /// <summary>
    /// Handles the creation of a new product by processing the <see cref="CreateProductCommand"/>.
    /// </summary>
    /// <remarks>
    /// This handler validates the incoming command, maps it to the domain entity,
    /// persists it using the <see cref="IProductRepository"/>, and returns the created product information.
    /// </remarks>
    /// <seealso cref="CreateProductCommand"/>
    /// <seealso cref="CreateProductResult"/>
    /// <seealso cref="IProductRepository"/>
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The repository used to persist product data.</param>
        /// <param name="mapper">The mapper used to transform command to entity and entity to result.</param>
        public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the product creation command, performing validation, persistence, and result mapping.
        /// </summary>
        /// <param name="command">The command containing the product information to be created.</param>
        /// <param name="cancellationToken">A cancellation token for the operation.</param>
        /// <returns>The result containing information about the created product.</returns>
        /// <exception cref="ValidationException">Thrown if the command fails validation.</exception>
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = _mapper.Map<Domain.Entities.Product>(command);

            var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
            var result = _mapper.Map<CreateProductResult>(createdProduct);
            return result;
        }
    }
}
