using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers.Category
{
      public class CategoryCreateController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryCreateController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult Create([FromBody] CouponCategory category)
        {
            _categoryRepository.Add(category);
            return Ok(category);
        }
    }
}