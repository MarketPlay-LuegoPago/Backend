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

        public CouponRepository(BaseContext context)
        {
            _context = context;
        }

        public object Coupons => throw new NotImplementedException();

        public void Add(Coupon coupon)
        {
            if (coupon.activation_date > coupon.expiration_date)
            {
                throw new ArgumentException("La Fecha de activación no puede ser mayor a la de expiración.");
            }
            if (coupon.creation_date > coupon.activation_date)
            {
                throw new ArgumentException("La Fecha de activación no puede ser menor a la de creación.");
            }
            if (coupon.expiration_date < coupon.creation_date)
            {
                throw new ArgumentException("La Fecha de expiración no puede ser menor a la de creación.");
            }
            if (coupon.creation_date > coupon.activation_date)
            {
                throw new ArgumentException("La Fecha de creación no puede ser mayor a la de activación.");
            }

            try
            {
                // Establecemos la fecha de creación
                coupon.creation_date = DateTime.Now;

                _context.Coupons.Add(coupon);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error no se puede crear el cupón: " + ex);
            }
        }

        public IEnumerable<Coupon> GetAll()
        {
            return _context.Coupons.ToList();
        }

        public Coupon GetById(int id)
        {
            return _context.Coupons.Find(id);
        }

        public void Remove(int id)
        {
            var coupon = _context.Coupons.Find(id);
            if (coupon != null)
            {
                _context.Coupons.Remove(coupon);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Coupon>> GetAllWithCategoriesAndEmployeesAsync()
        {
            return await _context.Coupons
                                 .Include(c => c.Category)
                                 .Include(c => c.EmployeeMarketing)
                                 .ToListAsync();
        }

        public async Task<Coupon> GetByIdWithCategoryAndEmployeeAsync(int id)
        {
            return await _context.Coupons
                                 .Include(c => c.Category)
                                 .Include(c => c.EmployeeMarketing)
                                 .FirstOrDefaultAsync(c => c.id == id);
        }

        public async Task<IEnumerable<Coupon>> SearchAsync(string? name, string? description, string? status, string? use_type)
        {
            var query = _context.Coupons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(c => c.name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                query = query.Where(c => c.description.Contains(description));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status.Contains(status));
            }

            if (!string.IsNullOrWhiteSpace(use_type))
            {
                query = query.Where(c => c.use_type.Contains(use_type));
            }

            return await query
                         .Include(c => c.Category)
                         .Include(c => c.EmployeeMarketing)
                         .ToListAsync();
        }

        public IEnumerable<Coupon> GetByOwnerId(int ownerId)
        {
            return _context.Coupons.Where(c => c.EmployeeMarketingId == ownerId).ToList();
        }

        public async Task<Coupon> GetCouponByIdAsync(int id)
        {
            return await _context.Coupons.FindAsync(id);
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

        Task<Respuesta> ICouponRepository.DeleteCouponAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        Task<Respuesta> ICouponRepository.UpdateCouponAsync(int id, Coupon updatedCoupon, int userId)
        {
            throw new NotImplementedException();
        }

        public class respuesta
        {
            public bool IsSuccess { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }
    }
}
