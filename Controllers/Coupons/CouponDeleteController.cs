using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Coupons
{
    public class CouponDeleteController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;
        public CouponDeleteController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpDelete("{id}")]
        [Route("api/coupons/{id}")]

        public IActionResult Delete(int id){
          _couponRepository.Remove(id);
          return Ok();
        }
    }
}