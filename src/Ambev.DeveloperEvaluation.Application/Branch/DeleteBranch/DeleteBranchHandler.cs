using Ambev.DeveloperEvaluation.Application.Branch.DeleteBranch;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

/// <summary>
/// Handles the deletion of a Branch entity based on the provided <see cref="DeleteBranchCommand"/>.
/// </summary>
/// <remarks>
/// This handler is responsible for validating the incoming delete command,
/// attempting to delete the corresponding Branch from the repository, 
/// and returning a response indicating the success of the operation.
/// 
/// If the Branch does not exist, a <see cref="KeyNotFoundException"/> is thrown.
/// If validation fails, a <see cref="FluentValidation.ValidationException"/> is thrown.
/// </remarks>
/// <seealso cref="DeleteBranchCommand"/>
/// <seealso cref="DeleteBranchResponse"/>
/// <seealso cref="IBranchRepository"/>
public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand, DeleteBranchResponse>
{
    private readonly IBranchRepository _BranchRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBranchHandler"/> class.
    /// </summary>
    /// <param name="BranchRepository">The repository interface used to access Branch data operations.</param>
    public DeleteBranchHandler(IBranchRepository BranchRepository)
    {
        _BranchRepository = BranchRepository;
    }

    /// <summary>
    /// Handles the request to delete a Branch based on the given ID.
    /// </summary>
    /// <param name="request">The command containing the Branch ID to be deleted.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A <see cref="DeleteBranchResponse"/> indicating the outcome of the deletion.</returns>
    /// <exception cref="ValidationException">Thrown when the request fails validation rules.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the Branch to be deleted is not found in the system.</exception>
    public async Task<DeleteBranchResponse> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteBranchValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _BranchRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Branch with ID {request.Id} not found");

        return new DeleteBranchResponse() { Success = true };
    }
}
