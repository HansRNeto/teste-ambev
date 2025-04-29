using Ambev.DeveloperEvaluation.Application.Customer.DeleteCustomer;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

/// <summary>
/// Handles the deletion of a Customer entity based on the provided <see cref="DeleteCustomerCommand"/>.
/// </summary>
/// <remarks>
/// This handler is responsible for validating the incoming delete command,
/// attempting to delete the corresponding Customer from the repository, 
/// and returning a response indicating the success of the operation.
/// 
/// If the Customer does not exist, a <see cref="KeyNotFoundException"/> is thrown.
/// If validation fails, a <see cref="FluentValidation.ValidationException"/> is thrown.
/// </remarks>
/// <seealso cref="DeleteCustomerCommand"/>
/// <seealso cref="DeleteCustomerResponse"/>
/// <seealso cref="ICustomerRepository"/>
public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, DeleteCustomerResponse>
{
    private readonly ICustomerRepository _CustomerRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCustomerHandler"/> class.
    /// </summary>
    /// <param name="CustomerRepository">The repository interface used to access Customer data operations.</param>
    public DeleteCustomerHandler(ICustomerRepository CustomerRepository)
    {
        _CustomerRepository = CustomerRepository;
    }

    /// <summary>
    /// Handles the request to delete a Customer based on the given ID.
    /// </summary>
    /// <param name="request">The command containing the Customer ID to be deleted.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A <see cref="DeleteCustomerResponse"/> indicating the outcome of the deletion.</returns>
    /// <exception cref="ValidationException">Thrown when the request fails validation rules.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the Customer to be deleted is not found in the system.</exception>
    public async Task<DeleteCustomerResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCustomerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _CustomerRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Customer with ID {request.Id} not found");

        return new DeleteCustomerResponse() { Success = true };
    }
}
