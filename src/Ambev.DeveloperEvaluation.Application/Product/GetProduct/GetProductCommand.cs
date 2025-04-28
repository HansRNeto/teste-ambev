using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct;

/// <summary>
/// Command to retrieve the details of a specific Product based on its unique identifier.
/// </summary>
/// <remarks>
/// This command is used in the MediatR pipeline to request the retrieval of a Product's information,
/// returning a <see cref="GetProductResult"/> containing all related details.
/// </remarks>
/// <seealso cref="GetProductResult"/>
public class GetProductCommand : IRequest<GetProductResult>
{
    /// <summary>
    /// Gets the unique identifier of the Product to be retrieved.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductCommand"/> class with the specified Product identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Product.</param>
    public GetProductCommand(Guid id)
    {
        Id = id;
    }
}