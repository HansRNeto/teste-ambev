using Ambev.DeveloperEvaluation.Application.Product.ListProducts;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product.Validators;

public class ListProductsValidatorTests
{
    private readonly ListProductsValidator _validator;

    public ListProductsValidatorTests()
    {
        _validator = new ListProductsValidator();
    }

    [Fact]
    public async Task Should_Have_Error_When_PageNumber_Is_Less_Than_One()
    {
        var command = new ListProductsCommand
        {
            PageNumber = 0,
            PageSize = 10,
            SortBy = "Name",
            SortDirection = "ASC"
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.PageNumber);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new ListProductsCommand
        {
            PageNumber = 1,
            PageSize = 10,
            SortBy = "Name",
            SortDirection = "ASC"
        };

        var result = await _validator.TestValidateAsync(command);

        result.IsValid.Should().BeTrue();
    }
}