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
  }
}