using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Branch: BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public bool IsActive { get; set; }
}