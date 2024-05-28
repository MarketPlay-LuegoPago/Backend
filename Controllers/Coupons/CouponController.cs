
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
  [Authorize] // El authorize asegura que solo los usuarios puedan entrar a este controlador
  public class CouponController : ControllerBase
  {
    private readonly ICouponRepository _couponRepository;
    public CouponController( ICouponRepository couponRepository)
    {
      _couponRepository = couponRepository;
    }
    // Mostramos los cupones 
    [HttpGet] //Metodo para  traer Todos los cupones del usuario creado
    public IActionResult GetCouponsAsync()    
    //Debo cambiar algo aqui para traeos TODOS y no uno por uno
    {
      //Obtenemos el id del usuario que inicia sesion por el login por medio de claimsTypes que esta en el controlador AuthLogin
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

      if(userId == null)
      {
        return BadRequest("No tienes acceso a este cupon");
      }
      //Obtenemos los cupones que el usuario a creado 
      
      var coupons = _couponRepository.GetByOwnerId(int.Parse(userId));  //Pasamos como parametro la variable del id del usuario
      return Ok(coupons);  //Hacemos el llamado de la variable que me guarda el contexto
    }
    [HttpGet("{id}")] //Recibimos el id del usuario para poder listar los cupones
    public IActionResult GetCouponAsync(int id)
    {
      //obtenemos el id del usuario mediante Jhonson web token
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Usamos el FindFirst para obtener el primer token que se genero
        if(userId == null)
        {
          return BadRequest("Usuario no valido");  // Hacemos la validacion del ususario que entra
        }
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
      return Ok(coupon); //listamos la cantidad de los cupones que tiene el id 
    }
  }
}
 