using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customer.ListCustomers;

/// <summary>
/// Represents the input parameters for listing Customers with pagination and optional sorting.
/// </summary>
/// <remarks>
/// This command contains pagination information (page number and page size),
/// and optional sorting criteria (field and direction) to retrieve a paged list of Customers.
/// </remarks>
public class ListCustomersCommand : IRequest<List<ListCustomersResult>>
{
    /// <summary>
    /// The page number to retrieve. Defaults to 1.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// The number of records to retrieve per page. Defaults to 10.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// The field name to sort the Customers by.
    /// </summary>
    public string? SortBy { get; set; } = string.Empty;

    /// <summary>
    /// The direction of sorting: "ASC" for ascending or "DESC" for descending. Defaults to "ASC".
    /// </summary>
    public string? SortDirection { get; set; } = "ASC";
}