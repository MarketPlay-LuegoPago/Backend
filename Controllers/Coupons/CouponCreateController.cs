using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;



namespace Backend.Controllers.Coupons
{   
    [Route("api/[controller]")]
    [ApiController]
    public class CouponCreateController : ControllerBase
    {
      
        private readonly ICouponRepository _couponRepository;
        public CouponCreateController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Coupon coupon)
        {   
            if (coupon == null)
            {
                return BadRequest();
            }
            try 
            {
            _couponRepository.Add(coupon);
            return Ok(coupon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new{ message = ex.Message});
            }
        }
    }
}