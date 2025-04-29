using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of <see cref="ISaleRepository"/> using Entity Framework Core for data access.
    /// This class provides CRUD operations for managing sales in the database.
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DefaultContext"/> instance used for database interactions.</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously creates a new sale in the database.
        /// Adds the given sale to the Sales table and saves the changes to the database.
        /// </summary>
        /// <param name="sale">The <see cref="Sale"/> entity to be added to the database.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
        /// <returns>The created <see cref="Sale"/> entity.</returns>
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        /// <summary>
        /// Asynchronously retrieves a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the sale.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
        /// <returns>The <see cref="Sale"/> entity if found, otherwise null.</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a paginated and sorted list of sales based on the provided parameters.
        /// </summary>
        /// <param name="page">The page number for pagination (starting from 1).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="sortBy">The field name to sort the results by (e.g., SaleDate, CustomerName, TotalAmount).</param>
        /// <param name="sortDirection">The sort direction ("ASC" for ascending, "DESC" for descending).</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A paginated and sorted collection of sales.</returns>
        public async Task<ICollection<Sale>> ListAsync(
            int page,
            int pageSize,
            string sortBy,
            string sortDirection,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Sales.AsQueryable();

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
        /// Update a Sale in the database.
        /// </summary>
        /// <param name="sale"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{Sale}"/> representing the asynchronous operation, with the updated Sale as the result.</returns>
        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken)
        {
            _context.Sales.Attach(sale);
            _context.Entry(sale).Property(x => x.BranchId).IsModified = true;
            _context.Entry(sale).Property(x => x.BranchName).IsModified = true;
            _context.Entry(sale).Property(x => x.CustomerId).IsModified = true;
            _context.Entry(sale).Property(x => x.CustomerName).IsModified = true;
            _context.Entry(sale).Property(x => x.SaleNumber).IsModified = true;
            _context.Entry(sale).Property(x => x.UpdatedAt).IsModified = true;
            await _context.SaveChangesAsync(cancellationToken);

            return sale;
        }

        /// <summary>
        /// Asynchronously deletes a sale by its unique identifier.
        /// If the sale is found, it is removed from the Sales table and changes are saved to the database.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the sale to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
        /// <returns>A boolean value indicating whether the sale was successfully deleted.</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}