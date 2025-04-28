using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branch.CreateBranch
{
    /// <summary>
    /// Handler for creating a new branch.
    /// </summary>
    /// <remarks>
    /// This class handles the process of validating and creating a new branch. It validates the <see cref="CreateBranchCommand"/>,
    /// maps it to a domain entity, creates the branch in the repository, and returns the result of the creation.
    /// </remarks>
    public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, CreateBranchResult>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBranchHandler"/> class.
        /// </summary>
        /// <param name="branchRepository">The repository for managing branch entities.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping between objects.</param>
        public CreateBranchHandler(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the creation of a new branch.
        /// </summary>
        /// <param name="command">The command containing the data for creating the branch.</param>
        /// <param name="cancellationToken">The cancellation token for task cancellation.</param>
        /// <returns>A <see cref="CreateBranchResult"/> containing the details of the created branch.</returns>
        /// <exception cref="ValidationException">Thrown if the command fails validation.</exception>
        public async Task<CreateBranchResult> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateBranchCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var branch = _mapper.Map<Domain.Entities.Branch>(command);

            var createdBranch = await _branchRepository.CreateAsync(branch, cancellationToken);
            var result = _mapper.Map<CreateBranchResult>(createdBranch);
            return result;
        }
    }
}
