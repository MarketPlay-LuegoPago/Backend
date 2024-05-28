/* using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Data;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponsController : ControllerBase
    {
        private readonly BaseContext _context;

        public CouponsController(BaseContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, [FromBody] Coupon coupon)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user ID from the token

            var existingCoupon = await _context.Coupons.FindAsync(id);

            if (existingCoupon == null)
            {
                return NotFound();
            }

            if (existingCoupon.creator_employee_id != userId)
            {
                return Forbid("No tienes permiso para modificar este cupón.");
            }

            // Update the coupon details
            existingCoupon.name = coupon.name;
            existingCoupon.description = coupon.description;
            // ... (otros campos que desees actualizar)

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
} */