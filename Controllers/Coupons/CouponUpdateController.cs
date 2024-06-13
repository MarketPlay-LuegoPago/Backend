using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Backend.Services;
using Backend.Models;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CouponsController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponsController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpPut]

        public async Task<IActionResult> UpdateCoupon(int id, [FromBody] Coupon updatedCoupon)
        {
            var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdClaim);

            var coupon = _couponRepository.GetById(id);

            if (coupon == null)
            {
                return NotFound("Cupón no encontrado.");
            }

            if (coupon.EmployeeMarketingId != userId)
            {
                return Forbid("No tienes permiso para editar este cupón.");
            }

            if (coupon.Status == "redimido")
            {
                return BadRequest("El cupón no se puede editar porque ya ha sido utilizado.");
            }


            updatedCoupon.id = id; // Asegurar que el ID del cupón no cambie
            updatedCoupon.EmployeeMarketingId = coupon.EmployeeMarketingId; // Mantener el ID del creador original

            var result = await _couponRepository.UpdateCouponAsync(id, updatedCoupon, userId);
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Ok("Cupón actualizado correctamente.");
        }
    }
}


            updatedCoupon.id = id; // Asegurar que el ID del cupón no cambie
            updatedCoupon.creator_employee_id = coupon.creator_employee_id; // Mantener el ID del creador original

            var result = await _couponRepository.UpdateCouponAsync(id, updatedCoupon, userId);
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Ok("Cupón actualizado correctamente.");
        }
    }
}

