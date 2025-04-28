using Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers
{
    /// <summary>
    /// Handles HTTP requests related to customers.
    /// This controller exposes an endpoint for creating a new customer.
    /// </summary>
    /// <remarks>
    /// The <see cref="CustomersController"/> class manages HTTP operations for customer-related actions within the application.
    /// It provides an endpoint to create new customers by accepting a request with customer details and returning the created customer response.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator to handle requests.</param>
        /// <param name="mapper">The mapper to transform DTOs between layers.</param>
        public CustomersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="request">The request containing customer details.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
        /// <returns>A response with the created customer data or validation errors.</returns>
        /// <response code="201">Customer successfully created. Returns the customer details in the response body.</response>
        /// <response code="400">Bad Request. Validation failed for the provided customer data.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateCustomerResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateCustomerRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<CreateCustomerCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Created(string.Empty, new ApiResponseWithData<CreateCustomerResponse>
                {
                    Success = true,
                    Message = "Customer created successfully",
                    Data = _mapper.Map<CreateCustomerResponse>(response)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}