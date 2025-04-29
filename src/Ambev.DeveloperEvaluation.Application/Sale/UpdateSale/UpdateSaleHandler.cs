using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale
{
    /// <summary>
    /// Handler for handling the update of a Sale.
    /// </summary>
    /// <remarks>
    /// This class handles the <see cref="UpdateSaleCommand"/> by validating the command,
    /// mapping it to a domain entity, and interacting with the repository to persist the Sale data.
    /// It also maps the result into <see cref="UpdateSaleResult"/>.
    /// </remarks>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository for Sale data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between DTOs and domain entities.</param>
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the <see cref="UpdateSaleCommand"/> and returns the update result.
        /// </summary>
        /// <param name="command">The command containing the Sale update data.</param>
        /// <param name="cancellationToken">The cancellation token for the operation.</param>
        /// <returns>The result of the Sale update, wrapped in a <see cref="UpdateSaleResult"/>.</returns>
        /// <exception cref="ValidationException">Thrown when the validation of the command fails.</exception>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command,
            CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingSale == null)
                throw new BadHttpRequestException($"Sale with ID {command.Id} not found.");
            
            var sale = _mapper.Map<Domain.Entities.Sale>(command);

            var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

            var result = _mapper.Map<UpdateSaleResult>(updatedSale);
            return result;
        }
    }
}