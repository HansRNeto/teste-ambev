using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customer.GetCustomer;

/// <summary>
/// Handles the retrieval of a Customer based on the provided <see cref="GetCustomerCommand"/>.
/// </summary>
/// <remarks>
/// This handler validates the incoming request, fetches the Customer entity from the repository,
/// maps it to the <see cref="GetCustomerResult"/>, and returns the mapped result.
/// If the Customer is not found, a <see cref="KeyNotFoundException"/> is thrown.
/// </remarks>
/// <seealso cref="GetCustomerCommand"/>
/// <seealso cref="GetCustomerResult"/>
/// <seealso cref="ICustomerRepository"/>
public class GetCustomerHandler : IRequestHandler<GetCustomerCommand, GetCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCustomerHandler"/> class.
    /// </summary>
    /// <param name="customerRepository"></param>
    /// <param name="mapper">The mapper used to transform the entity into a result object.</param>
    public GetCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the Customer retrieval operation, performing validation, entity fetching, and result mapping.
    /// </summary>
    /// <param name="request">The command containing the ID of the Customer to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A <see cref="GetCustomerResult"/> containing the details of the requested Customer.</returns>
    /// <exception cref="ValidationException">Thrown if the request validation fails.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no Customer is found with the given ID.</exception>
    public async Task<GetCustomerResult> Handle(GetCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCustomerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (customer == null)
            throw new KeyNotFoundException($"Customer with ID {request.Id} not found");

        return _mapper.Map<GetCustomerResult>(customer);
    }
}
