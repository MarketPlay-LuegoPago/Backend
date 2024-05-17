using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*  [Produces("application/json")]
     [Consumes("application/json")]
     [ApiConventionType(typeof(DefaultApiConventions))] */
    public class CuponesController : Controller
    {
        public readonly BaseContext _context;
        public CuponesController(BaseContext context)
        {
            _context = context;
        }

        //Listar Cupones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetCupones()
        {
            return await _context.Cupones.ToListAsync();
        }

        //Detalles Cupones
        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> GetCupones(int id)
        {
            var cupon = await _context.Cupones.FindAsync(id);

            if (cupon == null)
            {
                return NotFound();
            }

            return cupon;
        }


        //Crear Cupones
        [HttpPost]
        public async Task<ActionResult<Coupon>> PostCupon([FromBody]Coupon cupon)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Cupon = new Coupon
            {
                name = cupon.name,
                description = cupon.description,
                creation_date = DateTime.Now,
                activation_date = DateTime.Now,redemption_date = DateTime.Now,
                expiration_date = DateTime.UtcNow.AddDays(30),
                discount = cupon.discount,
                Status = cupon.Status,
                use_type = cupon.use_type,
                Quantity_uses = cupon.Quantity_uses,
                discount_type = cupon.discount_type,
                creator_employee_id = cupon.creator_employee_id,
                coupon_category = cupon.coupon_category
    
            };
            _context.Cupones.Add(cupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCupon", new { id = cupon.id }, cupon);
        }



        //Eliminar Cupones
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupon(int id)
        {
            var cupon = await _context.Cupones.FindAsync(id);
            if (cupon == null)
            {
                return NotFound();
            }

            _context.Cupones.Remove(cupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}