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
        private readonly RedemtionCoupon _redencionCuponService;

        public RedemptionCouponController(RedemtionCoupon redencionCuponService)
        {
            _redencionCuponService = redencionCuponService;
        }

        [HttpPost("redimir")]
        public async Task<IActionResult> RedimirCupon([FromBody] RedimirCuponRequest request)
        {
            try
            {
                var resultado = await _redencionCuponService.RedimirCupon(request.CodigoCupon, request.UsuarioId);
                return Ok(new { mensaje = "Cupón redimido exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }

    internal class RedencionCuponService
    {
    }

    public class RedimirCuponRequest
    {
        public int CodigoCupon { get; set; }
        public int UsuarioId { get; set; }
    }
}
