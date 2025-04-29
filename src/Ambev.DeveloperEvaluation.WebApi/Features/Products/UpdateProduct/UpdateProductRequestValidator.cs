using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    /// <summary>
    /// Validator for the UpdateProductRequest class.
    /// This class ensures that the properties of the UpdateProductRequest are valid before they are processed.
    /// </summary>
    /// <remarks>
    /// The validator checks the following:
    /// - Name: Should be provided and cannot exceed 100 characters.
    /// - Email: Must be in a valid email format.
    /// - IsActive: Should be a boolean value, defaulting to true if not provided.
    /// </remarks>
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name must be provided.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description must be provided.")
                .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");
            
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Product active status is required.");
        }
    }
}