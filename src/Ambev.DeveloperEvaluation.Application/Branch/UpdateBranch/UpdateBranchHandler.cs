using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Application.Branch.UpdateBranch
{
    /// <summary>
    /// Handler for handling the update of a Branch.
    /// </summary>
    /// <remarks>
    /// This class handles the <see cref="UpdateBranchCommand"/> by validating the command,
    /// mapping it to a domain entity, and interacting with the repository to persist the Branch data.
    /// It also maps the result into <see cref="UpdateBranchResult"/>.
    /// </remarks>
    public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchResult>
    {
        private readonly IBranchRepository _BranchRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateBranchHandler"/> class.
        /// </summary>
        /// <param name="BranchRepository">The repository for Branch data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between DTOs and domain entities.</param>
        public UpdateBranchHandler(IBranchRepository BranchRepository, IMapper mapper)
        {
            _BranchRepository = BranchRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the <see cref="UpdateBranchCommand"/> and returns the update result.
        /// </summary>
        /// <param name="command">The command containing the Branch update data.</param>
        /// <param name="cancellationToken">The cancellation token for the operation.</param>
        /// <returns>The result of the Branch update, wrapped in a <see cref="UpdateBranchResult"/>.</returns>
        /// <exception cref="ValidationException">Thrown when the validation of the command fails.</exception>
        public async Task<UpdateBranchResult> Handle(UpdateBranchCommand command,
            CancellationToken cancellationToken)
        {
            var validator = new UpdateBranchCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var existingBranch = await _BranchRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingBranch == null)
                throw new BadHttpRequestException($"Branch with ID {command.Id} not found.");
            
            var branch = _mapper.Map<Domain.Entities.Branch>(command);

            var updatedBranch = await _BranchRepository.UpdateAsync(branch, cancellationToken);

            var result = _mapper.Map<UpdateBranchResult>(updatedBranch);
            return result;
        }
    }
}