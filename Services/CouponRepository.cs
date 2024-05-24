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
    //Metodo Buscar
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
        query = query.Where(c => c.Status == status);
      }
      if (!string.IsNullOrWhiteSpace(use_type))
      {
        query = query.Where(c => c.use_type.Contains(use_type));
      }

      return await query.ToListAsync();
    }

public async Task<IEnumerable<Coupon>> GetAllWithCategoriesAndEmployeesAsync()
{
    return await _context.Coupons
        .Include(c => c.Category) // Incluye la relación con la categoría
        .Include(c => c.CreatorEmployee) // Incluye la relación con el empleado creador
        .ToListAsync(); // Trae todos los cupones con sus relaciones
}

     public async Task<Coupon> GetByIdWithCategoryAndEmployeeAsync(int id)
        {
            return await _context.Coupons
                .Include(c => c.Category)
                .Include(c => c.CreatorEmployee)
                .FirstOrDefaultAsync(c => c.id == id);
        }
  }
}