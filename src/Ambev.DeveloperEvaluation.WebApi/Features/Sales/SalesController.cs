using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    /// <summary>
    /// Controller responsible for handling sales-related operations. 
    /// Provides an endpoint for creating a new sale in the system.
    /// </summary>
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator to handle commands and queries.</param>
        /// <param name="mapper">Mapper to convert between DTOs and domain models.</param>
        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Creates a new sale in the system based on the provided <see cref="CreateSaleRequest"/>.
        /// The request is validated before processing and the sale is persisted in the system.
        /// </summary>
        /// <remarks>
        /// This endpoint allows users to create a new sale, including details like sale number, 
        /// customer information, branch information, and sale amount. 
        /// The request body should contain a valid <see cref="CreateSaleRequest"/> with necessary details.
        /// 
        /// If validation fails, the endpoint returns a 400 Bad Request with details about the validation errors.
        /// On success, it returns a 201 Created response with the created sale data.
        /// </remarks>
        /// <param name="request">The request object containing details of the sale to be created.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
        /// <returns>
        /// A <see cref="IActionResult"/> indicating the result of the operation:
        /// - 201 Created: The sale was successfully created.
        /// - 400 Bad Request: Validation failed or an error occurred during processing.
        /// </returns>
        /// <response code="201">Sale successfully created.</response>
        /// <response code="400">The request was invalid or an error occurred during processing.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateSaleRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<CreateSaleCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
                {
                    Success = true,
                    Message = "Sale created successfully",
                    Data = _mapper.Map<CreateSaleResponse>(response)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
