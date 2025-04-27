using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Customer: BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}