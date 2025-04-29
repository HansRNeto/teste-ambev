using Ambev.DeveloperEvaluation.Application.Sale.ListSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.Handlers;

public class ListSalesHandlerTests
{
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ListSalesHandler _handler;

    public ListSalesHandlerTests()
    {
        _saleRepositoryMock = new Mock<ISaleRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new ListSalesHandler(_saleRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Sales_List_When_Command_Is_Valid()
    {
        var command = new ListSalesCommand
        {
            PageNumber = 1,
            PageSize = 10,
            SortBy = "Name",
            SortDirection = "ASC"
        };

        var sales = new List<DeveloperEvaluation.Domain.Entities.Sale>
        {
            new() { Id = Guid.NewGuid(), TotalAmount = 100 },
            new() { Id = Guid.NewGuid(), TotalAmount = 200 }
        };

        var salesResult = new List<ListSalesResult>
        {
            new ListSalesResult { Id = sales[0].Id, TotalAmount = sales[0].TotalAmount },
            new ListSalesResult { Id = sales[1].Id, TotalAmount = sales[1].TotalAmount }
        };

        _saleRepositoryMock
            .Setup(r => r.ListAsync(command.PageNumber, command.PageSize, command.SortBy, command.SortDirection, It.IsAny<CancellationToken>()))
            .ReturnsAsync(sales);

        _mapperMock
            .Setup(m => m.Map<List<ListSalesResult>>(sales))
            .Returns(salesResult);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result[0].TotalAmount.Should().Be(100);
        result[1].TotalAmount.Should().Be(200);

        _saleRepositoryMock.Verify(x => x.ListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(x => x.Map<List<ListSalesResult>>(sales), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_Validation_Exception_When_Command_Is_Invalid()
    {
        var command = new ListSalesCommand
        {
            PageNumber = 0, 
            PageSize = 10,
            SortBy = "Name",
            SortDirection = "ASC"
        };

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();
    }
}
