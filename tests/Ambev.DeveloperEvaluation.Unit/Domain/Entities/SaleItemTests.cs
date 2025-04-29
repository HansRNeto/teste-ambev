using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Fact]
    public void Validate_Should_Return_Valid_Result_When_SaleItem_Is_Valid()
    {
        var saleItem = new SaleItem
        {
            SaleId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Product 1",
            Quantity = 2,
            UnitPrice = 50.00m,
            DiscountPercentage = 10.0m,
            DiscountAmount = 10.00m,
            TotalAmount = 90.00m,
            IsCancelled = false
        };

        var result = saleItem.Validate();

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_Should_Return_Invalid_Result_When_Required_Fields_Are_Missing()
    {
        var saleItem = new SaleItem
        {
            ProductName = "",
            Quantity = 0,
            UnitPrice = -5.00m,
            DiscountPercentage = -10.0m,
            DiscountAmount = -1.00m,
            TotalAmount = -20.00m,
            IsCancelled = false
        };

        var result = saleItem.Validate();

        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }
}