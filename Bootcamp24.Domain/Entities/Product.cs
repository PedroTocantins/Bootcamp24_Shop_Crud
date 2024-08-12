using Bootcamp24.Domain.Validations;
using System;

namespace Bootcamp24.Domain.Entities;

public class Product
{

    public Product(string name, string description, double price, int stock, Guid categoryId)
    {
        ValidateProduct(name, description, price, stock, categoryId);

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
    }

    public void Update(string name, string description, double price, int stock, Guid categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
    }

    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 500;

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Price { get; private set; }
    public int Stock { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; }

    private void ValidateProduct(string name, string description, double price, int stock, Guid categoryId)
    {
        ValidateName(name);
        ValidateDescription(description);
        ValidatePrice(price);
        ValidateStock(stock);
    }

    private void ValidateName(string name)
    {
        DomainExeptionValidation.ExceptionHandler(string.IsNullOrEmpty(name), "The name is required.");
        DomainExeptionValidation.ExceptionHandler(name.Length > MaxNameLength, $"The product name cannot exceed {MaxNameLength} charecters");
    }

    private void ValidateDescription(string description)
    {
        DomainExeptionValidation.ExceptionHandler(string.IsNullOrEmpty(description), "The description is required" );
        DomainExeptionValidation.ExceptionHandler(description.Length > MaxDescriptionLength, $"The product description cannot exceed {MaxDescriptionLength} charecters");
    }

    private void ValidatePrice(double price) 
    {
        DomainExeptionValidation.ExceptionHandler(price <= 0, "Priece must be greater than zero.");
    }

    private void ValidateStock(double stock) 
    {
        DomainExeptionValidation.ExceptionHandler(stock < 0, "Stock cannot be negative.");
    }
}
