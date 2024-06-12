/* using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;
        public CouponUpdateController(ICouponRepository couponRepository)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        [Route("update/{id}")]
        public IActionResult Update(int id, [FromBody] Coupon coupon)
        {

            var userId = HttpContext.User.Identity.Name;
            var cupon = _couponRepository.GetById(id);

            if (coupon == null)
            {
                return NotFound("Cupón no encontrado.");
            }

            if (cupon.Quantity_uses > 0)
            {
                return Forbid("No tienes permiso para modificar este cupón.");
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

