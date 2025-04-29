using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact]
    public void Validate_With_Valid_Sale_Should_Return_Valid_Result()
    {
        var sale = new Sale
        {
            SaleNumber = "SALE-001",
            SaleDate = DateTime.Now,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Customer Test",
            BranchId = Guid.NewGuid(),
            BranchName = "Branch Test",
            TotalAmount = 100.00m,
            IsCancelled = false
        };

        var validationResult = sale.Validate();

        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_With_Missing_SaleNumber_Should_Return_Invalid_Result()
    {
        var sale = new Sale
        {
            SaleNumber = "",
            SaleDate = DateTime.Now,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Customer Test",
            BranchId = Guid.NewGuid(),
            BranchName = "Branch Test",
            TotalAmount = 100.00m,
            IsCancelled = false
        };

        var validationResult = sale.Validate();

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().NotBeEmpty();
    }
}