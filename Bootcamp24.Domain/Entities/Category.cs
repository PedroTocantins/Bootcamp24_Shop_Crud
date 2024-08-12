using Bootcamp24.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp24.Domain.Entities;
public class Category
{
    public Category(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Products = [];
    }

    public void Update(string name, string description)
    {
        ValidateCategory(name, description);

        Name = name;
        Description = description;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ICollection<Product> Products { get; private set; }

    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 500;

    private void ValidateCategory(string name, string description)
    {
        ValidateName(name);
        ValidateDescription(description);
    }

    private void ValidateName(string name)
    {
        DomainExeptionValidation.ExceptionHandler(string.IsNullOrEmpty(name), "The name is required.");
        DomainExeptionValidation.ExceptionHandler(name.Length > MaxNameLength, $"The product name cannot exceed {MaxNameLength} charecters");
    }

    private void ValidateDescription(string description)
    {
        DomainExeptionValidation.ExceptionHandler(string.IsNullOrEmpty(description), "The description is required");
        DomainExeptionValidation.ExceptionHandler(description.Length > MaxDescriptionLength, $"The product description cannot exceed {MaxDescriptionLength} charecters");
    }
}
