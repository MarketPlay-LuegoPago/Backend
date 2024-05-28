using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedemptionCouponController : ControllerBase
    {
        private readonly RedemptionCouponService _redemptionCouponService;

        public RedemptionCouponController(RedemptionCouponService redemptionCouponService)
        {
            _redemptionCouponService = redemptionCouponService;
        }

        [HttpPost("redimir")]
        public async Task<IActionResult> RedimirCupon([FromBody] RedimirCuponRequest request)
        {
            try
            {
                var resultado = await _redemptionCouponService.RedimirCupon(request.CodigoCupon, request.UsuarioId);
                return Ok(new { mensaje = "Cupón redimido exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }

    public class RedimirCuponRequest
    {
        public int CodigoCupon { get; set; }
        public int UsuarioId { get; set; }
    }
}
