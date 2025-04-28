using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer
{
    /// <summary>
    /// Handler for handling the creation of a customer.
    /// </summary>
    /// <remarks>
    /// This class handles the <see cref="CreateCustomerCommand"/> by validating the command,
    /// mapping it to a domain entity, and interacting with the repository to persist the customer data.
    /// It also maps the result into <see cref="CreateCustomerResult"/>.
    /// </remarks>
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomerHandler"/> class.
        /// </summary>
        /// <param name="customerRepository">The repository for customer data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between DTOs and domain entities.</param>
        public CreateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the <see cref="CreateCustomerCommand"/> and returns the creation result.
        /// </summary>
        /// <param name="command">The command containing the customer creation data.</param>
        /// <param name="cancellationToken">The cancellation token for the operation.</param>
        /// <returns>The result of the customer creation, wrapped in a <see cref="CreateCustomerResult"/>.</returns>
        /// <exception cref="ValidationException">Thrown when the validation of the command fails.</exception>
        public async Task<CreateCustomerResult> Handle(CreateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var validator = new CreateCustomerCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var existingCustomer = await _customerRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (existingCustomer != null)
                throw new BadHttpRequestException($"Customer with email {command.Email} already exists");


            var customer = _mapper.Map<Domain.Entities.Customer>(command);

            var createdCustomer = await _customerRepository.CreateAsync(customer, cancellationToken);

            var result = _mapper.Map<CreateCustomerResult>(createdCustomer);
            return result;
        }
    }
}