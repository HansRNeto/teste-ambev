using Ambev.DeveloperEvaluation.Application.Branch.GetBranch;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch.Handlers;

public class GetBranchHandlerTests
{
    private readonly Mock<IBranchRepository> _branchRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetBranchHandler _handler;

    public GetBranchHandlerTests()
    {
        _branchRepositoryMock = new Mock<IBranchRepository>();
        _mapperMock = new Mock<IMapper>();

        _handler = new GetBranchHandler(_branchRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ExistingBranchId_ShouldReturnBranchDetails()
    {
        var branchId = Guid.NewGuid();
        var branchMock = new DeveloperEvaluation.Domain.Entities.Branch
        {
            Id = branchId,
            Name = "Test Branch",
            Address = "123 Test Avenue"
        };

        var branchResultMock = new GetBranchResult
        {
            Id = branchId,
            Name = "Test Branch",
            Address = "123 Test Avenue"
        };

        _branchRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.Is<Guid>(id => id == branchId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(branchMock);

        _mapperMock
            .Setup(mapper =>
                mapper.Map<GetBranchResult>(It.Is<DeveloperEvaluation.Domain.Entities.Branch>(b => b == branchMock)))
            .Returns(branchResultMock);

        var command = new GetBranchCommand(branchId);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(branchId);
        result.Name.Should().Be("Test Branch");
        result.Address.Should().Be("123 Test Avenue");
    }

    [Fact]
    public async Task Handle_NonExistingBranchId_ShouldThrowKeyNotFoundException()
    {
        var branchId = Guid.NewGuid();
        _branchRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.Is<Guid>(id => id == branchId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DeveloperEvaluation.Domain.Entities.Branch)null!);

        var command = new GetBranchCommand(branchId);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Branch with ID {branchId} not found");
    }
}