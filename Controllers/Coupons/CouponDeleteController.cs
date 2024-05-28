using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Backend.Services;
using Backend.Models;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Backend.Controllers.Coupons
{
    public class CouponDeleteController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;
        public CouponDeleteController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }



        [HttpPut]
        [Route("api/coupons/delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdClaim);

            var coupon = _couponRepository.GetById(id);

            if (coupon == null)
            {
                return NotFound("Cupón no encontrado.");
            }

            if (coupon.creator_employee_id != userId)
            {
                return Forbid("No tienes permiso para eliminar este cupón.");
            }

            if (coupon.Status == "redimido")
            {
                return BadRequest("El cupón no se puede eliminar porque ya ha sido utilizado.");
            }

            coupon.Status = "eliminado";

            var result = await _couponRepository.UpdateCouponAsync(id, coupon, userId);
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Ok("Cupón eliminado correctamente.");
        }
    }
} 