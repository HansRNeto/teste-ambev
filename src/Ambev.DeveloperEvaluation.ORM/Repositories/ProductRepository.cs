using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IProductRepository"/> using Entity Framework Core for data access.
    /// This class provides CRUD operations for managing products in the database.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DefaultContext"/> instance used for database interactions.</param>
        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Asynchronously creates a new product in the database.
        /// Adds the given product to the Products table and saves the changes to the database.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> entity to be added to the database.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
        /// <returns>The created <see cref="Product"/> entity.</returns>
        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        /// <summary>
        /// Asynchronously retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the product.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
        /// <returns>The <see cref="Product"/> entity if found, otherwise null.</returns>
        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a paginated and sorted list of products based on the provided parameters.
        /// </summary>
        /// <param name="page">The page number for pagination (starting from 1).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="sortBy">The field name to sort the results by (e.g., ProductDate, CustomerName, TotalAmount).</param>
        /// <param name="sortDirection">The sort direction ("ASC" for ascending, "DESC" for descending).</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A paginated and sorted collection of Products.</returns>
        public async Task<List<Product>> ListAsync(int page, int pageSize, string? sortBy, string? sortDirection, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = string.Equals(sortDirection, "DESC", StringComparison.OrdinalIgnoreCase)
                    ? query.OrderByDescending(e => EF.Property<object>(e, sortBy))
                    : query.OrderBy(e => EF.Property<object>(e, sortBy));
            }

            query = query.Skip(pageSize * (page - 1)).Take(pageSize);

            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Asynchronously deletes a product by its unique identifier.
        /// If the product is found, it is removed from the Products table and changes are saved to the database.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the product to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
        /// <returns>A boolean value indicating whether the product was successfully deleted.</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await GetByIdAsync(id, cancellationToken);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
