using Ambev.DeveloperEvaluation.Application.Branch.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branch.GetBranch;
using Ambev.DeveloperEvaluation.Application.Branch.ListBranches;
using Ambev.DeveloperEvaluation.Application.Branch.UpdateBranch;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.DeleteBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.ListBranches;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;
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
    /// It validates the request ID and returns a response containing the Branch's data, including Branch, branch, 
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
    /// Retrieves a paginated list of Branches with optional sorting.
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to retrieve a list of Branches with support for pagination and optional sorting by a specified field.
    /// It validates the request parameters and returns a paginated collection of Branches, including information such as Branch, branch, 
    /// total amount, and Branch status. The operation supports error handling for invalid requests or if no Branches are found.
    /// </remarks>
    /// <param name="pageNumber">The number of the page to be retrieved (starting from 1).</param>
    /// <param name="pageSize">The number of records to include in each page.</param>
    /// <param name="sortBy">The field to sort the Branches by (optional).</param>
    /// <param name="sortDirection">The direction of the sorting: 'asc' for ascending or 'desc' for descending (optional).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
    /// <returns>
    /// A response with a status of 200 OK containing the paginated list of Branches if successful.
    /// A 400 Bad Request response will be returned if the request parameters are invalid,
    /// and a 404 Not Found response will be returned if no Branches are found for the given parameters.
    /// </returns>
    /// <response code="200">Returns the paginated list of Branches successfully retrieved from the database.</response>
    /// <response code="400">If the provided pagination or sorting parameters are invalid or missing.</response>
    /// <response code="404">If no Branches are found matching the provided query parameters.</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<List<ListBranchesResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBranches(int pageNumber, int pageSize, string? sortBy,
        string? sortDirection, CancellationToken cancellationToken)
    {
        try
        {
            var request = new ListBranchesRequest()
            {
                PageNumber = pageNumber,
                SortDirection = sortDirection,
                SortBy = sortBy,
                PageSize = pageSize
            };
            var validator = new ListBranchesRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<ListBranchesCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            var result = _mapper.Map<List<ListBranchesResponse>>(response);
            return Ok(result, "List Branches retrieved successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Update a Branch.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request">The request containing Branch details.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
    /// <returns>A response with the updated Branch data or validation errors.</returns>
    /// <response code="201">Branch updated successfully. Returns the Branch details in the response body.</response>
    /// <response code="400">Bad Request. Validation failed for the provided Branch data.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateBranchResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBranch([FromRoute] Guid id, [FromBody] UpdateBranchRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var validator = new UpdateBranchRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateBranchCommand>(request);
            command.Id = id;
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<UpdateBranchResponse>
            {
                Success = true,
                Message = "Branch updated successfully",
                Data = _mapper.Map<UpdateBranchResponse>(response)
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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