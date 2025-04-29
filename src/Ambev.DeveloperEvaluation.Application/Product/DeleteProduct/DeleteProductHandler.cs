using Ambev.DeveloperEvaluation.Application.Product.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

/// <summary>
/// Handles the deletion of a Product entity based on the provided <see cref="DeleteProductCommand"/>.
/// </summary>
/// <remarks>
/// This handler is responsible for validating the incoming delete command,
/// attempting to delete the corresponding Product from the repository, 
/// and returning a response indicating the success of the operation.
/// 
/// If the Product does not exist, a <see cref="KeyNotFoundException"/> is thrown.
/// If validation fails, a <see cref="FluentValidation.ValidationException"/> is thrown.
/// </remarks>
/// <seealso cref="DeleteProductCommand"/>
/// <seealso cref="DeleteProductResponse"/>
/// <seealso cref="IProductRepository"/>
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
{
    private readonly IProductRepository _ProductRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductHandler"/> class.
    /// </summary>
    /// <param name="ProductRepository">The repository interface used to access Product data operations.</param>
    public DeleteProductHandler(IProductRepository ProductRepository)
    {
        _ProductRepository = ProductRepository;
    }

    /// <summary>
    /// Handles the request to delete a Product based on the given ID.
    /// </summary>
    /// <param name="request">The command containing the Product ID to be deleted.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A <see cref="DeleteProductResponse"/> indicating the outcome of the deletion.</returns>
    /// <exception cref="ValidationException">Thrown when the request fails validation rules.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the Product to be deleted is not found in the system.</exception>
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _ProductRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        return new DeleteProductResponse() { Success = true };
    }
}
