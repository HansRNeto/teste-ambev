using Ambev.DeveloperEvaluation.Application.Branch.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch.Handlers;

public class CreateBranchHandlerTests
{
    private readonly Mock<IBranchRepository> _mockBranchRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateBranchHandler _handler;

    public CreateBranchHandlerTests()
    {
        _mockBranchRepository = new Mock<IBranchRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreateBranchHandler(_mockBranchRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        var command = new CreateBranchCommand();
        var cancellationToken = new CancellationToken();

        var validator = new CreateBranchCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(command, cancellationToken));
        }
    }

    [Fact]
    public async Task Handle_ShouldCreateBranch_WhenCommandIsValid()
    {
        var command = new CreateBranchCommand
        {
            Name = "Filial Teste",
            Address = "Rua Teste, 123"
        };
        var cancellationToken = new CancellationToken();

        var branchEntity = new DeveloperEvaluation.Domain.Entities.Branch
        {
            Name = "Filial Teste",
            Address = "Rua Teste, 123"
        };

        var branchResult = new CreateBranchResult
        {
            Id = Guid.NewGuid(),
            Name = "Filial Teste",
            Address = "Rua Teste, 123"
        };

        _mockMapper.Setup(m => m.Map<DeveloperEvaluation.Domain.Entities.Branch>(command)).Returns(branchEntity);
        _mockMapper.Setup(m => m.Map<CreateBranchResult>(branchEntity)).Returns(branchResult);
        _mockBranchRepository.Setup(repo => repo.CreateAsync(It.IsAny<DeveloperEvaluation.Domain.Entities.Branch>(), cancellationToken)).ReturnsAsync(branchEntity);

        var result = await _handler.Handle(command, cancellationToken);

        result.Should().BeEquivalentTo(branchResult);
        _mockBranchRepository.Verify(repo => repo.CreateAsync(It.IsAny<DeveloperEvaluation.Domain.Entities.Branch>(), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallMapperAndRepository_WhenCommandIsValid()
    {
        var command = new CreateBranchCommand
        {
            Name = "Filial Teste",
            Address = "Rua Teste, 123"
        };
        var cancellationToken = new CancellationToken();

        var branchEntity = new DeveloperEvaluation.Domain.Entities.Branch
        {
            Name = "Filial Teste",
            Address = "Rua Teste, 123"
        };

        _mockMapper.Setup(m => m.Map<DeveloperEvaluation.Domain.Entities.Branch>(command)).Returns(branchEntity);
        _mockBranchRepository.Setup(repo => repo.CreateAsync(It.IsAny<DeveloperEvaluation.Domain.Entities.Branch>(), cancellationToken)).ReturnsAsync(branchEntity);

        await _handler.Handle(command, cancellationToken);

        _mockMapper.Verify(m => m.Map<DeveloperEvaluation.Domain.Entities.Branch>(command), Times.Once);
        _mockBranchRepository.Verify(repo => repo.CreateAsync(It.IsAny<DeveloperEvaluation.Domain.Entities.Branch>(), cancellationToken), Times.Once);
    }
}