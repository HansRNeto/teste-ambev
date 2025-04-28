using Ambev.DeveloperEvaluation.Application.Branch.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branch.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.DeleteBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches;

/// <summary>
/// Controller responsible for handling branch-related operations.
/// </summary>
/// <remarks>
/// Provides endpoints to manage branches in the system, such as creating a new branch.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class BranchsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="BranchsController"/> class.
    /// </summary>
    /// <param name="mediator">Mediator for sending commands and queries.</param>
    /// <param name="mapper">Mapper for converting between models and DTOs.</param>
    public BranchsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new branch.
    /// </summary>
    /// <param name="request">The branch creation request, containing the name and address of the branch.</param>
    /// <param name="cancellationToken">Cancellation token to support cancellation of the request.</param>
    /// <returns>Returns the newly created branch information.</returns>
    /// <response code="201">Branch successfully created.</response>
    /// <response code="400">Validation error or bad request. If the provided data is invalid or missing required fields.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateBranchResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateBranchRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateBranchCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateBranchResponse>
            {
                Success = true,
                Message = "Branch created successfully",
                Data = _mapper.Map<CreateBranchResponse>(response)
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Retrieves the details of a specific Branch by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to retrieve detailed information about a Branch using its unique ID.
    /// It validates the request ID and returns a response containing the Branch's data, including customer, branch, 
    /// total amount, and other relevant Branch details. The operation supports error handling in case the Branch ID is invalid 
    /// or not found in the system.
    /// </remarks>
    /// <param name="id">The unique identifier of the Branch to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
    /// <returns>
    /// A response with a status of 200 OK containing the Branch's data if successful. 
    /// A 400 Bad Request response will be returned if the request is invalid, 
    /// and a 404 Not Found response will be returned if the Branch with the provided ID does not exist.
    /// </returns>
    /// <response code="200">Returns the Branch's details successfully retrieved from the database.</response>
    /// <response code="400">If the provided ID is invalid or the request does not meet validation requirements.</response>
    /// <response code="404">If the Branch with the given ID is not found in the system.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetBranchResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBranch([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetBranchRequest { Id = id };
        var validator = new GetBranchRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetBranchCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        var result = _mapper.Map<GetBranchResponse>(response);
        return Ok(result, "Branch retrieved successfully");
    }

    /// <summary>
    /// Deletes a Branch by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This endpoint validates the provided Branch ID, sends a command to delete the Branch,
    /// and returns an appropriate HTTP response based on the outcome.
    /// 
    /// - Returns 200 OK if the Branch is successfully deleted.
    /// - Returns 400 Bad Request if the request validation fails.
    /// - Returns 404 Not Found if no Branch with the specified ID exists.
    /// 
    /// The Branch deletion operation is executed via the <see cref="IMediator"/> command dispatching mechanism,
    /// following the CQRS (Command Query Responsibility Segregation) pattern.
    /// </remarks>
    /// <param name="id">The unique identifier of the Branch to be deleted.</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
    /// <returns>
    /// A standardized <see cref="ApiResponse"/> indicating success or failure of the operation.
    /// </returns>
    /// <response code="200">Branch successfully deleted.</response>
    /// <response code="400">Invalid request (e.g., validation errors).</response>
    /// <response code="404">Branch not found for the provided ID.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBranch([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var request = new DeleteBranchRequest { Id = id };
            var validator = new DeleteBranchRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteBranchCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Branch deleted successfully"
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}