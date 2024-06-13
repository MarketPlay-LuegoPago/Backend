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
    public class CategoryDetailController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryDetailController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("{id}")]
        public CouponCategory Details(int id)
        {
            return _categoryRepository.GetById(id);
        }

    }
}