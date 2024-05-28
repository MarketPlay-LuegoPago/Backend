using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class RedemtionCoupon
    {
        private readonly BaseContext _context;

        public RedemtionCoupon(BaseContext context)
        {
            _context = context;
        }

        public async Task<bool> RedimirCupon(int codigoCupon, int userId)
        {
            // Buscar el cupón por código
            var cupon = await _context.Coupons.SingleOrDefaultAsync(c => c.id == codigoCupon);

            if (cupon == null)
            {
                throw new Exception("Cupón no encontrado.");
            }

            if (cupon.Status != "Activo")
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
    }
}