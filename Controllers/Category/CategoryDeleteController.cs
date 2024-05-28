using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;


namespace Backend.Controllers.Category
{
    public class CategoryDeleteController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryDeleteController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            _categoryRepository.Remove(id);
            return Ok();
        }
    }
}