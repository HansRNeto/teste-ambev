using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customer.ListCustomers;

/// <summary>
/// Handles the request to list Customers with pagination and sorting based on the provided <see cref="ListCustomersCommand"/>.
/// </summary>
/// <remarks>
/// This handler validates the incoming request, retrieves a list of Customers from the repository,
/// applies pagination and sorting, and maps the result to a list of <see cref="ListCustomersResult"/>.
/// If the request validation fails, a <see cref="ValidationException"/> is thrown.
/// </remarks>
/// <seealso cref="ListCustomersCommand"/>
/// <seealso cref="ListCustomersResult"/>
/// <seealso cref="ICustomerRepository"/>
public class ListCustomersHandler : IRequestHandler<ListCustomersCommand, List<ListCustomersResult>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListCustomersHandler"/> class.
    /// </summary>
    /// <param name="customerRepository">The repository used to fetch the Customers.</param>
    /// <param name="mapper">The mapper used to transform the entities into result objects.</param>
    public ListCustomersHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the request to list Customers with pagination and sorting.
    /// </summary>
    /// <remarks>
    /// This handler processes the command to list Customers, validating the request parameters such as page number, 
    /// page size, sorting options, and direction. It retrieves the Customer list from the repository, applies the 
    /// necessary mapping to return a response, and handles validation errors if any are found in the request.
    /// </remarks>
    /// <param name="command">The command containing the pagination and sorting details for the Customer list.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
    /// <returns>
    /// A list of <see cref="ListCustomersResult"/> representing the paginated and sorted Customers.
    /// If the request parameters are invalid, a validation exception is thrown.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown if the validation for the provided command parameters fails.
    /// </exception>
    public async Task<List<ListCustomersResult>> Handle(ListCustomersCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListCustomersValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var customers = await _customerRepository.ListAsync(command.PageNumber, command.PageSize, command.SortBy,
            command.SortDirection, cancellationToken);

        return _mapper.Map<List<ListCustomersResult>>(customers);
    }
}
