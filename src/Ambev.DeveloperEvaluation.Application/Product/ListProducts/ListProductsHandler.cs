using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.ListProducts;

/// <summary>
/// Handles the request to list products with pagination and sorting based on the provided <see cref="ListProductsCommand"/>.
/// </summary>
/// <remarks>
/// This handler validates the incoming request, retrieves a list of products from the repository,
/// applies pagination and sorting, and maps the result to a list of <see cref="ListProductsResult"/>.
/// If the request validation fails, a <see cref="ValidationException"/> is thrown.
/// </remarks>
/// <seealso cref="ListProductsCommand"/>
/// <seealso cref="ListProductsResult"/>
/// <seealso cref="IProductRepository"/>
public class ListProductsHandler : IRequestHandler<ListProductsCommand, List<ListProductsResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListProductsHandler"/> class.
    /// </summary>
    /// <param name="productRepository">The repository used to fetch the products.</param>
    /// <param name="mapper">The mapper used to transform the entities into result objects.</param>
    public ListProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the request to list products with pagination and sorting.
    /// </summary>
    /// <remarks>
    /// This handler processes the command to list products, validating the request parameters such as page number, 
    /// page size, sorting options, and direction. It retrieves the product list from the repository, applies the 
    /// necessary mapping to return a response, and handles validation errors if any are found in the request.
    /// </remarks>
    /// <param name="command">The command containing the pagination and sorting details for the product list.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
    /// <returns>
    /// A list of <see cref="ListProductsResult"/> representing the paginated and sorted products.
    /// If the request parameters are invalid, a validation exception is thrown.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown if the validation for the provided command parameters fails.
    /// </exception>
    public async Task<List<ListProductsResult>> Handle(ListProductsCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListProductsValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var products = await _productRepository.ListAsync(command.PageNumber, command.PageSize, command.SortBy,
            command.SortDirection, cancellationToken);

        return _mapper.Map<List<ListProductsResult>>(products);
    }
}
