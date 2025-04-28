using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItem.CreateSaleItem
{
    /// <summary>
    /// Handler responsible for processing the creation of a sale item.
    /// </summary>
    /// <remarks>
    /// This handler is responsible for:
    /// - Validating the input <see cref="CreateSaleItemCommand"/> using the <see cref="CreateSaleItemCommandValidator"/>.
    /// - Mapping the validated command to a <see cref="Domain.Entities.SaleItem"/>.
    /// - Creating the sale item using the <see cref="ISaleItemRepository"/>.
    /// - Returning the <see cref="CreateSaleItemResult"/> containing the created sale item details.
    /// </remarks>
    public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
    {
        private readonly ISaleItemItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public CreateSaleItemHandler(ISaleItemItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the creation of a sale item by validating, mapping, and saving the item.
        /// </summary>
        /// <param name="command">The command containing the details of the sale item to be created.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>
        /// The result of the created sale item, containing the ID of the newly created sale item.
        /// </returns>
        /// <exception cref="ValidationException">Thrown when the input command is not valid.</exception>
        public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleItemCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var saleItem = _mapper.Map<Domain.Entities.SaleItem>(command);

            var createdSale = await _itemRepository.CreateAsync(saleItem, cancellationToken);
            var result = _mapper.Map<CreateSaleItemResult>(createdSale);
            return result;
        }
    }
}
