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
    public class CouponDetailController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponDetailController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

         [HttpGet("{id}")]
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
                CreatorEmployeeName = coupon.EmployeeMarketing.name // Asignar el nombre del empleado creador
            };

            return Ok(couponDto);
        }
    }
}