using Ambev.DeveloperEvaluation.Application.Product.GetProduct;
using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customer.GetCustomer;

/// <summary>
/// Command to retrieve the details of a specific Customer based on its unique identifier.
/// </summary>
/// <remarks>
/// This command is used in the MediatR pipeline to request the retrieval of a Customer's information,
/// returning a <see cref="GetCustomerResult"/> containing all related details.
/// </remarks>
/// <seealso cref="GetCustomerResult"/>
public class GetCustomerCommand : IRequest<GetCustomerResult>
{
    /// <summary>
    /// Gets the unique identifier of the Customer to be retrieved.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCustomerCommand"/> class with the specified Customer identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Customer.</param>
    public GetCustomerCommand(Guid id)
    {
        Id = id;
    }
}