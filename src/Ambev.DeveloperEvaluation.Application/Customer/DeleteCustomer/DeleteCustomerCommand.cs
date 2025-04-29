using Ambev.DeveloperEvaluation.Application.Customer.DeleteCustomer;
using MediatR;

/// <summary>
/// Represents the command to delete an existing Customer by its unique identifier.
/// </summary>
/// <remarks>
/// This command encapsulates the ID of the Customer to be deleted from the system.
/// </remarks>
public class DeleteCustomerCommand : IRequest<DeleteCustomerResponse>
{
    /// <summary>
    /// Gets the unique identifier of the Customer to be deleted.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCustomerCommand"/> class with the specified Customer ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Customer to delete.</param>
    public DeleteCustomerCommand(Guid id)
    {
        Id = id;
    }
}