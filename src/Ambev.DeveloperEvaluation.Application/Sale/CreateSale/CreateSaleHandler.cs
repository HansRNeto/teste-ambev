using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    /// <summary>
    /// Handles the creation of a new sale by processing the <see cref="CreateSaleCommand"/>.
    /// </summary>
    /// <remarks>
    /// This handler validates the incoming command, applies business rules for discounts,
    /// maps it to the domain entity, persists it via the <see cref="ISaleRepository"/>,
    /// and returns the created sale information.
    /// </remarks>
    /// <seealso cref="CreateSaleCommand"/>
    /// <seealso cref="CreateSaleResult"/>
    /// <seealso cref="ISaleRepository"/>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository to persist the sale data.</param>
        /// <param name="mapper">The mapper to transform command to entity and entity to result.</param>
        /// <param name="productRepository">The repository to retrieve product information.</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Handles the sale creation command by performing validation, applying business rules for discounts,
        /// persisting the sale and its items, and mapping the result.
        /// </summary>
        /// <param name="command">The command containing the sale information to be created.</param>
        /// <param name="cancellationToken">A cancellation token for the operation.</param>
        /// <returns>The result containing information about the created sale.</returns>
        /// <exception cref="ValidationException">Thrown if the command fails validation.</exception>
        /// <exception cref="BadHttpRequestException">
        /// Thrown if a referenced product is not found or if the item quantity exceeds business constraints.
        /// </exception>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = _mapper.Map<Domain.Entities.Sale>(command);
            sale.SaleItems.Clear();
            
            foreach (var item in command.SaleItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product == null)
                    throw new BadHttpRequestException($"Product {item.ProductName} not found.");

                var saleItem = _mapper.Map<Domain.Entities.SaleItem>(item);
                saleItem.UnitPrice = product.Price;

                saleItem.DiscountPercentage = saleItem.Quantity switch
                {
                    >= 4 and <= 9 => 10,
                    >= 10 and <= 20 => 20,
                    > 20 => throw new BadHttpRequestException(
                        $"It is not possible to sell more than 20 units of the product {item.ProductName}."),
                    _ => 0
                };

                saleItem.DiscountAmount = saleItem.UnitPrice * saleItem.Quantity * saleItem.DiscountPercentage / 100;
                saleItem.TotalAmount = (saleItem.UnitPrice * saleItem.Quantity) - saleItem.DiscountAmount;

                sale.SaleItems.Add(saleItem);
            }
            
            sale.TotalAmount = sale.SaleItems.Sum(x => x.TotalAmount);

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            var result = _mapper.Map<CreateSaleResult>(createdSale);

            return result;
        }
    }
}