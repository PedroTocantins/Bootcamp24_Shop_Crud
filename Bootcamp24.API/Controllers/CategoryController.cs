using Bootcamp24.Domain.DTOs;
using Bootcamp24.Domain.Entities;
using Bootcamp24.Domain.Interfaces;
using Bootcamp24.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp24.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        return Ok(await _categoryRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var existentCategory = await _categoryRepository.GetByIdAsync(id);

        if (existentCategory == null) return NotFound();

        return Ok(existentCategory);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CategoryDTO categoryDTO)
    {
        if (categoryDTO == null) return BadRequest();

        var newCategory = new Category
        (
            categoryDTO.Name,
            categoryDTO.Description
        );

        var category = _categoryRepository?.AddAsync(newCategory);
        await _categoryRepository.UnitOfWork.SaveChangesAsync();
        return Ok(category);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] CategoryDTO category)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);

        if (existingCategory == null) return NotFound();

        existingCategory.Update(category.Name, category.Description);

        _categoryRepository.UpdateAsync(existingCategory);
        await _categoryRepository.UnitOfWork.SaveChangesAsync();
        return Ok(existingCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var categoryToDelete = await _categoryRepository.GetByIdAsync(id);
        if (categoryToDelete == null) return NotFound();

        _categoryRepository.DeleteAsync(categoryToDelete);
        await _categoryRepository.UnitOfWork.SaveChangesAsync();

        return Ok(new { message = "Success" });
    }
}
