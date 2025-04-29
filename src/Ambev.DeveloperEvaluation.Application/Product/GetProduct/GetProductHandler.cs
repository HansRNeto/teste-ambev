using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct;

/// <summary>
/// Handles the retrieval of a Product based on the provided <see cref="GetProductCommand"/>.
/// </summary>
/// <remarks>
/// This handler validates the incoming request, fetches the Product entity from the repository,
/// maps it to the <see cref="GetProductResult"/>, and returns the mapped result.
/// If the Product is not found, a <see cref="KeyNotFoundException"/> is thrown.
/// </remarks>
/// <seealso cref="GetProductCommand"/>
/// <seealso cref="GetProductResult"/>
/// <seealso cref="IProductRepository"/>
public class GetProductHandler : IRequestHandler<GetProductCommand, GetProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductHandler"/> class.
    /// </summary>
    /// <param name="productRepository"></param>
    /// <param name="mapper">The mapper used to transform the entity into a result object.</param>
    public GetProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the Product retrieval operation, performing validation, entity fetching, and result mapping.
    /// </summary>
    /// <param name="request">The command containing the ID of the Product to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A <see cref="GetProductResult"/> containing the details of the requested Product.</returns>
    /// <exception cref="ValidationException">Thrown if the request validation fails.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no Product is found with the given ID.</exception>
    public async Task<GetProductResult> Handle(GetProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        return _mapper.Map<GetProductResult>(product);
    }
}
