using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    /// <summary>
    /// Creates a new Product in the repository
    /// </summary>
    /// <param name="product">The Product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Product</returns>
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product if found, null otherwise</returns>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated and sorted list of products based on the provided parameters.
    /// </summary>
    /// <param name="page">The page number for pagination (starting from 1).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="sortBy">The field name to sort by (e.g., ProductDate, CustomerName, TotalAmount).</param>
    /// <param name="sortDirection">The sort direction ("ASC" for ascending, "DESC" for descending).</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>A paginated and sorted collection of Products.</returns>
    Task<List<Product>> ListAsync(int page, int pageSize, string? sortBy, string? sortDirection, CancellationToken cancellationToken);

    /// <summary>
    /// Update a Product in the repository
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The updated Product</returns>
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
    
    /// <summary>
    /// Deletes a Product from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Product was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}