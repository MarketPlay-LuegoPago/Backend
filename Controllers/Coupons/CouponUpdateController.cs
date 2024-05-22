using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPut("update/{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] Coupon coupon)
        {
            
            // Obtener el ID del usuario autenticado desde las reclamaciones del JWT
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("No se pudo obtener el ID del usuario.");
            }
            
            var userId = int.Parse(userIdClaim.Value); // Convertir el ID del usuario a entero

            // Buscar el cupón por ID
            var cupon = _couponRepository.GetById(id);
            if (cupon == null)
            {
                return NotFound("Cupón no encontrado.");
            }

            // Verificar si el cupón ha sido usado
            if (cupon.Quantity_uses > 0)
            {
                return BadRequest("El cupón ya ha sido usado y no puede ser actualizado.");
            }

            // Verificar si el usuario autenticado es el creador del cupón
            if (cupon.creator_employee_id != userId)
            {
                return Forbid("No tienes permiso para actualizar este cupón.");
            }

            // Actualizar el cupón
            _couponRepository.CouponUpdate(id, coupon);

            return Ok("Cupón actualizado correctamente.");
        }
    }
}
