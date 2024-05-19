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
        _context.Coupons.Update(coupon);
        _context.SaveChanges();
      }
    }
}