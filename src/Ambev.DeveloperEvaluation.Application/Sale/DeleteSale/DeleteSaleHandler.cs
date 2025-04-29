using Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

/// <summary>
/// Handles the deletion of a sale entity based on the provided <see cref="DeleteSaleCommand"/>.
/// </summary>
/// <remarks>
/// This handler is responsible for validating the incoming delete command,
/// attempting to delete the corresponding sale from the repository, 
/// and returning a response indicating the success of the operation.
/// 
/// If the sale does not exist, a <see cref="KeyNotFoundException"/> is thrown.
/// If validation fails, a <see cref="FluentValidation.ValidationException"/> is thrown.
/// </remarks>
/// <seealso cref="DeleteSaleCommand"/>
/// <seealso cref="DeleteSaleResponse"/>
/// <seealso cref="ISaleRepository"/>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository interface used to access sale data operations.</param>
    public DeleteSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the request to delete a sale based on the given ID.
    /// </summary>
    /// <param name="request">The command containing the sale ID to be deleted.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A <see cref="DeleteSaleResponse"/> indicating the outcome of the deletion.</returns>
    /// <exception cref="ValidationException">Thrown when the request fails validation rules.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the sale to be deleted is not found in the system.</exception>
    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        return new DeleteSaleResponse() { Success = true };
    }
}
