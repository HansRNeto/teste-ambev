using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IBranchRepository
{
    /// <summary>
    /// Creates a new Branch in the repository
    /// </summary>
    /// <param name="Branch">The Branch to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Branch</returns>
    Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Branch by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Branch if found, null otherwise</returns>
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated and sorted list of Branches based on the provided parameters.
    /// </summary>
    /// <param name="page">The page number for pagination (starting from 1).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="sortBy">The field name to sort by (e.g., CustomerDate, CustomerName, TotalAmount).</param>
    /// <param name="sortDirection">The sort direction ("ASC" for ascending, "DESC" for descending).</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>A paginated and sorted collection of Branches.</returns>
    Task<List<Branch>> ListAsync(int page, int pageSize, string? sortBy, string? sortDirection, CancellationToken cancellationToken);
    
    /// <summary>
    /// Deletes a Branch from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Branch was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}