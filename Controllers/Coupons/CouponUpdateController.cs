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
    [Route("api/coupons")]
    public class CouponUpdateController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;
        public CouponUpdateController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpPut("{id}")]
        [Route("update/{id}")]
        public IActionResult Update(int id, [FromBody] Coupon coupon)
        {
         if (id != coupon.id)
            {
                return BadRequest("Coupon ID mismatch");
            }

            var existingCoupon = _couponRepository.GetById(id);
            if (existingCoupon == null)
            {
                return NotFound();
            }

            _couponRepository.Update(coupon);
            return Ok();
        }
    }
}