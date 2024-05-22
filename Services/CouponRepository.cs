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
      public void Update(Coupon coupon)
      {
         var existingCoupon = _context.Coupons.Find(coupon.id);
            if (existingCoupon != null)
            {
                _context.Entry(existingCoupon).CurrentValues.SetValues(coupon);
                _context.SaveChanges();
            }
      }
        //Buscar por status
        public async Task<IEnumerable<Coupon>> SearchAsync(string? name, string? description, string? status)
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
              query = query.Where(c => c.Status == status);
          }

          return await query.ToListAsync();
      }

    }
}