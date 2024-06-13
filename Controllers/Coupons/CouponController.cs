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
                CreatorEmployeeName = c.CreatorEmployee.name // Asignar el nombre del empleado creador
            }).ToList();

            return Ok(couponDtos);
        }

        [HttpGet("Details{id}")]
        public async Task<ActionResult<CouponDto>> Details(int id)
        {
            var coupon = await _couponRepository.GetByIdWithCategoryAndEmployeeAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }

            var couponDto = new CouponDto
            {
                id = coupon.id,
                name = coupon.name,
                description = coupon.description,
                creation_date = coupon.creation_date,
                activation_date = coupon.activation_date,
                expiration_date = coupon.expiration_date,
                discount = coupon.discount,
                Status = coupon.Status,
                use_type = coupon.use_type,
                Quantity_uses = coupon.Quantity_uses,
                discount_type = coupon.discount_type,
                CategoryName = coupon.Category.Name,
                CreatorEmployeeName = coupon.CreatorEmployee.name // Asignar el nombre del empleado creador
            };

            return Ok(couponDto);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCoupons([FromQuery] string? name, [FromQuery] string? description, [FromQuery] string? status, [FromQuery] string? use_type)
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(description) && string.IsNullOrWhiteSpace(status) && string.IsNullOrWhiteSpace(use_type))
            {
                return BadRequest("At least one search parameter must be provided");
            }

            var coupons = await _couponRepository.SearchAsync(name, description, status, use_type);
            if (coupons == null || !coupons.Any())
            {
                return NotFound("No coupons found matching the specified criteria");
            }

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
                CreatorEmployeeName = c.CreatorEmployee.name // Asignar el nombre del empleado creador
            }).ToList();

            return Ok(couponDtos);
        }
    }
}

