using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IBranchRepository using Entity Framework Core.
/// </summary>
/// <remarks>
/// This repository provides methods for CRUD operations on the Branch entity, including creation, retrieval by ID, and deletion of branches.
/// It uses Entity Framework Core to interact with the database and persist changes.
/// </remarks>
public class BranchRepository : IBranchRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the BranchRepository class.
    /// </summary>
    /// <param name="context">The context for interacting with the database.</param>
    public BranchRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new branch and persists it to the database.
    /// </summary>
    /// <param name="branch">The branch to be created.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
    /// <returns>The created branch.</returns>
    public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        await _context.Branches.AddAsync(branch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return branch;
    }

    /// <summary>
    /// Retrieves a branch by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the branch to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
    /// <returns>The branch if found, otherwise null.</returns>
    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Branches.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
    
    /// <summary>
    /// Retrieves a paginated and sorted list of Branchs based on the provided parameters.
    /// </summary>
    /// <param name="page">The page number for pagination (starting from 1).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="sortBy">The field name to sort the results by (e.g., BranchDate, BranchName, TotalAmount).</param>
    /// <param name="sortDirection">The sort direction ("ASC" for ascending, "DESC" for descending).</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>A paginated and sorted collection of Branches.</returns>
    public async Task<List<Branch>> ListAsync(int page, int pageSize, string? sortBy, string? sortDirection, CancellationToken cancellationToken)
    {
        var query = _context.Branches.AsQueryable();

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
    /// Update a Branch in the database.
    /// </summary>
    /// <param name="branch"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task{Branch}"/> representing the asynchronous operation, with the updated Branch as the result.</returns>
    public async Task<Branch> UpdateAsync(Branch branch, CancellationToken cancellationToken)
    {
        _context.Branches.Attach(branch);
        _context.Entry(branch).Property(x => x.Name).IsModified = true;
        _context.Entry(branch).Property(x => x.Address).IsModified = true;
        _context.Entry(branch).Property(x => x.IsActive).IsModified = true;
        _context.Entry(branch).Property(x => x.UpdatedAt).IsModified = true;
        await _context.SaveChangesAsync(cancellationToken);

        return branch;
    }

    /// <summary>
    /// Deletes a branch by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the branch to delete.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
    /// <returns>True if the branch was successfully deleted, otherwise false.</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(id, cancellationToken);
        if (branch == null)
            return false;

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
