using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct
{
    /// <summary>
    /// Handler for handling the update of a Product.
    /// </summary>
    /// <remarks>
    /// This class handles the <see cref="UpdateProductCommand"/> by validating the command,
    /// mapping it to a domain entity, and interacting with the repository to persist the Product data.
    /// It also maps the result into <see cref="UpdateProductResult"/>.
    /// </remarks>
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The repository for Product data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between DTOs and domain entities.</param>
        public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the <see cref="UpdateProductCommand"/> and returns the update result.
        /// </summary>
        /// <param name="command">The command containing the Product update data.</param>
        /// <param name="cancellationToken">The cancellation token for the operation.</param>
        /// <returns>The result of the Product update, wrapped in a <see cref="UpdateProductResult"/>.</returns>
        /// <exception cref="ValidationException">Thrown when the validation of the command fails.</exception>
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command,
            CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var existingProduct = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingProduct == null)
                throw new BadHttpRequestException($"Product with ID {command.Id} not found.");
            
            var product = _mapper.Map<Domain.Entities.Product>(command);

            var createdProduct = await _productRepository.UpdateAsync(product, cancellationToken);

            var result = _mapper.Map<UpdateProductResult>(createdProduct);
            return result;
        }
    }
}