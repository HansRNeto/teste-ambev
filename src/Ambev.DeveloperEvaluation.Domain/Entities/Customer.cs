using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a customer entity with personal details and associated sales information.
/// </summary>
/// <remarks>
/// This entity contains the basic information about a customer such as:
/// <list type="bullet">
///     <item><description><see cref="Name"/>: The name of the customer.</description></item>
///     <item><description><see cref="Email"/>: The email address of the customer.</description></item>
///     <item><description><see cref="IsActive"/>: Indicates whether the customer is active or not.</description></item>
///     <item><description><see cref="CreatedAt"/>: The timestamp indicating when the customer was created.</description></item>
///     <item><description><see cref="UpdatedAt"/>: The timestamp indicating when the customer information was last updated.</description></item>
///     <item><description><see cref="Sales"/>: A collection of sales associated with the customer.</description></item>
/// </list>
/// This class also provides a method <see cref="Validate"/> to validate the customer data using the <see cref="CustomerValidator"/>.
/// If the validation fails, it returns a list of validation errors along with their details.
/// </remarks>
/// <seealso cref="AbstractValidator{T}"/>
/// <seealso cref="FluentValidation"/>
/// <seealso cref="ValidationResult"/>
/// <seealso cref="FluentValidation"/>
public class Customer : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of the customer.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the customer is active or not.
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Gets or sets the timestamp indicating when the customer was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp indicating when the customer information was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of sales associated with the customer.
    /// </summary>
    public virtual List<Sale> Sales { get; set; }

    /// <summary>
    /// Validates the customer's properties using the <see cref="CustomerValidator"/>.
    /// </summary>
    /// <remarks>
    /// This method checks whether the customer entity meets the validation rules defined in the <see cref="CustomerValidator"/>.
    /// It returns a <see cref="ValidationResultDetail"/> that includes a flag indicating whether the entity is valid or not,
    /// and a list of any validation errors.
    /// </remarks>
    /// <returns>A <see cref="ValidationResultDetail"/> indicating whether the customer is valid and any validation errors.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CustomerValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}