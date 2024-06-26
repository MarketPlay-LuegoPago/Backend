using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers.Category
{
  [ApiController]
  [Route("api/[controller]")]

  public class CategoryController : ControllerBase
  {
    private readonly ICategoryRepository _categoryRepository;
    public CategoryController(ICategoryRepository categoryRepository)
    {
      _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public IEnumerable<CouponCategory> GetCategories()
    {
      return _categoryRepository.GetAll();
    }

    [HttpGet("{id}")]
    public CouponCategory Details(int id)
    {
      return _categoryRepository.GetById(id);
    }

    [HttpGet("search")]
    public async Task <IActionResult> SearchCategory ([FromQuery] string? Name)
    {
      if (string.IsNullOrWhiteSpace(Name))
      {
        return BadRequest("At least one search parameter must be provided");
      }

      var categories = await _categoryRepository.SearchAsync(Name);
      if (categories == null ||!categories.Any())
      {
        return NotFound("No categories found matching the specified criteria");
      }
      return Ok(categories);
    }
  }
}