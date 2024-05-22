using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;

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
        public IEnumerable<Coupon> GetCoupons()
        {
            return _couponRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Coupon Details(int id)
        {
            return _couponRepository.GetById(id);
        }

       [HttpGet("search")]
        public async Task<IActionResult> SearchCoupons([FromQuery] string? name, [FromQuery] string? description, [FromQuery] string? status, [FromQuery] string? use_type)
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(description) && string.IsNullOrWhiteSpace(status) && string.IsNullOrWhiteSpace(use_type    ))
            {
                return BadRequest("At least one search parameter must be provided");
            }

            var coupons = await _couponRepository.SearchAsync(name, description, status, use_type);
            if (coupons == null || !coupons.Any())
            {
                return NotFound("No coupons found matching the specified criteria");
            }

            return Ok(coupons);
        }

    }
}
