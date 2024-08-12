using Bootcamp24.Domain.DTOs;
using Bootcamp24.Domain.Entities;
using Bootcamp24.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp24.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _productRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var myProduct = await _productRepository.GetByIdAsync(id);

        if (myProduct == null) return NotFound();

        return Ok(myProduct);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDTO)
    {
        if (productDTO == null) return BadRequest();

        var newProduct = new Product
        (
            productDTO.Name,
            productDTO.Description,
            productDTO.Price,
            productDTO.Stock,
            productDTO.CategoryId
        );

        var product = _productRepository?.AddAsync(newProduct);
        await _productRepository.UnitOfWork.SaveChangesAsync();
        return Ok();

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductDTO product)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);

        if (existingProduct == null) return NotFound();

        existingProduct.Update(product.Name, product.Description, product.Price, product.Stock, product.CategoryId);

        _productRepository.UpdateAsync(existingProduct);
        await _productRepository.UnitOfWork.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var myProduct = await _productRepository.GetByIdAsync(id);
        if (myProduct == null) return NotFound();

        _productRepository.DeleteAsync(myProduct);
        await _productRepository.UnitOfWork.SaveChangesAsync();

        return Ok(new { message = "Success" });
    }

}
