using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers.Coupons
{
  public class CouponController : ControllerBase
  {
    private readonly ICouponRepository _couponRepository;
    public CouponController(ICouponRepository couponRepository)
    {
      _couponRepository = couponRepository;
    }
    [HttpGet]
    [Route("api/coupons")]
    public IEnumerable<Coupon> GetCoupons()
    {
      return _couponRepository.GetAll();
    }

    [HttpGet]
    [Route("api/coupons/{id}")]
    public Coupon Details(int id)
    {
      return _couponRepository.GetById(id);
    }

    [HttpGet("status")]
    public async Task<IActionResult> SearchByStatus([FromQuery] string status)
    {
      if (string.IsNullOrWhiteSpace(status))
      {
        return BadRequest("Status parameter is required");
      }

      var coupons = await _couponRepository.SearchByStatusAsync(status);
      if (coupons == null || !coupons.Any())
      {
        return NotFound("No coupons found with the specified status");
      }

      return Ok(coupons);
    }


  }
}