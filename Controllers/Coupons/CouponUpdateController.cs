/* using System;
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

            var userId = HttpContext.User.Identity.Name;
            var cupon = _couponRepository.GetById(id);

            if (coupon == null)
            {
                return NotFound();
            }

            if (cupon.Quantity_uses > 0)
            {
                return BadRequest("El cupón ya ha sido usado y no puede ser actualizado.");
            }
            if (cupon.creator_employee_id != userId)
            {
                return Forbid("No tienes permiso para actualizar este cupón.");
            }

            _couponRepository.CouponUpdate(id, Coupon);

            return Ok("Cupón actualizado correctamente.");






    }
}

} */

