using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branch.GetBranch;

/// <summary>
/// Handles the retrieval of a Branch based on the provided <see cref="GetBranchCommand"/>.
/// </summary>
/// <remarks>
/// This handler validates the incoming request, fetches the Branch entity from the repository,
/// maps it to the <see cref="GetBranchResult"/>, and returns the mapped result.
/// If the Branch is not found, a <see cref="KeyNotFoundException"/> is thrown.
/// </remarks>
/// <seealso cref="GetBranchCommand"/>
/// <seealso cref="GetBranchResult"/>
/// <seealso cref="IBranchRepository"/>
public class GetBranchHandler : IRequestHandler<GetBranchCommand, GetBranchResult>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBranchHandler"/> class.
    /// </summary>
    /// <param name="branchRepository"></param>
    /// <param name="mapper">The mapper used to transform the entity into a result object.</param>
    public GetBranchHandler(IBranchRepository branchRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the Branch retrieval operation, performing validation, entity fetching, and result mapping.
    /// </summary>
    /// <param name="request">The command containing the ID of the Branch to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A <see cref="GetBranchResult"/> containing the details of the requested Branch.</returns>
    /// <exception cref="ValidationException">Thrown if the request validation fails.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no Branch is found with the given ID.</exception>
    public async Task<GetBranchResult> Handle(GetBranchCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetBranchValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var branch = await _branchRepository.GetByIdAsync(request.Id, cancellationToken);
        if (branch == null)
            throw new KeyNotFoundException($"Branch with ID {request.Id} not found");

        return _mapper.Map<GetBranchResult>(branch);
    }
}
