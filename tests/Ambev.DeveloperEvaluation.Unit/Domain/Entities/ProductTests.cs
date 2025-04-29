using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Unit tests for the <see cref="Product"/> entity.
/// </summary>
public class ProductTests
{
    [Fact]
    public void Validate_Should_Return_Valid_When_Product_Is_Correct()
    {
        var product = new Product
        {
            Name = "Product Test",
            Description = "Description Test",
            Price = 100.50m,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var result = product.Validate();

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_Should_Return_Invalid_When_Name_IsEmpty()
    {
        var product = new Product
        {
            Name = "", 
            Description = "Description Test",
            Price = 100.50m,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var validator = new ProductValidator();

        var result = validator.Validate(product);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e =>
            e.PropertyName == "Name" &&
            e.ErrorMessage == "Product name must be provided."
        );
    }

    [Fact]
    public void Validate_Should_Return_Invalid_When_Price_Is_Negative()
    {
        var product = new Product
        {
            Name = "Product Test",
            Description = "Description Test",
            Price = -10m,
            IsActive = true
        };

        var result = product.Validate();

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.Error == "GreaterThanOrEqualValidator");
    }
}