using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Backend.Services;
using Backend.Models;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class CouponController : ControllerBase
  {
    private readonly ICouponRepository _couponRepository;
    public CouponController( ICouponRepository couponRepository)
    {
      _couponRepository = couponRepository;
    }
    // Mostramos los cupones 
    [HttpGet]
    public IActionResult GetCoupons() 
    {
      //Obtenemos el id del usuario que inicia sesion por el login
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

      //Obtenemos los cupones que el usuario a creado 
      //var coupons = _couponRepository.GetByOwnerId(userId); //Pasamos como parametro la variable del id del usuario
      var coupons = _couponRepository.GetByOwnerId(int.Parse(userId));
      return Ok(coupons);
    }
    [HttpGet("{id}")] //Recibimos el id del usuario para poder listar los cupones
    public IActionResult GetCoupon(int id)
    {
      //obtenemos el id del usuario mediante Jhonson web token
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

      //Obtenemos el id del cupon 
      var coupon = _couponRepository.GetById(id);
      if (coupon == null)
      {
        return NotFound();
      }
      //Verificamos si el usuario actual es el creador del cupon.
      if (coupon.creator_employee_id != int.Parse(userId))
      {
        return BadRequest("No tienes permisos para acceder a este cupon");
      }
      return Ok(coupon);
    }
  }
}
