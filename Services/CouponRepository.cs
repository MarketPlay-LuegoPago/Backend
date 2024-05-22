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
        public async Task<IEnumerable<Coupon>> SearchByStatusAsync(string status)
        {
            var coupons = await _context.Coupons.ToListAsync();

            if(status == "Activo" || status == "Desactivado" ){

              coupons = _context.Coupons.Where(x => x.Status == status).ToList();

            }
            
            // Log the number of coupons found
            Console.WriteLine($"Found {coupons.Count} coupons with status '{status}'");
            
            return coupons;
        }

    }
}