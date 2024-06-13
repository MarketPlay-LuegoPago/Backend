using System;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class RedemptionCouponService
    {
        private readonly BaseContext _context;

        public RedemptionCouponService(BaseContext context)
        {
            _context = context;
        }

        public async Task<bool> RedimirCupon(int codigoCupon, int userId)
        {
            try
            {
                // Buscar el cupón por código
                var cupon = await _context.Coupons.SingleOrDefaultAsync(c => c.id == codigoCupon);

                if (cupon == null)
                {
                    throw new Exception("Cupón no encontrado.");
                }

                if (cupon.Status?.ToLower() != "activo")
                {
                    throw new Exception("Cupón no válido para redimir.");
                }

                // Actualizar el estado del cupón a Redimido
                cupon.Status = "Redimido";
                _context.Coupons.Update(cupon);

                // Registrar la redención
                var redencion = new Redemption
                {
                    buys_id = cupon.id, // Ajusta según corresponda
                    fecha_redencion = DateTime.UtcNow,
                    Coupon_id = cupon.id,
                    user_id = userId
                };
                _context.Redemptions.Add(redencion);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException dbEx)
            {
                // Captura la excepción interna para obtener más detalles
                var innerException = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new Exception($"Error al guardar los cambios en la base de datos: {innerException}", dbEx);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
    }
}
