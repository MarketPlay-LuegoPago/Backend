using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers.Category
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryUpdateController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryUpdateController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CouponCategory category)
        {
            if (category == null || category.CategoryId != id)
            {
                return BadRequest("Invalid category data");
            }

            var updated = await _categoryRepository.UpdateAsync(category);
            if (updated)
            {
                return Ok("Category updated");
            }
            else
            {
                return StatusCode(500, "An error occurred while updating the category");
            }
        }
    }
}
