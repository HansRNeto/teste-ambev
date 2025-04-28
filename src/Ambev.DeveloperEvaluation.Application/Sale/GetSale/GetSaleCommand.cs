using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

/// <summary>
/// Command to retrieve the details of a specific sale based on its unique identifier.
/// </summary>
/// <remarks>
/// This command is used in the MediatR pipeline to request the retrieval of a sale's information,
/// returning a <see cref="GetSaleResult"/> containing all related details.
/// </remarks>
/// <seealso cref="GetSaleResult"/>
public class GetSaleCommand : IRequest<GetSaleResult>
{
    /// <summary>
    /// Gets the unique identifier of the sale to be retrieved.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleCommand"/> class with the specified sale identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    public GetSaleCommand(Guid id)
    {
        Id = id;
    }
}