using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of <see cref="ICustomerRepository"/> using Entity Framework Core.
    /// </summary>
    /// <remarks>
    /// This class provides the concrete implementation of the <see cref="ICustomerRepository"/> interface
    /// for interacting with customer data in a database. It uses the Entity Framework Core ORM to perform
    /// operations such as creating, retrieving, and deleting customers.
    /// </remarks>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DefaultContext"/> instance to be used by the repository.</param>
        public CustomerRepository(DefaultContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Creates a new customer in the database.
        /// </summary>
        /// <param name="customer">The customer entity to be created.</param>
        /// <param name="cancellationToken">A cancellation token to observe while saving the customer.</param>
        /// <returns>A <see cref="Task{Customer}"/> representing the asynchronous operation, with the created customer as the result.</returns>
        public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await _context.Customers.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return customer;
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to retrieve.</param>
        /// <param name="cancellationToken">A cancellation token to observe while retrieving the customer.</param>
        /// <returns>A <see cref="Task{Customer}"/> representing the asynchronous operation, with the customer entity if found, or <c>null</c> otherwise.</returns>
        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a customer by their email address.
        /// </summary>
        /// <param name="email">The email address of the customer to retrieve.</param>
        /// <param name="cancellationToken">A cancellation token to observe while retrieving the customer.</param>
        /// <returns>A <see cref="Task{Customer}"/> representing the asynchronous operation, with the customer entity if found, or <c>null</c> otherwise.</returns>
        public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        /// <summary>
        /// Retrieves a paginated and sorted list of Customers based on the provided parameters.
        /// </summary>
        /// <param name="page">The page number for pagination (starting from 1).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="sortBy">The field name to sort the results by (e.g., CustomerDate, CustomerName, TotalAmount).</param>
        /// <param name="sortDirection">The sort direction ("ASC" for ascending, "DESC" for descending).</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A paginated and sorted collection of Customers.</returns>
        public async Task<List<Customer>> ListAsync(int page, int pageSize, string? sortBy, string? sortDirection, CancellationToken cancellationToken)
        {
            var query = _context.Customers.AsQueryable();

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
        /// Deletes a customer from the database by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to delete.</param>
        /// <param name="cancellationToken">A cancellation token to observe while deleting the customer.</param>
        /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, 
        /// with <c>true</c> if the customer was deleted, or <c>false</c> if the customer was not found.</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var customer = await GetByIdAsync(id, cancellationToken);
            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
