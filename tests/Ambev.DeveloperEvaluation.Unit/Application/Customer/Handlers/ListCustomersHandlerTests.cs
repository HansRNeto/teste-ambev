using Ambev.DeveloperEvaluation.Application.Customer.ListCustomers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer.Handlers
{
    public class ListCustomersHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ListCustomersHandler _handler;

        public ListCustomersHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new ListCustomersHandler(_customerRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Throw_Validation_Exception_When_Command_Is_Invalid()
        {
            var command = new ListCustomersCommand
            {
                PageNumber = 0,
                PageSize = 10
            };

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_Should_Return_Mapped_Customers_When_Command_Is_Valid()
        {
            var command = new ListCustomersCommand
            {
                PageNumber = 1,
                PageSize = 10
            };

            var customers = new List<DeveloperEvaluation.Domain.Entities.Customer> { new() { Id = Guid.NewGuid(), Name = "Customer 1" } };

            _customerRepositoryMock
                .Setup(repo => repo.ListAsync(command.PageNumber, command.PageSize, command.SortBy, command.SortDirection, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customers);

            _mapperMock
                .Setup(mapper => mapper.Map<List<ListCustomersResult>>(customers))
                .Returns([new ListCustomersResult { Id = customers[0].Id, Name = "Customer 1" }]);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Customer 1", result[0].Name);
        }
    }
}
