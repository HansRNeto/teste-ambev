using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSales;

/// <summary>
/// Handles the listing of sales with pagination and sorting based on the provided <see cref="ListSalesCommand"/>.
/// </summary>
/// <remarks>
/// This handler processes the command to list sales, validating the request parameters such as page number, 
/// page size, sorting options, and direction. It retrieves the sales list from the repository, applies the 
/// necessary mapping to return a response, and handles validation errors if any are found in the request.
/// </remarks>
/// <seealso cref="ListSalesCommand"/>
/// <seealso cref="ListSalesResult"/>
/// <seealso cref="ISaleRepository"/>
public class ListSalesHandler : IRequestHandler<ListSalesCommand, List<ListSalesResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListSalesHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository used to fetch sales data.</param>
    /// <param name="mapper">The mapper used to transform the entity into a result object.</param>
    public ListSalesHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the request to list sales with pagination and sorting.
    /// </summary>
    /// <param name="command">The command containing the pagination and sorting details for the sale list.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
    /// <returns>
    /// A list of <see cref="ListSalesResult"/> representing the paginated and sorted sales.
    /// If the request parameters are invalid, a validation exception is thrown.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown if the validation for the provided command parameters fails.
    /// </exception>
    public async Task<List<ListSalesResult>> Handle(ListSalesCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListSalesValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sales = await _saleRepository.ListAsync(command.PageNumber, command.PageSize, command.SortBy,
            command.SortDirection, cancellationToken);

        return _mapper.Map<List<ListSalesResult>>(sales);
    }
}