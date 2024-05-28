 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
  public class CouponRepository : ICouponRepository
  {
      private readonly BaseContext _context;

        public object Coupons => throw new NotImplementedException();

  
    public object Coupons => throw new NotImplementedException();

    public CouponRepository(BaseContext context)
      {
        _context = context;
      }

      public void Add(Coupon coupon)
      {
    
      if (coupon.activation_date > coupon.expiration_date)
      {
        throw new ArgumentException("La Fecha de activacion no puede ser mayor a la de expiracion.");
      }
      if (coupon.creation_date > coupon.activation_date)
      {
        throw new ArgumentException("La Fecha de activacion no puede ser menor a la de creacion.");
      }
       if (coupon.expiration_date < coupon.creation_date)
      {
        throw new ArgumentException("La Fecha de expiracion no puede ser menor a la de creacion.");
      }

       if (coupon.creation_date > coupon.activation_date)
      {
        throw new ArgumentException("La Fecha de creacion no puede ser mayor a la de activacion.");
      }

      try
      {

        // Establecemos la fecha de creacion
        coupon.creation_date = DateTime.Now;

        _context.Coupons.Add(coupon);
            _context.SaveChanges();
        }
      catch (Exception ex)
      {
        throw new ArgumentException("Error no se puede crear el cupon" + ex);
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
    }
}