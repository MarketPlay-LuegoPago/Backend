using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers.Coupons
{
    public class CouponCreateController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;
        public CouponCreateController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        [HttpPost]
        [Route("api/coupons")]
        public IActionResult Create([FromBody] Coupon coupon)
        {
            _couponRepository.Add(coupon);
            return Ok(coupon);
        }
    }
}