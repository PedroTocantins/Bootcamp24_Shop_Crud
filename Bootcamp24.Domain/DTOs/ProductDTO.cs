﻿namespace Bootcamp24.Domain.DTOs;

public class ProductDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Stock { get; set; }
    public Guid CategoryId { get; set; }
}
