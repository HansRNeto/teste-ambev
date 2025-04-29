using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branch.ListBranches;

/// <summary>
/// Handles the request to list Branches with pagination and sorting based on the provided <see cref="ListBranchesCommand"/>.
/// </summary>
/// <remarks>
/// This handler validates the incoming request, retrieves a list of Branches from the repository,
/// applies pagination and sorting, and maps the result to a list of <see cref="ListBranchesResult"/>.
/// If the request validation fails, a <see cref="ValidationException"/> is thrown.
/// </remarks>
/// <seealso cref="ListBranchesCommand"/>
/// <seealso cref="ListBranchesResult"/>
/// <seealso cref="IBranchRepository"/>
public class ListBranchesHandler : IRequestHandler<ListBranchesCommand, List<ListBranchesResult>>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListBranchesHandler"/> class.
    /// </summary>
    /// <param name="branchRepository">The repository used to fetch the Branches.</param>
    /// <param name="mapper">The mapper used to transform the entities into result objects.</param>
    public ListBranchesHandler(IBranchRepository branchRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the request to list Branches with pagination and sorting.
    /// </summary>
    /// <remarks>
    /// This handler processes the command to list Branches, validating the request parameters such as page number, 
    /// page size, sorting options, and direction. It retrieves the Branch list from the repository, applies the 
    /// necessary mapping to return a response, and handles validation errors if any are found in the request.
    /// </remarks>
    /// <param name="command">The command containing the pagination and sorting details for the Branch list.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
    /// <returns>
    /// A list of <see cref="ListBranchesResult"/> representing the paginated and sorted Branches.
    /// If the request parameters are invalid, a validation exception is thrown.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown if the validation for the provided command parameters fails.
    /// </exception>
    public async Task<List<ListBranchesResult>> Handle(ListBranchesCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListBranchesValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var branches = await _branchRepository.ListAsync(command.PageNumber, command.PageSize, command.SortBy,
            command.SortDirection, cancellationToken);

        return _mapper.Map<List<ListBranchesResult>>(branches);
    }
}