using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers.Coupons
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CouponDto>>> GetCoupons()
        {
            var coupons = await _couponRepository.GetAllWithCategoriesAndEmployeesAsync();
            var couponDtos = coupons.Select(c => new CouponDto
            {
                id = c.id,
                name = c.name,
                description = c.description,
                creation_date = c.creation_date,
                activation_date = c.activation_date,
                expiration_date = c.expiration_date,
                discount = c.discount,
                Status = c.Status,
                use_type = c.use_type,
                Quantity_uses = c.Quantity_uses,
                discount_type = c.discount_type,
                CategoryName = c.Category.Name,
                CreatorEmployeeName = c.EmployeeMarketing.name // Asignar el nombre del empleado creador
            }).ToList();

            return Ok(couponDtos);
        }

    }
}

