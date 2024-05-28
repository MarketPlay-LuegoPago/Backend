 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class CouponRepository : ICouponRepository
    {
        private readonly BaseContext _context;

        public object Coupons => throw new NotImplementedException();

        public CouponRepository(BaseContext context)
        {
            _context = context;
        }

        public void Add(Coupon coupon)
        {
            _context.Coupons.Add(coupon);
            _context.SaveChanges();
        }

        public IEnumerable<Coupon> GetAll()
        {
            return _context.Coupons.ToList();
        }

        public Coupon GetById(int id)
        {
            return _context.Coupons.Find(id);
        }

        public IEnumerable<Coupon> GetByOwnerId(int ownerId)
        {
            return _context.Coupons.Where(c => c.creator_employee_id == ownerId).ToList();
        }

        public async Task<Coupon> GetCouponByIdAsync(int id)
        {
            return await _context.Coupons.FindAsync(id);
        }





        public IEnumerable<Coupon> GetByOwnerId(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Coupon> GetByOwnerId(object ownerId)
        {
            throw new NotImplementedException();
        }



        public async Task<respuesta> UpdateCouponAsync(int id, Coupon updatedCoupon, int userId)
{
    // Recuperar la entidad existente del contexto
    var existingCoupon = await _context.Coupons.FindAsync(id);

    if (existingCoupon == null)
    {
        return new respuesta() { IsSuccess = false, StatusCode = 404, Message = "Cupón no encontrado." };
    }

    // Aplicar las actualizaciones a la entidad existente
    _context.Entry(existingCoupon).CurrentValues.SetValues(updatedCoupon);

    // Guardar los cambios
    await _context.SaveChangesAsync();

    return new respuesta() { IsSuccess = true, StatusCode = 200, Message = "Actualizado" };
}


        public async Task<respuesta> DeleteCouponAsync(int id, int userId)
        {
            var existingCoupon = await _context.Coupons.FindAsync(id);

            if (existingCoupon == null)
            {
                return new respuesta() { IsSuccess = false, StatusCode = 404, Message = "Cupón no encontrado." };
            }

            // Cambiar el estado del cupón a "eliminado"
            existingCoupon.Status = "eliminado";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return new respuesta() { IsSuccess = false, StatusCode = 500, Message = "Error al actualizar el estado del cupón. Detalles: " + ex.InnerException?.Message };
            }

            return new respuesta() { IsSuccess = true, StatusCode = 200, Message = "Cupón eliminado correctamente." };
        }



       
    }
}

public class respuesta{

    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
}
 
