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
        public async Task<ActionResult<IEnumerable<Cupon>>> GetCupones()
        {
            return await _context.Cupones.ToListAsync();
        }

        //Detalles Cupones
        [HttpGet("{id}")]
        public async Task<ActionResult<Cupon>> GetCupones(int id)
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
        public async Task<ActionResult<Cupon>> PostCupon([FromBody]Cupon cupon)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Cupon = new Cupon
            {
                Nombre = cupon.Nombre,
                Descripcion = cupon.Descripcion,
                FechaCreacion = DateTime.Now,
                FechaActivacion = cupon.FechaActivacion,
                FechaUso = cupon.FechaUso,
                FechaVencimiento = cupon.FechaVencimiento,
                PorcentajeDescuento = cupon.PorcentajeDescuento,
    
            };
            _context.Cupones.Add(cupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCupon", new { id = cupon.Id }, cupon);
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