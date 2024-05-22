using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;
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
      public void Add(Coupon coupon)
      {
        _context.Coupons.Add(coupon);
        _context.SaveChanges();
      }

      public void CouponUpdate(int id, Coupon updatedCoupon)
        {
            var existingCoupon = _context.Coupons.Find(id);
            if (existingCoupon != null)
            {
                // Aquí actualizamos sólo los campos específicos que queremos permitir
                existingCoupon.name = updatedCoupon.name;
                existingCoupon.description = updatedCoupon.description;
                existingCoupon.expiration_date = updatedCoupon.expiration_date;
                existingCoupon.discount = updatedCoupon.discount;
                existingCoupon.Status = updatedCoupon.Status;
                existingCoupon.use_type = updatedCoupon.use_type;
                existingCoupon.Quantity_uses = updatedCoupon.Quantity_uses;
                existingCoupon.discount_type = updatedCoupon.discount_type;
                existingCoupon.CategoryId = updatedCoupon.CategoryId;

                
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Cupón no encontrado.");
            }}

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
        var coupo = _context.Coupons.Find(id);
        if (coupo != null)
        {
          _context.Coupons.Remove(coupo);
          _context.SaveChanges();
        }
      }

    }
}