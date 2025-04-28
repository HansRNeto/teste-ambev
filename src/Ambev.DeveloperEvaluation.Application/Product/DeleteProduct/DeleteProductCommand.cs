using Ambev.DeveloperEvaluation.Application.Product.DeleteProduct;
using MediatR;

/// <summary>
/// Represents the command to delete an existing Product by its unique identifier.
/// </summary>
/// <remarks>
/// This command encapsulates the ID of the Product to be deleted from the system.
/// </remarks>
public class DeleteProductCommand : IRequest<DeleteProductResponse>
{
    /// <summary>
    /// Gets the unique identifier of the Product to be deleted.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductCommand"/> class with the specified Product ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Product to delete.</param>
    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}