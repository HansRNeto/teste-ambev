using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct
{
    /// <summary>
    /// Validator for the <see cref="CreateProductCommand"/>.
    /// </summary>
    /// <remarks>
    /// Validates the properties of the <see cref="CreateProductCommand"/> to ensure they meet business rules before being processed.
    /// </remarks>
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductCommandValidator"/> class.
        /// </summary>
        public CreateProductCommandValidator()
        {
            // Validate Name: must be provided and not empty
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(3, 100).WithMessage("Product name must be between 3 and 100 characters.");

            // Validate Description: must be provided and not empty
            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .Length(10, 500).WithMessage("Product description must be between 10 and 500 characters.");

            // Validate Price: must be greater than zero
            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");

            // Validate IsActive: no validation is needed here, but you can ensure it's a valid boolean
            RuleFor(command => command.IsActive)
                .NotNull().WithMessage("Product active status is required.");
        }
    }
}