using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    /// <summary>
    /// Handles the creation of a new sale by processing the <see cref="CreateSaleCommand"/>.
    /// </summary>
    /// <remarks>
    /// This handler validates the incoming command, maps it to the domain entity,
    /// persists it via the <see cref="ISaleRepository"/>, and returns the created sale information.
    /// </remarks>
    /// <seealso cref="CreateSaleCommand"/>
    /// <seealso cref="CreateSaleResult"/>
    /// <seealso cref="ISaleRepository"/>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository to persist the sale data.</param>
        /// <param name="mapper">The mapper to transform command to entity and entity to result.</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the sale creation command, performing validation, persistence, and result mapping.
        /// </summary>
        /// <param name="command">The command containing the sale information to be created.</param>
        /// <param name="cancellationToken">A cancellation token for the operation.</param>
        /// <returns>The result containing information about the created sale.</returns>
        /// <exception cref="ValidationException">Thrown if the command fails validation.</exception>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = _mapper.Map<Domain.Entities.Sale>(command);

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }
    }
}
