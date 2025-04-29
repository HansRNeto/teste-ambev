using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

/// <summary>
/// Handles the retrieval of a sale based on the provided <see cref="GetSaleCommand"/>.
/// </summary>
/// <remarks>
/// This handler validates the incoming request, fetches the sale entity from the repository,
/// maps it to the <see cref="GetSaleResult"/>, and returns the mapped result.
/// If the sale is not found, a <see cref="KeyNotFoundException"/> is thrown.
/// </remarks>
/// <seealso cref="GetSaleCommand"/>
/// <seealso cref="GetSaleResult"/>
/// <seealso cref="ISaleRepository"/>
public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository to retrieve sale data from.</param>
    /// <param name="mapper">The mapper used to transform the entity into a result object.</param>
    public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the sale retrieval operation, performing validation, entity fetching, and result mapping.
    /// </summary>
    /// <param name="request">The command containing the ID of the sale to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A <see cref="GetSaleResult"/> containing the details of the requested sale.</returns>
    /// <exception cref="ValidationException">Thrown if the request validation fails.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no sale is found with the given ID.</exception>
    public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        return _mapper.Map<GetSaleResult>(sale);
    }
}
