using Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;
using MediatR;

/// <summary>
/// Represents the command to delete an existing sale by its unique identifier.
/// </summary>
/// <remarks>
/// This command encapsulates the ID of the sale to be deleted from the system.
/// </remarks>
public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
{
    /// <summary>
    /// Gets the unique identifier of the sale to be deleted.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleCommand"/> class with the specified sale ID.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete.</param>
    public DeleteSaleCommand(Guid id)
    {
        Id = id;
    }

    public DeleteSaleCommand()
    {
        
    }
}