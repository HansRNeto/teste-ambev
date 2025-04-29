using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Application.Customer.UpdateCustomer
{
    /// <summary>
    /// Handler for handling the update of a customer.
    /// </summary>
    /// <remarks>
    /// This class handles the <see cref="UpdateCustomerCommand"/> by validating the command,
    /// mapping it to a domain entity, and interacting with the repository to persist the customer data.
    /// It also maps the result into <see cref="UpdateCustomerResult"/>.
    /// </remarks>
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCustomerHandler"/> class.
        /// </summary>
        /// <param name="customerRepository">The repository for customer data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between DTOs and domain entities.</param>
        public UpdateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the <see cref="UpdateCustomerCommand"/> and returns the update result.
        /// </summary>
        /// <param name="command">The command containing the customer update data.</param>
        /// <param name="cancellationToken">The cancellation token for the operation.</param>
        /// <returns>The result of the customer update, wrapped in a <see cref="UpdateCustomerResult"/>.</returns>
        /// <exception cref="ValidationException">Thrown when the validation of the command fails.</exception>
        public async Task<UpdateCustomerResult> Handle(UpdateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var validator = new UpdateCustomerCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var existingCustomer = await _customerRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (existingCustomer != null && existingCustomer.Id != command.Id)
                throw new BadHttpRequestException($"Customer with email {command.Email} already exists");
            
            var customer = _mapper.Map<Domain.Entities.Customer>(command);

            var updatedCustomer = await _customerRepository.UpdateAsync(customer, cancellationToken);

            var result = _mapper.Map<UpdateCustomerResult>(updatedCustomer);
            return result;
        }
    }
}